using System.Windows.Media;

namespace FinalProject.Model
{
    public interface ITransactionCategory
    {
        string Name { get; }
        Brush Color { get; }
    }
}
