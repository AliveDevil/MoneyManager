using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MoneyManager.Model;
using MoneyManager.Properties;
using ReactiveUI;
using System.Linq;
using System.Windows;

namespace MoneyManager.ViewModel
{
	public class TagListViewModel : ViewModelBase
	{
		private RelayCommand addTag;
		private RelayCommand deleteTag;
		private TagViewModel tag;
		private IReactiveDerivedList<TagViewModel> tags;

		public RelayCommand AddTag
		{
			get
			{
				return addTag ?? (addTag = new RelayCommand(() =>
				{
					var tag = DatabaseContext.Instance.TagSet.Create();
					tag.Key = "New Tag";
					DatabaseContext.Instance.TagSet.Add(tag);
					DatabaseContext.Instance.SaveChanges();
				}));
			}
		}

		public RelayCommand DeleteTag
		{
			get
			{
				return deleteTag ?? (deleteTag = new RelayCommand(() =>
				{
					if (MessageBox.Show(Resources.TagDelete, "", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
					{
						return;
					}
					if (((Tag)Tag).Default)
					{
						MessageBox.Show(Resources.TagDeleteErrorMessage, Resources.ErrorMessageTitle, MessageBoxButton.OK, MessageBoxImage.Error);
						return;
					}

					foreach (var item in DatabaseContext.Instance.RecordSet.AsEnumerable().Where(item => item.Tag == (Tag)tag))
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

		public IReactiveDerivedList<TagViewModel> Tags { get; set; }

		public TagListViewModel()
		{
			if (IsInDesignMode)
			{
			}
			else
			{
				Tags = DatabaseContext.Instance.TagSet.Local.CreateDerivedCollection(tag => new TagViewModel(tag));
			}
		}
	}
}
