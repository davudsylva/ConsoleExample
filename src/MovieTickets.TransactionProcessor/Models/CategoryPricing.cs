using MovieTickets.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTickets.TransactionProcessor.Models
{
    public class CategoryPricing
    {
        public TicketCategory Category { get; set; }
        public Decimal Price { get; set; }
        public Decimal BulkPrice { get; set; }
        public int BulkThreshold { get; set; }
    }
}
