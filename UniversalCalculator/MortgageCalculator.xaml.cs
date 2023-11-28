using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Calculator
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MortgageCalculator : Page
	{
		public MortgageCalculator()
		{
			this.InitializeComponent();
		}

		private async void calculateButton_Click(object sender, RoutedEventArgs e)
		{
			double principalBorrowed;
			double years;
			double months;
			double yearlyInterestRate;
			double monthlyInterestRate;
			double monthlyRepayment;

			try
			{
				principalBorrowed = double.Parse(principalTextBox.Text);
				if (principalBorrowed <= 0)
				{
					var msg = new MessageDialog("Please input principal borrowed value greater than $0 !!");
					await msg.ShowAsync();
					principalTextBox.Text = "";
					principalTextBox.Focus(FocusState.Programmatic);
					return;
				}
			}
			catch (Exception ex)
			{
				var dialougeMessage = new MessageDialog("Please enter values in numeric format !! " + ex.Message);
				await dialougeMessage.ShowAsync();
				principalTextBox.Text = "";
				principalTextBox.Focus(FocusState.Programmatic);
				return;
			}
			try
			{
				years = double.Parse(yearsTextBox.Text);
				if (years <= 0)
				{
					var msg = new MessageDialog("Please input years value greater than 0 !!");
					await msg.ShowAsync();
					yearsTextBox.Text = "";
					yearsTextBox.Focus(FocusState.Programmatic);
					return;
				}
			}
			catch (Exception ex)
			{
				var dialougeMessage = new MessageDialog("Please enter years in numeric format !! " + ex.Message);
				await dialougeMessage.ShowAsync();
				yearsTextBox.Text = "";
				yearsTextBox.Focus(FocusState.Programmatic);
				return;
			}
			try
			{
				yearlyInterestRate = double.Parse(yearlyInterestTextBox.Text);
				if (yearlyInterestRate <= 0)
				{
					var msg = new MessageDialog("Please input yearly interest rate value greater than 0 !!");
					await msg.ShowAsync();
					yearlyInterestTextBox.Text = "";
					yearlyInterestTextBox.Focus(FocusState.Programmatic);
					return;
				}
			}
			catch (Exception ex)
			{
				var dialougeMessage = new MessageDialog("Please enter values in numeric format !! " + ex.Message);
				await dialougeMessage.ShowAsync();
				yearlyInterestTextBox.Text = "";
				yearlyInterestTextBox.Focus(FocusState.Programmatic);
				return;
			}

			months = (years * 12);
			monthsTextBox.Text = months.ToString();
			monthlyInterestRate = (yearlyInterestRate / 12)/100;
			monthyInterestTextBox.Text = monthlyInterestRate.ToString();

			monthlyRepayment = principalBorrowed* (monthlyInterestRate * Math.Pow((1 + monthlyInterestRate), months)) / (Math.Pow((1 + monthlyInterestRate), months) - 1);
			monthlyRepaymentTextBox.Text = monthlyRepayment.ToString("C");
		}

		private void exitButton_Click(object sender, RoutedEventArgs e)
		{
			this.Frame.GoBack();
		}
	}
}
