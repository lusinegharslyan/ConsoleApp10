using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp10
{
    //    Exercise 2: Generic Repository Pattern
    //Objective: Implement a generic repository pattern to manage a collection of items.
    //Requirements:
    //Generics
    //Interfaces
    //Extension Methods
    //Description:
    //Create a generic interface IRepository<T> with methods like Add, Remove, and GetAll.
    //Implement a class Repository<T> that implements IRepository<T>.
    //Add extension methods to filter and sort items in the repository.


    public interface IRepository<TEntity, TKey> where TEntity : EntityBase<TKey> where TKey : struct
    {
        void Add(TEntity t);
        void Remove(TKey key);
        IEnumerable<TEntity> GetAll();
        void Update(TEntity t, TKey key);

        TEntity GetById(TKey key);
    }


    public class EntityBase<TKey>
    {
        public EntityBase(TKey key)
        {
            Key = key;
        }

        public TKey Key { get; set; }
    }

    public class Car : EntityBase<int>
    {
        public Car(string mark, string color, int price, int id) : base(id)
        {
            Mark = mark;
            Color = color;
            Price = price;

        }

        public string Mark { get; set; }
        public string Color { get; set; }
        public int Price { get; set; }
    }

    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : EntityBase<TKey> where TKey : struct
    {

        List<TEntity> entities = new List<TEntity>();
        public void Add(TEntity t)
        {
            entities.Add(t);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return entities;
        }

        public TEntity GetById(TKey key)
        {
            return entities.FirstOrDefault(entity => entity.Key.Equals(key));
        }

        public void Remove(TKey key)
        {
            var entity = GetById(key);
            entities.Remove(entity);
        }

        public void Update(TEntity t, TKey key)
        {
            var e = GetById(key);
            int index = entities.IndexOf(e);
            entities[index] = t;
        }

    }

    public delegate bool Where<T>(T t);
    public delegate bool Where1<T>(T t, T t1);
    static class Extentions
    {
        public static List<T> Filter<T>(this List<T> list, Where<T> condition)
        {
            List<T> result = new List<T>();
            foreach (var item in list)
            {
                if (condition(item))
                {
                    result.Add(item);
                }
            }
            return result;
        }

        public static List<T> Sorting<T>(this List<T> list, Where1<T> condition)
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                var min = list[i];
                if (condition(list[i], list[i + 1]))
                {
                    list[i] = list[i + 1];
                    list[i + 1] = min;
                }
            }
            return list;
        }



    }
    internal class Program
    {

        //public static List<T> SortingByAscending<T>(List<T> list)
        //{
        //    for (int i = 0; i < list.Count; i++)
        //    {
        //        var min = list[i];
        //        for (int j = i; j < list.Count; j++)
        //        {
        //            if (list[j].Price < min.Price)
        //            {

        //                min = list[j];
        //                list[j] = list[i];
        //                list[i] = min;
        //            }
        //        }
        //    }

        //    return list;
        //}

        //public static List<Car> SortingByDescending(List<Car> cars)
        //{
        //    for(int i = 0; i < cars.Count; i++)
        //    {
        //        var max = cars[i];
        //        for(int j = i; j < cars.Count; j++)
        //        {
        //            if (cars[j].Price > max.Price)
        //            {
        //                max = cars[j];
        //                cars[j] = cars[i];
        //                cars[i] = max;
        //            }
        //        }
        //    }
        //    return cars;
        //}


        static void Main(string[] args)
        {
            Repository<Car, int> carRepository = new Repository<Car, int>();
            carRepository.Add(new Car("Mersedes", "White", 1800, 1));
            carRepository.Add(new Car("Nissan", "Black", 2000, 2));
            carRepository.Add(new Car("Toyota", "Red", 1500, 3));
            carRepository.Add(new Car("Opel", "Gray", 2000, 4));
            carRepository.Add(new Car("Ford", "Red", 2500, 5));
            carRepository.Add(new Car("Kia", "White", 1800, 6));
            carRepository.Add(new Car("BMW", "Black", 2000, 7));
            //var result = carRepository.GetById(1);
            //Console.WriteLine(result.Mark);
            //carRepository.Remove(1);
            //var result2 = carRepository.GetAll();
            //foreach(var entity in result2)
            //{
            //    Console.WriteLine(entity.Mark);
            //}
            //carRepository.Update((new Car("Honda", "Yellow", 2)), 2);
            //var result3 = carRepository.GetById(2);
            //Console.WriteLine(result3.Mark);


            var result = carRepository.GetAll().ToList();
            //var r=result.Filter(x => x.Color == "White");
            //Console.WriteLine(r.Count);
            //foreach(var item in r)
            //{
            //    Console.WriteLine(item.Color);
            //}

            //result.Sort((x, y) =>{ y.Price < x.Price});

            result.Sorting((x, y) => y.Price < x.Price);


            //var sort = SortingByAscending(result);
            //foreach (var item in sort)
            //{
            //    Console.WriteLine(item.Price);
            //}
            //var sort = SortingByDescending(result);
            //foreach (var item in sort)
            //{
            //    Console.WriteLine(item.Price);
            //}
            //var sort= result.Sort(SortingByDescending);
            //foreach(var item in sort)
            //{
            //    Console.WriteLine(item.Price);
            //}

            //var sort1 = result.Sort(SortingByAscending);
            //foreach (var item in sort)
            //{
            //    Console.WriteLine(item.Price);
            //}
            Console.WriteLine("Hello World");
            Console.ReadLine();



        }
    }
}
