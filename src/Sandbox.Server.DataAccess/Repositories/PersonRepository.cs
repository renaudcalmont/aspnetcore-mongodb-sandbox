using Sandbox.Server.DataAccess.Repositories.Abstract;
using Sandbox.Server.DomainObjects.Interfaces.Repositories;
using Sandbox.Server.DomainObjects.Models;

namespace Sandbox.Server.DataAccess.Repositories
{
  public class PersonRepository : EntityRepository<Person>, IPersonRepository
  {

  }
}