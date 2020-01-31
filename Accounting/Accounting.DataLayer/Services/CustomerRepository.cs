using Accounting.DataLayer.Repositories;
using Accounting.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.DataLayer.Services
{
    public class CustomerRepository : ICustomerRepository
    {
        private Accounting_DBEntities db;
        public CustomerRepository(Accounting_DBEntities context)
        {
            db = context;
        }
        public bool DeleteCustomer(Customers custmer)
        {
            try
            {
                db.Entry(custmer).State = EntityState.Deleted;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteCustomer(int customerId)
        {
            try
            {
                var customer = GetCustomerById(customerId);
                DeleteCustomer(customer);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<Customers> GetAllCustomers()
        {
            return db.Customers.ToList();
        }

        public IEnumerable<Customers> GetCustomerByFilter(string parameter)
        {
            //polimorfism : اینترفیس ها می توانند به تمام فرزندان خود تغییر قیافه دهند
            return db.Customers.Where(c => c.FullName.Contains(parameter) || c.Email.Contains(parameter) || c.Mobile.Contains(parameter)).ToList();
        }

        public Customers GetCustomerById(int customerId)
        {
            return db.Customers.Find(customerId);
        }

        public int GetCustomerIDByName(string name)
        {
            return db.Customers.First(c => c.FullName == name).CustomerID;
        }

        public string GetCustomerNameByID(int customerId)
        {
            return db.Customers.Find(customerId).FullName;
        }

        public List<ListCustomerViewModel> GetNameCustomers(string filter = "")
        {
            if (filter == "")
                return db.Customers.Select(c => new ListCustomerViewModel() {
                    CustomerID = c.CustomerID,
                    FullName = c.FullName,
                }).ToList();
            return db.Customers.Where(c => c.FullName.Contains(filter)).Select(c => new ListCustomerViewModel()
            {
                CustomerID = c.CustomerID,
                FullName = c.FullName,
            }).ToList(); 
        }

        public bool Insert(Customers customer)
        {
            try
            {
                db.Customers.Add(customer);
                return true;
            }
            catch
            {
                return false;
            } 
        }

       

        public bool Update(Customers customer)
        {
            try
            {
                //اگر موقع استفاده از یوزینگ استفاده نکنیم در زمان آپدیت به مشکل می خوردیم که با دی تچ کردن به روش زیر مشکل حل می شود
                var local = db.Set<Customers>().Local.FirstOrDefault(f => f.CustomerID == customer.CustomerID);
                if (local != null)
                    db.Entry(local).State = EntityState.Detached;
                //
                db.Entry(customer).State = EntityState.Modified;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
