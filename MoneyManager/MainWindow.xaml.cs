using System.Windows;

namespace MoneyManager
{
	/// <summary>
	/// Interaktionslogik für MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void MenuAbout_Click(object source, RoutedEventArgs e)
		{
			AboutWindow window = new AboutWindow();
			window.Owner = this;
			window.ShowDialog();
			window = null;
		}

		private void MenuExit_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}
	}
}
