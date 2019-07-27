using System;
using System.Collections.Generic;
using System.Text;
using TransIT.BLL.DTOs;

namespace TransIT.BLL.Comparers
{
    public class UserComparer : IEqualityComparer<UserDTO>
    {
        /// <summary>
        /// Determines whether two objects of this type are equal.
        /// Id and Role.Id properties were ignored, because they are 
        /// assigned automatically by database.
        /// Password is also ignored, because password hash is saved
        /// to database, not password.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(UserDTO x, UserDTO y)
        {   
            return x != null && y != null
                && x.Email == y.Email
                && x.FirstName == y.FirstName
                && x.MiddleName == y.MiddleName
                && x.LastName == y.LastName
                && x.IsActive == y.IsActive
                && x.PhoneNumber == y.PhoneNumber
                && x.UserName == y.UserName
                && x.Role.Name == y.Role.Name
                && x.Role.TransName == y.Role.TransName;
        }

        public int GetHashCode(UserDTO obj)
        {
            return obj.GetHashCode();
        }
    }
}
