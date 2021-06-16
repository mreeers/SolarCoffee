using Data.Models;
using System.Collections.Generic;

namespace Services.Customer
{
    public interface ICustomerService
    {
        List<Data.Models.Customer> GetAllCustomers();
        ServiceResponse<Data.Models.Customer> CreateCustomers(Data.Models.Customer customer);
        ServiceResponse<bool> DeleteCurtomer(int id);
        Data.Models.Customer GetById(int id);
    }
}
