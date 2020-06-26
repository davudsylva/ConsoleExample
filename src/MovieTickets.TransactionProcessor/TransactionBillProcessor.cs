using MovieTickets.Contracts.Enums;
using MovieTickets.Contracts.Interfaces;
using MovieTickets.Contracts.Models;
using MovieTickets.TransactionProcessor.Helpers;
using MovieTickets.TransactionProcessor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace MovieTickets.TransactionProcessor
{
    public class TransactionBillProcessor : ITransactionProcessor
    {

        private List<IBillObserver> _observers;

        public TransactionBillProcessor()
        {
            _observers = new List<IBillObserver>();
        }

        public async Task ProcessBatch(IEnumerable<TicketTransaction> transactions)
        {
            var pricingTable = PricingHelper.GetPricingTable();

            var bills = new List<Bill>();
            foreach (var transaction in transactions)
            {
                var bill = new Bill() { TransactionId=transaction.TransactionId } ;
                var customerCounts = new Dictionary<TicketCategory, int>();
                foreach (var customer in transaction.Customers)
                {
                    var category = CategoryHelper.GetCategory(customer.Age);
                    if (customerCounts.ContainsKey(category))
                    {
                        customerCounts[category] = customerCounts[category] + 1;
                    }
                    else
                    {
                        customerCounts[category] = 1;
                    }
                }
                foreach (var category in customerCounts.Keys)
                {
                    var cost = PricingHelper.CalculateCost(category, customerCounts[category], pricingTable);
                    var lineItem = new BillItem() { Category = category, Count = customerCounts[category], ItemCost = cost };
                    bill.Items.Add(lineItem);
                }
                bill.TotalCost = bill.Items.Sum(x => x.ItemCost);
                bills.Add(bill);
            }
            await Notify(bills);
        }


        public void Attach(IBillObserver observer)
        {
            _observers.Add(observer);
        }

        private async Task Notify(List<Bill> bills)
        {
            var tasks = new List<Task>();
            foreach (var observer in _observers)
            {
                foreach (var bill in bills)
                {
                    var task = observer.Consume(bill);
                    tasks.Add(task);
                }
            }
            await Task.WhenAll(tasks);
        }
    }
}
