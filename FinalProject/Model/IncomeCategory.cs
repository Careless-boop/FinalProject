using System;
using System.Runtime.Serialization;
using System.Windows.Media;

namespace FinalProject.Model
{
    public enum IncomeType {Work, Family, Friends, Loan, Gift, Other }
    [Serializable]
    internal class IncomeCategory:ITransactionCategory
    {
        [DataMember]
        string _name;
        [DataMember]
        string _color;
        [DataMember]
        IncomeType _type;

        public string Name { get { return _name; } }

        public Brush Color 
        { 
            get 
            { 
                BrushConverter bc = new BrushConverter();
                return (Brush)bc.ConvertFrom(_color); 
            }
        }

        public IncomeType IncomeType { get { return _type; } }

        public IncomeCategory(string name, string color, IncomeType type)
        {
            _name = name;
            _color = color;
            _type = type;
        }
    }
}
