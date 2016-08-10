using Sandbox.Server.DomainObjects.Models.Abstract;

namespace Sandbox.Server.DomainObjects.Models
{
  public class Person : Entity
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
  }
}