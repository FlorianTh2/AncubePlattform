using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListMVC.Models;
using BookListMVC.Models.User;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BookListMVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }






        // Die Methode ConfigureServices bereitet Klassen und Objekte vor,
        // die der Dependency-Injection-Mechanismus von ASP.NET 5 bereitstellt.
        // Diese Klassen / Objekte, also die Injections werden als Services bezeichnet.
        // hier wird also UNTER ANDEREM die Klasse spezifiziert, die später injected werden soll
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));



            // 1. AddIdentity registriert die Services für ASP.NET Identity beim Dependency-Injection-Mechanismus von ASP.NET
            // 2. AddIdentity adds in general the repository (a wrapper for the injected database) to the application
            // 3. Adds Authentication
            //      - Identity uses CookieAuthenticationMiddleware per default (https://entwickler.de/online/asp-net-identity-159813.html)
            //      - (Session- ) Token wird bei User bei erfolgreicher Anmeldung hinterlegt
            // 4. ...
            services.AddIdentity<IdentityUser, IdentityRole>()
                // to make entityFramework function with identity
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 10;
                options.Password.RequiredUniqueChars = 3;

                //options.User.RequireUniqueEmail = true;
                //options.Lockout.MaxFailedAccessAttempts = 3;
                //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                //options.SignIn.RequireConfirmedEmail = true;
                // ...
            });

            // 3 types: AddSingleton - AddTransient - AddScoped
            // Singleton=1 instance per application start -> whole applicaton livetime
            // Transient=1 instance each time the Interface is requested (the controller ctor requests the interface and this controller and his ctor is requested for each individual http request)
            // Scoped=1 instaqnce for each request within the scope, so if one webrequests has multiple http request to the same object-> this object get reused
            //...<What is required by ctor (what interface), which implemented class to bind on that interface>

            // here scoped since singleton thows an error, i dont know why
            services.AddScoped<IUserRepository, SQLUserRepository>();
            // for rest apis
            // services.AddControllers();
            services.AddControllersWithViews();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
        }



        //private static void HandleMapTest1(IApplicationBuilder app)
        //{
        //    app.Run(async context =>
        //    {
        //        await context.Response.WriteAsync("Map Test 1");
        //    });
        //}

        //private static void HandleMapTest2(IApplicationBuilder app)
        //{
        //    app.Run(async context =>
        //    {
        //        await context.Response.WriteAsync("Map Test 2");
        //    });
        //}




        // configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // UseStatusCodePagesWithRedirects
                // UseStatusCodePagesWithReExcecute does same out of user-view
                // internel WithRedirects sends alters the given url to Error/Code inside the
                // http-pipeline = no re-executing of the pipeline and navigation to error page
                // with reExcute (better) the http-pipeline gets executed again with error-url since the start
                // (= no altering)
                //app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }
            else
            {
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
                app.UseExceptionHandler("/Error");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // parts of the pipeline are called middleware
            // the middleware (each component) has access to the request and to the response
            // each middleware has 3 top-commands to choose from: Run, Use, Map


            //// finish the http pipeline with the given messagE
            //// RUN FINISHES PIPELINE, IF U WANT TO GO TO NEXT PART OF PIPELINE (OTHER DELEGATE) USE "USE"
            //// uses only one request delegate (single annonymous function) for all requests
            //// here we get the Response and write into the Response "..."
            //app.Run(context => context.Response.WriteAsync("i get displayd"));


            //app.Use(async (context, next) =>{await next.Invoke();});
            //app.Run(async context =>{await context.Response.WriteAsync("Hello from 2nd delegate.");});

            // response: localhost:1234	Hello from non-Map delegate.
            //localhost: 1234 / map1 Map Test 1
            //localhost: 1234 / map2 Map Test 2
            //localhost: 1234 / map3 Hello from non - Map delegate.
            //app.Map("/map1", HandleMapTest1);
            //app.Map("/map2", HandleMapTest2);
            //app.Run(async context =>{await context.Response.WriteAsync("Hello from non-Map delegate. <p>");});


            app.Use(async (context, next) =>
            {
                logger.LogInformation("Middleware (MW) 1: Incoming Request");
                await next();
                logger.LogInformation("MW2: Outgoing Response");
            });


            //// forces port 80 to 443
            app.UseHttpsRedirection();

            app.Use(async (context, next) =>
            {
                logger.LogInformation("Middleware (MW) 3: Incoming Request");
                await next();
                logger.LogInformation("MW3: Outgoing Response");
            });

            //// returns static files and short-circuits further request processing.
            //// in short: if not used: you can not use content in wwwwroot folder
            //// this method marks files as static if they are in the wwwroot folder
            app.UseStaticFiles();

            app.Use(async (context, next) =>
            {
                logger.LogInformation("Middleware (MW) 5: Incoming Request");
                await next();
                logger.LogInformation("MW5: Outgoing Response");
            });

            app.UseRouting();

            // for identity-Framework
            //// attempts to authenticate the user before they're allowed access to secure resources
            app.UseAuthentication();

            ////authorizes a user to access secure resources.
            app.UseAuthorization();


            // REST APIs should use attribute routing.
            // since you can be more granular with what is needed and what is not needed
            // Also you can better use HTTP-Verbs since often with APIs you have multiple
            // HTTP-Verbs on the same method -> Get, Post...
            // MapControllers() enables attribute-routing
            // -> endpoints.MapControllers();
            // Attribute routing provides fine-grained control to make the ID required for some actions and not for others.
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=Home}/{action=Index}/{id?}");
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
