using FinalProject.Model;

namespace FinalProject.View.UserControls
{
    /// <summary>
    /// Interaction logic for TransactionControl.xaml
    /// </summary>
    public partial class TransactionControl
    {
        readonly uint _id;
        public uint Id {  get { return _id; } }
        public TransactionControl()
        {
            InitializeComponent();
        }
        public TransactionControl(Transaction transaction)
        {
            InitializeComponent();
            _id = transaction.Id;
            if (transaction is Expense expense)
            {
                TypeTextBlock.Text = expense.Category.Name;
                DescTextBlock.Text = expense.Description;
                DateTextBlock.Text = expense.Date.ToString();
                AmountTextBlock.Text = expense.Amount.ToString();
                AmountTextBlock.Foreground = expense.TextColor;
                MainBorder.BorderBrush = expense.TextColor;
                MainGrid.Background = expense.Category.Color;
            }
            else if (transaction is Income income)
            {
                TypeTextBlock.Text = income.Category.Name;
                DescTextBlock.Text = income.Description;
                DateTextBlock.Text = income.Date.ToString();
                AmountTextBlock.Text = income.Amount.ToString();
                AmountTextBlock.Foreground = income.TextColor;
                MainBorder.BorderBrush = income.TextColor;
                MainGrid.Background = income.Category.Color;
            }
        }
    }
}
