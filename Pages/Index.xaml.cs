using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using static stockItem;
using static StockItemServices;

namespace CRUD_Elcom
{
    public partial class Index : ContentPage
    {
        public Index()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            lvStockItems.ItemsSource = await StockItemServices.Instance.getItemsAsync();
            //We could create a method that deletes items once they hit 0 quantity
            //Or display alert once item is under a certain quantity level
        }

        //Using the addItem.xaml page with empty fields to then add to the database.
        private void btnAdd_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new addItem
            {
                BindingContext = new stockItem()
            });
        }

        //Uses the addItem.xaml page but uses the selected item to fill in the entrys
        private void lvStockItems_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                Application.Current.MainPage = new NavigationPage(new addItem
                {
                    BindingContext = (stockItem)e.SelectedItem
                });
            }
        }
    }
}
