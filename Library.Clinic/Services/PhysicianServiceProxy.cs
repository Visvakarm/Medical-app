using Library.Clinic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Clinic.Services
{
    public class PhysicianServiceProxy
    {
        private static object _lock = new object();
        public static PhysicianServiceProxy Current
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new PhysicianServiceProxy();
                    }
                }
                return instance;
            }
        }

        private static PhysicianServiceProxy? instance;
        private PhysicianServiceProxy()
        {
            instance = null;

            Physicians = new List<Physician>
                {
                    new Physician{Id = 1, Name = "Dr. Smith", LicenseNumber = "12345", GraduationDate = new DateTime(2000, 1, 1), Specializations = new List<string>{"Cardiology", "Internal Medicine"} } ,
                    new Physician{Id = 2, Name = "Dr. Johnson", LicenseNumber = "67890", GraduationDate = new DateTime(2005, 5, 5), Specializations = new List<string>{"Dermatology", "Pediatrics"} }
                };
        }

        public int LastKey
        {
            get
            {
                if (Physicians.Any())
                {
                    return Physicians.Select(x => x.Id).Max();
                }
                return 0;
            }
        }

        private List<Physician> physicians;
        public List<Physician> Physicians
        {
            get
            {
                return physicians;
            }

            private set
            {
                if (physicians != value)
                {
                    physicians = value;
                }
            }
        }

        public void AddOrUpdatePhysician(Physician physician)
        {
            bool isAdd = false;
            if (physician.Id <= 0)
            {
                physician.Id = LastKey + 1;
                isAdd = true;
            }

            if (isAdd)
            {
                Physicians.Add(physician);
            }
        }

        public void DeletePhysician(int id)
        {
            var physicianToRemove = Physicians.FirstOrDefault(p => p.Id == id);

            if (physicianToRemove != null)
            {
                Physicians.Remove(physicianToRemove);
            }
        }

        public IEnumerable<object> GetPhysicians()
        {
            return Physicians;
        }
    }
}
