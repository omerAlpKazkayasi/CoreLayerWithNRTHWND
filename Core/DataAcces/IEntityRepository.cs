﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAcces
{
    public interface IEntityRepository<T> where T : class,IEntity,new()
    {
        List<T> GetAll(Expression<Func<T,bool>> filter=null);

        T Get(Expression<Func<T, bool>> filter = null);
        T GetById(int id);
        void Add(T  entity);
        void Update(T  entity);
        void Delete(T entity);
        List<T> GetAllByCategory(int categoryId);
    }
}
