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
// Author: Ziyang Wang
// Date: 27/11/2023

namespace Calculator
{
	/// <summary>
	/// A page designed for currency conversion within a calculator application.
	/// Allows users to input a currency amount, select source and target currencies,
	/// and view the converted amount along with conversion rates.
	/// </summary>
	public sealed partial class CurrencyConverter : Page
	{
		// Conversion rates for testing
		private const double USD_TO_EURO = 0.85189982;
		private const double USD_TO_POUND = 0.72872436;
		private const double USD_TO_RUPEE = 74.257327;

		private const double EURO_TO_USD = 1.1739732;
		private const double EURO_TO_POUND = 0.8556672;
		private const double EURO_TO_RUPEE = 87.00755;

		private const double POUND_TO_USD = 1.371907;
		private const double POUND_TO_EURO = 1.1686692;
		private const double POUND_TO_RUPEE = 101.68635;

		private const double RUPEE_TO_USD = 0.011492628;
		private const double RUPEE_TO_EURO = 0.013492774;
		private const double RUPEE_TO_POUND = 0.0098339397;

		// Define currency Strings
		private const String USD = "US Dollar";
		private const String EUR = "Euro";
		private const String GBP = "British Pound";
		private const String IDR = "Indian Rupee";

		public CurrencyConverter()
		{
			this.InitializeComponent();
		}

		/// <summary>
		/// Event handler for the currency conversion button click.
		/// Retrieves user input, validates it, and performs currency conversion.
		/// Displays original and converted amounts along with conversion rates.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void currencyConversionButton_Click(object sender, RoutedEventArgs e)
		{
			double amount;

			// Validate that amountTextBox isn't empty and input is numeric
			try
			{

				if (string.IsNullOrWhiteSpace(amountTextBox.Text))
				{
					// if the amountTextBox is empty or whitespace-only, display an error message for user
					var dialogMessage = new MessageDialog("Error! Please enter a currency amount. It cannot be left blank.");
					await dialogMessage.ShowAsync();
					// if the amountTextBox is empty, set amount variable to 0
					amount = 0;
					// set amountTextBox to display 0 for visual assistance
					amountTextBox.Text = "0";
					// set focus to amountTextBox
					amountTextBox.Focus(FocusState.Programmatic);
					// return to caller
					return;
				}

				// try to parse the input from amountTextBox as a double datatype and assigns it to the amount variable
				amount = double.Parse(amountTextBox.Text);
			}

			catch (Exception theException)
			{
				// if exception is caught during parsing process, display an error message for user
				var dialogMessage = new MessageDialog("Error! Please enter a valid number for amount. " + theException.Message);
				await dialogMessage.ShowAsync();
				// set focus to amountTextBox
				amountTextBox.Focus(FocusState.Programmatic);
				// all content in it is selected for easier change
				amountTextBox.SelectAll();
				// return to caller
				return;
			}

			// Validate that amount is positive
			if (amount < 0)
			{
				// If amount is less than 0, then display an error message for user
				var invalidMessage = new MessageDialog("Error! Please enter a valid amount. Currency amount must be greater than 0.");
				await invalidMessage.ShowAsync();
				// set focus to amountTextBox
				amountTextBox.Focus(FocusState.Programmatic);
				// all content in it is selected for easier change
				amountTextBox.SelectAll();
				// return to caller
				return;
			}

			// Get selected currencies from ComboBoxes
			string fromCurrency = getCurrencyFromComboBox(fromComboBox.SelectedIndex);
			string toCurrency = getCurrencyFromComboBox(toComboBox.SelectedIndex);

			// Perform currency conversion
			double convertedAmount = convertCurrency(amount, fromCurrency, toCurrency);

			// Display results
			originalCurrencyTextBlock.Text = $"{amount} {fromCurrency}s = ";
			convertedCurrencyTextBlock.Text = $"${convertedAmount:F8} {toCurrency}s";
			forwardCurrencyTextBlock.Text = $"1 {fromCurrency} = {getConversionRate(fromCurrency, toCurrency):F8} {toCurrency}s";
			reversedCurrencyTextBlock.Text = $"1 {toCurrency} = {getConversionRate(toCurrency, fromCurrency):F8} {fromCurrency}s";
		}

