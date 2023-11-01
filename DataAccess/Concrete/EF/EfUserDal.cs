using Core.EF;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EF
{
	public class EfUserDal : EfEntityRepositoryBase<User, NORTHContextDb>, IUserDal
	{
		public List<OperationClaim> GetClaims(User user)
		{
			using (var context = new NORTHContextDb())
			{
				var result = from operationClaim in context.OperationClaims
							 join userOperationClaim in context.UserOperationClaims
								 on operationClaim.Id equals userOperationClaim.OperationClaimId
							 where userOperationClaim.UserID == user.Id
							 select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
				return result.ToList();

			}
		}
	}
}
