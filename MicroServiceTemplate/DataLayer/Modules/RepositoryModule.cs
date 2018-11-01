using System;
using System.Linq;
using Autofac;

namespace MicroServiceTemplate.DataLayer
{
    /// <summary>
    /// Repository module
    /// </summary>
    /// <seealso cref="Autofac.Module" />
    public sealed class RepositoryModule : Module
    {
        /// <summary>
        /// Override to add registrations to the container.
        /// </summary>
        /// <param name="builder">The builder through which components can be registered.</param>
        /// <exception cref="ArgumentNullException">builder - builder</exception>
        /// <remarks>Note that the ContainerBuilder parameter is unique to this module.</remarks>
        protected override void Load(ContainerBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder), $"{nameof(builder)} is null.");
            }

            var currentAssembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder
                .RegisterAssemblyTypes(currentAssembly)
                .Where(x => x.Name.EndsWith("Repository", StringComparison.Ordinal))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}