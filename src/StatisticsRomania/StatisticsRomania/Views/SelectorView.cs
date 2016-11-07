using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StatisticsRomania.Views
{
    public class SelectorView : ContentPage
    {
        public event EventHandler<string> ItemSelected;

        protected virtual void OnItemSelected(string selectedItem)
        {
            var handler = ItemSelected;
            if (handler != null)
            {
                handler(this, selectedItem);
            }
        }

        public string Title
        {
            get { return _titleLbl.Text; }
            set { _titleLbl.Text = value; }
        }

        public IEnumerable ItemsSource
        {
            get { return _listView.ItemsSource; }
            set { _listView.ItemsSource = value; }
        }

        public object SelectedItem
        {
            get { return _listView.SelectedItem; }
            set { _listView.SelectedItem = value; }
        }

        private Label _titleLbl;
        private ListView _listView;

        public SelectorView()
        {
            Init();
        }

        private void Init()
        {
            _titleLbl = new Label()
                               {
                                   HorizontalOptions = LayoutOptions.CenterAndExpand,
                                   BackgroundColor = Color.Black,
                                   TextColor = Color.White
                               };

            var template = new DataTemplate(typeof(TextCell));
            template.SetValue(TextCell.TextColorProperty, Color.FromRgb(198, 198, 204));
            template.SetBinding(TextCell.TextProperty, ".");
            _listView = new ListView();
            _listView.ItemTemplate = template;

            var btnCancel = new Button { Text = "Anulare", HorizontalOptions = LayoutOptions.FillAndExpand, };
            btnCancel.Clicked += btnCancel_Clicked;
            var btnOk = new Button { Text = "Selecteaza", HorizontalOptions = LayoutOptions.FillAndExpand, };
            btnOk.Clicked += btnOk_Clicked;

            var buttonGrid = new Grid()
            {
                RowDefinitions =
                                         {
                                             new RowDefinition {Height = GridLength.Auto},
                                         },
                ColumnDefinitions =
                                         {
                                             new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)},
                                             new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)}
                                         },
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };
            var frameBtnCancel = new Frame()
            {
                Content = btnCancel,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(10, 2, 5, 10)
            };
            var frameBtnSave = new Frame()
            {
                Content = btnOk,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(5, 2, 10, 10)
            };
            buttonGrid.Children.Add(frameBtnCancel, 0, 0);
            buttonGrid.Children.Add(frameBtnSave, 1, 0);

            Content = new StackLayout
                          {
                              HorizontalOptions = LayoutOptions.FillAndExpand,
                              BackgroundColor = Color.FromRgb(51, 51, 51),
                              Spacing = 5,
                              Children =
                                  {
                                      new StackLayout
                                          {
                                              HorizontalOptions = LayoutOptions.FillAndExpand,
                                              BackgroundColor = Color.Black,
                                              Children = {_titleLbl},
                                              Spacing = 10,
                                              Padding = 10
                                          },
                                      _listView,
                                      buttonGrid
                                  }
                          };
        }

        private async void btnOk_Clicked(object sender, EventArgs e)
        {
            OnItemSelected(_listView.SelectedItem.ToString());
            await Navigation.PopModalAsync(false);
        }

        private async void btnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync(false);
        }
    }
}