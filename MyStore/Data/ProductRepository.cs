using MyStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Data
{
    public interface IProductRepository
    {
        //data access code
        //CRUD
        IEnumerable<Product> GetAll();


        Product GetById(int productId);
        Product Add(Product newProduct);
        Product Update(Product productToUpdate);
        bool Exists(int id);
        bool Delete(Product productToDelete);
    }
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext context;
        public ProductRepository(StoreContext context)
        {
            this.context = context;
        }

        public IEnumerable<Product> GetAll()
        {
            return context.Products.ToList();
        }

        public Product GetById(int productId)
        {
            return context.Products.FirstOrDefault(x => x.Productid == productId);
        }
        public Product Add(Product newProduct)
        {
            var addedProduct = context.Products.Add(newProduct);
            context.SaveChanges();
            return addedProduct.Entity;
        }
        public Product Update(Product productToUpdate)
        {
            var updatedProduct = context.Products.Update(productToUpdate);
            context.SaveChanges();
            return updatedProduct.Entity;

        }
        public bool Exists(int id)
        {
            var exists = context.Products.Count(x => x.Productid == id);
            return exists == 1;
        }
        public bool Delete(Product productToDelete)
        {
         var deletedItem = context.Products.Remove(productToDelete);
            context.SaveChanges();
            return deletedItem != null;
        }
    }
}
