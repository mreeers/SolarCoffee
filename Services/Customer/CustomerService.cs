using Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Customer
{
    public class CustomerService : ICustomerService
    {
        private readonly DbContex _contex;

        public CustomerService(Data.DbContex contex)
        {
            _contex = contex;
        }

        /// <summary>
        /// Adds a new Customer record
        /// </summary>
        /// <param name="customer">Customer instance</param>
        /// <returns>ServiceResponse<Data.Models.Customer></returns>
        /// <exception cref="NotImplementedException"></exception>
        public ServiceResponse<Data.Models.Customer> CreateCustomers(Data.Models.Customer customer)
        {
            try
            {
                _contex.Customers.Add(customer);
                _contex.SaveChanges();
                return new ServiceResponse<Data.Models.Customer>
                {
                    IsSuccess = true,
                    Message = "New customer added",
                    Time = DateTime.UtcNow,
                    Data = customer
                };
            }
            catch(Exception ex)
            {
                return new ServiceResponse<Data.Models.Customer>
                {
                    IsSuccess = false,
                    Message = ex.StackTrace,
                    Time = DateTime.UtcNow,
                    Data = customer
                };
            }
        }

        /// <summary>
        /// Deletes a customer record
        /// </summary>
        /// <param name="id">int customer primary key</param>
        /// <returns>ServiceResponse<bool></returns>
        public ServiceResponse<bool> DeleteCurtomer(int id)
        {
            var customer = _contex.Customers.Find(id);

            if(customer == null)
            {
                return new ServiceResponse<bool>
                {
                    Time = DateTime.UtcNow,
                    IsSuccess = false,
                    Message = "Customer to delete not found",
                    Data = false
                };
            }
            try
            {
                _contex.Customers.Remove(customer);
                _contex.SaveChanges();
                return new ServiceResponse<bool>
                {
                    Time = DateTime.UtcNow,
                    IsSuccess = true,
                    Message = "Customer created!",
                    Data = true
                };
            }
            catch(Exception ex)
            {
                return new ServiceResponse<bool>
                {
                    Time = DateTime.UtcNow,
                    IsSuccess = false,
                    Message = ex.StackTrace,
                    Data = false
                };
            }
        }

        /// <summary>
        /// Returns a list of Customers from the database
        /// </summary>
        /// <returns>List<Customer></returns>
        public List<Data.Models.Customer> GetAllCustomers()
        {
            return _contex.Customers.Include(customer => customer.PrimaryAdress).OrderBy(customer => customer.LastName).ToList();
        }

        /// <summary>
        /// Gets a customer record by primary key
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Customer</returns>
        public Data.Models.Customer GetById(int id)
        {
            return _contex.Customers.Find(id);
        }
    }
}
