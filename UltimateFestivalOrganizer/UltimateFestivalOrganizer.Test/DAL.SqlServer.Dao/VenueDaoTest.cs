using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL.Common;
using UltimateFestivalOrganizer.DAL.Common.Dao;
using UltimateFestivalOrganizer.DAL.Common.Domain;

namespace UltimateFestivalOrganizer.Test.DAL.SqlServer.Dao
{
    /// <summary>
    /// Zusammenfassungsbeschreibung für VenueDaoTest
    /// </summary>
    [TestClass]
    public class VenueDaoTest :BaseTest
    {
        [TestMethod]
        public void TestFindAll()
        {
            IDatabase db = DALFactory.CreateDatabase();
            IVenueDao dao = DALFactory.CreateVenueDao(db);
            IList<Venue> entities = dao.findAll();
            Assert.AreEqual(entities.Count, 2);
        }
        [TestMethod]
        public void TestFindById()
        {
            IDatabase db = DALFactory.CreateDatabase();
            IVenueDao dao = DALFactory.CreateVenueDao(db);
            Venue v = dao.findById(1);
            Assert.AreEqual(v.Id, 1);
            Assert.AreEqual(v.Description, "Stage1");

        }
       
        [TestMethod]
        public void TestInsert()
        {
            IDatabase db = DALFactory.CreateDatabase();
            IVenueDao dao = DALFactory.CreateVenueDao(db);
            Venue v = new Venue();
            v.Description = "UNIT-TEST";
            v.Latitude = 2;
            v.Longitude = 2;
            v.ShortDescription = "NO";
            v.Address = "TestAddress";

            dao.Insert(v);

            Assert.AreEqual(dao.findAll().Count, 3);

        }
        [TestMethod]
        public void TestUpdate()
        {
            IDatabase db = DALFactory.CreateDatabase();
            IVenueDao dao = DALFactory.CreateVenueDao(db);
            Venue v = dao.findById(1);
            v.Description = "TEST-DESCRIPTION";

            dao.Update(v);
            Venue result = dao.findById(1);
            Assert.AreEqual(result.Description, v.Description);
        }
    }
}
