using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdatKezelő;
using System.Xml.Linq;

namespace AdatKezelőTeszt
{
    [TestClass]
    public class StatisztikaTeszts
    {//Luca
        [TestMethod]
        public void ExcelExportTest()
        {
       
            Statisztika stat = new Statisztika();

            XDocument doc = MakeFakeXML();

            stat.ExportToExcel(doc, @"D:\valami1.xlsx");
        }

        public XDocument MakeFakeXML()
        {
            XElement root = new XElement("root");
            var doc = new XDocument(root);
            for (int i = 0; i < 4; i++)
            {
                root.Add(new XElement("day",new XAttribute("datum",DateTime.Now), new XElement("valamiValamije", "valamik")));
            }
            return doc;
        }
        
    }

}
