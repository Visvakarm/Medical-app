using Library.Clinic.Models;
using Library.Clinic.Services;
using System.Collections.ObjectModel;
using System.Linq;

namespace App.Clinic.ViewModels
{
    public class AppointmentManagementViewModel
    {
        private AppointmentServiceProxey _appSvc = AppointmentServiceProxey.Current;

        public ObservableCollection<AppointmentViewModel> Appointments
        {
            get
            {
                return new ObservableCollection<AppointmentViewModel>(
                    _appSvc.Appointments.Select(a => new AppointmentViewModel(a)));
            }
        }
    }
}
