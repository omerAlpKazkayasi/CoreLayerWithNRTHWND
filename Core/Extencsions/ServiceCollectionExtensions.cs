using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extencsions
{
	public static class ServiceCollectionExtensions
	{
		//Apimizin servis bağımlıklarını ekelediğimiz veya araya girmesini istediğimiz servislerin tam kendisidir
		public static IServiceCollection AddDependencyResolvers(this IServiceCollection serviceCollection, ICoreModule[] modules)
		{
			foreach (var module in modules)
			{
				module.Load(serviceCollection);
			}
			return ServiceTool.Create(serviceCollection);

			//core katmanında ki bütün ınjecktionları bir araya toplamamızı sağlar
		}
	}
}
