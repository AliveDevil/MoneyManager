using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MoneyManager.Controls
{
	public class PropertyEditor : HeaderedItemsControl
	{
		public static readonly DependencyProperty ApplyCommandProperty = DependencyProperty.Register("ApplyCommand", typeof(ICommand), typeof(PropertyEditor), new PropertyMetadata(null));
		public static readonly DependencyProperty DiscardCommandProperty = DependencyProperty.Register("DiscardCommand", typeof(ICommand), typeof(PropertyEditor), new PropertyMetadata(null));

		public ICommand ApplyCommand
		{
			get { return (ICommand)GetValue(ApplyCommandProperty); }
			set { SetValue(ApplyCommandProperty, value); }
		}

		public ICommand DiscardCommand
		{
			get { return (ICommand)GetValue(DiscardCommandProperty); }
			set { SetValue(DiscardCommandProperty, value); }
		}

		static PropertyEditor()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(PropertyEditor), new FrameworkPropertyMetadata(typeof(PropertyEditor)));
		}
	}
}
