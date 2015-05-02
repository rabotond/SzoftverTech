using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Csillám_kezelőfelület;
using AdatKezelő;
namespace AdatKezelőTeszt
{

    [TestClass]
    public class EnumTeszt
    {
        
        [TestMethod]
        public void Enumtest()
        {

            int egyik = (int)Adomány_típus.Eledel;
            int masik = (int)Adomány_típus.Pénz;
            Assert.AreEqual(1, egyik);
            Assert.AreEqual(0, masik);
         
        }
    }
}
