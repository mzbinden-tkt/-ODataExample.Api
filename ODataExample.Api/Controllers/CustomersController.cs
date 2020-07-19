using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using ODataExample.Api.Brokers;
using ODataExample.Api.Models;

namespace ODataExample.Api.Controllers
{
    public class CustomersController : ODataController
    {
        private readonly StorageBroker _context;

        public CustomersController(StorageBroker context)
        {
            _context = context;
        }

        [EnableQuery]
        public IQueryable<Customer> Get()
        {
            return  _context.customers;
        }
        [EnableQuery]
        public SingleResult<Customer> Get([FromODataUri] Guid key)
        {
            IQueryable<Customer> result = _context.customers.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }

    }
}
