using Context.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Context.Models
{
    public static class ModelCreatingPartial
    {
        public static void Insert(ModelBuilder modelBuilder)
        {
            string password = "123456";
            var passwordService = new PasswordEncryptService();
            var encriptedPassword = passwordService.PasswordEncrypt(password);

        }
    }
}
