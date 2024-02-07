using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRUD_Elcom
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class addItem : ContentPage
	{
        public AsyncCommand AddCommand { get; }

        public addItem ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var stockItem = (stockItem)BindingContext;
            if (stockItem.StockItemId != 0)
            {
                btnDel.IsVisible = true;
            }
        }

        //async Task Add()
        //{

            
        //    ////if (manName == null)
        //    ////{
        //    ////    DisplayAlert("Invalid input", "Invalid input", "Cancel");
        //    ////}
            

        //    await StockItemServices.addItem(entNam, entCode, int.Parse(entQt.ToString()));
        //}


        async void btnSave_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(entNam.Text))
            {
                if (!string.IsNullOrEmpty(entCode.Text))
                {

                    if (entNam.Text.Count() > 100 || entCode.Text.Count() > 100)
                    {
                        await DisplayAlert("Exceeded character limit", "Please keep Manufactor name and/or Manufactor Code to less than 100 characters", "Close");
                    }
                    else
                    {
                        bool success = int.TryParse(entQt.Text, out var result);
                        if (success)
                        {
                            var StockItem = (stockItem)BindingContext;
                            StockItemServices db = StockItemServices.Instance;
                            await db.saveItemAsync(StockItem);
                            Application.Current.MainPage = new NavigationPage(new Index());
                        }
                        else
                        {
                            await DisplayAlert("Invalid number", "Quantity must be a number", "Close");
                        }
                    }
                }
                else
                {
                    await DisplayAlert("Empty Code input", "Manufactor code was left empty", "Close");
                }
            }
            else
            {
                await DisplayAlert("Empty Name input", "Manufactor name was left empty", "Close");
            }
        }

        private void btnExit_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new Index());
        }

        private async void btnDel_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Delete item", "Do you want to delete this item?", "Yes", "No"))
            {
                var stockItem = (stockItem)BindingContext;
                StockItemServices db = StockItemServices.Instance;
                await db.removeItem(stockItem);
                Application.Current.MainPage = new NavigationPage(new Index());
            }
        }
    }
}