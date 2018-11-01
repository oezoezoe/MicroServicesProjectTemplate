using Autofac;
using AutoMapper;
using MicroServiceTemplate.BusinessLayer;
using MicroServiceTemplate.DataLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Vanguard.Framework.Core.Cqrs;

namespace MicroServiceTemplate
{
    /// <summary>
    /// The startup class.
    /// </summary>
    public sealed class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public IConfiguration Configuration { get; }


        /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        /// <remarks>This method gets called by the runtime. Use this method to configure the HTTP request pipeline.</remarks>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        /// <summary>
        /// Configures the container.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <remarks>This method gets called by the runtime. Use this method to add Dependencies to the container.</remarks>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(currentAssembly)
                .AssignableTo<Profile>()
                .As<Profile>()
                //.OnActivated(e => System.Diagnostics.Debug.WriteLine(e.Instance.GetType()))
                .AutoActivate();

            builder.Register(ctx => new MapperConfiguration(cfg =>
            {
                var resolvedProfiles = ctx.Resolve<IEnumerable<Profile>>(); // Length is 0
                foreach (var resolvedProfile in resolvedProfiles)
                {
                    cfg.AddProfile(resolvedProfile);
                }
            }).CreateMapper())
                .SingleInstance();

            builder.RegisterModule<BusinessLayerModule>();
            builder.RegisterModule<DataLayerModule>();
        }
        
        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <remarks>This method gets called by the runtime. Use this method to add services to the container.</remarks>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }
    }
}