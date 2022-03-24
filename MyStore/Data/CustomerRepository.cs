using MyStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Data
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetAll();

        Customer Add(Customer newCustomer);
       Customer GetById(int customerId);

        void Update(Customer customerToUpdate);
        bool Exists(int id);
        bool Delete(Customer customerToDelete);
    }
    public class CustomerRepository:ICustomerRepository
    {
        private readonly StoreContext context;
        public CustomerRepository(StoreContext context)
        {
            this.context = context;
        }
        public IEnumerable<Customer> GetAll()
        {
            return context.Customers.ToList();
        }

        public Customer GetById(int productId)
        {
            return context.Customers.FirstOrDefault(x => x.Custid == productId);
        }
        public Customer Add(Customer newCustomer)
        {
            var addedCustomer = context.Customers.Add(newCustomer);
            context.SaveChanges();
            return addedCustomer.Entity;
        }

        public bool Exists(int id)
        {
            var exists =  context.Customers.Count(x => x.Custid == id);
            return exists == 1;
        }

        public void Update(Customer customerToUpdate)
        {
            context.Customers.Update(customerToUpdate);
            context.SaveChanges();
        }
        public bool Delete(Customer customerToDelete)
        {
            var deletedCustomer = context.Customers.Remove(customerToDelete);
            context.SaveChanges();
            return deletedCustomer != null;
        }

    }
}
