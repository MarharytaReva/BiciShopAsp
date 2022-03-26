using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BiciShop.Models.Services
{
    public class PhotoConvert
    {
        public static string GetPhotoBase64(IFormFile file)
        {
            try
            {

                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    string photoBase64 = Convert.ToBase64String(fileBytes);
                    return photoBase64;
                }
            }
            catch (Exception ex)
            {
                string path = Environment.CurrentDirectory + @"\wwwroot\Files\imageDefault.png";
                byte[] bytes = System.IO.File.ReadAllBytes(path);
                string photoBase64 = Convert.ToBase64String(bytes);
                return photoBase64;
            }
        }
    }
}
