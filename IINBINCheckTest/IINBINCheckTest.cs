using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IINBINCheck;

namespace IINBINCheckTest
{
  [TestClass]
  public class IINBINCheckTest
  {
    [TestMethod]
    public void IINDataTest()
    {
      IinBinCheckContext iinBinCheck = new IinBinCheckContext();

      iinBinCheck.SetInnBin = "830802300054";

      Assert.IsNotNull(iinBinCheck.IINData);

    }
  }
}
