﻿using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.Localization.Resource.Yaml.DependencyInjection;

namespace Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            configuration.AddOpenBehavior(typeof(AuthorizationBehavior<,>));
        });

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddYamlResourceLocalization();

        services.AddSubClassesOfType(Assembly.GetExecutingAssembly(), typeof(BaseBusinessRules));
        services.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).Where(t => t.ServiceType.Name.EndsWith("Manager"));

        return services;
    }

    public static IServiceCollection AddSubClassesOfType(
        this IServiceCollection services,
        Assembly assembly,
        Type type,
        Func<IServiceCollection, Type, IServiceCollection>? addWithLifeCycle = null
    )
    {
        var types = assembly.GetTypes().Where(t => t.IsSubclassOf(type) && type != t).ToList();
        foreach (Type? item in types)
            if (addWithLifeCycle == null)
                services.AddScoped(item);
            else
                addWithLifeCycle(services, type);
        return services;
    }

    public static IServiceCollection RegisterAssemblyTypes(this IServiceCollection services, Assembly assembly)
    {
        var types = assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract);
        foreach (Type? type in types)
        {
            var interfaces = type.GetInterfaces();
            foreach (var @interface in interfaces)
            {
                services.AddScoped(@interface, type);
            }
        }
        return services;
    }
}
