using FinalProject.Model;
using FinalProject.View;
using FinalProject.View.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Windows;
using System.Windows.Controls;

namespace FinalProject.ViewModel
{
    public class TransactionViewModel : INotifyPropertyChanged
    {
        private const string SavesFile = "savedtransactions.json";
        private ObservableCollection<Transaction> _transactions;
        public ObservableCollection<Transaction> Transactions
        {
            get { return _transactions; }
            set
            {
                _transactions = value;
                OnPropertyChanged(nameof(Transactions));
            }
        }

        public TransactionViewModel()
        {
            Transactions = new ObservableCollection<Transaction>();
        }

        
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public TransactionControl AddTran(bool expense, string amount, string description, ComboBox comboBox)
        {
            Transaction transaction = Validate(expense, amount, description, comboBox);
            Transactions.Add(transaction);
            SerializeObjects();
            return new TransactionControl(transaction);
        }
        public void RemoveTran(ListBox listBox)
        {
            if (listBox.SelectedItem == null)
            {
                throw new InvalidOperationException("None transaction selected!!!");
            }
            TransactionControl transaction = listBox.SelectedItem as TransactionControl;
            uint id = transaction.Id;
            Transaction tran = Transactions.FirstOrDefault(p => p.Id == id);
            
            if (tran != null)
            {
                Transactions.Remove(tran);
                listBox.Items.Remove(transaction);
                SerializeObjects();
            }
            else
            {
                throw new ArgumentException("Wrong Id???");
            }
            
        }
        public void ShowStats(Window current)
        {
            current.Hide();
            StatisticsWindow newWindow = new StatisticsWindow(_transactions);
            newWindow.Closed += (s, args) => current.Show(); // Show the current window when the new window is closed
            newWindow.Show();

        }
        public Transaction Validate(bool expense, string amount, string description, ComboBox comboBox)
        {
            decimal result;
            uint id = GetId();
            if (!decimal.TryParse(amount, out result))
            {
                throw new InvalidOperationException("Wrong transaction amount input.\nTry again!");
            }
            if (comboBox.SelectedItem == null)
            {
                throw new InvalidOperationException("You must select expense/income type!");
            }
            if (result == 0)
            {
                throw new InvalidOperationException("Transaction amount cannot be zero.");
            }
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new InvalidOperationException("Transaction description cannot be empty or null.");
            }
            if (expense)
            {
                if (Enum.IsDefined(typeof(ExpenseType), comboBox.SelectedItem.ToString()))
                {
                    if (result > 0)
                    {
                        result = decimal.Negate(result);
                    }
                    return new Expense(id, result, description, (ExpenseType)Enum.Parse(typeof(ExpenseType), comboBox.SelectedItem.ToString()));
                }
                throw new InvalidOperationException("Invalid expense type!!!");
            }
            else
            {
                if (Enum.IsDefined(typeof(IncomeType), comboBox.SelectedItem.ToString()))
                {
                    if (result < 0)
                    {
                        throw new InvalidOperationException("Income amount cannot be less than zero.");
                    }
                    return new Income(id, result, description, (IncomeType)Enum.Parse(typeof(IncomeType), comboBox.SelectedItem.ToString()));
                }
                throw new InvalidOperationException("Invalid income type!!!");
            }

        }
        public void Validate(Transaction transaction)
        {
            if (transaction.Amount == 0)
            {
                throw new InvalidOperationException("Transaction amount cannot be zero.");
            }
            if (string.IsNullOrWhiteSpace(transaction.Description))
            {
                throw new InvalidOperationException("Transaction description cannot be empty or null.");
            }
            if (transaction is Expense)
            {
                if (transaction.Amount > 0)
                {
                    throw new InvalidOperationException("Expense amount cannot be greater than zero.!!!");
                }
            }
            else
            {
                if (transaction.Amount < 0)
                {
                    throw new InvalidOperationException("Income amount cannot be less than zero.");
                }
            }
        }
        private uint GetId()
        {
            if(Transactions.Count == 0)
            {
                return 1;
            }
            uint id = Transactions.Last().Id;
            return id + 1;
        }
        public void SerializeObjects()
        {
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(List<Transaction>));
            FileStream fs = new FileStream(SavesFile, FileMode.Create);
            try
            {
                js.WriteObject(fs, _transactions);
            }
            catch(Exception)
            {
                throw new IOException("Error while saving transactions!!!");
            }
            finally { fs.Close();}
        }
        public void DeserializeObjects(ListBox listBox)
        {

            if (File.Exists(SavesFile))
            {
                string temp = File.ReadAllText(SavesFile);
                if (string.IsNullOrWhiteSpace(temp))
                {
                    return;
                }
                else
                {
                    FileStream fs = new FileStream(SavesFile, FileMode.Open);
                    try
                    {
                        DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(List<Transaction>));
                        List<Transaction> loadedTransactions = (List<Transaction>)js.ReadObject(fs);
                        foreach (Transaction transaction in loadedTransactions)
                        {
                            Validate(transaction);
                            Transactions.Add(transaction);
                            listBox.Items.Add(new TransactionControl(transaction));
                        }
                    }
                    catch (IOException)
                    {
                        throw new IOException("Error deserializing from file!!!");
                    }
                    finally
                    {
                        fs.Close();
                    }
                }
            }
        }
    }
}
