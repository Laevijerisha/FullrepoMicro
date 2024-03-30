using Microsoft.AspNetCore.Mvc;
using E_wasteManagementWebapi.Data;
using E_wasteManagementWebapi.Model;
using Microsoft.EntityFrameworkCore;

namespace E_wasteManagementWebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase

    {
       

        private readonly E_WasteDbContext _adcontext;
        public AdminController(E_WasteDbContext adcontext)
        {
            _adcontext = adcontext;
            
        }
        [HttpGet]
        public async Task<ActionResult<List<Center>>> GetCenter()
        {
            return Ok(await _adcontext.centers.ToListAsync());
        }



        [HttpGet("{id}")]
        public ActionResult<Center> GetCenter(int id)
        {
            var center = _adcontext.centers.Find(id);
            if (center == null)
            {
                return NotFound();
            }
            return center;
        }


        [HttpPost("Add Centers")]

        public IActionResult AddCenters([FromBody] Center addcenter)
        {
            _adcontext.centers.Add(addcenter);
            _adcontext.SaveChanges();
            return Ok();/*CreatedAtAction(nameof(addcenter), new { id = addcenter.CenterId }, addcenter);*/
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int CenterId, Center center)
        {
            if (CenterId != center.CenterId)
                return BadRequest();
            _adcontext.Entry(center).State = EntityState.Modified;
            await _adcontext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{CenterId}")]

        public async Task<IActionResult> Delete(int CenterId)
        {
            var center = await _adcontext.centers.FindAsync(CenterId);
            if (center == null)
            {
                return NotFound("Incorrect Center Id");
            }

            _adcontext.centers.Remove(center);
            await _adcontext.SaveChangesAsync();

            return Ok();
        }





        
    }
}


    

