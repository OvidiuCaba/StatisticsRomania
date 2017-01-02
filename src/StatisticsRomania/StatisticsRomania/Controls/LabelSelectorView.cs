using StatisticsRomania.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace StatisticsRomania.Controls
{
    public class LabelSelectorView : StackLayout
    {
        protected readonly SelectorView _selectorView;

        public string Title { get; set; }

        public Func<bool> IsLoading { get; set; }

        public Func<string> ChapterTarget { get; set; }

        public Func<List<string>> ItemsSource { get; set; }

        public string Text
        {
            get
            {
                return _label.Text;
            }
            set
            {
                _label.Text = value;
            }
        }

        private readonly Label _label;

        public LabelSelectorView(SelectorView selectorView)
        {
            _selectorView = selectorView;

            _label = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.FromRgb(25, 25, 25),
                TextColor = Color.White,
                FontSize = 18,
            };

            var labelChaptersTapGesture = new TapGestureRecognizer();
            labelChaptersTapGesture.Tapped += async (s, e) =>
            {
                if (IsLoading() || _selectorView.IsActive)
                    return;

                ConfigureSelectorView();

                await Navigation.PushModalAsync(_selectorView);
            };
            _label.GestureRecognizers.Add(labelChaptersTapGesture);

            VerticalOptions = LayoutOptions.CenterAndExpand;
            HorizontalOptions = LayoutOptions.FillAndExpand;
            Children.Add(_label);
            Padding = new Thickness(0, 0, 0, 1);
            BackgroundColor = Color.Silver;
        }

        private void ConfigureSelectorView()
        {
            _selectorView.Title = Title;
            _selectorView.Target = ChapterTarget();
            _selectorView.ItemsSource = null;
            _selectorView.ItemsSource = ItemsSource();
            _selectorView.SelectedItem = _label.Text;
        }
    }
}