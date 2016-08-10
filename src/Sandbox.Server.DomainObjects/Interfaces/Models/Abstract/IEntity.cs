using System;

namespace Sandbox.Server.DomainObjects.Interfaces.Models.Abstract
{
  public interface IEntity : IModel
  {
    Guid Id { get; set; }
    Guid Revision { get; set; }
  }
}