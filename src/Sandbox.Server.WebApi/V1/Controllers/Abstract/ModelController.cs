using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sandbox.Server.DomainObjects.Interfaces.Handlers.Abstract;
using Sandbox.Server.DomainObjects.Interfaces.Models.Abstract;

namespace Sandbox.Server.WebApi.V1.Controllers.Abstract
{
    public abstract class ModelController<TE, TEH> : Controller
      where TE : IEntity
      where TEH : IEntityHandler<TE>
    {
        protected readonly TEH _handler;

        protected ModelController(TEH handler)
        {
            _handler = handler;
        }

        /*// GET api/values
        [HttpGet]
        public IEnumerable<TE> Get()
        {
          return new LinkedList<TE>();
        }*/

        [HttpGet("{id}")]
        public async Task<TE> Get(Guid id)
        {
            return await _handler.Retrieve(id);
        }

        [HttpPost]
        public async Task<TE> Post([FromBody] TE instance)
        {
            return await _handler.Create(instance);
        }

        [HttpPut("{id}")]
        public async Task<TE> Put(int id, [FromBody] TE instance)
        {
            return await _handler.Update(instance);
        }

        [HttpDelete("{id}")]
        public async void Delete(Guid id)
        {
            var instance = await _handler.Retrieve(id);
            if (instance != null) {
                _handler.Delete(instance);
            }
        }
    }
}