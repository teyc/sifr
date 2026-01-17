using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Sifr.Shared.Models;

namespace Sifr.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private static readonly List<Transaction> Store = new();

        [HttpGet]
        public ActionResult<IEnumerable<Transaction>> Get() => Ok(Store);

        [HttpGet("{id:guid}")]
        public ActionResult<Transaction> Get(Guid id)
        {
            var t = Store.FirstOrDefault(x => x.Id == id);
            return t is null ? NotFound() : Ok(t);
        }

        [HttpPost]
        public ActionResult<Transaction> Post([FromBody] Transaction tx)
        {
            var now = DateTime.UtcNow;
            var newTx = tx with { Id = tx.Id == Guid.Empty ? Guid.NewGuid() : tx.Id, CreatedAt = now, UpdatedAt = now };
            Store.Add(newTx);
            return CreatedAtAction(nameof(Get), new { id = newTx.Id }, newTx);
        }

        [HttpPut("{id:guid}")]
        public IActionResult Put(Guid id, [FromBody] Transaction tx)
        {
            var idx = Store.FindIndex(x => x.Id == id);
            if (idx < 0) return NotFound();
            var updated = tx with { Id = id, UpdatedAt = DateTime.UtcNow };
            Store[idx] = updated;
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            var removed = Store.RemoveAll(x => x.Id == id);
            return removed > 0 ? NoContent() : NotFound();
        }
    }
}
