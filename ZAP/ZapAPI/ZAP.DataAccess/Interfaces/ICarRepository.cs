using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ZAP.DataAccess.Entities;

namespace ZAP.DataAccess.Repositories
{
    public interface ICarRepository
    {
        Car Get(int id);

        IEnumerable<Car> GetAll();

        IEnumerable<Car> Find(Expression<Func<Car, bool>> predicate);

        Car SingleOrDefault(Expression<Func<Car, bool>> predicate);


        void Add(Car car);

        void AddRange(IEnumerable<Car> cars);

        void Remove(Car car);

        void RemoveRange(IEnumerable<Car> cars);

        IEnumerable<Car> GetList(string search, IEnumerable<int> carTypeIds, IEnumerable<int> carClassIds, IEnumerable<int> carBrandIds, int? sortById);
    }
}