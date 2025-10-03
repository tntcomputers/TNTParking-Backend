using Microsoft.AspNetCore.Mvc;
using TNTAuthenticatorDB.Models.User;

namespace TNTParking_Backend.Controllers
{
    public class BaseController : Controller
    {
        public int UNIT_ID
        {
            get
            {
                try
                {
                    var user = (UserDTO?)HttpContext.Items["User"];
                    return user?.UnitId ?? 1;
                }
                catch (Exception)
                {
                    return 1;
                }
            }
        }

        public int USER_ID
        {
            get
            {
                try
                {
                    var user = (UserDTO?)HttpContext.Items["User"];
                    return user.Id;
                }
                catch (Exception)
                {
                    return 1;
                }

            }
        }

        public string USER_Name
        {
            get
            {
                try
                {
                    var user = (UserDTO?)HttpContext.Items["User"];
                    //return user.UserName;
                    return "tnt";
                }
                catch (Exception)
                {
                    return "";
                }

            }
        }

        public bool IsAdmin
        {
            get
            {
                try
                {
                    var user = (UserDTO?)HttpContext.Items["User"];
                    return user.IsAdmin;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool IsSuperAdmin
        {
            get
            {
                try
                {
                    var user = (UserDTO?)HttpContext.Items["User"];
                    return user.IsSuperAdmin;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}
