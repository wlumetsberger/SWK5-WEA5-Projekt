using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL.Common;
using UltimateFestivalOrganizer.DAL.Common.Dao;
using UltimateFestivalOrganizer.DAL.Common.Domain;
using System.Collections.Generic;

namespace UltimateFestivalOrganizer.Test.DAL.SqlServer.Dao
{
    [TestClass]
    public class PerformanceDaoTest : BaseTest
    {
        [TestMethod]
        public void TestFindAll()
        {
            IPerformanceDao dao = DALFactory.CreatePerformanceDao(DALFactory.CreateDatabase());
            IList<Performance> entities = dao.findAll();
            Assert.AreEqual(entities.Count, 2);
        }
        [TestMethod]
        public void TestFindById()
        {
            IPerformanceDao dao = DALFactory.CreatePerformanceDao(DALFactory.CreateDatabase());
            Performance performance = dao.findById(1);
            Assert.AreEqual(performance.Id, 1);
            Assert.IsFalse(performance.Canceld);
        }

        [TestMethod]
        public void TestInsert()
        {
            Performance performance = new Performance();
            IArtistDao artistDao = DALFactory.CreateArtistDao(DALFactory.CreateDatabase());
            Artist artist = artistDao.findById(1);

            IVenueDao venueDao = DALFactory.CreateVenueDao(DALFactory.CreateDatabase());
            Venue venue = venueDao.findById(1);

            performance.Artist = artist;
            performance.Venue = venue;
            performance.StagingTime = DateTime.Now; 

            IPerformanceDao dao = DALFactory.CreatePerformanceDao(DALFactory.CreateDatabase());
            dao.Insert(performance);

            IList<Performance> result = dao.findAll();
            Assert.AreEqual(result.Count, 3);
        }

        [TestMethod]
        public void TestUpdate()
        {
            IPerformanceDao dao = DALFactory.CreatePerformanceDao(DALFactory.CreateDatabase());
            Performance performance = dao.findById(1);
            IArtistDao artistDao = DALFactory.CreateArtistDao(DALFactory.CreateDatabase());
            Artist artist = artistDao.findById(2);
            performance.Artist = artist;
            performance.StagingTime = DateTime.UtcNow;
            dao.Update(performance);

            Performance result = dao.findById(1);
            //Assert.AreEqual(result.StagingTime, performance.StagingTime);
            Assert.AreEqual(result.Artist.Id, performance.Artist.Id);

        }
    }
}
