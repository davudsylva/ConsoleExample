using MovieTickets.Contracts.Enums;
using MovieTickets.Contracts.Interfaces;
using MovieTickets.Contracts.Models;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTickets.BillLogger
{
    public class ConsoleLogger : IBillObserver
    {
        private IConsoleWriter _writer;

        public ConsoleLogger(IConsoleWriter writer)
        {
            _writer = writer;
        }


        public async Task Consume(Bill bill)
        {
            var builder = new StringBuilder();
            builder.Append($"## Transaction {bill.TransactionId} ##\n");
            foreach (var item in bill.Items.OrderBy(x => GetDescription(x.Category)))
            {
                builder.Append($"{GetDescription(item.Category)} x {item.Count}: {String.Format("{0:C2}", item.ItemCost)}\n");
            }
            builder.Append($"\n");
            builder.Append($"Projected total cost: { String.Format("{0:C2}", bill.TotalCost)}\n");
            builder.Append($"\n");

            await _writer.Write(builder.ToString());
        }

        private String GetDescription(TicketCategory category)
        {
            switch (category)
            {
                case TicketCategory.Child:
                    return "Children";
                case TicketCategory.Teen:
                    return "Teen";
                case TicketCategory.Adult:
                    return "Adult";
                case TicketCategory.Senior:
                    return "Senior";
                default:
                    throw new ArgumentOutOfRangeException("category", category.ToString(), "Unhandled category");
            }
        }
    }
}
