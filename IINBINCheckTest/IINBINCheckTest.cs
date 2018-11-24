using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IinBinCheck;

namespace IinBinCheckTest
{
    [TestClass]
    public class IinBinCheckTest
    {
        IinBinCheckContext context = null;

        public IinBinCheckTest()
        {
            context = new IinBinCheckContext();
        }

        [TestMethod]
        public void Check_DirectAlgoritm_Test()
        {
            context.SetInnBin = "830302300054";

            context.Check();

            Assert.IsTrue(context.IsCheked);
        }

        [TestMethod]
        public void Check_InDirectAlgoritm_Test()
        {
            context.SetInnBin = "830302300054";
            context.ChekAlgoritm = new IndirectCheck();

            context.Check();

            Assert.IsFalse(context.IsCheked);
        }

        [TestMethod]
        public void IsBinOrIin_Inn_Test()
        {
            context.SetInnBin = "830302300054";

            var data = context.IsBinOrIin;

            Assert.IsTrue(context.IsBinOrIin == DocumentType.Iin);
        }

        [TestMethod]
        public void IsBinOrIin_Bin_Test()
        {
            context.SetInnBin = "070940006509";

            var data = context.IsBinOrIin;

            Assert.IsTrue(context.IsBinOrIin == DocumentType.Bin);
        }

        [TestMethod]
        public void IINData_Test()
        {
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

        [TestMethod]
        public void CheckBirthDateUsing_20_Century_Test()
        {
            context.SetInnBin = "830302300054";

            var data = context.IinData as IinModel;

            Assert.AreEqual(data.BirthDate.ToString("yyyy-MM-dd"), "1983-03-02");
        }

        [TestMethod]
        public void CheckBirthDateUsing_21_Century_Test()
        {
            context.SetInnBin = "071215651296";

            var data = context.IinData as IinModel;

            Assert.AreEqual(data.BirthDate.ToString("yyyy-MM-dd"), "2007-12-15");
        }

        [TestMethod]
        public void Check_20_CenturyOfIinData()
        {
            context.SetInnBin = "830302300054";

            var data = context.IinData as IinModel;

            Assert.AreEqual(data.Century, "20");
        }

        [TestMethod]
        public void Check_21_CenturyOfIinData()
        {
            context.SetInnBin = "071215651296";

            var data = context.IinData as IinModel;

            Assert.AreEqual(data.Century, "21");
        }

        [TestMethod]
        public void CheckIinIsMale()
        {
            context.SetInnBin = "830302300054";

            var data = context.IinData as IinModel;

            Assert.AreEqual(data.Gender, GenderType.Male);
        }

        [TestMethod]
        public void CheckIinIsFemale()
        {
            context.SetInnBin = "071215651296";

            var data = context.IinData as IinModel;

            Assert.AreEqual(data.Gender, GenderType.Female);
        }
    }
}
