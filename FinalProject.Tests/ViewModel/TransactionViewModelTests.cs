using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinalProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using FinalProject.Model;
using FinalProject.View.UserControls;
using System.Windows.Controls;

namespace FinalProject.ViewModel.Tests
{
    [TestClass()]
    public class TransactionViewModelTests
    {
        [TestMethod]
        public void Validate_WithValidInput_ReturnsValidTransaction()
        {
            TransactionViewModel viewModel = new TransactionViewModel();
            Transaction temp = new Expense(1, -100m, "Test", ExpenseType.Home);
            ListBox list = new ListBox();

            viewModel.Validate(temp);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Validate_WithInvalidInput_ThrowsException()
        {
            TransactionViewModel viewModel = new TransactionViewModel();
            Transaction temp = new Expense(1, 0, "Test", ExpenseType.Home);

            viewModel.Validate(temp);
        }

        [TestMethod]
        public void Valid_Input_Serialization()
        {
            TransactionViewModel viewModel = new TransactionViewModel();


        }
    }
}