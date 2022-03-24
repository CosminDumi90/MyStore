using AutoMapper;
using MyStore.Data;
using MyStore.Domain.Entities;
using MyStore.Infrastructure;
using MyStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Services
{
    public interface IOrderService
    {
        Order AddNewOrder(Order newOrder);
        bool Delete(int id);
        bool Exists(int id);
        IEnumerable<Order> GetAll(string? city, List<string>? country, Shippers shippers);

        OrderModel GetById(int id);
        void UpdateOrder(OrderModel orderModel);
    }

    public class OrderService : IOrderService
    {
        private readonly IOrderRepository repository;
        private readonly IMapper mapper;

        public OrderService(IOrderRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public IEnumerable<Order> GetAll(string? city, List<string>? country, Shippers shippers)
        {
            var allOrders = repository.GetAll(city, country, shippers).ToList();
            

            return allOrders;
        }



        public OrderModel GetById(int id)
        {
            var orderById = repository.GetById(id);
            var orderModelById = mapper.Map<OrderModel>(orderById);
            return orderModelById;
        }



        public void UpdateOrder(OrderModel orderModel)
        {
            var orderToUpdate = mapper.Map<Order>(orderModel);
            repository.Update(orderToUpdate);
        }

        public bool Exists(int id)
        {
            return repository.Exists(id);
        }
        public bool Delete(int id)
        {
            var orderToDelete = repository.GetById(id);
            return repository.Delete(orderToDelete);
        }

        public Order AddNewOrder(Order newOrder)
        {
            return repository.AddNewOrder(newOrder);
        }
    }
}
