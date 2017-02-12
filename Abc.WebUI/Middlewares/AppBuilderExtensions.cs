using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Abc.WebUI.Middlewares
{
    public static class AppBuilderExtensions
    {
        public static IApplicationBuilder UseNodeModules(this IApplicationBuilder app,string root)
        {
            //rootun içinde node_modules arayacağı için bunu bir pathe aldım
            var path = Path.Combine(root,"node_modules");
            //Provider ise bu static dosyaları taşıyan ara servis
            var provider = new PhysicalFileProvider(path);

            var option = new StaticFileOptions();
            option.RequestPath = "/node_modules";//eğer sana bu adresten bir istek gelirse
            option.FileProvider = provider; //bunun dosyası yukarıda tanımlanan provider

            //dolayısıyla 
            app.UseStaticFiles(option);

            return app;


        }
    }
}
