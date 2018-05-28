using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IinBinCheck;

namespace IinBinCheckTest
{
    [TestClass]
    public class IinBinCheckTest
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
            context.ChekAlgoritm = new IndirectCheck();

            context.Check();

            Assert.IsFalse(context.IsCheked);
        }

        [TestMethod]
        public void IsBinOrIin_Inn_Test()
        {
            IinBinCheckContext context = new IinBinCheckContext();

            context.SetInnBin = "830302300054";

            var data = context.IsBinOrIin;

            Assert.IsTrue(context.IsBinOrIin == DocumentType.Iin);
        }

        [TestMethod]
        public void IsBinOrIin_Bin_Test()
        {
            IinBinCheckContext context = new IinBinCheckContext();

            context.SetInnBin = "070940006509";

            var data = context.IsBinOrIin;

            Assert.IsTrue(context.IsBinOrIin == DocumentType.Bin);
        }

        [TestMethod]
        public void IINData_Test()
        {
            IinBinCheckContext context = new IinBinCheckContext();

            context.SetInnBin = "830302300054";

            var data = context.IinData;

            Assert.IsNotNull(data);
        }

        [TestMethod]
        public void BINData_Test()
        {
            IinBinCheckContext context = new IinBinCheckContext();

            context.SetInnBin = "070940006509";

            var data = context.BinData;

            Assert.IsNotNull(data);
        }

        [TestMethod]
        public void ChangeAlgoritm_Bin_Iin_Test()
        {
            IinBinCheckContext context = new IinBinCheckContext();

            context.SetInnBin = "070940006509"; // Bin

            context.ChekAlgoritm = new IndirectCheck();

            context.Check();

            Assert.IsFalse(context.IsCheked);

            context.SetInnBin = "830302300054"; // Iin

            context.ChekAlgoritm = new DirectCheck();

            context.Check();

            Assert.IsTrue(context.IsCheked);
        }
    }
}
