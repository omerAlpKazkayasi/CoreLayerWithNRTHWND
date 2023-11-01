using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Caching
{
	public interface ICacheManager
	{
		void Add(string key, object value,int duration);
		T Get<T>(string key);
		object Get(string key);
		bool IsAdd(string key);//cache de oluup olmadığını kontrol etme
		void Remove(string key);//cache den uçurma
		void RemoveByPattern(string pattern);//mesala product eklendiğinde prouct ile ilgili olanları uçur yok categori eklendiyse categori olanları uçur igibi gibi

	}
}
