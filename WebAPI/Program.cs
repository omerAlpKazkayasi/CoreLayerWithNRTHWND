using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Business.Abstract;
using Business.Concrete;
using Business.DependencyResolvers.AutoFac;
using Core.DependencyResolvers;
using Core.Extencsions;
using Core.Security.Encryption;
using Core.Security.JWT;
using Core.Utilities.IoC;
using DataAccess.Abstract;
using DataAccess.Concrete.EF;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			//builder.Services.AddSingleton<IProductService, ProductManager>();
			//builder.Services.AddSingleton<IProductDal, EfProductDal>();
			//Core katmanýna yazdýk aþaðýda ki dependecyi
			//builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			
			var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();

			builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateLifetime = true,
						ValidIssuer = tokenOptions.Issuer,
						ValidAudience = tokenOptions.Audience,
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
					};
				});
			builder.Services.AddDependencyResolvers(new ICoreModule[] {new CoreModule()});

			builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
			builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

			builder.Host.ConfigureContainer<ContainerBuilder>(options =>
			{
				options.RegisterModule(new AutofacBusinessModule());
			});

			var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

			
            app.UseHttpsRedirection();
			//yaþam döngüsünde hangi yapýlarýn devreye gireeðini söylüyoruz
			app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}