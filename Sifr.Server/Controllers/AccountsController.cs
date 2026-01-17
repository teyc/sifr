using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Sifr.Shared.Models;

namespace Sifr.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private static readonly List<Account> Store = new();

        [HttpGet]
        public ActionResult<IEnumerable<Account>> Get() => Ok(Store);

        [HttpGet("{id:guid}")]
        public ActionResult<Account> Get(Guid id)
        {
            var a = Store.FirstOrDefault(x => x.Id == id);
            return a is null ? NotFound() : Ok(a);
        }

        [HttpPost]
        public ActionResult<Account> Post([FromBody] Account acc)
        {
            var newAcc = acc with { Id = acc.Id == Guid.Empty ? Guid.NewGuid() : acc.Id };
            Store.Add(newAcc);
            return CreatedAtAction(nameof(Get), new { id = newAcc.Id }, newAcc);
        }
    }
}
