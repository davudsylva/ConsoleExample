using MovieTickets.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace MovieTickets.Contracts.Interfaces
{
    public interface ITransactionFeed
    {
        Task<IEnumerable<TicketTransaction>> GetNextBatch();
    }
}
