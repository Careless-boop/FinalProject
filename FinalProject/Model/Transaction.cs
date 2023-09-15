using System;
using System.Runtime.Serialization;
using System.Windows.Media;

namespace FinalProject.Model
{
    [DataContract]
    [KnownType(typeof(Income))]
    [KnownType(typeof(Expense))]
    public abstract class Transaction
    {
        [DataMember]
        protected uint _id;
        [DataMember]
        protected decimal _amount;
        [DataMember]
        protected string _description;
        [DataMember]
        protected DateTime _date;

        public uint Id { get { return _id; } set { _id = value; } }
        public decimal Amount { get { return _amount; } }
        public string Description { get { return _description; } }
        public DateTime Date { get { return _date; } set { _date = value; } }
        public abstract Brush TextColor { get; }
        public abstract ITransactionCategory Category { get; }

        public Transaction(uint id, decimal amount, string description)
        {
            _id = id;
            _amount = amount;
            _description = description;
            _date = DateTime.Now;
        }
    }
}
