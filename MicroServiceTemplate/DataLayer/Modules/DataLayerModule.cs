using System;
using Autofac;

namespace MicroServiceTemplate.DataLayer
{
    /// <summary>
    /// Autofac module for registering data layer modules
    /// </summary>
    /// <seealso cref="Autofac.Module" />
    public sealed class DataLayerModule : Module
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

            builder.RegisterModule<RepositoryModule>();
        }
    }
}