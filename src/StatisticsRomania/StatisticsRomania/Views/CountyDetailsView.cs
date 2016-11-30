using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Utils;
using OxyPlot;
using OxyPlot.Axes;
using StatisticsRomania.Controls;
using StatisticsRomania.ViewModels;
using Xamarin.Forms;
using DevExpress.Mobile.DataGrid;
using OxyPlot.Xamarin.Forms;
using OxyPlot.Series;
using StatisticsRomania.Helpers;

namespace StatisticsRomania.Views
{
    public class CountyDetailsView : ContentPage
    {
        private CountyDetailsViewModel _viewModel;
        private PickerWithNoSpellCheck _pickerChapters;
        private Label _labelCounties;
        private Label _labelCounties2;

        private GridControl degChapterData;
        private PlotView plotView;

        private StackLayout dataControls;

        private readonly SelectorView _selectorView = new SelectorView();

        private bool isSelectorActive = false;

        public CountyDetailsView()
        {
            Title = "Statistici judetene";

            Init();
        }

        private async Task Init()
        {
            MessagingCenter.Subscribe<SelectorView, string>(this, "County1", async (s, e) =>
            {
                _labelCounties.Text = e;
                await LoadData();
            });

            MessagingCenter.Subscribe<SelectorView, string>(this, "County2", async (s, e) =>
            {
                _labelCounties2.Text = e;
                await LoadData();
            });

            _viewModel = new CountyDetailsViewModel();
            await _viewModel.GetCounties();
            _viewModel.GetChapters();

            var lblCounty = new Label
            {
                VerticalOptions = LayoutOptions.Center,
                Text = "Judet:"
            };

            _labelCounties = new Label()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.FromRgb(51, 51, 51),
                TextColor = Color.White,
                FontSize = 18
            };

            var labelCountiesTapGesture = new TapGestureRecognizer();
            labelCountiesTapGesture.Tapped += async (s, e) =>
            {
                if (isSelectorActive)
                    return;

                isSelectorActive = true;

                ConfigureSelectorView("Selecteaza judetul", "County1", _viewModel.CountyList.Keys.OrderBy(x => x).Skip(1).ToList(), _labelCounties.Text);

                await Navigation.PushModalAsync(_selectorView);

                isSelectorActive = false;
            };
            _labelCounties.GestureRecognizers.Add(labelCountiesTapGesture);
            var frameToSimulateUnderline = new StackLayout()
                          {
                              VerticalOptions = LayoutOptions.CenterAndExpand,
                              HorizontalOptions = LayoutOptions.Start,
                              Children = {_labelCounties},
                              Padding = new Thickness(0, 0, 0, 1),
                              BackgroundColor = Color.Silver
                          };

            var lblCompare = new Label
            {
                VerticalOptions = LayoutOptions.Center,
                Text = "compara cu"
            };
            _labelCounties2 = new Label()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.FromRgb(51, 51, 51),
                TextColor = Color.White,
                FontSize = 18
            };

