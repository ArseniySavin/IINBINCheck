using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IINBINCheck
{
    public struct Data
    {
        public DateTime BirtDate { get; set; }
        public GenderType Gender { get; set; }
        public DateTime RegistrationDate { get; set; }
        public CompanyType Type { get; set; }
        public SpecialType SpecialCompanyType { get; set; }
        public string SequenceNumber { get; set; }
        public int Rank { get; set; }
        public DocumentType DocumentType { get; set; }
    }

    public enum GenderType
    {
        NaN = 0,
        Male = 1,
        Female = 2
    }

    public enum CompanyType
    {
        NaN = 0,
        LegalResident = 4,
        LegalNoResident = 5,
        Individual = 6
    }

    public enum SpecialType
    {
        HeadOffice = 0,
        DepartOffice = 1,
        Agency = 2,
        Other = 3,
        Farm = 4,
        NaN = 100,
    }

    public enum DocumentType
    {
        IIN,
        BIN
    }
}
