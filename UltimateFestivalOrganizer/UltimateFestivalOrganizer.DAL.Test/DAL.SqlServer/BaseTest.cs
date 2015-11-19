using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL.Common;
using UltimateFestivalOrganizer.DAL.SqlServer;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Configuration;

namespace UltimateFestivalOrganizer.Test.DAL.SqlServer
{

  
   
    [TestClass]
    public class BaseTest
    {
        protected String DatbaseConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["UltimateFestivalOrganizerDB"].ConnectionString;
        }

        protected String ScriptFolder()
        {
            return ConfigurationManager.AppSettings.Get("ScriptFolder");
        }

        public static void ExecuteScript(string file, string connection)
        {
            string script = File.ReadAllText(file);
            IList<string> commands = Regex.Split(script, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();
                foreach(string command in commands)
                {
                    if(command.Trim() != "")
                    {
                        using(var sqlcommand = con.CreateCommand())
                        {
                            sqlcommand.CommandText = command;
                            sqlcommand.ExecuteNonQuery();
                        }
                    }
                }
                con.Close();
            }
        }

  
    
        [TestInitialize()]
        public void Initialize()
        {
            BaseTest.ExecuteScript(ScriptFolder() + "DropDatabase.sql", DatbaseConnectionString());
            BaseTest.ExecuteScript(ScriptFolder() + "CreateDatabase.sql", DatbaseConnectionString());
            BaseTest.ExecuteScript(ScriptFolder() + "InsertData.sql", DatbaseConnectionString());
        }

        [TestCleanup()]
        public void Cleanup()
        {
        }
    }
}