		/// <summary>
		/// Event handler for the exit button click.
		/// Navigates to the main menu when the exit button is clicked.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void exitButton_Click(object sender, RoutedEventArgs e)
		{
			if (this.Frame != null)
			{
				this.Frame.Navigate(typeof(MainMenu));
			}
		}

		/// <summary>
		/// Performs currency conversion based on predefined conversion rates.
		/// Takes the amount, source currency, and target currency as parameters.
		/// </summary>
		/// <param name="amount"></param>
		/// <param name="fromCurrency"></param>
		/// <param name="toCurrency"></param>
		/// <returns>A double representing converted currency amount</returns>
		private double convertCurrency(double amount, string fromCurrency, string toCurrency)
		{
			// Perform currency conversion using the predefined rates
			switch (fromCurrency + toCurrency)
			{
				case USD + EUR:
					return amount * USD_TO_EURO;
				case USD + GBP:
					return amount * USD_TO_POUND;
				case USD + IDR:
					return amount * USD_TO_RUPEE;
				case EUR + USD:
					return amount * EURO_TO_USD;
				case EUR + GBP:
					return amount * EURO_TO_POUND;
				case EUR + IDR:
					return amount * EURO_TO_RUPEE;
				case GBP + USD:
					return amount * POUND_TO_USD;
				case GBP + EUR:
					return amount * POUND_TO_EURO;
				case GBP + IDR:
					return amount * POUND_TO_RUPEE;
				case IDR + USD:
					return amount * RUPEE_TO_USD;
				case IDR + EUR:
					return amount * RUPEE_TO_EURO;
				case IDR + GBP:
					return amount * RUPEE_TO_POUND;
				default:
					return amount; // Handle identical currency pairs
			}
		}

		/// <summary>
		/// Retrieves the conversion rate based on predefined rates.
		/// Takes the source and target currencies as parameters.
		/// </summary>
		/// <param name="fromCurrency"></param>
		/// <param name="toCurrency"></param>
		/// <returns> A corresponding contant of currency conversion rate or integer 1</returns>
		private double getConversionRate(string fromCurrency, string toCurrency)
		{
			// Retrieve conversion rate using the predefined rates
			switch (fromCurrency + toCurrency)
			{
				case USD + EUR:
					return USD_TO_EURO;
				case USD + GBP:
					return USD_TO_POUND;
				case USD + IDR:
					return USD_TO_RUPEE;
				case EUR + USD:
					return EURO_TO_USD;
				case EUR + GBP:
					return EURO_TO_POUND;
				case EUR + IDR:
					return EURO_TO_RUPEE;
				case GBP + USD:
					return POUND_TO_USD;
				case GBP + EUR:
					return POUND_TO_EURO;
				case GBP + IDR:
					return POUND_TO_RUPEE;
				case IDR + USD:
					return RUPEE_TO_USD;
				case IDR + EUR:
					return RUPEE_TO_EURO;
				case IDR + GBP:
					return RUPEE_TO_POUND;
				default:
					return 1; // Handle identical currency pairs
			}
		}

		/// <summary>
		/// Retrieves the currency string based on the selected index of a ComboBox.
		/// Takes the selected index as a parameter.
		/// </summary>
		/// <param name="selectedIndex"></param>
		/// <returns>A String, either corresponding currency, or an empty string</returns>
		private string getCurrencyFromComboBox(int selectedIndex)
		{
			// Get currency from ComboBox based on index
			switch (selectedIndex)
			{
				case 0:
					return USD;
				case 1:
					return EUR;
				case 2:
					return GBP;
				case 3:
					return IDR;
				default:
					return string.Empty;
			}
		}
	}
}
