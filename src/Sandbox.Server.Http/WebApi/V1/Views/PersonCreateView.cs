using Sandbox.Server.DomainObjects.Models;
using Sandbox.Server.Http.WebApi.V1.Views.Abstract;

namespace Sandbox.Server.Http.WebApi.V1.Views
{
    public class PersonCreateView : EntityView<Person>
    {
        public PersonCreateView() : base()
        {
        }

        public PersonCreateView(Person model) : base(model)
        {
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}