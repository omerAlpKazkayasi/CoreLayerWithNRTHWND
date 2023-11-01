using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Business
{
	public class BusinessRules
	{
		public static IResults Run(params IResults[] logics)//burada parametreleri virgülle ayırdığımızda her metodu bir dizenin içine atmış oluruz
		{
			//List<IResults> results = new List<IResults>();

			foreach (var logic in logics) 
			{
				if (!logic.IsSuccess)
				{
					//Burada IResult üzerinden gidiyorsak zaten mesala buraya geldiğinde metodun içerisinde succes ya true ya false döneceği için false döndüğünde Error result dneceği için direk logici dönüyoruz yani error result 
					return logic;//yani burada error result döndürecek
					//results.Add(logic);
				}
			}
			return null;
		}
	}
}
