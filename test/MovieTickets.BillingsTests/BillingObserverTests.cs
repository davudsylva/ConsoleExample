using Moq;
using System.Collections.Generic;
using MovieTickets.BillLogger;
using MovieTickets.Contracts.Models;
using Xunit;
using System.Threading.Tasks;
using MovieTickets.Contracts.Enums;

namespace MovieTickets.BillingsTests
{
    public class BillingObserverTests
    {
        string _formattedOutput;
        const int _transactionId = 99;

        // Given a valid bill
        // When we handle it
        // Then it should be formatted and passed to the writer to output
        [Fact]
        public async Task WhenInvolvedConsoleWriterShouldBeCalled()
        {
            var testEnv = EstablishEnvironment();

            var bill = new Bill()
            {
                TransactionId = _transactionId,
                Items = new List<BillItem>()
                {
                    new BillItem() { Category=TicketCategory.Adult, Count=2, ItemCost=2.0M}
                },
                TotalCost = 12.34M
            };

            await testEnv.logger.Consume(bill);

            testEnv.mockWriter.Verify(x => x.Write(It.IsAny<string>()), Times.Once);

            // TODO: We can check the actual format is what we want here
            Assert.Contains("Adult", _formattedOutput);
        }


        // TODO: We'd include extra tests here to verify the format is as expected


        (ConsoleLogger logger, Mock<IConsoleWriter> mockWriter) EstablishEnvironment()
        {
            var mockConsoleWriter = new Mock<IConsoleWriter>();
            mockConsoleWriter.Setup(x => x.Write(It.IsAny<string>())).Callback<string>(fmt => _formattedOutput += fmt);
            var consoleLogger = new ConsoleLogger(mockConsoleWriter.Object);

            return (consoleLogger, mockConsoleWriter);
        }
    }
}
