using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sifr.Shared.Models;

namespace Sifr.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AccountsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> Get()
        {
            return await _context.Accounts.ToListAsync();
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Account>> Get(Guid id)
        {
            var account = await _context.Accounts.FindAsync(id);
            return account is null ? NotFound() : Ok(account);
        }

        [HttpPost]
        public async Task<ActionResult<Account>> Post([FromBody] Account acc)
        {
            var now = DateTime.UtcNow;
            var newAcc = acc with { 
                Id = acc.Id == Guid.Empty ? Guid.NewGuid() : acc.Id, 
                CreatedAt = now, 
                UpdatedAt = now 
            };
            
            _context.Accounts.Add(newAcc);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = newAcc.Id }, newAcc);
        }
    }
}
