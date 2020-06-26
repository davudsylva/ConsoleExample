using MovieTickets.Contracts.Enums;
using MovieTickets.Contracts.Interfaces;
using MovieTickets.Contracts.Models;
using System;
using System.Text;
using System.Threading.Tasks;

namespace MovieTickets.BillLogger
{
    public class ConsoleWriter: IConsoleWriter
    {
        public async Task Write(string text)
        {
            Console.Write(text);
        }
    }
}