            var labelCounties2TapGesture = new TapGestureRecognizer();
            labelCounties2TapGesture.Tapped += async (s, e) =>
            {
                if (isSelectorActive)
                    return;

                isSelectorActive = true;

                ConfigureSelectorView("Selecteaza judetul", "County2", _viewModel.CountyList.Keys.OrderBy(x => x).ToList(), _labelCounties2.Text);

                await Navigation.PushModalAsync(_selectorView);

                isSelectorActive = false;
            };
            _labelCounties2.GestureRecognizers.Add(labelCounties2TapGesture);
            var frameToSimulateUnderline2 = new StackLayout()
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = { _labelCounties2 },
                Padding = new Thickness(0, 0, 0, 1),
                BackgroundColor = Color.Silver,
            };

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

            degChapterData = new GridControl();
            degChapterData.IsReadOnly = true;
            degChapterData.HorizontalOptions = LayoutOptions.FillAndExpand;
            degChapterData.VerticalOptions = LayoutOptions.FillAndExpand;
            degChapterData.Columns.Add(new TextColumn() { Caption = "An", FieldName = "Year", IsReadOnly = true, AllowSort = DefaultBoolean.False});
            degChapterData.Columns.Add(new TextColumn() { Caption = "Luna", FieldName = "YearFraction", IsReadOnly = true, AllowSort = DefaultBoolean.False });
            var valueColumn = new TextColumn()
                                  {
                                      FieldName = "Value",
                                      IsReadOnly = true,
                                      AllowSort = DefaultBoolean.False
                                  };
            valueColumn.SetBinding(TextColumn.CaptionProperty, new Binding("ValueColumnCaption", source: _viewModel));
            degChapterData.Columns.Add(valueColumn);
            var valueColumn2 = new TextColumn()
            {
                FieldName = "Value2",
                IsReadOnly = true,
                AllowSort = DefaultBoolean.False
            };
            valueColumn2.SetBinding(TextColumn.CaptionProperty, new Binding("Value2ColumnCaption", source: _viewModel));
            valueColumn2.SetBinding(TextColumn.IsVisibleProperty, new Binding("Value2ColumnVisibility", source: _viewModel));
            degChapterData.Columns.Add(valueColumn2);
            degChapterData.ItemsSource = _viewModel.ChapterDataReversed;
            degChapterData.RowTap += degChapterData_RowTap;

            plotView = new PlotView();
            plotView.HorizontalOptions = LayoutOptions.FillAndExpand;
            plotView.VerticalOptions = LayoutOptions.FillAndExpand;
            plotView.Model = new PlotModel();
            var series = new LineSeries();
            series.ItemsSource = _viewModel.ChapterData;
            plotView.Model.Series.Add(series);
            plotView.Model.Title = "Evolutie indicator";
            plotView.BackgroundColor = Color.FromRgb(51, 51, 51);

            dataControls = new StackLayout()
                               {
                                   Orientation = StackOrientation.Vertical,
                                   HorizontalOptions = LayoutOptions.FillAndExpand,
                                   VerticalOptions = LayoutOptions.FillAndExpand,
                                   Spacing = 0,
                                   Children = {degChapterData, plotView}
                               };

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
                                lblCounty, frameToSimulateUnderline, lblCompare, frameToSimulateUnderline2
                            }
                    },
                    new StackLayout()
                    {
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Orientation = StackOrientation.Horizontal,
                        Children =
                            {
                                lblChapter, _pickerChapters
                            }
                    },
                    dataControls
                }
            };

            Func<int, string> getCountyFromSettings = countyIdFromSettings => _viewModel.CountyList.ContainsValue(countyIdFromSettings) ? _viewModel.CountyList.FirstOrDefault(x => x.Value == countyIdFromSettings).Key : string.Empty;

            _labelCounties.Text = Settings.County1 < 1 ? "Alba" : getCountyFromSettings(Settings.County1);
            _labelCounties2.Text = getCountyFromSettings(Settings.County2);
            _pickerChapters.SelectedIndex = Settings.Chapter;
            degChapterData.SelectedRowHandle = -1;

            _pickerChapters.SelectedIndexChanged += pickerChapters_SelectedIndexChanged;

            await LoadData();
        }

        private void ConfigureSelectorView(string title, string target, List<string> itemsSource, string selectedItem)
        {
            _selectorView.Title = title;
            _selectorView.Target = target;
            _selectorView.ItemsSource = itemsSource;
            _selectorView.SelectedItem = selectedItem;
        }

        void degChapterData_RowTap(object sender, RowTapEventArgs e)
        {
            // Disable row selection on row tapping
            var grid = sender as GridControl;
            if (grid.SelectedRowHandle > -1)
            {
                grid.SelectedRowHandle = -1;
            }
        }

        private bool _isPortrait;

        protected override void OnSizeAllocated(double width, double height)
        {
            try
            {
                base.OnSizeAllocated(width, height);

                if (plotView == null || dataControls == null)
                {
                    return;
                }

                if (height > width) // portrait
                {
                    plotView.HeightRequest = height/3;
                    plotView.WidthRequest = -1;
                    dataControls.Orientation = StackOrientation.Vertical;
                }
                else
                {
                    dataControls.Orientation = StackOrientation.Horizontal;
                    plotView.HeightRequest = -1;
                    plotView.WidthRequest = width/2;
                    degChapterData.ForceLayout();
                }

                _isPortrait = height > width;

                plotView.Model.IsLegendVisible = plotView.Model.Series.Count > 1;

                if (plotView.Model.IsLegendVisible)
                {
                    if (_isPortrait)
                    {
                        plotView.Model.LegendPlacement = LegendPlacement.Outside;
                        plotView.Model.LegendPosition = LegendPosition.RightTop;
                        plotView.Model.LegendOrientation = LegendOrientation.Vertical;
                    }
                    else
                    {
                        plotView.Model.LegendPlacement = LegendPlacement.Outside;
                        plotView.Model.LegendPosition = LegendPosition.TopRight;
                        plotView.Model.LegendOrientation = LegendOrientation.Horizontal;
                    }
                }
            }
            catch
            {
                // TODO: Find out why the exception occur and find a better solution
                // Do nothing: from some reason the app crashes when I change the selection
            }
        }

        private async Task LoadData()
        {
            Settings.County1 = _viewModel.CountyList.ContainsKey(_labelCounties.Text) ? _viewModel.CountyList[_labelCounties.Text] : 0;
            Settings.County2 = _viewModel.CountyList.ContainsKey(_labelCounties2.Text) ? _viewModel.CountyList[_labelCounties2.Text] : 0;
            Settings.Chapter = _pickerChapters.SelectedIndex;

            var selectedChapter = _pickerChapters.SelectedIndex >= 0
                                      ? _pickerChapters.Items[_pickerChapters.SelectedIndex]
                                      : string.Empty;

            await _viewModel.GetChapterData(Settings.County1, Settings.County2, selectedChapter);

            if (plotView == null)
                return;

            plotView.Model = new PlotModel();
            plotView.Model.Title = "Evolutie indicator";

            plotView.Model.TextColor = OxyColors.LightGray;

            var dtAxis = new DateTimeAxis();
            dtAxis.Position = AxisPosition.Bottom;
            dtAxis.IntervalType = DateTimeIntervalType.Months;
            dtAxis.StringFormat = "yyyy-MM";
            dtAxis.IsPanEnabled = false;
            dtAxis.IsZoomEnabled = false;

            var verticalAxis = new LinearAxis();
            verticalAxis.Position = AxisPosition.Left;
            verticalAxis.IsPanEnabled = false;
            verticalAxis.IsZoomEnabled = false;

            plotView.Model.Axes.Add(dtAxis);
            plotView.Model.Axes.Add(verticalAxis);

            plotView.Model.Series.Clear();

            var series = new LineSeries();
            series.ItemsSource = _viewModel.ChapterData;
            series.DataFieldX = "TimeStamp";
            series.DataFieldY = "Value";
            series.Title = _labelCounties.Text;
            plotView.Model.Series.Add(series);

            if (_viewModel.Value2ColumnVisibility)
            {
                var series2 = new LineSeries();
                series2.ItemsSource = _viewModel.ChapterData;
                series2.DataFieldX = "TimeStamp";
                series2.DataFieldY = "Value2";
                series2.Title = _labelCounties2.Text;
                plotView.Model.Series.Add(series2);
            }

            plotView.Model.IsLegendVisible = plotView.Model.Series.Count > 1;

            if (plotView.Model.IsLegendVisible)
            {
                if (_isPortrait)
                {
                    plotView.Model.LegendPlacement = LegendPlacement.Outside;
                    plotView.Model.LegendPosition = LegendPosition.RightTop;
                    plotView.Model.LegendOrientation = LegendOrientation.Vertical;
                }
                else
                {
                    plotView.Model.LegendPlacement = LegendPlacement.Outside;
                    plotView.Model.LegendPosition = LegendPosition.TopRight;
                    plotView.Model.LegendOrientation = LegendOrientation.Horizontal;
                }
            }

            plotView.Model.InvalidatePlot(true);
        }

        private async void pickerChapters_SelectedIndexChanged(object sender, EventArgs e)
        {
            await LoadData();
        }
    }
}