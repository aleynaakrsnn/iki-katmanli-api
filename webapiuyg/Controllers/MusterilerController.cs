using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using webapiuyg.Models;

namespace webapiuyg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusterilerController : ControllerBase
    {
        private IMusterilerRepository musteriRepository;

        private IWebHostEnvironment webHostEnvironment;

        public MusterilerController(IMusterilerRepository repo, IWebHostEnvironment environment)
        {
            musteriRepository = repo;
            webHostEnvironment = environment;
        }

       
        [HttpGet]
        public IEnumerable<Musteriler> GetMusteriler()
        {
            return musteriRepository.GetAllMusteriler().ToList();
        }

        [HttpGet("{id}")]
        public Musteriler GetMusteriById(int id)
        {
            return musteriRepository.GetMusteriById(id);
        }



        [HttpPost]
        public Musteriler Create([FromBody] Musteriler musteriler)
        {
            return musteriRepository.AddMusteri(musteriler);
        }



        [HttpPut]
        public Musteriler Update([FromForm] Musteriler musteriler)
        {
            return musteriRepository.UpdateMusteri(musteriler);
        }


        [HttpDelete("{id}")]
        public void Delete(int? id) => musteriRepository.DeleteMusteri(id);

    }

}
