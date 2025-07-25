using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity
{
    public class OrderItem
    {
        [Key]
        [ValidateNever]
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice  { get; set; }
        [ValidateNever]
        public string? ProductImg { get; set; }
        public int Qty { get; set; }

        public int ProductId { get; set; }
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }

    }
}
