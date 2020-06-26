using MovieTickets.Contracts;
using MovieTickets.Contracts.Interfaces;
using MovieTickets.Contracts.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace MovieTickets.JsonDataFeed
{
    public class JsonTicketFeed : ITransactionFeed
    {
        public async Task<IEnumerable<TicketTransaction>> GetNextBatch()
        {
            var options = new JsonSerializerOptions();
            var jsonString = File.ReadAllText("TestData/transactions.json");
            var transactions = JsonSerializer.Deserialize<List<TicketTransaction>>(jsonString, options);
            return transactions;
        }
    }
}
