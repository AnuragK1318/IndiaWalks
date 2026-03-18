using AutoMapper;
using IndiaWalks.APi.Abstract;
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
        private readonly IRegion _region;

        public RegionController(IndiaWalksDbContext context,IMapper mapper,IRegion region)
        {
            this.context = context;
            this.mapper = mapper;
            _region = region;
            
        }

        [HttpGet]
        [Route("GetAllRegions")]
        public async Task<IActionResult> GetAllRegions()
        {
            var region = await _region.GetAllRegionsAsync();
            //If not found empty list will be retruned
            return Ok(region);
        }

        [HttpGet]
        [Route("GetRegionById/{id}")]
        public async Task<IActionResult> GetRegionById([FromRoute]int id)
        {
            //get data from domain model
            var region = await _region.GetRegionbyIdAsync(id);

            if (region == null)
            {
                return NotFound();
            }
            return Ok(region);
        }
        [HttpPost]
        [Route("AddRegion")]
        public async Task<IActionResult> AddRegion([FromBody] AddRegionRequestDto requestDto)
        {
            var addedRegion = await _region.AddRegionAsync(requestDto);

            return CreatedAtAction(nameof(GetRegionById), new { id = addedRegion.Id }, addedRegion);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateRegion([FromRoute] int id, [FromBody] UpdateRegionRequestDto updateregiondto)
        {
            var updatedRegion = await _region.updateRegionAsync(id,updateregiondto);
            if(updatedRegion == null)
            {
                return NotFound("Region with id : {id} not found ");
            }
            return Ok(updatedRegion);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteRegion([FromRoute] int id)
        {
            var region = await _region.DeleteRegionAsync(id);

            if (region == null)
            {
                return NotFound();
            }
            return Ok(region);
        }
    }
}
