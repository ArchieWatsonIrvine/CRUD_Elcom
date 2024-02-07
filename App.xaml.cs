using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRUD_Elcom
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new Page1();
        }

        protected override async void OnStart()
        {
            //This will create/init the database
            await StockItemServices.Instance.initDatabase();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
