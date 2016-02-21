using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Utils;
using OxyPlot;
using OxyPlot.Axes;
using StatisticsRomania.ViewModels;
using Xamarin.Forms;
using DevExpress.Mobile.DataGrid;
using OxyPlot.Xamarin.Forms;
using OxyPlot.Series;

namespace StatisticsRomania.Views
{
    public class CountyDetailsView : ContentPage
    {
        private CountyDetailsViewModel _viewModel;
        private Picker _pickerChapters;
        private Picker _pickerCounties;

        private PlotView plotView;

        public CountyDetailsView()
        {
            Title = "Statistici judenete";

            Init();
        }

        private async Task Init()
        {
            _viewModel = new CountyDetailsViewModel();
            await _viewModel.GetCounties();
            _viewModel.GetChapters();

            var lblCounty = new Label
            {
                VerticalOptions = LayoutOptions.Center,
                Text = "Judet:"
            };

            _pickerCounties = new Picker()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            foreach (var county in _viewModel.CountyList)
            {
                _pickerCounties.Items.Add(county.Key);
            }
            _pickerCounties.SelectedIndexChanged += pickerCounties_SelectedIndexChanged;

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

            var degChapterData = new GridControl();
            degChapterData.IsReadOnly = true;
            degChapterData.HorizontalOptions = LayoutOptions.FillAndExpand;
            degChapterData.Columns.Add(new TextColumn() { Caption = "An", FieldName = "Year", IsReadOnly = true, AllowSort = DefaultBoolean.False});
            degChapterData.Columns.Add(new TextColumn() { Caption = "Luna", FieldName = "YearFraction", IsReadOnly = true, AllowSort = DefaultBoolean.False });
            degChapterData.Columns.Add(new TextColumn() { Caption = "Valoare", FieldName = "Value", IsReadOnly = true, AllowSort = DefaultBoolean.False });
            degChapterData.ItemsSource = _viewModel.ChapterData;

            plotView = new PlotView();
            plotView.HorizontalOptions = LayoutOptions.FillAndExpand;
            plotView.VerticalOptions = LayoutOptions.FillAndExpand;
            plotView.Model = new PlotModel();
            var series = new LineSeries();
            series.ItemsSource = _viewModel.ChapterData;
            plotView.Model.Series.Add(series);

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
                                lblCounty, _pickerCounties
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
                    //degChapterData,
                    plotView
                }
            };

            _pickerCounties.SelectedIndex = 0;
            _pickerChapters.SelectedIndex = 0;
        }

        private async Task LoadData()
        {
            var selectedCounty = _pickerCounties.SelectedIndex >= 0
                                     ? _viewModel.CountyList[_pickerCounties.Items[_pickerCounties.SelectedIndex]]
                                     : -1;
            var selectedChapter = _pickerChapters.SelectedIndex >= 0
                                      ? _pickerChapters.Items[_pickerChapters.SelectedIndex]
                                      : string.Empty;

            await _viewModel.GetChapterData(selectedCounty, selectedChapter);

            if (plotView == null)
                return;

            plotView.Model = new PlotModel();

            plotView.Model.TextColor = OxyColors.Aqua;

            var dtAxis = new DateTimeAxis();
            dtAxis.Position = AxisPosition.Bottom;
            dtAxis.IntervalType = DateTimeIntervalType.Months;
            dtAxis.StringFormat = "yyyy-MM";
            plotView.Model.Axes.Add(dtAxis);

            var series = new LineSeries();
            series.ItemsSource = _viewModel.ChapterData;
            series.DataFieldX = "TimeStamp";
            series.DataFieldY = "Value";

            plotView.Model.Series.Clear();
            plotView.Model.Series.Add(series);

            plotView.Model.InvalidatePlot(true);
        }

        private async void pickerChapters_SelectedIndexChanged(object sender, EventArgs e)
        {
            await LoadData();
        }

        private async void pickerCounties_SelectedIndexChanged(object sender, EventArgs e)
        {
            await LoadData();
        }
    }
}