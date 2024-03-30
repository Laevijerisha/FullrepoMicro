using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using E_wasteManagementWebapi.Data;
using E_wasteManagementWebapi.DTO;
using E_wasteManagementWebapi.Model;
using System.Reflection;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Org.BouncyCastle.Asn1.Ocsp;
using NuGet.Configuration;
namespace E_wasteManagementWebapi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly E_WasteDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _configuration;


        public ItemController(E_WasteDbContext context, IWebHostEnvironment environment, IConfiguration configuration)
        {
            _context = context;
            _environment = environment;
            _configuration = configuration;

        }
        // return await _context.waste_items.Include(x => x.Users).ToListAsync();
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetItems()
        {
            var wasteItems = _context.waste_items.ToList();

            var waste = new List<object>();

            foreach (var item in wasteItems)
            {
                var ItemData = new
                {
                    ItemId = item.ItemId,
                    ItemName = item.ItemName,
                    ItemImage = item.ItemImage,
                    UniqueFileName = item.UniqueFileName,
                    ItemQuantity = item.ItemQuantity,
                    ItemCondition = item.ItemCondition,
                    ItemLocation = item.ItemLocation,
                    RequestStatus = item.RequestStatus,
                    ApprovedItemStatus = item.ApprovedItemStatus,
                    UserId = item.UserId,

                    imageUrl = String.Format("{0}://{1}{2}/wwwroot/images/{3}", Request.Scheme, Request.Host.Host, Request.PathBase, item.UniqueFileName)

                };

                waste.Add(ItemData);
            }

            return waste;
        }

        [HttpGet("{id}/Image")]
        public IActionResult GetImage(int id)
        {
            var item = _context.waste_items.Find(id);
            if (item == null)
            {
                return NotFound(); // User not found
            }

            // Construct the full path to the image file
            var imagePath = Path.Combine(_environment.WebRootPath, "images", item.UniqueFileName);

            // Check if the image file exists
            if (!System.IO.File.Exists(imagePath))
            {
                return NotFound(); // Image file not found
            }

            // Serve the image file
            return PhysicalFile(imagePath, "image/jpeg");
        }
        [HttpGet("{userId}/history")]
        public async Task<ActionResult<IEnumerable<E_WasteItem>>> GetItemHistory(int userId)
        {
            var userItems = await _context.waste_items
            .Where(item => item.Users.UserId == userId)
            .ToListAsync();

            if (userItems == null)
            {
                return NotFound();
            }

            return userItems;
        }

        [HttpGet("approved")]
        public async Task<ActionResult<IEnumerable<E_WasteItem>>> GetApprovedItem()
        {
            var approvedItems = await _context.waste_items
            .Where(item => item.RequestStatus == "Approve")
            .ToListAsync();

            if (approvedItems == null)
            {
                return NotFound();
            }

            return approvedItems;
        }

        [HttpPost]
        public async Task<IActionResult> PostItem(ItemDTO items)
        {
            var uniqueFileName = $"{Guid.NewGuid()}_{items.ItemImage.FileName}";
            // Set the user ID of the item to the authenticated user's ID
            User user = _context.users.Find(items.UserId);
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "images");
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await items.ItemImage.CopyToAsync(stream);
            }
            items.UniqueFileName = uniqueFileName;
            // Set the status of the item to 'Pending'
            items.RequestStatus = "Pending";
            items.ApprovedItemStatus = "None";

            if (user == null)
            {
                return NotFound();
            }


            var item = new E_WasteItem
            {
                ItemName = items.ItemName,
                ItemImage = items.ItemImage,
                UniqueFileName = items.UniqueFileName,
                ItemQuantity = items.ItemQuantity,
                ItemCondition = items.ItemCondition,
                ItemLocation = items.ItemLocation,
                RequestStatus = items.RequestStatus,
                ApprovedItemStatus = items.ApprovedItemStatus,
                Users = user
            };

            _context.waste_items.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetItems), new { id = items.ItemId }, item);
        }


        [HttpPost]
        public async Task<IActionResult> Put(ApprovedItemDTO DTO)
        {
            var status = await _context.waste_items.FindAsync(DTO.Id);



            // Check if the RequestStatus is "approved"

            // Update the ApprovedItemStatus column
            status.ApprovedItemStatus = DTO.ApprovedItemStatus;


            _context.waste_items.Update(status);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletewasteItems(int id)
        {
            var servicedetails = _context.waste_items.Find(id);
            if (servicedetails == null)
            {
                return NotFound(); // PetAccessory not found
            }
            _context.waste_items.Remove(servicedetails);
            await _context.SaveChangesAsync();
            return NoContent(); // Successfully deleted
        }

        [HttpPost]
        public ActionResult FindUserHistory(string email)
        {
            var user = _context.users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var orders = _context.waste_items
                .Include(x => x.Users)
                .Where(s => s.UserId == user.UserId)
                .ToList();

            return Ok(new { data = orders });
        }



        [HttpPost]
        public async Task<IActionResult> updateStatus(UpdateStatusDTO DTO)
        {

            var status = await _context.waste_items.FindAsync(DTO.Id);

            status.RequestStatus = DTO.status;

            _context.waste_items.Update(status);
            await _context.SaveChangesAsync();
            return Ok();
        }


    }
}
