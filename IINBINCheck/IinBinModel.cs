using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace IinBinCheck
{
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
        Iin,
        Bin
    }

    public abstract class IinBinModelBase
    {
        public virtual string SequenceNumber { get; set; }
        public virtual int Rank { get; set; }
        public virtual DocumentType DocumentType { get; set; }
    }

    public class BinModel : IinBinModelBase
    {
        public CompanyType Type { get; set; }
        public DateTime RegistrationDate { get; set; }
        public SpecialType SpecialCompanyType { get; set; }

        internal BinModel GetLegalData(char[] iinArray)
        {
            BinModel _legalData = null;
            try
            {
                string regDate = string.Format("{2}{3}-{0}{1}-01", iinArray[0], (iinArray[1] == '0') ? "1" : iinArray[1].ToString(), iinArray[2], iinArray[3]);

                _legalData = new BinModel
                {
                    RegistrationDate = DateTime.ParseExact(regDate, "yy-MM-dd", CultureInfo.InvariantCulture),
                    Type = (CompanyType)Enum.Parse(typeof(CompanyType), iinArray[4].ToString()),
                    SpecialCompanyType = (SpecialType)Enum.Parse(typeof(SpecialType), iinArray[5].ToString()),
                    SequenceNumber = string.Format("{0}{1}{2}{3}{4}", iinArray[6].ToString(), iinArray[7].ToString(), iinArray[8].ToString(), iinArray[9].ToString(), iinArray[10].ToString()),
                    Rank = Convert.ToInt32(iinArray[11].ToString()),
                    DocumentType = DocumentType.Bin
                };
            }
            catch (Exception ex)
            {
                throw new IinBinExeption(ex.Message);
            }
            return _legalData;
        }
    }

    public class IinModel : IinBinModelBase
    {
        public DateTime BirtDate { get; set; }
        public GenderType Gender { get; set; }

        internal IinModel GetIndividualData(char[] iinArray)
        {
            IinModel _individualData = null;
            try
            {
                string regDate = string.Format("{0}{1}-{2}{3}-{4}{5}", iinArray[0], iinArray[1], iinArray[2], iinArray[3], iinArray[4], iinArray[5]);
                int absGender = Math.Abs(iinArray[5] % 2);
                _individualData = new IinModel
                {
                    BirtDate = DateTime.ParseExact(regDate, "yy-MM-dd", CultureInfo.InvariantCulture),
                    Gender = absGender == 1 ? GenderType.Male : GenderType.Female,
                    SequenceNumber = string.Format("{0}{1}{2}{3}{4}", iinArray[6].ToString(), iinArray[7].ToString(), iinArray[8].ToString(), iinArray[9].ToString(), iinArray[10].ToString()),
                    Rank = Convert.ToInt32(iinArray[11].ToString()),
                    DocumentType = DocumentType.Iin
                };
            }
            catch (Exception ex)
            {
                throw new IinBinExeption(ex.Message);
            }

            return _individualData;
        }
    }
}
