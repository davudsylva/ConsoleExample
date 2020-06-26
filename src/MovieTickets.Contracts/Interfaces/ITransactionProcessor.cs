using MovieTickets.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace MovieTickets.Contracts.Interfaces
{
    public interface ITransactionProcessor
    {
        Task ProcessBatch(IEnumerable<TicketTransaction> transactions);
        void Attach(IBillObserver observer);
    }
}
