using Autofac;
using AutoMapper;
using FluentValidation;
using MediatR;
using MediatR.Extensions.Autofac.DependencyInjection;
using System;
using System.Collections.Generic;
using WhatsBack.Application.Behaviours;

namespace WhatsBack.Application.Extensions
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            ResolveAutoMapper(builder);
            ResolveValidator(builder);
            ResolveMeditRHandlers(builder);

        }
        private void ResolveAutoMapper(ContainerBuilder builder)
        {
            builder.Register<IConfigurationProvider>(ctx => new MapperConfiguration(cfg => cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies()))).SingleInstance();

            builder.Register<IMapper>(ctx => new Mapper(ctx.Resolve<IConfigurationProvider>(), ctx.Resolve)).InstancePerDependency();
        
        }
        private void ResolveValidator(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(ValidationBehavior<,>)).As(typeof(IPipelineBehavior<,>)).InstancePerDependency().PropertiesAutowired();
        } 
        private void ResolveMeditRHandlers(ContainerBuilder builder)
        {

            builder.RegisterMediatR(AppDomain.CurrentDomain.GetAssemblies());
            //Get the assembly name
            var assembly = AppDomain.CurrentDomain.GetAssemblies();

            // Register all the Command classes (they implement IRequestHandler) in assembly holding the Commands
            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>)).PropertiesAutowired();

            // Register the DomainEventHandler classes (they implement INotificationHandler<>) 
            // in assembly holding the Domain Events
            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(INotificationHandler<>)).PropertiesAutowired();

            // Register the Command's Validators (Validators based on FluentValidation library)
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
                .AsImplementedInterfaces().PropertiesAutowired();
        }


    }
}
