using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
	public sealed partial class MainMenu : Page
	{
		public MainMenu()
		{
			this.InitializeComponent();
		}

		// Navigate to Math Calculator Application
		private void mathCalculatorButton_Click(object sender, RoutedEventArgs e)
		{
			if (this.Frame != null)
			{
				this.Frame.Navigate(typeof(MainPage));
			}
		}

		// Navigate to Mortgage Calculator Application
		private void mortgageCalculatorButton_Click(object sender, RoutedEventArgs e)
		{
	
			if (this.Frame != null)
			{
				this.Frame.Navigate(typeof(MortgageCalculator));
			}
		}

		// Navigate to Currency Calculator Application
		private void currencyCalculatorButton_Click(object sender, RoutedEventArgs e)
		{
			// Uncomment following code after created CurrencyConverter xaml page
			if (this.Frame != null)
			{
				this.Frame.Navigate(typeof(CurrencyConverter));
			}
		}

		// Exit the Application
		private void exitButton_Click(object sender, RoutedEventArgs e)
		{
			System.Environment.Exit(0);
		}

		private void tripCalculatorButton_Click(object sender, RoutedEventArgs e)
		{
			//Trip calculator C# code will be developed later
		}
	}
}
