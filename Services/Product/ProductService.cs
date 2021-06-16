using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Product
{
    public class ProductService : IProductService
    {
        private readonly DbContex _context;

        public ProductService(Data.DbContex context)
        {
            _context = context;
        }

        /// <summary>
        /// Archives a Product by setting boolean IsArchived to true
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ServiceResponse<Data.Models.Product> ArchiveProduct(int id)
        {
            try
            {
                var product = _context.Products.Find(id);
                product.IsArchived = true;
                _context.SaveChanges();

                return new ServiceResponse<Data.Models.Product>
                {
                    Data = product,
                    Time = DateTime.UtcNow,
                    Message = "Archived Product",
                    IsSeccess = true
                };
            }
            catch(Exception ex)
            {
                return new ServiceResponse<Data.Models.Product>
                {
                    Data = null,
                    Time = DateTime.UtcNow,
                    Message = ex.StackTrace,
                    IsSeccess = false
                };
            }
        }

        /// <summary>
        /// Adds new product to the database
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public ServiceResponse<Data.Models.Product> CreateProduct(Data.Models.Product product)
        {
            try
            {
                _context.Products.Add(product);

                var newInventory = new ProductInventory
                {
                    Product = product,
                    QuantityOnHand = 0, 
                    IdealQuantity = 10,
                };
                _context.ProductInventories.Add(newInventory);

                _context.SaveChanges();
                return new ServiceResponse<Data.Models.Product>
                {
                    Data = product,
                    Time = DateTime.UtcNow,
                    Message = "Saved new product",
                    IsSeccess = true
                };
            }
            catch(Exception ex)
            {
                return new ServiceResponse<Data.Models.Product>
                {
                    Data = product,
                    Time = DateTime.UtcNow,
                    Message = ex.StackTrace,
                    IsSeccess = false
                };
            }
        }

        /// <summary>
        /// Retrives all product from the database
        /// </summary>
        /// <returns></returns>
        public List<Data.Models.Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        /// <summary>
        /// Retrives a Product by primary key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Data.Models.Product GetProductById(int id)
        {
            return _context.Products.Find(id);
        }
    }
}
