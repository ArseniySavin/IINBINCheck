using System;
using System.Collections.Generic;

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
      char[] _iinbinArray;
      bool _isCheked;

    public ContextIinCheck(string value, ChekAlgoritm chekAlgoritm)
    {
      if (chekAlgoritm != null && string.IsNullOrEmpty(value))
        throw new ArgumentNullException("IIN/BIN and chekAlgoritm is NULL or empty");

      _iinbinArray = value.ToArray<char>();
      ContextChekAlgoritm = chekAlgoritm;
    }

    public ContextIinCheck(string iin, ChekAlgoritm chekAlgoritm, bool check)
    {
      if (chekAlgoritm != null && string.IsNullOrEmpty(iin))
        throw new ArgumentNullException("IIN/BIN and chekAlgoritm is NULL or empty");

      _iinbinArray = iin.ToArray<char>();
      ContextChekAlgoritm = chekAlgoritm;

      if (check)
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

          if(_iinbinArray.Length == 12)
              _isCheked = ContextChekAlgoritm.Checked(_iinbinArray);
          else
              _isCheked = false;
      }

      /// <summary>
      /// Decrypted info data for Legal
      /// </summary>
      public Legale BINData
      {
        get
        {
          if (_iinbinArray == null)
            throw new ArgumentNullException("Object array is NULL");

          if (_iinbinArray.Length == 12)
            return new Legale().GetLegalData(_iinbinArray);
          else
            throw new NotEqualLengthExeption();
        }
      }

      /// <summary>
      /// Decrypted info data for Individual
      /// </summary>
      public IINBINData IINData
      {
        get
        {
          if(_iinbinArray == null)
            throw new ArgumentNullException("Object array is NULL");

          if (_iinbinArray.Length == 12)
            return new Individual().GetIndividualData(_iinbinArray);
          else
            throw new NotEqualLengthExeption();
      }
      }
  }
}
