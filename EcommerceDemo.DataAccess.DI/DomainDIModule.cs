using Autofac;
using EcommerceDemo.DataAccess;
using EcommerceDemo.DataAccess.Domain;

namespace EcommerceDemo.DataAccess.DI
{
    public class DomainDIModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>().AsImplementedInterfaces();
            builder.RegisterType<ECommerceDemoEntities>().AsSelf();
            base.Load(builder);
        }        
    }
}
