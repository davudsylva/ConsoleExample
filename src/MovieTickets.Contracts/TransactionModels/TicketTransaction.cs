using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTickets.Contracts.Models
{
    public class TicketTransaction
    {
        public int TransactionId { get; set; }
        public List<TransactionCustomer> Customers { get; set; }
    }
}
