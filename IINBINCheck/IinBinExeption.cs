using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IinBinCheck
{
    public class IinBinExeption : Exception
    {
        public IinBinExeption(string message) : base(message)
        {
        }
    }

    public class NotEqualLengthExeption : IinBinExeption
    {
        public NotEqualLengthExeption() : base("Not equals length 12")
        {
        }
    }

    public class NotBINExeption : IinBinExeption
    {
        public NotBINExeption() : base("Not the BIN")
        {
        }
    }

    public class NotIINExeption : IinBinExeption
    {
        public NotIINExeption() : base("Not the IIN")
        {
        }
    }
}
