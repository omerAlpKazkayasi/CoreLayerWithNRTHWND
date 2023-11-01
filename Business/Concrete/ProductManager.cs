using Business.Abstract;
using Business.BusinessAspects.AutoFac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.AutoFac.Caching;
using Core.Aspects.AutoFac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidationException = FluentValidation.ValidationException;

namespace Business.Concrete
{
	public class ProductManager : IProductService
	{
		IProductDal _productDal;
		ICategoryService _categoryService;


		public ProductManager(IProductDal productDal, ICategoryService categoryService)
		{
			_productDal = productDal;
			_categoryService =categoryService;
		}

		//[SecuredOperation("product.add,admin")]
		[ValidationAspect(typeof(ProductValidator))]
		public IResults Add(Product product)
		{


			//business koduyla validation kodu karıştırılır bussine kodu ayrı doğrulama kodu ayrı
			//if (product.ProductName.Length<2)
			//{
			//    return new ErrorResult(Messages.ProductNameInvalid);

			//}KD

			//var context=new ValidationContext<Product>(product);

			//ProductValidator productValidator= new ProductValidator();

			//var result =productValidator.Validate(context);


			//if (!result.IsValid)
			//{
			//	throw new ValidationException(result.Errors);
			//}




			IResults result = BusinessRules.Run(CheckIfProductCountOfCategory(product.ProductId),
				CheckProductName(product.ProductName),CheckCategoryCount());
			if (result != null)
			{
				return result;
			}
			ValidationTool.Validate(new ProductValidator(), product);

			_productDal.Add(product);
			return new SuccesResult(Messages.ProductAdded);


			//aşağıyı daha verimli çalıştırmayı öğrendik 


			//if (CheckProductName(product.ProductName).IsSuccess)
			//{
			//	if (CheckIfProductCountOfCategory(product.ProductId).IsSuccess)
			//	{
			//		ValidationTool.Validate(new ProductValidator(), product);

			//		_productDal.Add(product);
			//		return new SuccesResult(Messages.ProductAdded);
			//	}
			//}

			//return new ErrorResult();
		}

		[CacheAspect]
		[CacheRemoveAspect("IProductService.Get")]
		public IDataResult<List<Product>> GetAll()
		{
			if (DateTime.Now.Hour == 22)
			{
				return new ErrorDataResult<List<Product>>(Messages.ProductNameInvalid);
			}
			return new SuccesDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductAdded);
		}

		public IDataResult<List<Product>> GetAllByCategoryId(int id)
		{
			if (DateTime.Now.Hour == 22)
			{
				return new ErrorDataResult<List<Product>>(Messages.ProductNameInvalid);
			}
			return new SuccesDataResult<List<Product>>(_productDal.GetAllByCategory(id), Messages.ProductAdded);
		}


		public IDataResult<Product> GetById(int id)

		{
			if (DateTime.Now.Hour == 22)
			{
				return new ErrorDataResult<Product>(Messages.ProductNameInvalid);
			}
			return new SuccesDataResult<Product>(_productDal.GetById(id));
		}

		public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
		{
			if (DateTime.Now.Hour == 22)
			{
				return new ErrorDataResult<List<Product>>(Messages.ProductNameInvalid);
			}
			return new SuccesDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));


		}

		public IDataResult<List<ProductDetails>> GetProductDetails()
		{
			if (DateTime.Now.Hour == 22)
			{
				return new ErrorDataResult<List<ProductDetails>>(Messages.ProductNameInvalid);
			}
			return new SuccesDataResult<List<ProductDetails>>(_productDal.GetProductDetail());
		}
		private IResults CheckIfProductCountOfCategory(int categoryid)
		{
			var list = _productDal.GetAllByCategory(categoryid).Count;
			if (list > 9)
			{
				return new ErrorResult(Messages.ProductCounOfCategoryError);
			}
			return new SuccesResult();
		}
		private IResults CheckProductName(string name)
		{
			var result = _productDal.GetAll(p => p.ProductName == name).Any();
			if (result)
			{
				return new ErrorResult(Messages.ProductNameInvalid);
			}
			return new SuccesResult();
		}
		private IResults CheckCategoryCount()
		{
			var result = _categoryService.GetAll();
			if (result.Data.Count>15)
			{
				return new ErrorResult();
			}
			return new SuccesResult();
		}
	}
}
