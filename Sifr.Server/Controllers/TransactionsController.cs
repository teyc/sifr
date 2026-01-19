using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sifr.Shared.Models;
using Sifr.Server.Services;

namespace Sifr.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IAccountingValidationService _validationService;

        public TransactionsController(ApplicationDbContext context, IAccountingValidationService validationService)
        {
            _context = context;
            _validationService = validationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> Get()
        {
            return await _context.Transactions.ToListAsync();
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Transaction>> Get(Guid id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            return transaction is null ? NotFound() : Ok(transaction);
        }

        [HttpPost]
        public async Task<ActionResult<Transaction>> Post([FromBody] Transaction tx)
        {
            var now = DateTime.UtcNow;
            var newTx = tx with { 
                Id = tx.Id == Guid.Empty ? Guid.NewGuid() : tx.Id, 
                CreatedAt = now, 
                UpdatedAt = now 
            };
            
            _context.Transactions.Add(newTx);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = newTx.Id }, newTx);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Transaction tx)
        {
            if (id != tx.Id) return BadRequest();

            // Fetch existing as NoTracking to avoid tracking conflict when we attach the updated version
            var existing = await _context.Transactions.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (existing == null) return NotFound();

            // Create updated record, preserving CreatedAt from existing
            var updatedTx = tx with 
            { 
                Id = id, 
                CreatedAt = existing.CreatedAt, // Preserve original creation time
                UpdatedAt = DateTime.UtcNow 
            };
            
            // Mark the new object as Modified
            _context.Transactions.Update(updatedTx);
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(id)) return NotFound();
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null) return NotFound();

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost("{id:guid}/match")]
        public async Task<IActionResult> MatchTransaction(Guid id, [FromBody] TransactionMatchRequest request)
        {
            var transaction = await _context.Transactions.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (transaction == null) return NotFound();

            // Validate double-entry requirements
            var validation = _validationService.ValidateTransactionMatch(transaction, request.DebitAccountId, request.CreditAccountId);
            if (!validation.IsValid)
            {
                return BadRequest(new { errors = validation.Errors });
            }

            // Create updated record
            var matchedTransaction = transaction with
            {
                AccountId = request.DebitAccountId, // Primary account for display
                Status = TransactionStatus.Matched,
                UpdatedAt = DateTime.UtcNow
            };
            
            // Note: In a real implementation, create JournalEntry here.
            
            _context.Transactions.Update(matchedTransaction);
            await _context.SaveChangesAsync();

            return Ok(matchedTransaction);
        }
        
        private bool TransactionExists(Guid id)
        {
            return _context.Transactions.Any(e => e.Id == id);
        }
    }

    public record TransactionMatchRequest(Guid DebitAccountId, Guid CreditAccountId);
}
