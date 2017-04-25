using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using Acr.UserDialogs;
using Xamarin.Forms;

namespace Rad.Factories
{
    public static class ControlFactory
    {

        public static StackLayout GetLabelEntryStackLayout(string labelName,  out Entry entry)
        {
            var lbl = new Label() { Text = labelName, HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center };
            entry = new Entry() { HorizontalOptions = LayoutOptions.EndAndExpand, WidthRequest = 200 };
           
            var stack = new StackLayout()
            {
                Padding =  10,
                Spacing = 0,
                Orientation = StackOrientation.Horizontal,
                Children = { lbl, entry },
            };
            return stack;
        }

        public static StackLayout GetLabelPickerStackLayout(string labelName, out Picker picker)
        {
            var lbl = new Label() { Text = labelName, HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center };
            picker = new Picker() { HorizontalOptions = LayoutOptions.EndAndExpand, WidthRequest = 200 };

            var stack = new StackLayout()
            {
                Padding = 10,
                Spacing = 0,
                Orientation = StackOrientation.Horizontal,
                Children = { lbl, picker },
            };
            return stack;
        }

        public static StackLayout GetStacksLayout(params StackLayout[] Stacks)
        {
            var Stack= new StackLayout
            {
                Spacing = 10,
                Padding = new Thickness(20),
            };

            foreach (var stack in Stacks)
            {
                Stack.Children.Add(stack);
            }
            return Stack;
        }

        public static Button GetButton(string btnText)
        {
            var btn = new Button
            {
                Text = btnText,
                TextColor = Color.White,
                BackgroundColor = Color.Teal
            };
            return btn;
        }


        //https://www.nuget.org/packages/Acr.UserDialogs/
        public static async void GetConfirmDialog(Action onYes, Action onNo = null)
        {
            var config = new ConfirmConfig() { Title = "Alert", Message = "your changes will be lost.  are you sure you want to go back?" };
            config.UseYesNo();
            config.OnConfirm += b =>
            {
                if (b)
                    onYes();
                else
                {
                    if (onNo != null)
                        onNo();
                }
            };
            UserDialogs.Instance.Confirm(config);
        }

    }
}
