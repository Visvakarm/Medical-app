using Library.Clinic.Models;
using System.Collections.Generic;
using System.Linq;

namespace Library.Clinic.Services
{
    public class AppointmentServiceProxey
    {
        private static readonly AppointmentServiceProxey _current = new AppointmentServiceProxey();
        public static AppointmentServiceProxey Current => _current;

        private readonly List<Appointment> _appointments = new List<Appointment>();

        public bool ScheduleAppointment(Appointment appointment)
        {
            // Check if the appointment is within working hours (Monday-Friday, 9 AM - 5 PM)
            if (appointment.StartTime.HasValue)
            {
                var startTime = appointment.StartTime.Value;
                if (startTime.DayOfWeek == DayOfWeek.Saturday ||
                    startTime.DayOfWeek == DayOfWeek.Sunday ||
                    startTime.Hour < 9 ||
                    startTime.Hour >= 17)
                {
                    return false;
                }
            }

            // Check for double booking
            if (_appointments.Any(a => a.PatientId == appointment.PatientId && a.StartTime == appointment.StartTime))
            {
                return false;
            }

            _appointments.Add(appointment);
            return true;
        }

        public void CancelAppointment(int appointmentId)
        {
            var appointment = _appointments.FirstOrDefault(a => a.Id == appointmentId);
            if (appointment != null)
            {
                appointment.IsCancelled = true;
            }
        }

        public void AddOrUpdate(Appointment appointment)
        {
            var existingAppointment = _appointments.FirstOrDefault(a => a.Id == appointment.Id);
            if (existingAppointment != null)
            {
                _appointments.Remove(existingAppointment);
            }
            _appointments.Add(appointment);
        }

        public List<Appointment> Appointments => _appointments;
    }
}
