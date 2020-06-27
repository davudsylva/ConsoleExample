using MovieTickets.Contracts.Enums;
using MovieTickets.TransactionProcessor.Helpers;
using System;
using Xunit;

namespace MovieTickets.ProcessingTests
{
    public class CategorisationTests
    {
        // Given a a customer of a given age
        // When we categorise it
        // Then the correct category should be returned
        [Theory]
        [InlineData(0, TicketCategory.Child)]
        [InlineData(10, TicketCategory.Child)]
        [InlineData(11, TicketCategory.Teen)]
        [InlineData(17, TicketCategory.Teen)]
        [InlineData(18, TicketCategory.Adult)]
        [InlineData(64, TicketCategory.Adult)]
        [InlineData(65, TicketCategory.Senior)]
        public void AgeShouldBeCategorised(int age, TicketCategory expectedCategory)
        {
            var actualCategory = CategoryHelper.GetCategory(age);

            Assert.Equal(expectedCategory, actualCategory);
        }
    }
}
