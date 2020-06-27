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
        const string _exceptionMsg = "bad thing";

        // Given a feed that returns a transaction
        // When we process it
        // Then we expect the processor to be invoked
        [Fact]
        public async Task VerifyBatchCallsProcessor()
        {
            var testEnv = EstablishEnvironment(withFeedException:false);

            await testEnv.analyser.Run();

            testEnv.mockProcessor.Verify(x => x.ProcessBatch(It.IsAny<List<TicketTransaction>>()), Times.Once);
        }


        // Given a feed is called to get transactions
        // When the feed throws an exception
        // Then we expect the for the exception to be propogated and no processing to take place.
        [Fact]
        public async Task VerifyBatchFeedExceptionPassThrough()
        {
            var testEnv = EstablishEnvironment(withFeedException: true);

            Exception ex = await Assert.ThrowsAsync<Exception>(() => testEnv.analyser.Run());

            Assert.Equal(_exceptionMsg, ex.Message);
            testEnv.mockProcessor.Verify(x => x.ProcessBatch(It.IsAny<List<TicketTransaction>>()), Times.Never);
        }

        // TODO: Add an exhaustive set of tests

        (BatchCostAnalyzer analyser, 
            Mock<ITransactionFeed> mockFeed, 
            Mock<ITransactionProcessor> mockProcessor,
            Mock<IBillObserver> mockObserver) 
            EstablishEnvironment(bool withFeedException)
        {
            var mockFeed = new Mock<ITransactionFeed>();
            var mockProcessor = new Mock<ITransactionProcessor>();
            var mockObserver = new Mock<IBillObserver>();

            if (withFeedException)
            {
                mockFeed.Setup(x => x.GetNextBatch()).ThrowsAsync(new Exception(_exceptionMsg));
            }
            else
            {
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
            }

            var analyser = new BatchCostAnalyzer(mockFeed.Object, mockProcessor.Object, mockObserver.Object);

            return (analyser, mockFeed, mockProcessor, mockObserver);
        }
    }
}
