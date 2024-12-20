﻿using Library.Clinic.Models;
using Library.Clinic.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace App.Clinic.ViewModels
{
    public class PhysicianViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<string> _specializations;

        public Physician? Model { get; set; }
        public ICommand? DeleteCommand { get; set; }
        public ICommand? EditCommand { get; set; }
        public ICommand? AddSpecializationCommand { get; set; }


        public event PropertyChangedEventHandler? PropertyChanged;

        public int Id
        {
            get
            {
                if (Model == null)
                {
                    return -1;
                }

                return Model.Id;
            }

            set
            {
                if (Model != null && Model.Id != value)
                {
                    Model.Id = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Name
        {
            get => Model?.Name ?? string.Empty;
            set
            {
                if (Model != null)
                {
                    Model.Name = value;
                    OnPropertyChanged();
                }
            }
        }

        public string LicenseNumber
        {
            get => Model?.LicenseNumber ?? string.Empty;
            set
            {
                if (Model != null)
                {
                    Model.LicenseNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<string> Specializations
        {
            get => _specializations;
            set
            {
                if (_specializations != value)
                {
                    _specializations = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime GraduationDate
        {
            get => Model?.GraduationDate ?? DateTime.MinValue;
            set
            {
                if (Model != null && Model.GraduationDate != value)
                {
                    Model.GraduationDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public void SetupCommands()
        {
            DeleteCommand = new Command(DoDelete);
            EditCommand = new Command((p) => DoEdit(p as PhysicianViewModel));
            AddSpecializationCommand = new Command<string>(AddSpecialization);
        }

        private void DoDelete()
        {
            if (Id > 0)
            {
                PhysicianServiceProxy.Current.DeletePhysician(Id);
                Shell.Current.GoToAsync("//Physicians");
            }
        }

        private void DoEdit(PhysicianViewModel? pvm)
        {
            if (pvm == null)
            {
                return;
            }
            var selectedPhysicianId = pvm?.Id ?? 0;
            Shell.Current.GoToAsync($"//PhysicianDetails?physicianId={selectedPhysicianId}");
        }

        public PhysicianViewModel()
        {
            Model = new Physician();
            _specializations = new ObservableCollection<string>();
            SetupCommands();
        }

        public PhysicianViewModel(Physician? _model)
        {
            Model = _model;
            _specializations = new ObservableCollection<string>(Model?.Specializations ?? new List<string>());
            SetupCommands();
        }
        private void AddSpecialization(string specialization)
        {
            if (!string.IsNullOrWhiteSpace(specialization))
            {
                Specializations.Add(specialization);

            
                if (Model != null && Model.Specializations != null)
                {
                    Model.Specializations.Add(specialization);
                }
                OnPropertyChanged(nameof(Specializations));
            }
        }
        public void ExecuteAdd()
        {
            if (Model != null)
            {
                PhysicianServiceProxy
                .Current
                .AddOrUpdatePhysician(Model);
            }

            Shell.Current.GoToAsync("//Physicians");
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
