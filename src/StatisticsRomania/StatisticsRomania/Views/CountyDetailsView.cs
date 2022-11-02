using DevExpress.Utils;
using StatisticsRomania.Controls;
using StatisticsRomania.Helpers;
using StatisticsRomania.ViewModels;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using DevExpress.Maui.DataGrid;
using DevExpress.Maui.Charts;
using ValueType = DevExpress.Maui.Charts.ValueType;

namespace StatisticsRomania.Views
{
    public class CountyDetailsView : ContentPage
    {
        private CountyDetailsViewModel _viewModel;

        private Grid _grid;

        private PickerWithNoSpellCheck _pickerChapters;
        private PickerWithNoSpellCheck _pickerCounties;
        private PickerWithNoSpellCheck _pickerCounties2;

        private StackLayout _firstRowOfHeader;
        private DataGridView _degChapterData;
        // TODO rename field name
        private ChartView _plotView;

        private AdMobView _adMobView;

        public CountyDetailsView()
        {
            Title = "Statistici judetene";

            On<iOS>().SetUseSafeArea(true);

            Init();
        }

        private async Task Init()
        {
            _viewModel = new CountyDetailsViewModel();
            _viewModel.GetCounties();
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

            _degChapterData = new DataGridView();
            _degChapterData.SetBinding(DataGridView.IsVisibleProperty, new Binding("HasData", source: _viewModel));
            _degChapterData.IsReadOnly = true;
            //_degChapterData.AllowResizeColumns = false;
            _degChapterData.HorizontalOptions = LayoutOptions.FillAndExpand;
            _degChapterData.VerticalOptions = LayoutOptions.FillAndExpand;
            _degChapterData.Columns.Add(new TextColumn() { Caption = "An", FieldName = "Year", IsReadOnly = true, AllowSort = DevExpress.Utils.DefaultBoolean.False });
            _degChapterData.Columns.Add(new TextColumn() { Caption = "Luna", FieldName = "YearFraction", IsReadOnly = true, AllowSort = DevExpress.Utils.DefaultBoolean.False });
            var valueColumn = new TextColumn()
            {
                FieldName = "Value",
                IsReadOnly = true,
                AllowSort = DevExpress.Utils.DefaultBoolean.False
            };
            valueColumn.SetBinding(TextColumn.CaptionProperty, new Binding("ValueColumnCaption", source: _viewModel));
            _degChapterData.Columns.Add(valueColumn);
            var valueColumn2 = new TextColumn()
            {
                FieldName = "Value2",
                IsReadOnly = true,
                AllowSort = DevExpress.Utils.DefaultBoolean.False
            };
            valueColumn2.SetBinding(TextColumn.CaptionProperty, new Binding("Value2ColumnCaption", source: _viewModel));
            valueColumn2.SetBinding(TextColumn.IsVisibleProperty, new Binding("Value2ColumnVisibility", source: _viewModel));
            _degChapterData.Columns.Add(valueColumn2);
            _degChapterData.ItemsSource = _viewModel.ChapterData;
            _degChapterData.Tap += _degChapterData_Tap;

            _plotView = new ChartView();
            _plotView.SetBinding(IsVisibleProperty, new Binding("HasData", source: _viewModel));
            _plotView.HorizontalOptions = LayoutOptions.FillAndExpand;
            _plotView.VerticalOptions = LayoutOptions.FillAndExpand;
            var series = new LineSeries();
            series.Data = new SeriesDataAdapter() { DataSource = _viewModel.ChapterData};
            _plotView.Series.Add(series);
            // TODO: fix this
            //_plotView.Model.Title = "Evolutie indicator";
            if (Device.RuntimePlatform == Device.Android)
                _plotView.BackgroundColor = Color.FromRgb(51, 51, 51);

            var gridTop = Device.RuntimePlatform == Device.Android ? 5 : 0;

            _grid = new Grid
            {
                //VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(
                    left: 0,
                    right: 0,
                    bottom: 0,
                    top: gridTop),
            };

            _firstRowOfHeader = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Horizontal,
                Padding = new Thickness(0, 2),
                Children =
            {
                _pickerCounties, lblCompare, _pickerCounties2
            }
            };

            this.Content = _grid;

            _pickerCounties.SelectedIndex = Settings.County1;
            _pickerCounties2.SelectedIndex = Settings.County2;
            _pickerChapters.SelectedIndex = Settings.Chapter;
            _degChapterData.SelectedRowHandle = -1;

            _pickerCounties.SelectedIndexChanged += picker_SelectedIndexChanged;
            _pickerCounties2.SelectedIndexChanged += picker_SelectedIndexChanged;
            _pickerChapters.SelectedIndexChanged += picker_SelectedIndexChanged;

