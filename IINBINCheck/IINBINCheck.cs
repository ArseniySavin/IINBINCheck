using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IINBINCheck
{
    public abstract class ChekAlgoritm
    {
        protected int rank;
        protected char modRank;
        internal virtual char CurrentRank { get { return modRank; } }

        protected virtual bool Check(char[] iinArray, int[] direct)
        {
            try
            {
                for(int i = 0; i < 11; i++)
                {
                    rank = ((direct[i] * iinArray[i]) + rank);
                }

                modRank = (rank % 11).ToString().Cast<char>().First();

                if(modRank == iinArray[11])
                    return true;
                else
                    return false;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        internal virtual Data Data(char[] iinArray)
        {
            try
            {
                #region Bad lazy code. This code must be refactored
                // TODO refactored lazy code

                if (iinArray[4] > '3')
                {
                    string regDate = string.Format("{2}{3}-{0}{1}-01", iinArray[0], (iinArray[1] == '0') ? 1 : iinArray[1], iinArray[2], iinArray[3]);

                    return new Data
                    {
                        RegistrationDate = DateTime.ParseExact(regDate, "yy-MM-dd", CultureInfo.InvariantCulture),
                        Type = (CompanyType)Enum.Parse(typeof(CompanyType), iinArray[4].ToString()),
                        SpecialCompanyType = (SpecialType)Enum.Parse(typeof(SpecialType), iinArray[5].ToString()),
                        SequenceNumber = string.Format("{0}{1}{2}{3}{4}", iinArray[6].ToString(), iinArray[7].ToString(), iinArray[8].ToString(), iinArray[9].ToString(), iinArray[10].ToString()),
                        Rank = Convert.ToInt32(iinArray[11].ToString()),
                        DocumentType = DocumentType.BIN
                    };
                }
                else
                {
                    string regDate = string.Format("{0}{1}-{2}{3}-{4}{5}", iinArray[0], iinArray[1], iinArray[2], iinArray[3], iinArray[4], iinArray[5]);
                    int absGender = Math.Abs(iinArray[5] % 2);
                    return new Data
                    {
                        BirtDate = DateTime.ParseExact(regDate, "yy-MM-dd", CultureInfo.InvariantCulture),
                        Gender = absGender == 1 ? GenderType.Male : GenderType.Female,
                        SequenceNumber = string.Format("{0}{1}{2}{3}{4}", iinArray[6].ToString(), iinArray[7].ToString(), iinArray[8].ToString(), iinArray[9].ToString(), iinArray[10].ToString()),
                        Rank = Convert.ToInt32(iinArray[11].ToString()),
                        SpecialCompanyType = SpecialType.NaN,
                        DocumentType = DocumentType.IIN
                    };
                }
                #endregion
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        internal abstract bool Checked(char[] iinArray);
    }

    public class DirectChek : ChekAlgoritm
    {
        protected int[] directChain = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
        internal override bool Checked(char[] iinArray)
        {
            return Check(iinArray, directChain);
        }
    }

    public class IndirectChek : ChekAlgoritm
    {
        protected int[] inderectChain = new int[] { 3, 4, 5, 6, 7, 8, 9, 10, 11, 1, 2 };
        internal override bool Checked(char[] iinArray)
        {
            return Check(iinArray, inderectChain);
        }
    }

    public class ContextIinCheck
    {
        char[] _iinArray;
        bool _isCheked;

        public ContextIinCheck(string iin, ChekAlgoritm chekAlgoritm)
        {
            if(chekAlgoritm != null && string.IsNullOrEmpty(iin))
                throw new ArgumentNullException("iin and chekAlgoritm");

            _iinArray = iin.ToArray<char>();
            ContextChekAlgoritm = chekAlgoritm;
        }

        public ContextIinCheck(string iin, ChekAlgoritm chekAlgoritm, bool check)
        {
            if(chekAlgoritm != null && string.IsNullOrEmpty(iin))
                throw new ArgumentNullException("iin and chekAlgoritm");

            _iinArray = iin.ToArray<char>();
            ContextChekAlgoritm = chekAlgoritm;

            if(check)
                Check();

        }

        /// <summary>
        /// Instance accessor algoritm
        /// </summary>
        public ChekAlgoritm ContextChekAlgoritm
        {
            private get;
            set;
        }

        /// <summary>
        /// Current absolute controling number
        /// </summary>
        public char CurrentRank
        {
            get
            {
                return ContextChekAlgoritm.CurrentRank;
            }
        }

        /// <summary>
        /// Result cheking
        /// </summary>
        public bool IsCheked
        {
            get
            {
                return _isCheked;
            }
        }

        /// <summary>
        /// Start check by algoritm.
        /// </summary>
        public void Check()
        {

            if(_iinArray.Length == 12)
                _isCheked = ContextChekAlgoritm.Checked(_iinArray);
            else
                _isCheked = false;
        }

        /// <summary>
        /// Decrypted info data
        /// </summary>
        public Data IINBINData
        {
            get
            {
                if(_iinArray.Length == 12)
                    return ContextChekAlgoritm.Data(_iinArray);
                else
                    return new Data();
            }
        }
    }
}
