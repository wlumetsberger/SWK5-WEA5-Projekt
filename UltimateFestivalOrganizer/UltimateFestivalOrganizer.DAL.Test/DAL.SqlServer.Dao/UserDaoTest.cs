using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL.Common;
using UltimateFestivalOrganizer.DAL.SqlServer.Dao;
using UltimateFestivalOrganizer.DAL.Common.Dao;
using System.Collections.Generic;
using DAL.Common.Domain;
using System.Reflection;

namespace UltimateFestivalOrganizer.Test.DAL.SqlServer.Dao
{
    [TestClass]
    public class UserDaoTest: BaseTest
    {
        [TestMethod]
        public void TestFindAll()
        {
            IDatabase db = DALFactory.CreateDatabase();
            IUserDao dao = DALFactory.CreateUserDao(db);
            IList<User> entities = dao.findAll();
            Assert.AreEqual(entities.Count, 2);
        }
        [TestMethod]
        public void TestFindById()
        {
            IDatabase db = DALFactory.CreateDatabase();
            IUserDao dao = DALFactory.CreateUserDao(db);
            User u = dao.findById(1);
            Assert.AreEqual(u.Id, 1);
            Assert.AreEqual(u.Email, "admin@test.at");

        }
        [TestMethod]
        public void TestFindByUniqueProperty()
        {
            IDatabase db = DALFactory.CreateDatabase();
            IUserDao dao = DALFactory.CreateUserDao(db);
            PropertyInfo info = typeof(User).GetProperty("Email");
            User u = dao.findByUniqueProperty(info, "admin@test.at");
            Assert.AreEqual(u.Email, "admin@test.at");
            Assert.AreEqual(u.Id, 1);
        }
        [TestMethod]
        public void TestInsert()
        {
            IDatabase db = DALFactory.CreateDatabase();
            IUserDao dao = DALFactory.CreateUserDao(db);
            User u = new User();
            u.Email = "UnitTestUser@test.at";
            u.FirstName = "Unit Test";
            u.LastName = "Unit Test";
            u.Password = "asdf";
            dao.Insert(u);
            PropertyInfo info = typeof(User).GetProperty("Email");
            User result = dao.findByUniqueProperty(info, "UnitTestUser@test.at");
            Assert.AreEqual(result.Email, "UnitTestUser@test.at");

        }
        [TestMethod]
        public void TestUpdate()
        {
            IDatabase db = DALFactory.CreateDatabase();
            IUserDao dao = DALFactory.CreateUserDao(db);
            User u = dao.findById(1);
            u.FirstName = "Larry";
            dao.Update(u);
            User result = dao.findById(1);
            Assert.AreEqual(result.FirstName, "Larry");
        }
    }
}
