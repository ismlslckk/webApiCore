using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiCore.Filters;
using WebApiCore.Models;

namespace WebApiCore.Controllers
{
    //[ModelValidation] startup classında global olarak tanımladığımız için gerek kalmadı.sadece bu class için kullansaydık global eklemezdik.
    [Route("api/users")]
    public class UserController : Controller
    {
        /*
         * [FromBody]postmanden json olarak post için Body 'den raw seçilir json yazılır ve headerada application/json eklenir.
         * [FromForm]postmanden form olarak post için Body 'den form-data seçilir form alanları doldurulır  ve headerada application/x-www-form-urlencoded eklenir.
         * [FromForm]postmanden x-www-form-urlencoded olarak post için Body 'den x-www-form-urlencoded seçilir form alanları doldurulır  ve headerada application/x-www-form-urlencoded eklenir.
         * ayrıca modeli yollarken tüm alanları doldurmak gerekmez sadece 1 alan bile post edebiliriz.
             */
        [HttpPost("createUser")]
        public ActionResult createUser([FromForm]User user)
        {
            return Ok("Created user");
        }
    }
}