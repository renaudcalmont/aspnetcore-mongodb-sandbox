using Microsoft.AspNetCore.Mvc;
using Sandbox.Server.DomainObjects.Interfaces.Handlers;
using Sandbox.Server.DomainObjects.Interfaces.Handlers.Abstract;
using Sandbox.Server.DomainObjects.Models;
using Sandbox.Server.WebApi.V1.Controllers.Abstract;

namespace Sandbox.Server.WebApi.V1.Controllers
{
    [Route("api/v1/[controller]")]
    public class PersonController : ModelController<Person, IEntityHandler<Person>>
    {
      public PersonController(IPersonHandler handler) : base(handler)
      {
      }
    }
}
