using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdatKezelő;
using System.Diagnostics;
using System.IO;
namespace AdatKezelőTeszt
{
    [TestClass]
    public class HibalogletezikTeszt
    {
        [TestMethod]
        public void HibalogletezikTest()
        {
            string path = System.IO.Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName,
               @"SzoftverTech\Csillám_kezelőfelület\bin\Debug\Hibalog.xml");


            bool meg_van = false;

            if (File.Exists(path))
            { meg_van = true; }

            Assert.AreEqual(true, meg_van);
               
        
        }
    }
}
