using GalaSoft.MvvmLight;
using MoneyManager.Model;
using ReactiveUI;

namespace MoneyManager.ViewModel
{
	public sealed class TagViewModel : MyViewModelBase
	{
		private Tag tag;

		public int Id { get { return tag.Id; } }

		public string Key
		{
			get
			{
				if (tag == null) { return ""; }
				return tag.Key;
			}
			set
			{
				tag.Key = value;
				RaiseAndSave("Key");
			}
		}

		public IReactiveDerivedList<RecordViewModel> Records { get; set; }
		
		public TagViewModel(Tag tag)
		{
			this.tag = tag;
			this.Records = this.tag.Records.CreateDerivedCollection(record => new RecordViewModel(record));
		}

		public static explicit operator Tag(TagViewModel viewModel)
		{
			return viewModel.tag;
		}
	}
}
