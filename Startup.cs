using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace InterceptorExample
{
    public class MyDbCommandInterceptor : DbCommandInterceptor
    {
    }

    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<DatabaseContext>(options =>
            {
                // Does not work when using postgres
                options.UseNpgsql("Server=localhost;Port=5432;Database=test;User Id=postgres;Password=mysecretpassword;");

                // Does work when using in memory
                // options.UseInMemoryDatabase("Sample Application");

                // Add a simple do-nothing interceptor
                options.AddInterceptors(new MyDbCommandInterceptor());

                // Add XRay interceptor
                options.AddXRayInterceptor();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseXRay("Sample Application");

            app.UseDeveloperExceptionPage();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
