using GalaSoft.MvvmLight;
using MoneyManager.Model;
using ReactiveUI;

namespace MoneyManager.ViewModel
{
	public sealed class TagViewModel : ViewModelBase
	{
		private IReactiveDerivedList<RecordViewModel> records;
		private Tag tag;

		public int Id { get { return tag.Id; } }

		public string Key
		{
			get
			{
				return tag.Key;
			}
			set
			{
				tag.Key = value;
				RaisePropertyChanged("Key");
			}
		}

		public IReactiveDerivedList<RecordViewModel> MyProperty
		{
			get { return records ?? (records = tag.Records.CreateDerivedCollection(record => new RecordViewModel(record))); }
		}

		public TagViewModel(Tag tag)
		{
			this.tag = tag;
		}
	}
}
