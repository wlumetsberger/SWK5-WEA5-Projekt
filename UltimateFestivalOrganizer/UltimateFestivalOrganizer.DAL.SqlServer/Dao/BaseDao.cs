﻿using DAL.Common;
using DAL.Common.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UltimateFestivalOrganizer.DAL.Common.Attributes;
using UltimateFestivalOrganizer.DAL.Common.Dao;
using UltimateFestivalOrganizer.DAL.Common.Domain;
using UltimateFestivalOrganizer.DAL.Common.Util;

namespace UltimateFestivalOrganizer.DAL.SqlServer.Dao { 

    /// <summary>
    /// Every DAO in our Project has to extend this DAO
    /// If Pocos are written in correct way, this dao provides anything you will need for our project
    /// Only this Dao implements the IDAO interface every other Dao should only use BaseDao
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseDao<T> : IDao, IBaseDao<T> where T : BaseEntity, new()
    {
        protected IDatabase database;

        public BaseDao(IDatabase db)
        {
            this.database = db;
        }
        /// <summary>
        /// Generates a Default Select statement
        /// </summary>
        /// <returns></returns>
        protected string GetDefaultSelect()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            bool first = true;
            typeof(T).GetProperties().ToList().ForEach(x =>
            {
                if (!first)
                {
                    sql.Append(" , ");
                }
                sql.Append("[");
                sql.Append(x.Name);
                sql.Append("]");
                first = false;
            });
            sql.Append(" FROM [");
            sql.Append(DatabaseUtil.GetTableNameOfType(typeof(T)));
            sql.Append("]");
            return sql.ToString();
        }
        /// <summary>
        /// Generates Default delete Statment
        /// </summary>
        /// <returns></returns>
        private string GetDefaultDelte()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append( "DELETE FROM [");
            sql.Append(DatabaseUtil.GetTableNameOfType(typeof(T)));
            sql.Append("] WHERE [");
            sql.Append(DatabaseUtil.getIdPropertyOfElement(typeof(T)).Name);
            sql.Append("]=@Id");
            return sql.ToString();
        }
        /// <summary>
        /// Generate Default Update Statement
        /// </summary>
        /// <returns></returns>
        private string GetDefaultUpdate()
        {
            StringBuilder  sql =  new StringBuilder("UPDATE [");
            sql.Append(DatabaseUtil.GetTableNameOfType(typeof(T)));
            sql.Append("] SET ");
            bool first = true;
            typeof(T).GetProperties().All((x) => {
                // only if key is not id add 
                if (x.GetCustomAttribute(typeof(Id)) == null)
                {
                    if (!first)
                    {
                        sql.Append(" , ");
                    }
                    sql.Append("[");
                    sql.Append(x.Name);
                    sql.Append("]=@");
                    sql.Append(x.Name);
                    first = false;
                }
                return true;
            });
            sql.Append(" WHERE [");
            sql.Append(DatabaseUtil.getIdPropertyOfElement(typeof(T)).Name);
            sql.Append("]=@WHEREID");
            return sql.ToString();
        }
        /// <summary>
        /// Creates the Insert Statment for any Entity
        /// Properties with annotation Autogenerated are not included
        /// </summary>
        /// <returns></returns>
        private string GetDefaultInsert()
        {
            StringBuilder sql = new StringBuilder("INSERT INTO [");
            sql.Append(DatabaseUtil.GetTableNameOfType(typeof(T)));
            sql.Append("] ( ");
            bool first = true;
            typeof(T).GetProperties().ToList().ForEach( x => {
                // only if key is not autogenerated add 
                if (x.GetCustomAttribute(typeof(AutogenerateIdAttribute)) == null)
                {
                    if(!first){
                        sql.Append(" , ");
                    }
                    sql.Append("[");
                    sql.Append(x.Name);
                    sql.Append("]");
                    first = false;
                 }
            });
            
            first = true;
            sql.Append(" ) VALUES ( ");
            typeof(T).GetProperties().ToList().ForEach((x) => {
                // do it like above
                if (x.GetCustomAttribute(typeof(AutogenerateIdAttribute)) == null)
                {
                    if (!first)
                    {
                        sql.Append(" , ");
                    }
                    sql.Append("@");
                    sql.Append(x.Name);
                    first = false;
                }
            });
            sql.Append(" ) ");
            return sql.ToString();
        }

        /// <summary>
        /// Fetch Foreignkey Properties 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="info"></param>
        /// <param name="key"></param>
        private void fetchRelationShip(T element, PropertyInfo info,object key)
        {
            object[] obj = new object[1];
            IDao dao = DALFactory.CreateDao(this.database, info.PropertyType);
            if(dao != null)
            {
                obj[0] = dao.findById(key);
                if (obj[0] != null)
                {
                    info.GetSetMethod().Invoke(element, obj);
                }
            }
        }
        /// <summary>
        /// Map the Data from Current Reader Position to an Entity
        /// ForeignKeys will be Fetched too -> Eager mode
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        protected T ConvertSingleResultToObject(IDataReader reader)
        {
            T element = new T();           
            typeof(T).GetProperties().All((x) => {
                int idx = reader.GetOrdinal(x.Name);
                if (reader.IsDBNull(idx))
                {
                    return true;
                }
                object[] value = new object[1];
                value[0] = reader.GetValue(idx);
                if(x.PropertyType.BaseType == typeof(BaseEntity))
                {
                    fetchRelationShip(element,x,reader.GetValue(idx));
                }
                else
                {
                    if(value[0] != DBNull.Value)
                    {
                        x.GetSetMethod().Invoke(element, value);
                    }
                   
                }
                
                return true;
            });
            return element;
            
        }
        /// <summary>
        /// Opens the DataReader and Reads all Records
        /// If possible, the values are Mapped into an Entity
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        protected IList<T> ConvertResultToList(IDataReader reader)
        {
            IList<T> retVal = new List<T>();
            while (reader.Read())
            {
                retVal.Add(ConvertSingleResultToObject(reader));
            }
            return retVal;
        }
        /// <summary>
        /// Find all Elements for an Entity
        /// </summary>
        /// <returns></returns>
        public IList<T> findAll()
        {
            DbCommand command = database.CreateCommand(this.GetDefaultSelect());
            using (IDataReader reader = database.ExecuteReader(command))
            {
                return ConvertResultToList(reader);
            }
        }
        /// <summary>
        /// Find an Enity by its Id Property
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T findById(object id)
        {
            return findByUniqueProperty(DatabaseUtil.getIdPropertyOfElement(typeof(T)), id);
           
        }

        public IList<T> findByProperty(PropertyInfo prop , object value)
        {
            
            StringBuilder sql = new StringBuilder(this.GetDefaultSelect());
            sql.Append(" WHERE ");
            sql.Append(prop.Name);
            sql.Append("=@Id");
            // resolve foreign key properties correct
            object valueToSet = value;
            if (prop.PropertyType.BaseType == typeof(BaseEntity))
            {
                valueToSet = ((BaseEntity)value).Id;
            }
                DbCommand command = database.CreateCommand(sql.ToString());
            database.DefineParameter(command, "@Id", DbType.Object, valueToSet);
            using (IDataReader reader = database.ExecuteReader(command))
            {
                return ConvertResultToList(reader);
            }
        }
      
        public bool Delete(T element)
        {
            DbCommand command = database.CreateCommand(this.GetDefaultDelte());
            database.DefineParameter(command, "@Id", DbType.Object, element.Id);
            return database.ExecuteNonQuery(command) == 1;



        }
        /// <summary>
        /// Find an Entity within its Unique Key
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public T findByUniqueProperty(PropertyInfo prop, object value)
        {
            StringBuilder sql = new StringBuilder(this.GetDefaultSelect());
            sql.Append(" WHERE ");
            sql.Append(prop.Name);
            sql.Append("=@Id");

            DbCommand command = database.CreateCommand(sql.ToString());
            database.DefineParameter(command, "@Id", DbType.Object, value);
            using (IDataReader reader = database.ExecuteReader(command))
            {
                if (reader.Read())
                {
                    return ConvertSingleResultToObject(reader);
                }
                return null;
            }
        }
        /// <summary>
        /// Insert an Element in Database
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public T Insert(T element)
        {
            DbCommand command = database.CreateCommand(this.GetDefaultInsert());
            typeof(T).GetProperties().All((x) =>
            {
                if(x.GetCustomAttribute(typeof(AutogenerateIdAttribute)) == null)
                {
                    var value = x.GetGetMethod().Invoke(element, null);
                    var field = "@" + x.Name;
                    if (x.PropertyType.BaseType == (typeof(BaseEntity)))
                    {
                        var entity = (BaseEntity)value;
                        value = DatabaseUtil.getIdOfElement<BaseEntity>(entity);
                    }
                    if(value == null)
                    {
                        value = DBNull.Value;
                    }
                    database.DefineParameter(command, field, DbType.Object, value);
                }
                return true;
            });
            int id = database.ExecuteInsertAndReturnId(command);
            return this.findById(id);
            
        }
        /// <summary>
        /// Update an Element in Database
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public bool Update(T element)
        {
            DbCommand command = database.CreateCommand(this.GetDefaultUpdate());
            database.DefineParameter(command, "@WHEREID", DbType.Object, DatabaseUtil.getIdOfElement<T>(element));
            typeof(T).GetProperties().ToList().ForEach((x) =>
            {
                if (x != null && x.GetCustomAttribute(typeof(Id)) == null)
                {
                    string field = "@" + x.Name;
                    var value = x.GetGetMethod().Invoke(element, null);
                    if (x.PropertyType.BaseType == (typeof(BaseEntity)))
                    {
                        var entity = (BaseEntity)value;
                        value = DatabaseUtil.getIdOfElement<BaseEntity>(entity);
                    }
                    if (value == null)
                    {
                        value = DBNull.Value;
                    }
                    database.DefineParameter(command, field, DbType.Object, value);
                }
            });
            Console.WriteLine("Execute Update: " + command.CommandText);
            return database.ExecuteNonQuery(command)==1;
        }
        /// <summary>
        /// Implement method and Call Correct Generic One
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        object IDao.findById(object id)
        {
            return findById(id);
        }
        /// <summary>
        /// Implement method and Call Correct Generic One
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        object IDao.findByUniqueProperty(PropertyInfo prop, object value)
        {
            return findByUniqueProperty(prop, value);
        }
        /// <summary>
        /// Implement method and Call Correct Generic One
        /// </summary>
        /// <returns></returns>
        object IDao.findAll()
        {
            return findAll();
        }

        object IDao.findByProperty(PropertyInfo prop, object value)
        {
            return findByProperty(prop, value);
        }
        /// <summary>
        /// Implement method and Call Correct Generic One
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        bool IDao.Update(object element)
        {
            return Update((T)element);
        }
        /// <summary>
        /// Implement method and Call Correct Generic One
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        object IDao.Insert(object element)
        {
            return Insert((T)element);
        }
    }
}
