using JSM.POCs.Communication.MessagePack.PriceAPI.Formatters;
using MessagePack;
using MessagePack.AspNetCoreMvcFormatter;
using MessagePack.Resolvers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace JSM.POCs.Communication.MessagePack.PriceAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "JSM.POCs.Communication.MessagePack.PriceAPI", Version = "v1" });
            });

            services.AddControllers().AddMvcOptions(options =>
            {
                options.InputFormatters.Add(new MessagePackInputFormatterLogger(
                    new MessagePackInputFormatter(StandardResolver.Options.WithSecurity(MessagePackSecurity.UntrustedData))
                ));
                options.OutputFormatters.Clear();
                options.OutputFormatters.Add(new MessagePackOutputFormatterLogger(
                    new MessagePackOutputFormatter(StandardResolver.Options)
                ));
                options.OutputFormatters.Add(new SystemTextJsonOutputFormatter(new JsonSerializerOptions()));
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "JSM.POCs.Communication.MessagePack.PriceAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
