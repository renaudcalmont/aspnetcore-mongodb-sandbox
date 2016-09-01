using System;
using Sandbox.Server.DomainObjects.Models;
using Sandbox.Server.Http.WebApi.V1.Views.Abstract;

namespace Sandbox.Server.Http.WebApi.V1.Views
{
    public class PersonRetrieveView : EntityView<Person>
    {
        public PersonRetrieveView() : base()
        {
        }

        public PersonRetrieveView(Person entity) : base(entity)
        {
        }

        public Guid Id { get; set; }

        public Guid Revision { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}