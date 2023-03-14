using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWallet.Test
{
    internal class TestStartup
    {
        public IConfiguration Configuration { get; }
        public TestStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add MemoryCache service
            services.AddMemoryCache();

            // Add other services as needed
        }
    }
}
