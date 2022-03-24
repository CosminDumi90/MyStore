using MyStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyStore.Infrastructure;

namespace MyStore.Data
{
    public interface IOrderRepository
    {

        Order AddNewOrder(Order newOrder);
        bool Delete(Order ordertToDelete);
        bool Exists(int id);
        IQueryable<Order> GetAll(string? city, List<string>? country, Shippers shippers);
   
        Order GetById(int orderId);
        void Update(Order orderToUpdate);
    }
    public class OrderRepository : IOrderRepository
    {
        private readonly StoreContext context;
        public OrderRepository(StoreContext context)
        {
            this.context = context;
        }

        public IQueryable<Order> GetAll(string? city, List<string>? country, Shippers shippers)
        {
            var query = this.context.Orders
                //.Include(x => x.Cust)//foreing key for customer
                .Include(x=>x.OrderDetails)
                .Select(x => x);
            if (!string.IsNullOrEmpty(city))
            {
                query = query.Where(x => x.Shipcity == city);
            }
            query = query.Where(x => x.Shipperid == (int)shippers);
            if (country.Any())
            {
                query = query.Where(x => country.Contains(x.Shipcountry));
            }
            //var pageNumber = 3;  filtru pentru afisare a rezultatelor pe pagina
            //var itemsPerPage = 20;
            //query = query.Skip(pageNumber-1 * itemsPerPage).Take(itemsPerPage);
            
            return query;
        }
        //public IQueryable<Order> GetAll(List<string> shipCities/*, List<string> shipcities*/)
        //{
        //    var query = this.context.Orders.Include(x => x.Cust).Select(x => x);

        //    if (shipCities.Count!=0)
        //    {
        //        query = query.Where(x => shipCities.Contains(x.Shipcity));
        //    }
        //    return query;
        //}
        public Order GetById(int orderId)
        {
            return context.Orders.FirstOrDefault(x => x.Orderid == orderId);
        }

 
        public void Update(Order orderToUpdate)
        {
            context.Orders.Update(orderToUpdate);
            context.SaveChanges();
        }
        public bool Exists(int id)
        {
            var exists = context.Orders.Count(x => x.Orderid == id);
            return exists == 1;
        }
        public bool Delete(Order ordertToDelete)
        {
           var deletedOrder = context.Remove(ordertToDelete);
            context.SaveChanges();
            return deletedOrder != null;
        }

        public Order AddNewOrder(Order newOrder)
        {
            var savedEntity = context.Orders.Add(newOrder).Entity;
            context.SaveChanges();
            return savedEntity;

        }

    }
}
