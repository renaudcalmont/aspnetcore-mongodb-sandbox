using System.Linq;
using System.Reflection;

namespace Sandbox.Server.Http.WebApi.V1.Views.Abstract
{
    public abstract class EntityView<TE>
        where TE : Sandbox.Server.DomainObjects.Interfaces.Models.Abstract.IEntity
    {
        public EntityView() : base()
        {
        }

        public EntityView(TE entity)
        {
            if (entity != null)
            {
                foreach (var propertyInfo in entity.GetType().GetProperties().ToArray())
                {
                    if (this.GetType().GetProperty(propertyInfo.Name) != null)
                    {
                        this.GetType().GetProperty(propertyInfo.Name).SetValue(this, propertyInfo.GetValue(entity, null));
                    }
                }
            }
        }

        public TE Hydrate(TE entity)
        {
            if (entity != null)
            {
                foreach (var propertyInfo in this.GetType().GetProperties().ToArray())
                {
                    if (entity.GetType().GetProperty(propertyInfo.Name) != null)
                    {
                        entity.GetType().GetProperty(propertyInfo.Name).SetValue(entity, propertyInfo.GetValue(this, null));
                    }
                }
            }

            return entity;
        }
    }
}