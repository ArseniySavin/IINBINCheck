using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IINBINCheck
{
  public class IINBINCheckExeption : Exception
  {
    public IINBINCheckExeption(string message) : base(message)
    {
    }
  }

  public class NotEqualLengthExeption : IINBINCheckExeption
  {
    public NotEqualLengthExeption() : base("Not equals length 12")
    {
    }
  }

  public class NotBINExeption : IINBINCheckExeption
  {
    public NotBINExeption() : base("Not the BIN")
    {
    }
  }

  public class NotIINExeption : IINBINCheckExeption
  {
    public NotIINExeption() : base("Not the IIN")
    {
    }
  }
}
