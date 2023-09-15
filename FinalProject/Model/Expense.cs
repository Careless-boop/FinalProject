using FinalProject.Additional;
using System.Runtime.Serialization;
using System.Windows.Media;

namespace FinalProject.Model
{
    [DataContract]
    [KnownType (typeof(ExpenseCategory))]
    public class Expense : Transaction
    {
        [DataMember]
        string _textColor = "#8B0000";
        [DataMember]
        ExpenseCategory _category;

        public override Brush TextColor 
        { 
            get 
            { 
                BrushConverter bc = new BrushConverter();
                return (Brush)bc.ConvertFrom(_textColor); 
            } 
        }
        public override ITransactionCategory Category { get { return _category; } }
        public Expense(uint id, decimal amount, string description,ExpenseType expenseType) : base(id, amount, description)
        {
            _category=TranCategCreator.ExpenceCateg(expenseType);
        }
    }
}
