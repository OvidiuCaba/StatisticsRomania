using DevExpress.Utils;
using StatisticsRomania.Controls;
using StatisticsRomania.Helpers;
using StatisticsRomania.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration;
using DevExpress.Maui.DataGrid;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using Plugin.MauiMTAdmob.Controls;

namespace StatisticsRomania.Views
{
    public class CountyStandingsView : ContentPage
    {
        private CountyStandingsViewModel _viewModel;
        private PickerWithNoSpellCheck _pickerChapters;
        private PickerWithNoSpellCheck _pickerYears;
        private PickerWithNoSpellCheck _pickerYearFractions;
        private DataGridView _degStandings;

        public CountyStandingsView()
        {
            Title = "Clasamente";

            On<iOS>().SetUseSafeArea(true);

            Init();
        }

        private async Task Init()
        {
            _viewModel = new CountyStandingsViewModel();

            BindingContext = _viewModel;

            _viewModel.GetChapters();
            _viewModel.GetYears();
            _viewModel.GetYearFractions();

            _pickerChapters = new PickerWithNoSpellCheck()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Title = "Selecteaza indicatorul statistic",
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

            _degStandings = new DataGridView();
            _degStandings.IsReadOnly = true;
            //_degStandings.AllowResizeColumns = false;
            _degStandings.HorizontalOptions = LayoutOptions.FillAndExpand;
            _degStandings.TotalSummaryVisibility = DevExpress.Maui.Core.VisibilityState.Always;
            _degStandings.Columns.Add(new TextColumn() { Caption = "Pozitie", FieldName = "Position", IsReadOnly = true, AllowSort = DefaultBoolean.False });
            _degStandings.Columns.Add(new TextColumn() { Caption = "Judet", FieldName = "County", IsReadOnly = true, AllowSort = DefaultBoolean.False });
            var valueColumn = new TextColumn()
            {
                FieldName = "Value",
                IsReadOnly = true,
                AllowSort = DefaultBoolean.False,
                DisplayFormat = "{0:0}",
            };
            valueColumn.SetBinding(TextColumn.CaptionProperty, new Binding("ValueColumnCaption", source: _viewModel));
            _degStandings.Columns.Add(valueColumn);
            _degStandings.ItemsSource = _viewModel.Standings;
            _degStandings.Tap += _degStandings_Tap;
            _degStandings.TotalSummaries.Add(new GridColumnSummary { FieldName = "County", Type = SummaryType.Count, DisplayFormat = "TOTAL" });
            _degStandings.TotalSummaries.Add(new GridColumnSummary { FieldName = "Value", Type = SummaryType.None, DisplayFormat = "{0:0}" });
            // TODO: this doesn't work - perhaps wrap it into a grid [wrapped, works, but not nice, to be fixed later]
            //_degStandings.SetBinding(DataGridView.IsVisibleProperty, new Binding("HasData", BindingMode.OneWayToSource, source: _viewModel));

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

            var gridTop = Device.RuntimePlatform == Device.Android ? 5 : 0;

            var grid = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(
                        left: 0,
                        right: 0,
                        bottom: 0,
                        top: gridTop),
            };
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.Add(_pickerChapters, 0, 0);
            var header = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Horizontal,
                Padding = new Thickness(5, 0, 0, 0),
                Children =
                {
                    lblYear, _pickerYears, lblYearFraction, _pickerYearFractions
                }
            };
            grid.Add(header, 0, 1);
            // TODO: ugly workaround, to be cleaned up later
            var grid2 = new Grid
            {
                _degStandings
            };
            grid2.SetBinding(DataGridView.IsVisibleProperty, new Binding("HasData", BindingMode.Default, source: _viewModel));
            //grid.Add(_degStandings, 0, 2);
            grid.Add(grid2, 0, 2);
            grid.Add(stackNoData, 0, 2);

            var adMobView = new MTAdView
            {
                AdSize = Plugin.MauiMTAdmob.Extra.BannerSize.Smart,
                AdsId = "ca-app-pub-4024802291999001/1426356760",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };
            adMobView.LoadAd();
            grid.Add(adMobView, 0, 3);

            this.Content = grid;

            _pickerChapters.SelectedIndex = Settings.StandingsChapter;
            _pickerYears.SelectedIndex = _pickerYears.Items.IndexOf(Settings.Year.ToString());
            _pickerYearFractions.SelectedIndex = _pickerYearFractions.Items.IndexOf(Settings.Month.ToString());

            _pickerChapters.SelectedIndexChanged += picker_SelectedIndexChanged;
            _pickerYears.SelectedIndexChanged += picker_SelectedIndexChanged;
            _pickerYearFractions.SelectedIndexChanged += picker_SelectedIndexChanged;

            await LoadData();
        }

        private async void btnForceDataLoading_Clicked(object sender, EventArgs e)
        {
            _pickerYears.SelectedIndexChanged -= picker_SelectedIndexChanged;
            _pickerYearFractions.SelectedIndexChanged -= picker_SelectedIndexChanged;

            _pickerYears.SelectedIndex = _pickerYears.Items.IndexOf(_viewModel.LastAvailableYear.ToString());
            _pickerYearFractions.SelectedIndex = _pickerYearFractions.Items.IndexOf(_viewModel.LastAvailableYearFraction.ToString());

            _pickerYears.SelectedIndexChanged += picker_SelectedIndexChanged;
            _pickerYearFractions.SelectedIndexChanged += picker_SelectedIndexChanged;

            await LoadData();
        }

        private void _degStandings_Tap(object sender, DataGridGestureEventArgs e)
        {
            // Disable row selection on row tapping
            var grid = sender as DataGridView;
            if (grid.SelectedRowHandle > -1)
            {
                grid.SelectedRowHandle = -1;
            }
        }

        private async void picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            if (_pickerChapters.SelectedItem == null || _pickerYears.SelectedItem == null || _pickerYearFractions.SelectedItem == null)
                return;

            Settings.StandingsChapter = _pickerChapters.SelectedIndex;
            Settings.Year = int.Parse((string)_pickerYears.SelectedItem);
            Settings.Month = _pickerYearFractions.SelectedIndex == 0 ? -1 : int.Parse((string)_pickerYearFractions.SelectedItem);

            var selectedChapter = _pickerChapters.SelectedIndex >= 0
                                      ? _pickerChapters.Items[_pickerChapters.SelectedIndex]
                                      : string.Empty;

            _degStandings.TotalSummaries.First(x => x.FieldName == "Value").Type = new[] { "ExportFob", "ImportCif", "SoldFobCif", "NumberOfTourists", "NumberOfNights", "NumberOfEmployees", "Unemployed", "BornAlive", "Deceased", "NaturalGrowth", "Marriages", "Divorces", "DeceasedUnderOneYearOld", "BuildingPermits" }
                                                                                                .Contains(_viewModel.ChapterList[selectedChapter].Name) ? SummaryType.Sum : SummaryType.Average;

            var selectedYear = _pickerYears.SelectedIndex >= 0 ? int.Parse(_pickerYears.Items[_pickerYears.SelectedIndex]) : -1;

            var selectedYearFraction = _pickerYearFractions.SelectedIndex > 0 ? int.Parse(_pickerYearFractions.Items[_pickerYearFractions.SelectedIndex]) : -1;

            await _viewModel.GetStandings(selectedChapter, selectedYear, selectedYearFraction);
        }
    }
}