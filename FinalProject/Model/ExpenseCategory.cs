using System;
using System.Runtime.Serialization;
using System.Windows.Media;

namespace FinalProject.Model
{
    public enum ExpenseType { Home, Rent, Food, Drugs, Car, Other }
    [Serializable]
    internal class ExpenseCategory : ITransactionCategory
    {
        [DataMember]
        string _name;
        [DataMember]
        string _color;
        [DataMember]
        ExpenseType _type;

        public string Name { get { return _name; } }

        public Brush Color 
        { 
            get 
            { 
                BrushConverter bc = new BrushConverter();
                
                return (Brush)bc.ConvertFrom(_color); 
            } 
        }

        public ExpenseType ExpenseType { get { return _type; } }

        public ExpenseCategory(string name, string color, ExpenseType type)
        {
            _name = name;
            _color = color;
            _type = type;
        }
    }
}
