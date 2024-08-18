using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages<T> where T : class,IEntity,new()
    {
        public static string EntityAdded = $"{typeof(T).Name} is added"; // Using string interpolation
        public static string EntityDeleted = $"{typeof(T).Name} is deleted"; 
        public static string EntityUpdated = $"{typeof(T).Name} is updated"; 
        public static string EntityNotAdded = $"{typeof(T).Name} could not be added"; 
        public static string EntityNotDeleted = $"{typeof(T).Name} could not be deleted";
        public static string EntityNotUpdated = $"{typeof(T).Name} could not be updated"; 
        public static string MaintenanceTime = "We are in Maintenance time!";
        public static string EntityListed = $"{typeof(T).Name}'s are listed!";
        public static string EntityNotListed = $"{typeof(T).Name}'s could not be listed!";
    }
}
