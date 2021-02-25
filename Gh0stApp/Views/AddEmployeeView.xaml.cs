using Gh0stApp.Models;
using Gh0stApp.Services;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gh0stApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddEmployeeView : ContentPage
    {
        EmployeeService _services;
        bool _isUpdate;
        int employeeID;
        public AddEmployeeView()
        {
            InitializeComponent();
            _services = new EmployeeService();
            _isUpdate = false;
        }
        public AddEmployeeView(EmployeeModel obj)
        {
            InitializeComponent();
            _services = new EmployeeService();
            if (obj != null)
            {
                employeeID = obj.Id;
                txtName.Text = obj.Name;
                txtEmail.Text = obj.Email;
                txtAddress.Text = obj.Address;
                _isUpdate = true;
            }
        }
        private async void btnSaveUpdate_Clicked(object sender, EventArgs e)
        {
            EmployeeModel obj = new EmployeeModel();
            obj.Name = txtName.Text;            
            obj.Email = txtEmail.Text;
            obj.Address = txtAddress.Text;
            if (_isUpdate)
            {
                obj.Id = employeeID;
                await _services.UpdateEmployee(obj);
            }
            else
            {
                _services.InsertEmployee(obj);
            }
            await this.Navigation.PopAsync();
        }
    }
}