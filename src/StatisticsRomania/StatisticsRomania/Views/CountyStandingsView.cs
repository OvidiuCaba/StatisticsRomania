using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatisticsRomania.Controls;
using StatisticsRomania.ViewModels;
using Xamarin.Forms;
using DevExpress.Mobile.DataGrid;
using DevExpress.Utils;
using StatisticsRomania.Helpers;

namespace StatisticsRomania.Views
{
    public class CountyStandingsView : ContentPage
    {
        private CountyStandingsViewModel _viewModel;
        private PickerWithNoSpellCheck _pickerChapters;
        private PickerWithNoSpellCheck _pickerYears;
        private PickerWithNoSpellCheck _pickerYearFractions;

        public CountyStandingsView()
        {
            Title = "Clasamente";

            Init();
        }

        private async Task Init()
        {
            _viewModel = new CountyStandingsViewModel();

            BindingContext = _viewModel;

            _viewModel.GetChapters();
            _viewModel.GetYears();
            _viewModel.GetYearFractions();

            var lblChapter = new Label
            {
                VerticalOptions = LayoutOptions.Center,
                Text = "Indicator:"
            };

            _pickerChapters = new PickerWithNoSpellCheck()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            foreach (var chapter in _viewModel.ChapterList)
            {
                _pickerChapters.Items.Add(chapter.Key);
            }

            var lblYear = new Label
            {
                VerticalOptions = LayoutOptions.Center,
                Text = "An:"
            };

            _pickerYears = new PickerWithNoSpellCheck()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            foreach (var year in _viewModel.YearList)
            {
                _pickerYears.Items.Add(year);
            }

            var lblYearFraction = new Label
            {
                VerticalOptions = LayoutOptions.Center,
                Text = "Luna:"
            };

            _pickerYearFractions = new PickerWithNoSpellCheck()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            foreach (var yearFraction in _viewModel.YearFractionList)
            {
                _pickerYearFractions.Items.Add(yearFraction);
            }

            var degStandings = new GridControl();
            degStandings.IsReadOnly = true;
            degStandings.HorizontalOptions = LayoutOptions.FillAndExpand;
            degStandings.Columns.Add(new TextColumn() { Caption = "Pozitie", FieldName = "Position", IsReadOnly = true, AllowSort = DefaultBoolean.False });
            degStandings.Columns.Add(new TextColumn() { Caption = "Judet", FieldName = "County", IsReadOnly = true, AllowSort = DefaultBoolean.False });
            var valueColumn = new TextColumn()
            {
                FieldName = "Value",
                IsReadOnly = true,
                AllowSort = DefaultBoolean.False
            };
            valueColumn.SetBinding(TextColumn.CaptionProperty, new Binding("ValueColumnCaption", source: _viewModel));
            degStandings.Columns.Add(valueColumn);
            degStandings.ItemsSource = _viewModel.Standings;
            degStandings.RowTap += degAverageGrosSalary_RowTap;
            degStandings.SetBinding(GridControl.IsVisibleProperty, "HasData");

            var lblNoData = new Label()
                                  {
                                      HorizontalOptions = LayoutOptions.FillAndExpand,
                                      Text = "Nu exista date disponibile pentru intervalul selectat",
                                      HorizontalTextAlignment = TextAlignment.Center,
                                      VerticalTextAlignment = TextAlignment.Center,
                                      FontSize = 27,
                                  };
            var btnForceDataLoading = new Button()
                                          {
                                              Text = "Forteaza incarcarea datelor",
                                          };
            btnForceDataLoading.Clicked += btnForceDataLoading_Clicked;

            var stackNoData = new StackLayout()
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Spacing = 20,
                Padding = new Thickness(0),
                Children =
                    {
                        lblNoData,
                        btnForceDataLoading
                    }
            };
            stackNoData.SetBinding(Label.IsVisibleProperty, "DoesNotHaveData");

            this.Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(
                    left: 0,
                    right: 0,
                    bottom: 0,
                    top: Device.OnPlatform(iOS: 20, Android: 5, WinPhone: 0)),
                Children = { 
                    new StackLayout()
                    {
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Orientation = StackOrientation.Horizontal,
                        Padding = new Thickness(0, 2),
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
                    degStandings,
                    stackNoData
                }
            };

            _pickerChapters.SelectedIndex = Settings.StandingsChapter;
            _pickerYears.SelectedIndex = _pickerYears.Items.IndexOf(App.LastYearAvailableData.ToString());
            _pickerYearFractions.SelectedIndex = _pickerYearFractions.Items.IndexOf(App.LastMonthAvailableData.ToString());

            _pickerChapters.SelectedIndexChanged += pickerChapters_SelectedIndexChanged;
            _pickerYears.SelectedIndexChanged += _pickerYears_SelectedIndexChanged;
            _pickerYearFractions.SelectedIndexChanged += _pickerYearFractions_SelectedIndexChanged;

            await LoadData();
        }

        void btnForceDataLoading_Clicked(object sender, EventArgs e)
        {
            _pickerYears.SelectedIndex = _pickerYears.Items.IndexOf(_viewModel.LastAvailableYear.ToString());
            _pickerYearFractions.SelectedIndex = _pickerYearFractions.Items.IndexOf(_viewModel.LastAvailableYearFraction.ToString());
        }

        void degAverageGrosSalary_RowTap(object sender, RowTapEventArgs e)
        {
            // Disable row selection on row tapping
            var grid = sender as GridControl;
            if (grid.SelectedRowHandle > -1)
            {
                grid.SelectedRowHandle = -1;
            }
        }

        private async void _pickerYearFractions_SelectedIndexChanged(object sender, EventArgs e)
        {
            await LoadData();
        }

        private async void _pickerYears_SelectedIndexChanged(object sender, EventArgs e)
        {
            await LoadData();
        }

        private async void pickerChapters_SelectedIndexChanged(object sender, EventArgs e)
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            Settings.StandingsChapter = _pickerChapters.SelectedIndex;

            var selectedChapter = _pickerChapters.SelectedIndex >= 0
                                      ? _pickerChapters.Items[_pickerChapters.SelectedIndex]
                                      : string.Empty;

            var selectedYear = _pickerYears.SelectedIndex >= 0 ? int.Parse(_pickerYears.Items[_pickerYears.SelectedIndex]) : -1;

            var selectedYearFraction = _pickerYearFractions.SelectedIndex >= 0 ? int.Parse(_pickerYearFractions.Items[_pickerYearFractions.SelectedIndex]) : -1;

            await _viewModel.GetStandings(selectedChapter, selectedYear, selectedYearFraction);
        }
    }
}