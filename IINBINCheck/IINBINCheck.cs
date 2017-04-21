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

  internal class DirectChek : ChekAlgoritm
  {
    protected int[] directChain = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
    internal override bool Checked(char[] iinArray)
    {
      return Check(iinArray, directChain);
    }
  }

  internal class IndirectChek : ChekAlgoritm
  {
    protected int[] inderectChain = new int[] { 3, 4, 5, 6, 7, 8, 9, 10, 11, 1, 2 };
    internal override bool Checked(char[] iinArray)
    {
      return Check(iinArray, inderectChain);
    }
  }

  public class IinBinCheckContext
  {
    char[] _iinbinArray;
    bool _isCheked;

    public IinBinCheckContext()
    {

    }

    public IinBinCheckContext(string value, ChekAlgoritm chekAlgoritm)
    {
      if(chekAlgoritm != null && string.IsNullOrEmpty(value))
        throw new ArgumentNullException("IIN/BIN and chekAlgoritm is NULL or empty");

      _iinbinArray = value.ToArray<char>();
      ContextChekAlgoritm = chekAlgoritm;
    }

    public IinBinCheckContext(string value, ChekAlgoritm chekAlgoritm, bool check)
    {
      if(chekAlgoritm != null && string.IsNullOrEmpty(value))
        throw new ArgumentNullException("IIN/BIN and chekAlgoritm is NULL or empty");

      _iinbinArray = value.ToArray<char>();
      ContextChekAlgoritm = chekAlgoritm;

      if(check)
        Check();

    }

    /// <summary>
    /// Set value for convert to the array
    /// </summary>
    public string SetInnBin
    {
      set
      {
        _iinbinArray = value.ToArray<char>();
      }
    }

    /// <summary>
    /// Check is BIN or IIN
    /// </summary>
    public DocumentType IsBinOrIin
    {
      get
      {
        if(_iinbinArray.Length == 0)
          throw new ArgumentNullException("IIN/BIN length is zero");

        if(_iinbinArray[4] > '3')
          return DocumentType.BIN;
        else
          return DocumentType.IIN;
      }
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
        if(_iinbinArray == null)
          throw new ArgumentNullException("BIN array is NULL");

        if(IsBinOrIin != DocumentType.BIN)
          throw new NotBINExeption();

        if(_iinbinArray.Length == 12)
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
          throw new ArgumentNullException("IIN array is NULL");

        if(IsBinOrIin != DocumentType.IIN)
          throw new NotIINExeption();

        if(_iinbinArray.Length == 12)
          return new Individual().GetIndividualData(_iinbinArray);
        else
          throw new NotEqualLengthExeption();
      }
    }
  }
}
