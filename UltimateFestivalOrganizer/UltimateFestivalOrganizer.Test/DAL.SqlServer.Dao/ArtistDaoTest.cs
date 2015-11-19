using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UltimateFestivalOrganizer.DAL.Common.Dao;
using UltimateFestivalOrganizer.DAL.SqlServer.Dao;
using System.Collections.Generic;
using UltimateFestivalOrganizer.DAL.Common.Domain;
using System.Reflection;
using DAL.Common;

namespace UltimateFestivalOrganizer.Test.DAL.SqlServer.Dao
{
    [TestClass]
    public class ArtistDaoTest : BaseTest
    {
        [TestMethod]
        public void TestFindAll()
        {
            IArtistDao dao = DALFactory.CreateArtistDao(DALFactory.CreateDatabase());
            IList<Artist> entities = dao.findAll();
            Assert.AreEqual(entities.Count, 3);
        }
        [TestMethod]
        public void TestFindById()
        {
            IArtistDao dao = DALFactory.CreateArtistDao(DALFactory.CreateDatabase());
            Artist artist = dao.findById(1);
            Assert.AreEqual(artist.Id, 1);
            Assert.AreEqual(artist.Name, "Larry Page");
        }

        [TestMethod]
        public void TestFindByIdAndCheckFetchCatagory()
        {
            IArtistDao dao = DALFactory.CreateArtistDao(DALFactory.CreateDatabase());
            Artist artist = dao.findById(1);
            Assert.AreEqual(artist.Id, 1);
            Assert.AreEqual(artist.Name, "Larry Page");

            ICatagoryDao catDao = DALFactory.CreateCatagoryDao(DALFactory.CreateDatabase());
            Catagory cat = catDao.findById(1);
            Assert.AreEqual(artist.Catagory.Id, cat.Id);
        }

        [TestMethod]
        public void TestFindByUniqueProperty()
        {
            IArtistDao dao = DALFactory.CreateArtistDao(DALFactory.CreateDatabase());
            PropertyInfo info = typeof(Artist).GetProperty("Email");
            Artist artist = dao.findByUniqueProperty(info, "l.page@gmx.com");
            Assert.AreEqual(artist.Id, 1);
            Assert.AreEqual(artist.Email, "l.page@gmx.com");
            Assert.AreEqual(artist.Name, "Larry Page");
        }

        [TestMethod]
        public void TestInsert()
        {
            PropertyInfo info = typeof(Artist).GetProperty("Email");
            Artist artist = new Artist();
            artist.Email = "UnitTestArtist@test.at";
            artist.Name = "UnitTestArtist";
            artist.Link = "I do not have any Link";
            artist.Country = "AUT";

            IArtistDao dao = DALFactory.CreateArtistDao(DALFactory.CreateDatabase());
            dao.Insert(artist);

            Artist result = dao.findByUniqueProperty(info, "UnitTestArtist@test.at");
            Assert.AreEqual(result.Email, artist.Email);
            Assert.AreEqual(result.Name, artist.Name);
            Assert.AreEqual(result.Link, artist.Link);
            Assert.AreEqual(result.Country, artist.Country);
        }

        [TestMethod]
        public void TestUpdate()
        {
            IArtistDao dao = DALFactory.CreateArtistDao(DALFactory.CreateDatabase());
            Artist artist = dao.findById(1);
            artist.Email = "larry.page@gmail.com";
            dao.Update(artist);

            Artist result = dao.findById(1);
            Assert.AreEqual(result.Email, artist.Email);

        }
    }
}
