using Moq;
using MovieTickets.Contracts.Interfaces;
using MovieTickets.Contracts.Models;
using MovieTickets.CostAnalyzer;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MovieTickets.CostAnalyzerTests
{
    public class BatchProcessing
    {
        // Given a feed that returns a transaction
        // When we process it
        // Then we expect the processor to be invoked
        [Fact]
        public async Task VerifyBatchCallsProcessor()
        {
            var mockFeed = new Mock<ITransactionFeed>();
            var mockProcessor = new Mock<ITransactionProcessor>();
            var mockObserver = new Mock<IBillObserver>();

            mockFeed.Setup(x => x.GetNextBatch()).ReturnsAsync(
                new List<TicketTransaction>() { 
                    new TicketTransaction() { 
                        TransactionId = 99, 
                        Customers = new List<TransactionCustomer>() 
                        { 
                            new TransactionCustomer() { Age = 12, Name = "Bob" } 
                        } 
                    } 
                }
            );

            var analyser = new BatchCostAnalyzer(mockFeed.Object, mockProcessor.Object, mockObserver.Object);

            await analyser.Run();

            mockProcessor.Verify(x => x.ProcessBatch(It.IsAny<List<TicketTransaction>>()), Times.Once);
        }
    }
}
