using Microsoft.EntityFrameworkCore;
using mpc_DZ.Models;

namespace mpc_DZ.Services
{
    public interface ICustomersService
    {
        public Task<IEnumerable<Customer>?> Read();
        public Task<Customer>? Details(int id);
        public Task<Customer>? Create(Customer? customer);
        public Task<Customer>? Edit(int id, Customer? customer);
        public Task<bool>? Delete(int id);
        public Task<IEnumerable<Customer>?> Sort();
        public Task<IEnumerable<Customer>?> Filter(string command, string information);
    }
    public class CustomersService : ICustomersService
    {
        private readonly CustomersContext _customersContext;
        private readonly ILogger<CustomersService> _logger;
        public CustomersService(CustomersContext customersContext, ILogger<CustomersService> logger)
        {
            _customersContext = customersContext;
            _logger = logger;
        }
        public async Task<Customer>? Create(Customer? customer)
        {
            if (customer==null)
            {
                _logger.LogWarning("Not create!!!");
                return null;
            }
            await _customersContext.AddAsync(customer);
            await _customersContext.SaveChangesAsync();
            return customer;
        }

        public async Task<bool>? Delete(int id)
        {
            var customer = await _customersContext.Customers.FindAsync(id);
            if (customer==null)
            {
                _logger.LogInformation("Not Found customer!!!");
                return false;
            }
            _customersContext.Customers.Remove(customer);
            await _customersContext.SaveChangesAsync();
            return true;
        }

        public async Task<Customer>? Details(int id)
        {
            var customer = await _customersContext.Customers.FindAsync(id);
            if (customer == null)
            {
                return null;
            }
            return customer;
        }

        public async Task<Customer>? Edit(int id, Customer? customer)
        {
            if(id!=customer.Id || customer==null)
            {
                _logger.LogWarning("Attemt is updated customer with null ...");
                return null;
            }
            try
            {
                _customersContext.Customers.Update(customer);
                await _customersContext.SaveChangesAsync();
                return customer;
            }
            catch(DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<IEnumerable<Customer>?> Filter(string command, string information)
        {
            IEnumerable<Customer>? customers;
            if (command=="FirstName")
            {
                customers = await _customersContext.Customers.Where(c => c.FirstName == information).ToListAsync();
            }
            else if(command=="LastName")
            {
                customers = await _customersContext.Customers.Where(c => c.FirstName == information).ToListAsync();
            }
            else
            {
                _logger.LogInformation("Not Found command!!!");
                return null;
            }
            if (customers==null)
            {
                _logger.LogInformation("Not Found customer!!!");
                return null;
            }
            return customers;
             
        }

        public async Task<IEnumerable<Customer>?> Read()
        {
            var customers = await _customersContext.Customers.ToListAsync();
            if (customers==null)
            {
                _logger.LogWarning("Attemt is created customer with null ...");
                return null;
            }
            return customers;
        }

        public async Task<IEnumerable<Customer>?> Sort()
        {
            var customers = await _customersContext.Customers.ToListAsync();
            if (customers == null)
            {
                _logger.LogWarning("Attemt is created customer with null ...");
                return null;
            }
            customers.Sort();
            
            await _customersContext.SaveChangesAsync();
            return customers;
        }

    }
    
}
