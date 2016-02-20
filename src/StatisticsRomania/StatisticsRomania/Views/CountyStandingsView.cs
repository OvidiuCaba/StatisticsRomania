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
        private Picker _pickerYears;
        private Picker _pickerYearFractions;

        public CountyStandingsView()
        {
            Title = "Clasamente";

            Init();
        }

        private async Task Init()
        {
            _viewModel = new CountyStandingsViewModel();
            _viewModel.GetChapters();
            _viewModel.GetYears();
            _viewModel.GetYearFractions();

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

            var lblYear = new Label
            {
                VerticalOptions = LayoutOptions.Center,
                Text = "An:"
            };

            _pickerYears = new Picker()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            foreach (var year in _viewModel.YearList)
            {
                _pickerYears.Items.Add(year);
            }
            _pickerYears.SelectedIndexChanged += _pickerYears_SelectedIndexChanged;

            var lblYearFraction = new Label
            {
                VerticalOptions = LayoutOptions.Center,
                Text = "Luna / semestru / trimestru:"
            };

            _pickerYearFractions = new Picker()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            foreach (var yearFraction in _viewModel.YearFractionList)
            {
                _pickerYearFractions.Items.Add(yearFraction);
            }
            _pickerYearFractions.SelectedIndexChanged += _pickerYearFractions_SelectedIndexChanged;

            await LoadData();

            var degAverageGrosSalary = new GridControl();
            degAverageGrosSalary.IsReadOnly = true;
            degAverageGrosSalary.HorizontalOptions = LayoutOptions.FillAndExpand;
            degAverageGrosSalary.Columns.Add(new TextColumn() { Caption = "Pozitie", FieldName = "Position", IsReadOnly = true, AllowSort = DefaultBoolean.False });
            degAverageGrosSalary.Columns.Add(new TextColumn() { Caption = "Judet", FieldName = "County", IsReadOnly = true, AllowSort = DefaultBoolean.False });
            degAverageGrosSalary.Columns.Add(new TextColumn() { Caption = "Valoare", FieldName = "Value", IsReadOnly = true, AllowSort = DefaultBoolean.False });
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
                    new StackLayout()
                    {
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Orientation = StackOrientation.Horizontal,
                        Children =
                            {
                                lblYear, _pickerYears, lblYearFraction, _pickerYearFractions
                            }
                    },
                    degAverageGrosSalary,
                }
            };

            _pickerChapters.SelectedIndex = 0;
        }

        void _pickerYearFractions_SelectedIndexChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        void _pickerYears_SelectedIndexChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private async void pickerChapters_SelectedIndexChanged(object sender, EventArgs e)
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            var selectedChapter = _pickerChapters.SelectedIndex >= 0
                                      ? _pickerChapters.Items[_pickerChapters.SelectedIndex]
                                      : string.Empty;

            await _viewModel.GetStandings(selectedChapter);
        }
    }
}