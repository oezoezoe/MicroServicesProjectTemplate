using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Vanguard.Framework.Core.Cqrs;

namespace MicroServiceTemplate.BusinessLayer
{
    /// <summary>
    /// Cqrs module
    /// </summary>
    /// <seealso cref="Autofac.Module" />
    public sealed class CqrsModule : Module
    {
        /// <summary>
        /// Override to add registrations to the container.
        /// </summary>
        /// <param name="builder">The builder through which components can be
        /// registered.</param>
        /// <exception cref="ArgumentNullException">builder - builder</exception>
        /// <remarks>
        /// Note that the ContainerBuilder parameter is unique to this module.
        /// </remarks>
        protected override void Load(ContainerBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder), $"{nameof(builder)} is null.");
            }

            var currentAssembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder
              .RegisterType<CommandDispatcher>()
              .As<ICommandDispatcher>()
              .InstancePerLifetimeScope();

            builder
                .RegisterType<QueryDispatcher>()
                .As<IQueryDispatcher>()
                .InstancePerLifetimeScope();

            builder
                .RegisterAssemblyTypes(currentAssembly)
                .Where(x => x.Name.EndsWith("QueryHandler", StringComparison.Ordinal))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder
                .RegisterAssemblyTypes(currentAssembly)
                .Where(x => x.Name.EndsWith("CommandHandler", StringComparison.Ordinal))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

        }
    }
}
