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
        // Given a valid bill
        // When we handle it
        // Then it should be formatted and passed to the writer to output
        [Fact]
        public async Task WhenInvolvedConsoleWriterShouldBeCalled()
        {
            const int transactionId = 99;

            var consoleWriter = new Mock<IConsoleWriter>();
            var consoleLogger = new ConsoleLogger(consoleWriter.Object);

            var bill = new Bill()
            {
                TransactionId = transactionId,
                Items = new List<BillItem>()
                  {
                      new BillItem() { Category=TicketCategory.Adult, Count=2, ItemCost=2.0M}
                  }
            };

            await consoleLogger.Consume(bill);

            consoleWriter.Verify(x => x.Write(It.IsAny<string>()), Times.Once);
        }


        // TODO: We'd include extra tests here to verify the format is as expected
    }
}
