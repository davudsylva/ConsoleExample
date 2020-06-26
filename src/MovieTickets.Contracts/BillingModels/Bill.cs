using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTickets.Contracts.Models
{
    public class Bill
    {
        public Bill()
        {
            Items = new List<BillItem>();
        }

        public int TransactionId { get; set; }
        public List<BillItem> Items { get; set; }
        public decimal TotalCost { get; set; }
    }
}
