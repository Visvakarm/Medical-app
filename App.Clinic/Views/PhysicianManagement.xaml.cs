using App.Clinic.ViewModels;
using System.ComponentModel;

namespace App.Clinic.Views;

public partial class PhysicianManagement : ContentPage, INotifyPropertyChanged
{
    public PhysicianManagement()
    {
        InitializeComponent();
        BindingContext = new PhysicianManagementViewModel();
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void AddClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//PhysicianDetails?physicianId=0");

    }

    private void EditClicked(object sender, EventArgs e)
    {
        var selectedPhysicianId = (BindingContext as PhysicianManagementViewModel)?
            .SelectedPhysician?.Id ?? 0;
        Shell.Current.GoToAsync($"//PhysicianDetails?physicianId={selectedPhysicianId}");
    }

    private void DeleteClicked(object sender, EventArgs e)
    {
        (BindingContext as PhysicianManagementViewModel)?.Delete();
    }

    private void PhysicianManagement_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as PhysicianManagementViewModel)?.Refresh();
    }

    private void RefreshClicked(object sender, EventArgs e)
    {
        (BindingContext as PhysicianManagementViewModel)?.Refresh();
    }
}
