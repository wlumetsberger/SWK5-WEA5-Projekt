using DAL.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UltimateFestivalOrganizer.DAL.Common.Attributes;

namespace UltimateFestivalOrganizer.DAL.Common.Util
{
    public class DatabaseUtil
    {
        /// <summary>
        /// Returns ID Property for an Entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="element"></param>
        /// <returns></returns>
        public static object getIdOfElement<T>(T element) where T :BaseEntity
        {
            try {
                var idProperty = element.GetType().GetProperties().Where(x => x.GetCustomAttribute(typeof(Id)) != null).First();
                if(idProperty != null && idProperty.GetGetMethod() != null)
                {
                    return idProperty.GetGetMethod().Invoke(element, null);
                }
                
            }catch(NullReferenceException e)
            { 
                
            }
            return null;
        }
        /// <summary>
        /// Returns the TableName for an Entity
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string GetTableNameOfType(Type t)
        {
            if(t.BaseType != typeof(BaseEntity))
            {
                throw new ArgumentException("Type muss vom Type BaseEntity abgeleitet sein");
            }
            else
            {
                return ((TableAttribute)t.GetCustomAttribute(typeof(TableAttribute))).TableName;
            }
        }
        /// <summary>
        /// Returns the ID PropertyInfo for an Entity
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static PropertyInfo getIdPropertyOfElement(Type t)
        {
            if(t.BaseType != typeof(BaseEntity))
            {
                throw new ArgumentException("Type muss vom Type BaseEntity abgeleitet sein");
            }
            else
            {
                
                    return t.GetProperties().Where(x => x.GetCustomAttribute(typeof(Id)) != null).Single();

            }
        }
    }
}
