using System;
using Sandbox.Server.DomainObjects.Models;
using Sandbox.Server.WebApi.V1.Views.Abstract;

namespace Sandbox.Server.WebApi.V1.Views
{
    public class PersonUpdateView : EntityView<Person>
    {
        public PersonUpdateView() : base()
        {
        }

        public PersonUpdateView(Person entity) : base(entity)
        {
        }

        public Guid Revision { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}