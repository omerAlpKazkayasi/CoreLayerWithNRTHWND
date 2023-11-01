using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
		public static string ProductAdded="Ürün Eklendi";
        public static string ProductNameInvalid = "Ürün ismi geçersiz";

		public static string ProductCounOfCategoryError = "Bir kategoride 10dan fazla ürün olamaz";
		internal static string? AuthorizationDenied = "giriş reddedildi";
		internal static string UserRegistered = "Kayıt Olundu";
		internal static User UserNotFound;
		internal static User PasswordError;
		internal static string SuccessfulLogin = "giriş yapıldı";
		internal static string UserAlreadyExists = "aynı kullanınıcı mevcut";
		internal static object AccessTokenCreated = "token yaratıldı";
	}
}
