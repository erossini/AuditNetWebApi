using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Projects.Providers;
using Microsoft.AspNetCore.Mvc;
using Projects.Providers.Database;

namespace Projects.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private IContactsProvider _provider;

        public ContactsController(IContactsProvider provider)
        {
            _provider = provider;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<ContactEntity>> Get()
        {
            return Ok(_provider.GetValues());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactEntity>> Get(int id)
        {
            return Ok(await _provider.GetAsync(id));
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<int>> Post(ContactEntity value)
        {
            return Ok(await _provider.InsertAsync(value));
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task Put(int id, ContactEntity value)
        {
            await _provider.ReplaceAsync(id, value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return Ok(await _provider.DeleteAsync(id));
        }

        // DELETE api/values/delete
        [HttpDelete]
        [Route("delete")]
        public async Task<ActionResult<bool>> Delete(string ids)
        {
            return Ok(await _provider.DeleteMultipleAsync(ids.Split(',').Select(s => long.Parse(s)).ToArray()));
        }
    }
}