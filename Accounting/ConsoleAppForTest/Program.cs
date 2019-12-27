using Accounting.DataLayer;
using Accounting.DataLayer.Repositories;
using Accounting.DataLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppForTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ICustomerRepository customer = new CustomerRepository();

            // test getAll
            var list = customer.GetAllCustomers();

            //test insert
            Customers newCustomer = new Customers()
            {
                FullName = "سارا احمدی",
                Mobile = "09122222323",
                Email = "s@yahoo.com",
                Address = "aaaa",
                CustomerImage = "NoPhoto"
            };
            customer.Insert(newCustomer);
            customer.Save();

        }
    }
}
