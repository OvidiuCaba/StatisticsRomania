using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatisticsRomania.ViewModels;
using Xamarin.Forms;

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
            await _viewModel.GetCounties();

            var lblCounty = new Label
                                {
                                    Text = "Judet:"
                                };
            var pickerCounties = new Picker();
            foreach (var county in _viewModel.CountyList)
            {
                pickerCounties.Items.Add(county.Key);
            }

            this.Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(
                    left: 0,
                    right: 0,
                    bottom: 0,
                    top: Device.OnPlatform(iOS: 20, Android: 0, WinPhone: 0)),
                Children = { 
                    new Frame()
                        {
                            Content = lblCounty,
                            Padding = new Thickness(10, 6, 10, 2)
                        },
                    new Frame()
                    {
                        Content = pickerCounties,
                        Padding = new Thickness(10, 2),
                    },
                }
            };
        }
    }
}