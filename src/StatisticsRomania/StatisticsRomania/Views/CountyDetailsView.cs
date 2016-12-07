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
using StatisticsRomania.Lib;
using StatisticsRomania.BusinessObjects;
using StatisticsRomania.Repository.Seeders;

namespace StatisticsRomania.Views
{
    public class CountyDetailsView : BaseView<CountyDetailsViewModel>
    {
        protected override string ChapterTarget
        {
            get
            {
                return "Chapter";
            }
        }

        private LabelSelectorView _labelSelectorViewCounties;
        private LabelSelectorView _labelSelectorViewCounties2;

        private GridControl degChapterData;
        private PlotView plotView;

        private StackLayout dataControls;

        public CountyDetailsView()
        {
            Title = "Statistici judetene";

            Init();
        }

        private async Task Init()
        {
            MessagingCenter.Subscribe<SelectorView, string>(this, "County1", async (s, e) =>
            {
                _labelSelectorViewCounties.Text = e;
                await LoadData();
            });

            MessagingCenter.Subscribe<SelectorView, string>(this, "County2", async (s, e) =>
            {
                _labelSelectorViewCounties2.Text = e;
                await LoadData();
            });

            MessagingCenter.Subscribe<SelectorView, string>(this, ChapterTarget, async (s, e) =>
            {
                _labelSelectorViewChapters.Text = e;
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

            _labelSelectorViewCounties = new LabelSelectorView(_selectorView)
            {
                Title = "Selecteaza judetul",
                ChapterTarget = () => "County1",
                ItemsSource = () => _viewModel.CountyList.Keys.OrderBy(x => x).Skip(1).ToList(),
            };

            var lblCompare = new Label
            {
                VerticalOptions = LayoutOptions.Center,
                Text = "compara cu"
            };
            _labelSelectorViewCounties2 = new LabelSelectorView(_selectorView)
            {
                Title = "Selecteaza judetul",
                ChapterTarget = () => "County2",
                ItemsSource = () => _viewModel.CountyList.Keys.OrderBy(x => x).ToList(),
            };

            var lblChapter = new Label
            {
                VerticalOptions = LayoutOptions.Center,
                Text = "Indicator:"
            };

            _labelSelectorViewChapters = CreateLabelChapters();

            degChapterData = new GridControl();
            degChapterData.IsReadOnly = true;
            degChapterData.HorizontalOptions = LayoutOptions.FillAndExpand;
            degChapterData.VerticalOptions = LayoutOptions.FillAndExpand;
            degChapterData.Columns.Add(new TextColumn() { Caption = "An", FieldName = "Year", IsReadOnly = true, AllowSort = DefaultBoolean.False });
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
                Children = { degChapterData, plotView }
            };

            this.Content = new StackLayout
            {
                BackgroundColor = Color.FromRgb(25, 25, 25),
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
                                lblCounty, _labelSelectorViewCounties, lblCompare, _labelSelectorViewCounties2
                            }
                    },
                    new StackLayout()
                    {
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Orientation = StackOrientation.Horizontal,
                        Children =
                            {
                                lblChapter, _labelSelectorViewChapters
                            }
                    },
                    dataControls
                }
            };

            Func<int, string> getCountyFromSettings = countyIdFromSettings => _viewModel.CountyList.ContainsValue(countyIdFromSettings) ? _viewModel.CountyList.FirstOrDefault(x => x.Value == countyIdFromSettings).Key : string.Empty;

            _labelSelectorViewCounties.Text = Settings.County1 < 1 ? "Alba" : getCountyFromSettings(Settings.County1);
            _labelSelectorViewCounties2.Text = getCountyFromSettings(Settings.County2);
            try
            {
                _labelSelectorViewChapters.Text = Settings.Chapter;
                if (string.IsNullOrEmpty(_labelSelectorViewChapters.Text))
                    _labelSelectorViewChapters.Text = _viewModel.ChapterList.First().Key;
            }
            catch
            {
                // old versions of app store an integer; if cast fails, we initialize the selected chapter with the first element in the list
                _labelSelectorViewChapters.Text = _viewModel.ChapterList.First().Key;
            }
            degChapterData.SelectedRowHandle = -1;

            await LoadData();
#if DEBUG
            try
            {
                //Debug.WriteLine("Start pushing data");
                //await AzureService.Insert(AverageNetSalarySeeder.GetData().Cast<Data>().ToList());
                //Debug.WriteLine("End AverageNetSalarySeeder");
                //await AzureService.Insert(AverageGrossSalarySeeder.GetData().Cast<Data>().ToList());
                //Debug.WriteLine("End AverageGrossSalarySeeder");
                //await AzureService.Insert(NumberOfTouristsSeeder.GetData().Cast<Data>().ToList());
                //Debug.WriteLine("End NumberOfTouristsSeeder");
                //await AzureService.Insert(NumberOfNightsSeeder.GetData().Cast<Data>().ToList());
                //Debug.WriteLine("End NumberOfNightsSeeder");
                //await AzureService.Insert(NumberOfEmployeesSeeder.GetData().Cast<Data>().ToList());
                //Debug.WriteLine("End NumberOfEmployeesSeeder");
                //await AzureService.Insert(UnemployedSeeder.GetData().Cast<Data>().ToList());
                //Debug.WriteLine("End UnemployedSeeder");
                //await AzureService.Insert(ExportFobSeeder.GetData().Cast<Data>().ToList());
                //Debug.WriteLine("End ExportFobSeeder");
                //await AzureService.Insert(ImportCifSeeder.GetData().Cast<Data>().ToList());
                //Debug.WriteLine("End ImportCifSeeder");
                //await AzureService.Insert(SoldFobCifSeeder.GetData().Cast<Data>().ToList());
                //Debug.WriteLine("End SoldFobCifSeeder");
            }
            catch(Exception ex)
            {

            }
#endif
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
            Settings.County1 = _viewModel.CountyList.ContainsKey(_labelSelectorViewCounties.Text) ? _viewModel.CountyList[_labelSelectorViewCounties.Text] : 0;
            Settings.County2 = _viewModel.CountyList.ContainsKey(_labelSelectorViewCounties2.Text) ? _viewModel.CountyList[_labelSelectorViewCounties2.Text] : 0;
            Settings.Chapter = _labelSelectorViewChapters.Text;

            await _viewModel.GetChapterData(Settings.County1, Settings.County2, Settings.Chapter);

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
            series.Title = _labelSelectorViewCounties.Text;
            plotView.Model.Series.Add(series);

            if (_viewModel.Value2ColumnVisibility)
            {
                var series2 = new LineSeries();
                series2.ItemsSource = _viewModel.ChapterData;
                series2.DataFieldX = "TimeStamp";
                series2.DataFieldY = "Value2";
                series2.Title = _labelSelectorViewCounties2.Text;
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