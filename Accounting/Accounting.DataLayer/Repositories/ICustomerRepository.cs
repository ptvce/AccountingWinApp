using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.DataLayer.Repositories
{
    public interface ICustomerRepository
    {
        List<Customers> GetAllCustomers();
        IEnumerable<Customers> GetCustomerByFilter(string parameter);
        Customers GetCustomerById(int customerId);
        bool Insert(Customers customer);
        bool Update(Customers customer);
        bool DeleteCustomer(Customers custmer);
        bool DeleteCustomer(int customerId);
    }
}
