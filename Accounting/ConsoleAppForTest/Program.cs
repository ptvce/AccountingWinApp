using Accounting.DataLayer;
using Accounting.DataLayer.Context;
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
            UnitOfWork db = new UnitOfWork();

            var list = db.CustomerRepository.GetAllCustomers();
           

            Customers newCustomer = new Customers()
            {
                FullName = "سارا احمدی",
                Mobile = "09122222323",
                Email = "s@yahoo.com",
                Address = "aaaa",
                CustomerImage = "NoPhoto"
            };
            db.CustomerRepository.Insert(newCustomer);
           // db.CustomerRepository.Save();
            db.Dispose();
            //Accounting_DBEntities db = new Accounting_DBEntities();
            //ICustomerRepository customer = new CustomerRepository();

            //// test getAll
            //var list = customer.GetAllCustomers();

            ////test insert
            //Customers newCustomer = new Customers()
            //{
            //    FullName = "سارا احمدی",
            //    Mobile = "09122222323",
            //    Email = "s@yahoo.com",
            //    Address = "aaaa",
            //    CustomerImage = "NoPhoto"
            //};
            //customer.Insert(newCustomer);
            //customer.Save();

        }
    }
}
