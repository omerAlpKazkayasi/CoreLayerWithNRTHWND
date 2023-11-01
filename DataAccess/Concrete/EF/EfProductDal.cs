using Core.EF;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EF
{
    public class EfProductDal : EfEntityRepositoryBase<Product, NORTHContextDb>, IProductDal
    {

        public List<ProductDetails> GetProductDetail()
        {
            using (NORTHContextDb ctx=new NORTHContextDb())
            {
                var result = from p in ctx.Products
                             join c in ctx.Categories
                             on p.CategoryId equals c.CategoryId
                             select new ProductDetails
                             {
                                 ProductId = p.ProductId,
                                 ProductName = p.ProductName,
                                 CategoryName = c.CategoryName,
                                 UnitsInStock = p.UnitsInStock
                             };
                return result.ToList();


            }
        }
    }
}
