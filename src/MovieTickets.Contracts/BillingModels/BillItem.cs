using MovieTickets.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTickets.Contracts.Models
{
    public class BillItem
    {
        public TicketCategory Category { get; set; }
        public int Count { get; set; }
        public decimal ItemCost { get; set; }
    }
}
