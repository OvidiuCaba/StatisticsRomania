using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatisticsRomania.ViewModels;
using Xamarin.Forms;
using DevExpress.Mobile.DataGrid;
using DevExpress.Utils;

namespace StatisticsRomania.Views
{
    public class CountyStandingsView : ContentPage
    {
        private CountyStandingsViewModel _viewModel;
        private Picker _pickerChapters;

        public CountyStandingsView()
        {
            Title = "Clasamente";

            Init();
        }

        private async Task Init()
        {
            _viewModel = new CountyStandingsViewModel();
            _viewModel.GetChapters();

            var lblChapter = new Label
            {
                VerticalOptions = LayoutOptions.Center,
                Text = "Indicator:"
            };

            _pickerChapters = new Picker()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            foreach (var chapter in _viewModel.ChapterList)
            {
                _pickerChapters.Items.Add(chapter.Key);
            }
            _pickerChapters.SelectedIndexChanged += pickerChapters_SelectedIndexChanged;

            await LoadData();

            var degAverageGrosSalary = new GridControl();
            degAverageGrosSalary.IsReadOnly = true;
            degAverageGrosSalary.HorizontalOptions = LayoutOptions.FillAndExpand;
            degAverageGrosSalary.Columns.Add(new TextColumn() { Caption = "Year", FieldName = "Year", IsReadOnly = true, AllowSort = DefaultBoolean.False });
            degAverageGrosSalary.Columns.Add(new TextColumn() { Caption = "Month", FieldName = "YearFraction", IsReadOnly = true, AllowSort = DefaultBoolean.False });
            degAverageGrosSalary.Columns.Add(new TextColumn() { Caption = "Value", FieldName = "Value", IsReadOnly = true, AllowSort = DefaultBoolean.False });
            degAverageGrosSalary.ItemsSource = _viewModel.Standings;

            this.Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(
                    left: 0,
                    right: 0,
                    bottom: 0,
                    top: Device.OnPlatform(iOS: 20, Android: 0, WinPhone: 0)),
                Children = { 
                    new StackLayout()
                    {
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Orientation = StackOrientation.Horizontal,
                        Children =
                            {
                                lblChapter, _pickerChapters
                            }
                    },
                    degAverageGrosSalary,
                }
            };

            _pickerChapters.SelectedIndex = 0;
        }

        private async Task LoadData()
        {
            var selectedChapter = _pickerChapters.SelectedIndex >= 0
                                      ? _pickerChapters.Items[_pickerChapters.SelectedIndex]
                                      : string.Empty;

            await _viewModel.GetStandings(selectedChapter);
        }

        private async void pickerChapters_SelectedIndexChanged(object sender, EventArgs e)
        {
            await LoadData();
        }
    }
}