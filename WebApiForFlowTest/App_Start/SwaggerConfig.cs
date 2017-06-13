using System.Web.Http;
using WebActivatorEx;
using WebApiForFlowTest;
using Swashbuckle.Application;
using TRex.Metadata;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace WebApiForFlowTest
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    // For more info https://github.com/domaindrivendev/Swashbuckle/
                    c.SingleApiVersion("v1", "WebApiTest");
                    c.ReleaseTheTRex();
                })
                .EnableSwaggerUi(c => { });
            
        }
    }
}
