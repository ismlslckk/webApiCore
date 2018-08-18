using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiCore.Data;
using WebApiCore.Models;


namespace WebApiCore.Controllers
{
    [Route("api/values")]
    public class ValuesController : Controller
    {
        private readonly DataContext _context;

        public ValuesController(DataContext context)
        {
            //ctor 'a parametre olarak gelen context startup.cs den gelir.
            _context = context;
        }

        // ~/degerler yaparak en üstteki route ezmiş oluruz bu sayede
        // localhost:49272/degerler diyerek almış oluruz.
        //assenkron işlem yapmak için.
        [HttpGet("")]
        public async Task<ActionResult> GetValues()
        {
            //ilişkili tablolarda include ile almamız gerekir.
            var values = await _context.Values.Include(x => x.TwoTables).ToListAsync();
            return Ok(values);
        }

        [HttpGet("twoTables")]
        public async Task<ActionResult> GetTowTables()
        {
            //ilişkili tablolarda include ile almamız gerekir.
            var values = await _context.TwoTables.Include(x => x.Value).ToListAsync();
            return Ok(values);
        }


        ////ActionResult sayesinde http durum kodlarını dönebiliyoruz.
        //[HttpGet]
        //public ActionResult GetValues()
        //{
        //    var values = _context.Values.ToList();
        //    return Ok(values);//OK diyerek 200 döndürüyoruz.
        //}

        //optional constraint
        // [HttpGet("deneme/{id:decimal=2}")] bu şekildede kullanılabilir.
        [HttpGet("deneme/{id:decimal?}")]
        public async Task<ActionResult> GetValue(decimal id=2)
        {
            var value = await _context.Values.FirstOrDefaultAsync(v => v.Id == id);
            return Ok(value);
        }

        [HttpPost("createLink")]
        public ActionResult PostRouteLink([FromBody]Value value)
        {
            _context.Values.Add(value);
            _context.SaveChanges();

            int id = value.Id;

            return Ok(new Uri(Url.Link("getValue",new { id=id })));


        }

        //not:Request.RequestUri ile "host/api/controller" değerini elde edebiliriz.
        

        //{id:int} şeklinde constraint(kısıtlama) yapılabilir.
        //{id:range(1,3)}
        [HttpGet("{id:int:min(1):max(30000)}",Name ="getValue")]
        public async Task<ActionResult> GetValue(int id)
        {
            var value = await _context.Values.FirstOrDefaultAsync(v => v.Id == id);
            return Ok(value);
          
        }

        [HttpGet("{name:alpha:lastletter}")]
        public async Task<ActionResult> GetValue(string name)
        {
            var value = await _context.Values.FirstOrDefaultAsync(v => v.Name.ToLower() == name.ToLower());
            return Ok(value);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

         
        [HttpPut("guncelle")]
        public ActionResult Guncelle([FromBody]Value value)
        {
            Value newValue = _context.Values.FirstOrDefault(x => x.Id == value.Id);
            if (newValue == null)
                return StatusCode(StatusCodes.Status404NotFound,"Değer bulunamadı.");
            else
            {
                newValue.Name = value.Name;
                _context.SaveChanges();
                return Ok(true);
            }
        }

        [HttpDelete("delete/{id}")]
        public ActionResult Delete(int id)
        {
            Value newValue = _context.Values.FirstOrDefault(x => x.Id == id);
            if (newValue == null)
                return StatusCode(StatusCodes.Status404NotFound, "Değer bulunamadı.");
            else
            {
                _context.Values.Remove(newValue);
                if (_context.SaveChanges()>0)
                {
                    return Ok(true);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "silme islemi yapılamadı.");

                }
            }
        }

        /*
         [FromBody]int id -> kullanıldıgında id degerini body den oku deriz.
         [FromUri]Employee emp -> kullanıldıgında ise employee nesnesini urlden al deriz.
             */

        [HttpGet("filter/{gender}/{top}")]
        public ActionResult QueryStringGet(string gender="all",int? top=0)
        {
            IQueryable<Value> query = _context.Values;
            switch (gender)
            {
                case "all":
                    {
                        break;
                    }
                case "male":
                    {
                        query = query.Where(x=>x.Gender==gender.ToLower());
                        break;
                    }
                case "female":
                    {
                        query = query.Where(x => x.Gender == gender.ToLower());
                        break;
                    }
                default:
                    return StatusCode(StatusCodes.Status400BadRequest, new { errorMessage = "gender is male or female" } );
            }

            if (top > 0)
                query = query.Take(top.Value);
            return Ok(query.ToList());
        }

        //urlden modeli almak için bu şekilde bir kullanım uygulanır diğer parametreleride body den
        //almak için fromBody kullanılır.
        [HttpPost("customRequest")]
        public ActionResult customRequest([FromQuery]Value value,[FromBody]customType customType)
        {
            return Ok();
        }
        public class customType
        {
            public int Id { get; set; }
            public string country { get; set; }
        }

        //id degerini ?id seklinde urlden modelide bodyden almak icin
        [HttpPost("deneme2")]
        public ActionResult idUrldenIcerikBodyden([FromQuery]int id, [FromBody]Value value)
        {
            return Ok();
        }

    }
}
