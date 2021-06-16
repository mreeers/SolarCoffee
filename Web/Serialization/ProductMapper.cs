﻿using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ViewModels;

namespace Web.Serialization
{
    public class ProductMapper
    { 
        /// <summary>
        /// Maps a Product data model a ProductModel view model
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public static ProductModel SerializeProductModel(Product product)
        {
            return new ProductModel
            {
                Id = product.Id,
                CreateOn = product.CreateOn,
                UpdatedOn = product.UpdatedOn,
                Price = product.Price,
                Name = product.Name,
                Description = product.Description,
                IsTaxable = product.IsTaxable,
                IsArchived = product.IsArchived
            };
        }

        /// <summary>
        /// Maps a ProductModel view model to a Product data model
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public static Product SerializeProductModel(ProductModel product)
        {
            return new Product
            {
                Id = product.Id,
                CreateOn = product.CreateOn,
                UpdatedOn = product.UpdatedOn,
                Price = product.Price,
                Name = product.Name,
                Description = product.Description,
                IsTaxable = product.IsTaxable,
                IsArchived = product.IsArchived
            };
        }
    }
}
