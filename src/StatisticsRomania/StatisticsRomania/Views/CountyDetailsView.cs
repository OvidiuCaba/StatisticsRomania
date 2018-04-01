using DevExpress.Mobile.DataGrid;
using DevExpress.Utils;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Xamarin.Forms;
using StatisticsRomania.Controls;
using StatisticsRomania.Helpers;
using StatisticsRomania.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace StatisticsRomania.Views
{
    public class CountyDetailsView : ContentPage
    {
        private CountyDetailsViewModel _viewModel;
        private PickerWithNoSpellCheck _pickerChapters;
        private PickerWithNoSpellCheck _pickerCounties;
        private PickerWithNoSpellCheck _pickerCounties2;

        private GridControl _degChapterData;
        private PlotView _plotView;

        private StackLayout _dataControls;

        public CountyDetailsView()
        {
            Title = "Statistici judetene";

            On<iOS>().SetUseSafeArea(true);

            Init();
        }

        private async Task Init()
        {
            _viewModel = new CountyDetailsViewModel();
            await _viewModel.GetCounties();
            _viewModel.GetChapters();

            _pickerCounties = new PickerWithNoSpellCheck()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            foreach (var county in _viewModel.CountyList)
            {
                _pickerCounties.Items.Add(county.Key);
            }

            var lblCompare = new Label
            {
                VerticalOptions = LayoutOptions.Center,
                Text = "compara cu"
            };
            _pickerCounties2 = new PickerWithNoSpellCheck()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            _pickerCounties2.Items.Add("──────");
            foreach (var county in _viewModel.CountyList)
            {
                _pickerCounties2.Items.Add(county.Key);
            }

            _pickerChapters = new PickerWithNoSpellCheck()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Title = "Selecteaza indicatorul statistic",
            };
            foreach (var chapter in _viewModel.ChapterList)
            {
                _pickerChapters.Items.Add(chapter.Key);
            }

            _degChapterData = new GridControl();
            _degChapterData.IsReadOnly = true;
            _degChapterData.HorizontalOptions = LayoutOptions.FillAndExpand;
            _degChapterData.VerticalOptions = LayoutOptions.FillAndExpand;
            _degChapterData.Columns.Add(new TextColumn() { Caption = "An", FieldName = "Year", IsReadOnly = true, AllowSort = DefaultBoolean.False});
            _degChapterData.Columns.Add(new TextColumn() { Caption = "Luna", FieldName = "YearFraction", IsReadOnly = true, AllowSort = DefaultBoolean.False });
            var valueColumn = new TextColumn()
                                  {
                                      FieldName = "Value",
                                      IsReadOnly = true,
                                      AllowSort = DefaultBoolean.False
                                  };
            valueColumn.SetBinding(TextColumn.CaptionProperty, new Binding("ValueColumnCaption", source: _viewModel));
            _degChapterData.Columns.Add(valueColumn);
            var valueColumn2 = new TextColumn()
            {
                FieldName = "Value2",
                IsReadOnly = true,
                AllowSort = DefaultBoolean.False
            };
            valueColumn2.SetBinding(TextColumn.CaptionProperty, new Binding("Value2ColumnCaption", source: _viewModel));
            valueColumn2.SetBinding(TextColumn.IsVisibleProperty, new Binding("Value2ColumnVisibility", source: _viewModel));
            _degChapterData.Columns.Add(valueColumn2);
            _degChapterData.ItemsSource = _viewModel.ChapterDataReversed;
            _degChapterData.RowTap += degChapterData_RowTap;

            _plotView = new PlotView();
            _plotView.HorizontalOptions = LayoutOptions.FillAndExpand;
            _plotView.VerticalOptions = LayoutOptions.FillAndExpand;
            _plotView.Model = new PlotModel();
            var series = new LineSeries();
            series.ItemsSource = _viewModel.ChapterData;
            _plotView.Model.Series.Add(series);
            _plotView.Model.Title = "Evolutie indicator";
            if (Device.RuntimePlatform == Device.Android)
                _plotView.BackgroundColor = Color.FromRgb(51, 51, 51);

            _dataControls = new StackLayout()
                               {
                                   Orientation = StackOrientation.Vertical,
                                   HorizontalOptions = LayoutOptions.FillAndExpand,
                                   VerticalOptions = LayoutOptions.FillAndExpand,
                                   Spacing = 0,
                                   Children = {_degChapterData, _plotView}
                               };
            _dataControls.SetBinding(StackLayout.IsVisibleProperty, new Binding("HasData", source: _viewModel));

            var btnTest = new Button()
                              {
                                  Text = "Test"
                              };
            btnTest.Clicked += btnTest_Clicked;

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
                                _pickerCounties, lblCompare, _pickerCounties2//, btnTest
                            }
                    },
                    _pickerChapters,
                    //degChapterData,
                    //plotView
                    _dataControls
                }
            };

            _pickerCounties.SelectedIndex = Settings.County1;
            _pickerCounties2.SelectedIndex = Settings.County2;
            _pickerChapters.SelectedIndex = Settings.Chapter;
            _degChapterData.SelectedRowHandle = -1;

            _pickerCounties.SelectedIndexChanged += picker_SelectedIndexChanged;
            _pickerCounties2.SelectedIndexChanged += picker_SelectedIndexChanged;
            _pickerChapters.SelectedIndexChanged += picker_SelectedIndexChanged;

            await LoadData();
        }

        private async void btnTest_Clicked(object sender, EventArgs e)
        {
            var view = new SelectorView("Selecteaza judetul", _viewModel.CountyList.Keys.ToList());
            await Navigation.PushModalAsync(view);
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

        private bool _wasPortrait;
        private double _width, _height;

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (width == _width && height == _height)
                return;

            if (_plotView == null || _dataControls == null)
            {
                return;
            }

            _width = width;
            _height = height;

            var isPortrait = height > width;

            // Note: Ugly workaround to fix rotation to landscape issue
            //       When a transition from portrait to landscape happens, OnSizeAllocated() is called [at last] twice twice and HeightRequest must be set each time for this bug to go away
            _dataControls.HeightRequest = _wasPortrait ? 100 : -1;

            if (isPortrait)
            {
                _plotView.HeightRequest = height/3;
                _plotView.WidthRequest = -1;
                _dataControls.Orientation = StackOrientation.Vertical;
            }
            else
            {
                _dataControls.Orientation = StackOrientation.Horizontal;
                _plotView.HeightRequest = -1;
                _plotView.WidthRequest = width / 2;
                _degChapterData.ForceLayout();
            }

            // TODO: duplicate code, try to clean it
            _plotView.Model.IsLegendVisible = _plotView.Model.Series.Count > 1;

            if (_plotView.Model.IsLegendVisible)
            {
                if (isPortrait)
                {
                    _plotView.Model.LegendPlacement = LegendPlacement.Outside;
                    _plotView.Model.LegendPosition = LegendPosition.RightTop;
                    _plotView.Model.LegendOrientation = LegendOrientation.Vertical;
                }
                else
                {
                    _plotView.Model.LegendPlacement = LegendPlacement.Outside;
                    _plotView.Model.LegendPosition = LegendPosition.TopRight;
                    _plotView.Model.LegendOrientation = LegendOrientation.Horizontal;
                }
            }

            _wasPortrait = isPortrait;
        }

        private async Task LoadData()
        {
            Settings.County1 = _pickerCounties.SelectedIndex;
            Settings.County2 = _pickerCounties2.SelectedIndex;
            Settings.Chapter = _pickerChapters.SelectedIndex;

            var selectedCounty = _pickerCounties.SelectedIndex >= 0 ? _viewModel.CountyList[_pickerCounties.Items[_pickerCounties.SelectedIndex]] : -1;
            var selectedCounty2 = _pickerCounties2.SelectedIndex >= 1 ? _viewModel.CountyList[_pickerCounties.Items[_pickerCounties2.SelectedIndex - 1]] : -1;
            var selectedChapter = _pickerChapters.SelectedIndex >= 0 ? _pickerChapters.Items[_pickerChapters.SelectedIndex] : string.Empty;

            await _viewModel.GetChapterData(selectedCounty, selectedCounty2, selectedChapter);

            if (_plotView == null)
                return;

            _plotView.Model = new PlotModel();
            _plotView.Model.Title = "Evolutie indicator";

            if (Device.RuntimePlatform == Device.Android)
                _plotView.Model.TextColor = OxyColors.LightGray;

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

            _plotView.Model.Axes.Add(dtAxis);
            _plotView.Model.Axes.Add(verticalAxis);

            _plotView.Model.Series.Clear();

            var series = new LineSeries();
            series.ItemsSource = _viewModel.ChapterData;
            series.DataFieldX = "TimeStamp";
            series.DataFieldY = "Value";
            series.Title = _pickerCounties.Items[_pickerCounties.SelectedIndex];
            _plotView.Model.Series.Add(series);

            if (_viewModel.Value2ColumnVisibility)
            {
                var series2 = new LineSeries();
                series2.ItemsSource = _viewModel.ChapterData;
                series2.DataFieldX = "TimeStamp";
                series2.DataFieldY = "Value2";
                series2.Title = _pickerCounties2.Items[_pickerCounties2.SelectedIndex];
                _plotView.Model.Series.Add(series2);
            }

            _plotView.Model.IsLegendVisible = _plotView.Model.Series.Count > 1;

            if (_plotView.Model.IsLegendVisible)
            {
                if (_wasPortrait)
                {
                    _plotView.Model.LegendPlacement = LegendPlacement.Outside;
                    _plotView.Model.LegendPosition = LegendPosition.RightTop;
                    _plotView.Model.LegendOrientation = LegendOrientation.Vertical;
                }
                else
                {
                    _plotView.Model.LegendPlacement = LegendPlacement.Outside;
                    _plotView.Model.LegendPosition = LegendPosition.TopRight;
                    _plotView.Model.LegendOrientation = LegendOrientation.Horizontal;
                }
            }

            _plotView.Model.InvalidatePlot(true);
        }

        private async void picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            await LoadData();
        }
    }
}
