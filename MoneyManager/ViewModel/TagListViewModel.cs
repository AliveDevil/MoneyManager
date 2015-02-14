using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MoneyManager.Model;
using ReactiveUI;
using System.Linq;
using System.Windows;

namespace MoneyManager.ViewModel
{
	public class TagListViewModel : ViewModelBase
	{
		private RelayCommand addCommand;
		private RelayCommand deleteCommand;
		private TagViewModel tag;
		private IReactiveDerivedList<TagViewModel> tags;

		public RelayCommand AddCommand
		{
			get
			{
				return addCommand ?? (addCommand = new RelayCommand(() =>
				{
					var tag = DatabaseContext.Instance.TagSet.Create();
					tag.Key = "New Tag";
					DatabaseContext.Instance.TagSet.Add(tag);
					DatabaseContext.Instance.SaveChanges();
				}));
			}
		}

		public RelayCommand DeleteCommand
		{
			get
			{
				return deleteCommand ?? (deleteCommand = new RelayCommand(() =>
				{
					if (DatabaseContext.Instance.TagSet.Count() <= 1)
					{
						MessageBox.Show("", "", MessageBoxButton.OK, MessageBoxImage.Error);
						return;
					}
					foreach (var item in DatabaseContext.Instance.RecordSet.Where(item => item.Tag == (Tag)tag))
					{
						item.Tag = DatabaseContext.Instance.DefaultTag;
					}
					DatabaseContext.Instance.TagSet.Remove((Tag)tag);
					DatabaseContext.Instance.SaveChanges();
				}));
			}
		}

		public TagViewModel Tag
		{
			get
			{
				return tag;
			}
			set
			{
				tag = value;
				RaisePropertyChanged("Tag");
			}
		}

		public IReactiveDerivedList<TagViewModel> Tags
		{
			get { return tags ?? (tags = DatabaseContext.Instance.TagSet.CreateDerivedCollection(tag => new TagViewModel(tag))); }
		}

		public TagListViewModel()
		{
		}
	}
}
