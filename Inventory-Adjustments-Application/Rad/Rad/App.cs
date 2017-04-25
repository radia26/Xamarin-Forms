using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rad.ViewModel;
using Rad.Views.InventoryView;
using Xamarin.Forms;

namespace Rad
{
    public class App : Application
    {
        DataBaseManager database = new DataBaseManager();


        //back button: look for CustomContentPage

        public App()
        {
            //var database = new DataBaseManager();

            MainPage = new NavigationPage(new InventoryListPage(database));
            //MainPage = new ContentPage
            //{
            //    Content = new StackLayout
            //    {
            //        VerticalOptions = LayoutOptions.Center,
            //        Children = {
            //            new Label {
            //                XAlign = TextAlignment.Center,
            //                Text = "Welcome to Xamarin Forms!"
            //            }
            //        }
            //    }
            //};
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
