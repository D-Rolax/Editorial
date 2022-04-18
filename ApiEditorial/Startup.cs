using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiEditorial.Data;
using ApiEditorial.Data.Inventario;
using ApiEditorial.Data.Usuarios;
using ApiEditorial.Data.Operaciones.Promotores;
using ApiEditorial.Data.Operaciones.Caja;
using ApiEditorial.Data.Operaciones.Inventario;

namespace ApiEditorial
{
    public class Startup
    {
      
            readonly string MiCors = "MiCors";
            public Startup(IConfiguration configuration)
            {
                Configuration = configuration;
            }

            public IConfiguration Configuration { get; }

            // This method gets called by the runtime. Use this method to add services to the container.
            public void ConfigureServices(IServiceCollection services)
            {
                services.AddScoped<LibrosRepository>();
                services.AddScoped<InventarioInicialRepository>();
                services.AddScoped<CargoRepository>();
                services.AddScoped<PersonalRepository>();
                services.AddScoped<ExistenciaTextRepository>();
                 services.AddScoped<DetallePedidoRepository>();
            services.AddScoped<ContratoRepository>();
            services.AddScoped<ClienteRepository>();
            services.AddScoped<ColegioRepository>();
            services.AddScoped<PruebaRepository>();
            services.AddScoped<AmortizacionRepository>();
            services.AddScoped<VentasRepository>();
            services.AddScoped<AlmacenRepository>();
                services.AddCors(options =>
                {
                    options.AddPolicy(name: MiCors,
                        builder =>
                        {
                            builder.WithHeaders("*");
                            builder.WithOrigins("*");

                        }
                        );
                });
                services.AddControllers();
            }

            // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
            public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            {
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }

                app.UseHttpsRedirection();

                app.UseRouting();
                app.UseCors(MiCors);
                app.UseAuthorization();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
            }
        }
    
}
