using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using Java.Util;
using StatisticsRomania.Droid.Renderers;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using StatisticsRomania.Controls;

[assembly: ExportRenderer(typeof(PickerWithNoSpellCheck), typeof(MyPickerRenderer))]
namespace StatisticsRomania.Droid.Renderers
{
    public class MyPickerRenderer : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
                Control.InputType = InputTypes.TextFlagNoSuggestions;

        }
    }
}