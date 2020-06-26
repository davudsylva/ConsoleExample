using MovieTickets.Contracts.Enums;
using MovieTickets.TransactionProcessor.Helpers;
using MovieTickets.TransactionProcessor.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace MovieTickets.ProcessingTests
{
    public class PricingTests
    {
        // Given a set of tickets for the same category
        // When we calculate the price
        // Then the correct entry from the table will use the correct prive to calculate the actual cost
        [Theory]
        [InlineData(4, false, "6.0")]
        [InlineData(4, true, "4.4")]
        public void PriceShouldBeCalculated(int count, bool isBulk, string expectedCost)
        {
            var unitCost = 1.5M;
            var bulkCost = 1.1M;

            var pricingTable = new List<CategoryPricing>() {
                new CategoryPricing { Category = TicketCategory.Child, Price = unitCost-1, BulkPrice = bulkCost-1, BulkThreshold = isBulk ? count-1 : count+1 },
                new CategoryPricing { Category = TicketCategory.Teen, Price = unitCost, BulkPrice = bulkCost, BulkThreshold = isBulk ? count-1 : count+1 },
                new CategoryPricing { Category = TicketCategory.Adult, Price = unitCost+1, BulkPrice = bulkCost+2, BulkThreshold = isBulk ? count-1 : count+1 }
            };

            var actualCost = PricingHelper.CalculateCost(TicketCategory.Teen, count, pricingTable);

            Assert.Equal(Decimal.Parse(expectedCost), actualCost);
        }
    }
}
