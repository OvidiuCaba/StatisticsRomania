using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace StatisticsRomania.Views
{
    public class SelectorView : ContentPage
    {
        private readonly string _title;
        private readonly List<string> _itemsSource;

        public SelectorView(string title, List<string> itemsSource)
        {
            _title = title;
            _itemsSource = itemsSource;

            Init();
        }

        private void Init()
        {
            var titleLbl = new Label()
                               {
                                   HorizontalOptions = LayoutOptions.CenterAndExpand,
                                   Text = _title,
                                   BackgroundColor = Colors.Black,
                                   TextColor = Colors.White
                               };

            var template = new DataTemplate(typeof(TextCell));
            template.SetValue(TextCell.TextColorProperty, Color.FromRgb(198, 198, 204));
            template.SetBinding(TextCell.TextProperty, ".");
            var listView = new ListView();
            listView.ItemTemplate = template;
            listView.ItemsSource = _itemsSource;

            Content = new StackLayout()
                          {
                              HorizontalOptions = LayoutOptions.FillAndExpand,
                              BackgroundColor = Color.FromRgb(51, 51, 51),
                              Spacing = 5,
                              Children =
                                  {
                                      new StackLayout()
                                          {
                                              HorizontalOptions = LayoutOptions.FillAndExpand,
                                              BackgroundColor = Colors.Black,
                                              Children = {titleLbl},
                                              Spacing = 10,
                                              Padding = 10
                                          },
                                      listView
                                  }
                          };
        }
    }
}