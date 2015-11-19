using DAL.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UltimateFestivalOrganizer.DAL.Common.Dao;
using UltimateFestivalOrganizer.DAL.Common.Domain;

namespace UltimateFestivalOrganizer.Test.DAL.SqlServer.Dao
{
    [TestClass]
    public class CatagoryDaoTest : BaseTest
    {
        [TestMethod]
        public void TestFindAll()
        {
            ICatagoryDao dao = DALFactory.CreateCatagoryDao(DALFactory.CreateDatabase());
            IList<Catagory> entities = dao.findAll();
            Assert.AreEqual(entities.Count, 2);
        }
        [TestMethod]
        public void TestFindById()
        {
            ICatagoryDao dao = DALFactory.CreateCatagoryDao(DALFactory.CreateDatabase());
            Catagory cat = dao.findById(1);
            Assert.AreEqual(cat.Id, 1);
            Assert.AreEqual(cat.Name, "Klassik");
        }

        [TestMethod]
        public void TestInsert()
        {

            Catagory cat = new Catagory();
            cat.Name = "UNIT-TEST";
            cat.Description = "asdf";

            ICatagoryDao dao = DALFactory.CreateCatagoryDao(DALFactory.CreateDatabase());
            dao.Insert(cat);
            PropertyInfo info = typeof(Catagory).GetProperty("Name");
            Catagory result = dao.findByUniqueProperty(info, "UNIT-TEST");
            Assert.AreEqual(result.Name, cat.Name);
            Assert.AreEqual(result.Description, cat.Description);
        }

        [TestMethod]
        public void TestUpdate()
        {
            ICatagoryDao dao = DALFactory.CreateCatagoryDao(DALFactory.CreateDatabase());
            Catagory cat = dao.findById(1);
            cat.Name = "Test";
            dao.Update(cat);

            Catagory result = dao.findById(1);
            Assert.AreEqual(result.Name, cat.Name);

        }
    }
}
