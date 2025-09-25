using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_System
{
    //Define global variables here
    public enum FormMode
    {
        NEW = 0,
        UPDATE = 1,
        VIEW = 2,
        DELETE = 4
    }

    public static class Globals
    {

    }

    //Manage all sessions here
    public class UserSession
    {
        private static UserSession _instance;
        private UserSession() { } 

        public static UserSession Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UserSession();
                }
                return _instance;
            }
        }

        public string UserId { get; set; }
        public string UserKey { get; set; }
        public string CompanyName { get; set; }
        public int CompanyKey { get; set; }
        public string BranchName { get; set; }
        public string WindowsUserName { get; set; }
        public string BranchCode { get; set; }

        public List <FeaturePermission> Permissions { get; set; } = new List <FeaturePermission>();

        public FeaturePermission GetPermission (string featureName)
        {
            return Permissions.FirstOrDefault( p => p.ObjName == featureName );
        }

        public void ClearSession ()
        {
            UserId = null;
            Permissions.Clear();
        }
    }

    //Load user permissions
    public class FeaturePermission
    {
        public string ObjName { get; set; }
        public bool CanAccess { get; set; }
        public bool CanCreateNew { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }
        public bool CanSpecial { get; set; } //Have special access
    }
}
