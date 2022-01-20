using Autofac;
using EcommerceDemo.Api.Services;
using EcommerceDemo.Core.DI;

namespace EcommerceDemo.Api.DI
{
    public class DIModule : Module
    {        
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductServices>().AsImplementedInterfaces();
            builder.RegisterType<ProductCategoryAttributeServices>().AsImplementedInterfaces();
            builder.RegisterType<ProductCategoryServices>().AsImplementedInterfaces();
            builder.RegisterModule(new CoreDIModule());
            base.Load(builder);
        }

    }
}
