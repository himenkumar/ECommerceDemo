using Autofac;
using EcommerceDemo.Core.Services;
using EcommerceDemo.DataAccess.DI;

namespace EcommerceDemo.Core.DI
{
    public class CoreDIModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductAppServices>().AsImplementedInterfaces();
            builder.RegisterType<ProductCategoryAttributeAppServices>().AsImplementedInterfaces();
            builder.RegisterType<ProductCategoryAppServices>().AsImplementedInterfaces();
            builder.RegisterModule(new DomainDIModule());
            base.Load(builder);
        }
    }
}
