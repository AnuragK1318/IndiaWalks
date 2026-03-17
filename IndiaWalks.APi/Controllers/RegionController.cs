using AutoMapper;
using IndiaWalks.APi.Context;
using IndiaWalks.APi.Domain;
using IndiaWalks.APi.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System.Xml;

namespace IndiaWalks.APi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly IndiaWalksDbContext context;
        private readonly IMapper mapper;

        public RegionController(IndiaWalksDbContext context,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
            
        }

        [HttpGet]
        [Route("GetAllRegions")]
        public async Task<IActionResult> GetAllRegions()
        {
            //Get Region Domain data
            var regionDomain = await context.Regions.ToListAsync();

            //map the data
            if (regionDomain.Any())
            {
                var regionDto = mapper.Map<List<RegionDto>>(regionDomain);

                //return dto to client
                return Ok(regionDto);
            }
                return BadRequest("No regions found");
        }

        [HttpGet]
        [Route("GetRegionById/{id}")]
        public async Task<IActionResult> GetRegionById([FromRoute]int id)
        {
            //get data from domain model
            var regionDomain=await context.Regions.FirstOrDefaultAsync(x => x.Id == id);

            //map the data to dto
            if (regionDomain==null)
            {
                return NotFound();
            }
            else
            {
                var regionDto = mapper.Map<RegionDto>(regionDomain);
                return Ok(regionDto);
            }
        }
        [HttpPost]
        [Route("AddRegion")]
        public async Task<IActionResult> AddRegion([FromBody] AddRegionRequestDto requestDto)
        {
            //map Dto to domain model
            var regionDomain = mapper.Map<Region>(requestDto);

            //Save changes to DB
            await context.Regions.AddAsync(regionDomain);
            await context.SaveChangesAsync();

            //Convert domain to dto
            var regionDto = mapper.Map<RegionDto>(regionDomain);

            return CreatedAtAction(nameof(GetRegionById), new { id=regionDto},regionDto);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateRegion([FromRoute] int id, [FromBody] UpdateRegionRequestDto updateregiondto)
        {
            //check if region exists
            var regionDomain = await context.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (regionDomain == null)
            {
                return NotFound("Region not found");
            }
            else
            {
                //map DTo to domain
                mapper.Map(updateregiondto, regionDomain);

                //save changes
                await context.SaveChangesAsync();

                //Convert domain to Dto
                var regiondto=mapper.Map<RegionDto>(regionDomain);

                return Ok(regiondto);
            }
        }
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteRegion([FromRoute] int id)
        {
            var regionDomain=await context.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (regionDomain == null)
            {
                return NotFound();
            }
            else
            {
                context.Regions.Remove(regionDomain);
                await context.SaveChangesAsync();

                //return deleted object in dto form
                var regiondto=mapper.Map<RegionDto>(regionDomain);

                return Ok(regiondto);
            }
        }
    }
}
