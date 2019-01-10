﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmployeeDataAccess;

namespace EmployeeService
{
    public class EmployeeSecurity
    {
        public static bool Login(string username, string password)
        {
            using(EmployeeDBEntities db = new EmployeeDBEntities())
            {
                return db.Users.Any(user => user.Username.Equals(username,
                    StringComparison.OrdinalIgnoreCase) && user.Password == password);
            }
        }
    }
}