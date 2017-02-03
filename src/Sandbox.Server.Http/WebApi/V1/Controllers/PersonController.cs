using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sandbox.Server.DomainObjects.Interfaces.Handlers;
using Sandbox.Server.DomainObjects.Interfaces.Handlers.Abstract;
using Sandbox.Server.DomainObjects.Models;
using Sandbox.Server.Http.WebApi.V1.Controllers.Abstract;
using Sandbox.Server.Http.WebApi.V1.Views;

namespace Sandbox.Server.Http.WebApi.V1.Controllers
{
    [Route("api/v1/persons")]
    public class PersonController : EntityController<Person, IEntityHandler<Person>>
    {
        public PersonController(IPersonHandler handler) : base(handler)
        {
        }

        /// <summary>
        /// Create a new instance of Person
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PersonRetrieveView> Post([FromBody] PersonCreateView request)
        {
            var entity = new Person();
            request.Hydrate(entity);
            entity = await _handler.Create(entity);

            Response.StatusCode = 201;
          var view = new PersonRetrieveView(entity);
          return view;
        }

        /// <summary>
        /// Get a specific user based on Guid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<PersonRetrieveView> Get(Guid id)
        {
          var entity = await _handler.Retrieve(id);
          if (entity == null)
          {
            Response.StatusCode = 404;
            return null;
          }

          var view = new PersonRetrieveView(entity);
          return view;
        }

        /// <summary>
        /// Update a specific Person instance based on guid (param) and PersonUpdateView (body)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<PersonRetrieveView> Put(Guid id, [FromBody] PersonUpdateView request)
        {
            var entity = new Person();
            request.Hydrate(entity);
            entity.Id = id;
            entity = await _handler.Update(entity);

          var view = new PersonRetrieveView(entity);
          return view;
        }

        /// <summary>
        /// Delete a specific Person instance based on guid
        /// </summary>
        /// <remarks>
        /// Note that id is a Guid, not an integer
        /// </remarks>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public virtual async void Delete(Guid id)
        {
            var instance = await _handler.Retrieve(id);
            if (instance == null) {
              Response.StatusCode = 404;
              return;
            }
            
            _handler.Delete(instance);
        }

        /// <summary>
        /// Retrieve all Person instances stored
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<PersonRetrieveView>> Get()
        {
          var list = await _handler.RetrieveAll();
          var view = new HashSet<PersonRetrieveView>();
          foreach (var entity in list)
          {
            var viewItem = new PersonRetrieveView(entity);
            view.Add(viewItem);
          }

          return view;
        }

        /// <summary>
        /// Get all Revisions, returning a dictionary of Id/Revision guids
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("revisions")]
        public async Task<Dictionary<Guid, Guid>> GetRevisions()
        {
          var list = await _handler.RetrieveAll();
          var view = new Dictionary<Guid, Guid>();
          foreach (var entity in list)
          {
            view.Add(entity.Id, entity.Revision);
          }

          return view;
        }
    }
}
