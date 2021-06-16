using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ViewModels;

namespace Web.Serialization
{
    public static class CustomerMapper
    {
        /// <summary>
        /// Serializes a Customer data model into a CustomerModel view model
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public static CustomerModel SerializeCustomer(Customer customer)
        {
            return new CustomerModel
            {
                Id = customer.Id,
                CreateOn = customer.CreateOn,
                UpdatedOn = customer.UpdatedOn,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PrimaryAddress = MapCustomerAddress(customer.PrimaryAdress)
            };
        }

        /// <summary>
        /// Serialize a CustomerModel view model into a Customer data model
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public static Customer SerializeCustomer(CustomerModel customer)
        {
            return new Customer
            {
                Id = customer.Id,
                CreateOn = customer.CreateOn,
                UpdatedOn = customer.UpdatedOn,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PrimaryAdress = MapCustomerAddress(customer.PrimaryAddress)
            };
        }

        /// <summary>
        /// Maps a CustomerAdddress data model to a CustomerAddressModel view model
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static CustomerAddressModel MapCustomerAddress(CustomerAddress address)
        {
            return new CustomerAddressModel
            {
                Id = address.Id,
                AdressLine1 = address.AdressLine1,
                AdressLine2 = address.AdressLine2,
                City = address.City,
                State = address.State,
                Country = address.Country,
                PostalCode = address.PostalCode,
                CreateOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };
        }

        /// <summary>
        /// Maps a CustomerAdddressModel view model to a CustomerAddress data model
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static CustomerAddress MapCustomerAddress(CustomerAddressModel address)
        {
            return new CustomerAddress
            {
                Id = address.Id,
                AdressLine1 = address.AdressLine1,
                AdressLine2 = address.AdressLine2,
                City = address.City,
                State = address.State,
                Country = address.Country,
                PostalCode = address.PostalCode,
                CreateOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };
        }
    }
}
