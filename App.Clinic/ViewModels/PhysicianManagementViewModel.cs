using Library.Clinic.Models;
using Library.Clinic.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace App.Clinic.ViewModels
{
    public class PhysicianManagementViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public PhysicianViewModel? SelectedPhysician { get; set; }
        public string? Query { get; set; }
        public ObservableCollection<PhysicianViewModel> Physicians
        {
            get
            {
                var retVal = new ObservableCollection<PhysicianViewModel>(
                    PhysicianServiceProxy
                    .Current
                    .Physicians
                    .Where(p => p != null)
                    .Where(p => p.Name.ToUpper().Contains(Query?.ToUpper() ?? string.Empty))
                    .Take(100)
                    .Select(p => new PhysicianViewModel(p))
                    );

                return retVal;
            }
        }

        public void Delete()
        {
            if (SelectedPhysician == null)
            {
                return;
            }
            PhysicianServiceProxy.Current.DeletePhysician(SelectedPhysician.Id);

            Refresh();
        }

        public void Refresh()
        {
            NotifyPropertyChanged(nameof(Physicians));
        }
    }
}
