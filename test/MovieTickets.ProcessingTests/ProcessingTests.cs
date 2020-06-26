using Moq;
using MovieTickets.Contracts.Enums;
using MovieTickets.Contracts.Interfaces;
using MovieTickets.Contracts.Models;
using MovieTickets.TransactionProcessor;
using MovieTickets.TransactionProcessor.Helpers;
using MovieTickets.TransactionProcessor.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MovieTickets.ProcessingTests
{
    public class ProcessingTests
    {
        // Given a set of transactions
        // When passed to the processor
        // Then we expect a call for each transaction to be made to the observer
        [Theory]
        [InlineData(4, false, "6.0")]
        [InlineData(4, true, "4.4")]
        public async Task ProcessShouldCallObserver(int count, bool isBulk, string expectedCost)
        {
            var processor = new TransactionBillProcessor();

            var mockObserver = new Mock<IBillObserver>();

            processor.Attach(mockObserver.Object);

            var transactions = new List<TicketTransaction>()
            {
                new TicketTransaction() { TransactionId= 98, Customers=new List<TransactionCustomer>() {new TransactionCustomer() { Age=20, Name="Bill"} }},
                new TicketTransaction() { TransactionId= 99, Customers=new List<TransactionCustomer>() {new TransactionCustomer() { Age=21, Name="Bob"} }}
            };

            await processor.ProcessBatch(transactions);

            mockObserver.Verify(x => x.Consume(It.IsAny<Bill>()), Times.Exactly(2));
        }
    }
}
