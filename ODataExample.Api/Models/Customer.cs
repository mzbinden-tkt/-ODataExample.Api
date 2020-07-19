using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODataExample.Api.Models
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }

    }
}
