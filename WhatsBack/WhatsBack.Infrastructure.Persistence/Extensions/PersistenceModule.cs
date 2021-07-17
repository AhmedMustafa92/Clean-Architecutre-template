using WhatsBack.Application.Interfaces.Repositories;
using WhatsBack.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using WhatsBack.Infrastructure.Persistence.Repositories;
using WhatsBack.Application.Interfaces;
using Autofac;

namespace WhatsBack.Infrastructure.Persistence.Extensions
{
    public class PersistenceModule :Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            RegisterDbConext(builder);
            ResolveUnitOfWork(builder);
            ResolveRepositories(builder);

        }

        private void RegisterDbConext(ContainerBuilder builder)
        {
            builder.Register(x =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
                optionsBuilder.UseSqlServer(x.Resolve<IConfiguration>().GetConnectionString("DefaultConnection"));
                return new ApplicationDbContext(optionsBuilder.Options);
            }).InstancePerLifetimeScope();
        }
        private void ResolveRepositories(ContainerBuilder builder)
        {
            var repositoryClass = typeof(ProductRepository);
            var repositoryInterface = typeof(IProductRepository);
            Type[] repository = System.Reflection.Assembly.Load(repositoryClass.Assembly.GetName()).GetTypes().Where(x => x.Name.Trim().ToLower().EndsWith("repository")).ToArray();
            Type[] iRepository = System.Reflection.Assembly.Load(repositoryInterface.Assembly.GetName()).GetTypes().Where(x => x.Name.Trim().ToLower().EndsWith("repository") && x.IsInterface).ToArray();
            Resolve(builder, repository, iRepository);

        }
        private void Resolve(ContainerBuilder builder, Type[] repository, Type[] irepository)
        {
            foreach (Type repositoryInterface in irepository)
            {
                Type classType = repository.FirstOrDefault(x => repositoryInterface.IsAssignableFrom(x));
                if (classType != null)
                {
                    builder.RegisterType(classType).As(repositoryInterface).PropertiesAutowired().InstancePerLifetimeScope();
                }
            }
        }
        private void ResolveUnitOfWork(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork.UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope().PropertiesAutowired();
        }


    }
}
