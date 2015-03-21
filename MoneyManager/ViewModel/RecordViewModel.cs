using GalaSoft.MvvmLight;
using MoneyManager.Model;
using System;
using System.Linq;
namespace MoneyManager.ViewModel
{
	public class RecordViewModel : MyViewModelBase
	{
		private Record record;
		private bool donotsave;

		public string Description
		{
			get
			{
				return record.Description;
			}
			set
			{
				if(!donotsave){
				record.Description = value;
				RaiseAndSave("Description");
				}
			}
		}

		public string Tag
		{
			get
			{
				return record.Tag.Key;
			}
			set
			{
				if (!donotsave)
				{
					Tag foundTag = DatabaseContext.Instance.TagSet.SingleOrDefault(tag => tag.Key == value);
					record.Tag = foundTag ?? DatabaseContext.Instance.DefaultTag;
					RaiseAndSave("Tag");
				}
			}
		}

		public DateTime Timestamp
		{
			get
			{
				return record.Timestamp;
			}
			set
			{
				if (!donotsave)
				{
					record.Timestamp = value;
					RaiseAndSave("Timestamp");
				}
			}
		}

		public float Value
		{
			get
			{
				return record.Value;
			}
			set
			{
				if (!donotsave)
				{
					record.Value = value;
					RaiseAndSave("Value");
				}
			}
		}

		public RecordViewModel(Record record, bool @readonly)
		{
			this.record = record;
			this.donotsave = @readonly;
			RaisePropertyChanged();
		}

		public static explicit operator Record(RecordViewModel viewModel)
		{
			return viewModel.record;
		}
	}
}
