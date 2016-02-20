using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Utils;
using StatisticsRomania.ViewModels;
using Xamarin.Forms;
using DevExpress.Mobile.DataGrid;

namespace StatisticsRomania.Views
{
    public class CountyDetailsView : ContentPage
    {
        private CountyDetailsViewModel _viewModel;

        public CountyDetailsView()
        {
            Title = "Statistici judenete";

            Init();
        }

        private async Task Init()
        {
            _viewModel = new CountyDetailsViewModel();
            //BindingContext = _viewModel;
            await _viewModel.GetCounties();

            var lblCounty = new Label
            {
                VerticalOptions = LayoutOptions.Center,
                Text = "Judet:"
            };

            var pickerCounties = new Picker()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            foreach (var county in _viewModel.CountyList)
            {
                pickerCounties.Items.Add(county.Key);
            }

            await _viewModel.GetAverageGrossSalaries();
            var degAverageGrosSalary = new GridControl();
            degAverageGrosSalary.IsReadOnly = true;
            degAverageGrosSalary.HorizontalOptions = LayoutOptions.FillAndExpand;
            degAverageGrosSalary.Columns.Add(new TextColumn() { Caption = "Year", FieldName = "Year", IsReadOnly = true, AllowSort = DefaultBoolean.False});
            degAverageGrosSalary.Columns.Add(new TextColumn() { Caption = "Month", FieldName = "YearFraction", IsReadOnly = true, AllowSort = DefaultBoolean.False });
            degAverageGrosSalary.Columns.Add(new TextColumn() { Caption = "Value", FieldName = "Value", IsReadOnly = true, AllowSort = DefaultBoolean.False });
            degAverageGrosSalary.ItemsSource = _viewModel.AverageGrossSalaryCollection;

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
                                    lblCounty, pickerCounties
                                }
                        },
                        degAverageGrosSalary,
                }
            };
        }
    }
}