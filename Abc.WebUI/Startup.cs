using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Abc.Services.Abstract;
using Abc.Services.Concrete;
using Abc.DataAccess.Abstract;
using Abc.DataAccess.Concrete.EntityFramework;
using Abc.WebUI.Middlewares;
using Abc.WebUI.Services;
using Abc.WebUI.Entities;
using Microsoft.EntityFrameworkCore;

namespace Abc.WebUI
{
    public class Startup
    {
        
        public void ConfigureServices(IServiceCollection services)
        {
            //Eğer bir metod senden servis kullanmanı isterse ona manager ver
            services.AddScoped<IProductService, ProductManager>();
            //Managerlar da bir dataacces layera ihtiyac duydugu için gerekliolan nesneyi burada tanımla //Yarın NhibreNet kullanırsan onu da burada tanımlayabilirsin
            services.AddScoped<IProductDAL, EfProductDAL>();

            //Aynı sebebple Categry için
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ICategoryDAL, EfCategoryDAL>();

            services.AddSingleton<ICartSessionService, CartSessionService>(); //Cart Sessionları için
            services.AddSingleton<ICartService, CartService>();// CartServisleri için
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();//yazdıgımız CartSessionService içindeki dependces injectionu tanıtıyoruz

            services.AddDistributedMemoryCache();//Sessionu farklı yerlerde tutabilmek
            services.AddSession(); // sessionu kullanabilmek

            //IDENTITIY
            services.AddDbContext<CustomIdentityDbContext>(options => options.UseSqlServer(@"Server=EMIRHAZIR\SQLEXPRESS; Database=UserLog; User Id=sa; Password=123" ));//Identity database ini ayırıyorum
         /*   services.AddEntityFramework(Configuration); */services.AddIdentity<CustomIdentityUser,CustomIdentityRole>().AddEntityFrameworkStores<CustomIdentityDbContext>().AddDefaultTokenProviders();//Identityi kullanmak için 

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseFileServer(); //node_modules lerin yolunu wwwroot a vericem o yüzden bir static file ekleyeceğim 
            app.UseNodeModules(env.ContentRootPath); // ki bunu kendim yazıyorum Middlewares/AppBuilderExtensions a bak
            app.UseIdentity(); //Identity
            app.UseSession();
           
            app.UseMvcWithDefaultRoute();
        }
    }
}
