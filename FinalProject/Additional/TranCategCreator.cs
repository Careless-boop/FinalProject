using FinalProject.Model;

namespace FinalProject.Additional
{
    internal class TranCategCreator
    {
        public static ExpenseCategory ExpenceCateg(ExpenseType expenseType)
        {
            ExpenseCategory expenseCategory;
            switch (expenseType)
            {
                case ExpenseType.Home:
                    expenseCategory = new ExpenseCategory("Home", "#F57F5C", ExpenseType.Home);
                    break;
                case ExpenseType.Rent:
                    expenseCategory = new ExpenseCategory("Rent", "#FC7260", ExpenseType.Rent);
                    break;
                case ExpenseType.Food:
                    expenseCategory = new ExpenseCategory("Food", "#E66365", ExpenseType.Food);
                    break;
                case ExpenseType.Drugs:
                    expenseCategory = new ExpenseCategory("Drugs", "#FF69A8", ExpenseType.Drugs);
                    break;
                case ExpenseType.Car:
                    expenseCategory = new ExpenseCategory("Car", "#F56CDE", ExpenseType.Car);
                    break;
                default:
                    expenseCategory = new ExpenseCategory("Other", "#BDADAC", ExpenseType.Other);
                    break;
            }
            return expenseCategory;
        }
        public static IncomeCategory IncomeCateg(IncomeType incomeType)
        {
            IncomeCategory incomeCategory;
            switch (incomeType)
            {
                case IncomeType.Work:
                    incomeCategory = new IncomeCategory("Work", "#DDF562", IncomeType.Work);
                    break;
                case IncomeType.Family:
                    incomeCategory = new IncomeCategory("Family", "#99FC68", IncomeType.Family);
                    break;
                case IncomeType.Friends:
                    incomeCategory = new IncomeCategory("Friends", "#6AE67A", IncomeType.Friends);
                    break;
                case IncomeType.Gift:
                    incomeCategory = new IncomeCategory("Gift", "#68FCB8", IncomeType.Gift);
                    break;
                case IncomeType.Loan:
                    incomeCategory = new IncomeCategory("Loan", "#6CF5ED", IncomeType.Loan);
                    break;
                default:
                    incomeCategory = new IncomeCategory("Other", "#A9BAAA", IncomeType.Other);
                    break;
            }
            return incomeCategory;
        }

    }
}
