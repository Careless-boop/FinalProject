using FinalProject.Additional;
using System.Runtime.Serialization;
using System.Windows.Media;

namespace FinalProject.Model
{
    [DataContract]
    [KnownType (typeof(IncomeCategory))]
    public class Income:Transaction
    {
        [DataMember]
        string _textColor = "#006400";
        [DataMember]
        IncomeCategory _category;

        public override Brush TextColor 
        { 
            get 
            { 
                BrushConverter bc = new BrushConverter();
                return (Brush)bc.ConvertFrom(_textColor); 
            } 
        }
        public override ITransactionCategory Category { get { return _category; }}
        public Income(uint id, decimal amount, string description, IncomeType incomeType) : base(id, amount, description)
        {
            _category = TranCategCreator.IncomeCateg(incomeType);
        }
    }
}
