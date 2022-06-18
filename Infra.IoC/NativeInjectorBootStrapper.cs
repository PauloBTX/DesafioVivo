


using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Services;
using Infra.Data.Context;
using Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infra.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<VivoContext>();

            #region Services
            services.AddScoped<IClienteService, ClienteService>();
            #endregion

            #region Repositories
            services.AddScoped<IClienteRepository, ClienteRepository>();
            #endregion
        }
    }
}
