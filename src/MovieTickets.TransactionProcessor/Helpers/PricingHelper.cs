using MovieTickets.Contracts.Enums;
using MovieTickets.TransactionProcessor.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MovieTickets.TransactionProcessor.Helpers
{
    public class PricingHelper
    {
        private const decimal CostChild = 5.0M;
        private const decimal CostTeen = 12.0M;
        private const decimal CostAdult = 25.0M;
        private const decimal CostSenior = 17.5M;
        private const decimal BulkCostChild = 3.75M;
        private const decimal BulkCostTeen = 0.0M;
        private const decimal BulkCostAdult = 0.0M;
        private const decimal BulkCostSenior = 0.5M;
        private const int ThresholdChild = 3;
        private const int ThresholdTeen = 0;
        private const int ThresholdAdult = 0;
        private const int ThresholdSenior = 0;

        public static decimal CalculateCost(TicketCategory category, int count, List<CategoryPricing> pricingTable)
        {
            var pricing = pricingTable.FirstOrDefault(x => x.Category == category);
            if (pricingTable == null)
            {
                throw new Exception($"No price information for {category.ToString()}");
            }
            if (pricing.BulkThreshold > 0 && count >= pricing.BulkThreshold)
            {
                return pricing.BulkPrice * count;
            }
            else
            {
                return pricing.Price * count;
            }
        }

        public static List<CategoryPricing> GetPricingTable()
        {
            var pricingTable = new List<CategoryPricing>() {
                new CategoryPricing { Category = TicketCategory.Child, Price = CostChild, BulkPrice = BulkCostChild, BulkThreshold = ThresholdChild },
                new CategoryPricing { Category = TicketCategory.Teen, Price = CostTeen, BulkPrice = BulkCostChild, BulkThreshold = ThresholdTeen },
                new CategoryPricing { Category = TicketCategory.Adult, Price = CostAdult, BulkPrice = BulkCostChild, BulkThreshold = ThresholdAdult },
                new CategoryPricing { Category = TicketCategory.Senior, Price = CostSenior, BulkPrice = BulkCostChild, BulkThreshold = ThresholdSenior }
            };
            return pricingTable;
        }
    }
}
