using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity
{
    public class Helper
    {
        public static async Task<string> UploadImage(List<IFormFile> Files, string FolderName)
        {
            foreach (var file in Files)
            {
                if (file.Length > 0)
                {
                    string ImageName = Guid.NewGuid().ToString() + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + ".jpg";
                    var filePaths = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/Uploads/" + FolderName + "/" + ImageName);
                    using (var stream = System.IO.File.Create(filePaths))
                    {
                        await file.CopyToAsync(stream);
                        return ImageName;
                    }
                }
            }
            return string.Empty;
        }

        //default Admin
       public  const string Fname = "Admin";
       public const string Lname = "Admin";
       public const string Email = "ibrahemtabaneh@gmail.com";
       public const string password = "123456";
       public const string UserName = "ibrahemtabaneh@gmail.com";


        public enum Roles
        {
            Admin,
            Customer
        };
    }
}
