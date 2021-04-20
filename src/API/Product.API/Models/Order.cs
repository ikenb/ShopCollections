using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Product.API.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public List<OrderSummary> OrderSummaries { get; set; }

        public string FirstName { get; set; }


        public string LastName { get; set; }


        public string AddressLine1 { get; set; }


        public string AddressLine2 { get; set; }


        public string ZipCode { get; set; }

        public string City { get; set; }


        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public decimal OrderTotal { get; set; }

        public DateTime OrderPlaced { get; set; }
    }
}
