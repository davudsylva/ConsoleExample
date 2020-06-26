using MovieTickets.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieTickets.Contracts.Interfaces
{
    public interface IBillObserver
    {
        Task Consume(Bill bill);
    }
}
