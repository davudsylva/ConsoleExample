using MovieTickets.Contracts.Enums;
using MovieTickets.Contracts.Interfaces;
using MovieTickets.Contracts.Models;
using System;
using System.Text;
using System.Threading.Tasks;

namespace MovieTickets.BillLogger
{
    public interface IConsoleWriter
    {
        Task Write(string text);
    }
}
