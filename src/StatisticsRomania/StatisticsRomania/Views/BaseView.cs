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

        protected Label _labelChapters;

        protected readonly SelectorView _selectorView = new SelectorView();

        protected bool isSelectorActive = false;

        protected void CreateLabelChapters()
        {
            _labelChapters = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.FromRgb(51, 51, 51),
                TextColor = Color.White,
                FontSize = 18
            };

            var labelChaptersTapGesture = new TapGestureRecognizer();
            labelChaptersTapGesture.Tapped += async (s, e) =>
            {
                if (isSelectorActive)
                    return;

                isSelectorActive = true;

                ConfigureSelectorView("Selecteaza indicatorul", ChapterTarget, _viewModel.ChapterList.Keys.OrderBy(x => x).ToList(), _labelChapters.Text);

                await Navigation.PushModalAsync(_selectorView);

                isSelectorActive = false;
            };
            _labelChapters.GestureRecognizers.Add(labelChaptersTapGesture);
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
