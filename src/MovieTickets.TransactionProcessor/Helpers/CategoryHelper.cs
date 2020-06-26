using MovieTickets.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTickets.TransactionProcessor.Helpers
{
    public class CategoryHelper
    {
        public static TicketCategory GetCategory(int age)
        {
            if (age < 11)
            {
                return TicketCategory.Child;
            }
            else if (age >= 11 && age < 18)
            {
                return TicketCategory.Teen;
            }
            else if (age >= 18 && age < 65)
            {
                return TicketCategory.Adult;
            }
            else
            {
                return TicketCategory.Senior;
            }
        }
    }
}