            _adMobView = new AdMobView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };

            await LoadData();
        }

        private void _degChapterData_Tap(object sender, DataGridGestureEventArgs e)
        {
            // Disable row selection on row tapping
            var grid = sender as DataGridView;
            if (grid.SelectedRowHandle > -1)
            {
                grid.SelectedRowHandle = -1;
            }
        }

        private bool _wasPortrait;
        private double _width, _height;

        protected override void OnSizeAllocated(double width, double height)
        {
            try
            {
                base.OnSizeAllocated(width, height);
            }
            catch
            {
                // I have no idea why the exception is thrown, but it looks like we can ignore it
            }

            if (width == _width && height == _height)
                return;

            if (_plotView == null || _degChapterData == null)
            {
                return;
            }

            _width = width;
            _height = height;

            var isPortrait = height > width;

            _grid.RowDefinitions.Clear();
            _grid.ColumnDefinitions.Clear();
            _grid.Children.Clear();

            if (isPortrait)
            {
                _grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                //_grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                _grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
                //_grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(_height / 3, GridUnitType.Absolute) });
                //_grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

                _grid.ColumnDefinitions.Add(new ColumnDefinition());

                _grid.Add(_firstRowOfHeader, 0, 0);
                _grid.Add(_pickerChapters, 0, 1);
                try
                {
                    //_grid.Add(_degChapterData, 0, 2);
                    //_grid.Add(_plotView, 0, 3);
                    //_grid.Add(_adMobView, 0, 4);
                }
                catch { }
                // TODO: rmeove this
                //_grid.Add(_plotView, 0, 3);
                //_grid.Add(_adMobView, 0, 4);
            }
            else
            {
                _grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                _grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                _grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
                _grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

                _grid.ColumnDefinitions.Add(new ColumnDefinition());
                _grid.ColumnDefinitions.Add(new ColumnDefinition());

                // TODO: test rotation
                //_grid.Add(_firstRowOfHeader, 0, 2, 0, 1);
                Grid.SetRow(_firstRowOfHeader, 0);
                Grid.SetColumnSpan(_firstRowOfHeader, 2);
                //_grid.Add(_pickerChapters, 0, 2, 1, 2);
                Grid.SetRow(_pickerChapters, 1);
                Grid.SetColumnSpan(_pickerChapters, 2);
                _grid.Add(_degChapterData, 0, 2);
                _grid.Add(_plotView, 1, 2);
                //_grid.Add(_adMobView, 0, 2, 3, 4);
                Grid.SetRow(_adMobView, 3);
                Grid.SetColumnSpan(_adMobView, 2);
            }

            // TODO: duplicate code, try to clean it
            // TODO: null ref exc
            //_plotView.Legend.Visible = _plotView.Series.Count > 1;

            // TODO: fix Legend placement if possible
            //if (_plotView.Legend.Visible)
            //{
            //    if (isPortrait)
            //    {
            //        _plotView.Model.LegendPlacement = LegendPlacement.Outside;
            //        _plotView.Model.LegendPosition = LegendPosition.RightTop;
            //        _plotView.Model.LegendOrientation = LegendOrientation.Vertical;
            //    }
            //    else
            //    {
            //        _plotView.Model.LegendPlacement = LegendPlacement.Outside;
            //        _plotView.Model.LegendPosition = LegendPosition.TopRight;
            //        _plotView.Model.LegendOrientation = LegendOrientation.Horizontal;
            //    }
            //}

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

            // TODO: fix title
            //_plotView.Model.Title = "Evolutie indicator";

            // TODO: fix colors
            //if (Device.RuntimePlatform == Device.Android)
            //    _plotView.Model.TextColor = OxyColors.LightGray;

            var dtAxis = new DateTimeAxisX();
            // TODO: fix this
            //dtAxis.Position = AxisPosition.Bottom;
            //dtAxis.IntervalType = DateTimeIntervalType.Months;
            //dtAxis.StringFormat = "yyyy-MM";
            //dtAxis.IsPanEnabled = false;
            //dtAxis.IsZoomEnabled = false;

            var verticalAxis = new NumericAxisY();
            // TODO: fix this
            //verticalAxis.Position = AxisPosition.Left;
            //verticalAxis.IsPanEnabled = false;
            //verticalAxis.IsZoomEnabled = false;

            _plotView.AxisX = dtAxis;
            _plotView.AxisY = verticalAxis;

            _plotView.Series.Clear();

            var series = new LineSeries();
            series.Data = new SeriesDataAdapter() 
            { 
                DataSource = _viewModel.ChapterData,
                ArgumentDataMember = "TimeStamp",
            };
            ((SeriesDataAdapter)series.Data).ValueDataMembers.Add(new ValueDataMember { Type = ValueType.Value, Member = "Value" });
            // TODO:
            //series.Title = _pickerCounties.Items[_pickerCounties.SelectedIndex];
            _plotView.Series.Add(series);

            if (_viewModel.Value2ColumnVisibility)
            {
                var series2 = new LineSeries();
                series2.Data = new SeriesDataAdapter() 
                { 
                    DataSource = _viewModel.ChapterData,
                    ArgumentDataMember = "TimeStamp",
                };
                ((SeriesDataAdapter)series2.Data).ValueDataMembers.Add(new ValueDataMember { Type = ValueType.Value, Member = "Value2" });
                // TODO:
                //series2.Title = _pickerCounties2.Items[_pickerCounties2.SelectedIndex];
                _plotView.Series.Add(series2);
            }

            _plotView.Legend.Visible = _plotView.Series.Count > 1;

            // TODO: fix Legend placement if possible
            //if (_plotView.Model.IsLegendVisible)
            //{
            //    if (_wasPortrait)
            //    {
            //        _plotView.Model.LegendPlacement = LegendPlacement.Outside;
            //        _plotView.Model.LegendPosition = LegendPosition.RightTop;
            //        _plotView.Model.LegendOrientation = LegendOrientation.Vertical;
            //    }
            //    else
            //    {
            //        _plotView.Model.LegendPlacement = LegendPlacement.Outside;
            //        _plotView.Model.LegendPosition = LegendPosition.TopRight;
            //        _plotView.Model.LegendOrientation = LegendOrientation.Horizontal;
            //    }
            //}

            // TODO: is this still needed?
            //_plotView.Model.InvalidatePlot(true);
        }

        private async void picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            await LoadData();
        }
    }
}