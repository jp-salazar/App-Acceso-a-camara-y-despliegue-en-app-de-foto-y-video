using Gh0stApp.Models;
using Gh0stApp.Services;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gh0stApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowEmployeeView : ContentPage
    {
        EmployeeService services;

        public ShowEmployeeView()
        {
            InitializeComponent();
            services = new EmployeeService();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            showEmployee();
        }
        private void showEmployee()
        {
            var res = services.GetAllEmployees().Result;
            lstData.ItemsSource = res;
        }

        private void btnAddRecord_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new AddEmployeeView() );            
        }

        private async void lstData_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                EmployeeModel obj = (EmployeeModel)e.SelectedItem;
                string res = await DisplayActionSheet("Operacion", "Cancelar", null, "Actualizar", "Eliminar");

                switch (res)
                {
                    case "Actualizar":
                        await this.Navigation.PushAsync(new AddEmployeeView(obj));
                        break;
                    case "Eliminar":
                        services.DeleteEmployee(obj);
                        showEmployee();
                        break;
                }
                lstData.SelectedItem = null;
            }
        }
    }
}