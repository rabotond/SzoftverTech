using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace AdatKezelőTeszt
{
    [TestClass]
    public class Hostletezik
    {
        [TestMethod]
        public void HostletezikTeszt()
        {
            string path = System.IO.Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName,
               @"SzoftverTech\ReklamHost\bin\Debug\ReklamHost.exe");

            bool meg_van = false;

            if (File.Exists(path))
            { meg_van = true; }

            Assert.AreEqual(true, meg_van);
        }
    }
}
