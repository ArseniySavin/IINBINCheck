using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IINBINCheck;

namespace IINBINCheckTest
{
  [TestClass]
  public class IINBINCheckTest
  {
    [TestMethod]
    public void Check_DirectAlgoritm_Test()
    {
      IinBinCheckContext context = new IinBinCheckContext();

      context.SetInnBin = "830302300054";

      context.Check();

      Assert.IsTrue(context.IsCheked);
    }

    [TestMethod]
    public void Check_InDirectAlgoritm_Test()
    {
      IinBinCheckContext context = new IinBinCheckContext();

      context.SetInnBin = "830302300054";
      context.ChekAlgoritm = new IndirectChek();

      context.Check();

      Assert.IsFalse(context.IsCheked);
    }

    [TestMethod]
    public void IsBinOrIin_Test()
    {
      IinBinCheckContext context = new IinBinCheckContext();

      context.SetInnBin = "830302300054";

      var data = context.IsBinOrIin;

      Assert.IsTrue(context.IsBinOrIin == DocumentType.IIN);
    }

    [TestMethod]
    public void IINData_Test()
    {
      IinBinCheckContext context = new IinBinCheckContext();

      context.SetInnBin = "830302300054";

      var  data = context.IINData;

      Assert.IsNotNull(data);
    }
  }
}
