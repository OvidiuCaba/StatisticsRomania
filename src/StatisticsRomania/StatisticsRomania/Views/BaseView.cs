using StatisticsRomania.Controls;
using StatisticsRomania.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StatisticsRomania.Views
{
    public abstract class BaseView<TViewModel> : ContentPage
        where TViewModel : BaseViewModel
    {
        protected abstract string ChapterTarget { get; }

        protected TViewModel _viewModel;

        protected LabelSelectorView _labelSelectorViewChapters;

        protected static readonly SelectorView _selectorView = new SelectorView();

        protected bool isSelectorActive = false;

        protected LabelSelectorView CreateLabelChapters()
        {
            return new LabelSelectorView(_selectorView)
            {
                Title = "Selecteaza indicatorul",
                ChapterTarget = () => ChapterTarget,
                ItemsSource = () => _viewModel.ChapterList.Keys.OrderBy(x => x).ToList(),
            };
        }

        protected void ConfigureSelectorView(string title, string target, List<string> itemsSource, string selectedItem)
        {
            _selectorView.Title = title;
            _selectorView.Target = target;
            _selectorView.ItemsSource = itemsSource;
            _selectorView.SelectedItem = selectedItem;
        }
    }
}