using MovieTickets.Contracts;
using MovieTickets.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieTickets.CostAnalyzer
{
    public class BatchCostAnalyzer
    {
        private readonly ITransactionFeed _ticketFeed;
        private readonly ITransactionProcessor _transactionProcessor;

        public BatchCostAnalyzer(ITransactionFeed ticketFeed, ITransactionProcessor transactionProcessor, IBillObserver observer)
        {
            _ticketFeed = ticketFeed;
            _transactionProcessor = transactionProcessor;
            _transactionProcessor.Attach(observer);
        }

        public async Task Run()
        {
            // TODO: In reality, this would be a loop to handle dequeuing transactions and processing them.
            var batch = await _ticketFeed.GetNextBatch();
            await _transactionProcessor.ProcessBatch(batch);
        }
    }
}
