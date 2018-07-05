using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SamplesRequest.Repositories;
using SamplesRequest.Services;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using SamplesRequest.Models.Models.DAL.DBContext;
using SamplesRequest.Models.Models.DAL.Repositories;
using SamplesRequest.Models.Models.Entities;
using SamplesRequest.Models.Models.ViewModels;
using Security.Models;
using Security.Models.DAL;
using Security.Models.Entities;
using User = SamplesRequest.Models.Models.Entities.User;
using Security.Models.DAL.Repositories;
using System.Linq;

namespace SamplesRequest
{
    public class Startup
    {

        public IContainer ApplicationContainer { get; private set; }

        public Startup(IConfiguration configuration)
        {
            
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper();
            services.AddMvc();
            services.AddDbContext<SamplesRequestDBContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), sqlServerOptions =>
                {
                    sqlServerOptions.MigrationsAssembly("SamplesRequest.Models");
                });
            });

            services.AddDbContext<SecurityDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SecurityConnection"), sqlServerOptions =>
                {
                    sqlServerOptions.MigrationsAssembly("Security.Models");
                });
            });

            
            services.AddIdentity<Security.Models.Entities.User, Role>().AddUserStore<ApplicationIdentityUserStore>()
            .AddRoleStore<ApplicationIdentityRoleStore>();

        
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            services.Configure<AreaSettings>(Configuration.GetSection("AreaSettings"));
            var builder = new ContainerBuilder();
            builder.Populate(services);

            //Register here all the dependencies using the AUTOFAC builder
            builder.RegisterType<RoleClaimsTransformer>().As<IClaimsTransformation>();
            

            builder.RegisterType<SamplesRequestsRepository>().As<ISamplesRequestsRepository>();
            builder.RegisterType<SampleRequestServiceController>().As<ISampleRequestServiceController>();
            builder.RegisterType<GenericRepository<SamplePriority>>().As<IGenericRepository<SamplePriority>>();
            builder.RegisterType<GenericRepository<SamplePurpose>>().As<IGenericRepository<SamplePurpose>>();
            builder.RegisterType<GenericRepository<User>>().As<IGenericRepository<User>>();
            builder.RegisterType<WorkflowRepository>().As<IWorkflowRepository>();
            builder.RegisterType<GenericRepository<WorkflowUser>>().As<IGenericRepository<WorkflowUser>>();
            builder.RegisterType<ViewRenderService>().As<IViewRenderService>();
            builder.RegisterType<EmailService>().As<IEmailService>();
            builder.RegisterType<GenericRepository<RequestWorkflowStep>>().As<IGenericRepository<RequestWorkflowStep>>();
            builder.RegisterType<GenericRepository<Client>>().As<IGenericRepository<Client>>();
            builder.RegisterType<GenericRepository<Address>>().As<IGenericRepository<Address>>();
            builder.RegisterType<UsersRepository>().As<IUsersRepository>();
            builder.RegisterType<RolesRepository>().As<IRolesRepository>();


            this.ApplicationContainer = builder.Build();

            return new AutofacServiceProvider(this.ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifeTime)
        {

            app.Use(async (context, next) =>
            {
                using (var dbcontext = new SecurityDbContext(Configuration.GetConnectionString("SecurityConnection")))
                {
                    if (dbcontext.Users.FirstOrDefault(e => e.uname == context.User.Identity.Name) == null)
                    {
                        Security.Models.Entities.User user = new Security.Models.Entities.User();
                        user.uname = context.User.Identity.Name;
                        user.active = true;
                        dbcontext.Users.Add(user);
                        dbcontext.SaveChanges();
                    }

                    await next();
                }
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "node_modules")),
                RequestPath = "/node_modules"
            });

            app.UseStaticFiles();

            app.UseAuthentication();
            

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=SampleRequests}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "SampleRequests", action = "Index" });
            });

            appLifeTime.ApplicationStopped.Register(() => this.ApplicationContainer.Dispose());

            
        }
    }
}
