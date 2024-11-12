using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Clinic.Models
{
    public class Physician
    {
        public override string ToString()
        {
            return Display;
        }
        ///Move to view model
        public string Display
        {
            get
            {
                return $"[{Id}] {Name} , {LicenseNumber},{GraduationDate},{SpecializationString}";
            }
        }
        public int Id { get; set; }
        private string? name;
        public string Name
        {
            get
            {
                return name ?? string.Empty;
            }

            set
            {
                name = value;
            }
        }
  private string _licenseNumber;
public string LicenseNumber
{
    get
    {
        return _licenseNumber ?? string.Empty;
    }
    set
    {
        _licenseNumber = value;
    }
}
        public DateTime GraduationDate { get; set; }

        /// Move to view model
        public string SpecializationString  {
            get {
                return string.Join(",", Specializations);
            }

        }
        public List<string> Specializations { get; set; }

        public Physician()
        {
            Name = string.Empty;
            LicenseNumber = string.Empty;
            GraduationDate = DateTime.MinValue;
            Specializations = new List<string>();
        }

        public string GetLicenseView()
        {
            return $"License Number: {LicenseNumber}\nGraduation Date: {GraduationDate.ToShortDateString()}\nSpecializations: {string.Join(", ", Specializations)}";
        }
    }
}
