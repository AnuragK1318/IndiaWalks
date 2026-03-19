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
        public async Task<IActionResult> GetAllRegions()
        {
            //call repo method
            var regionDomainModel = await _region.GetAllRegionsAsync();
            //Map Domain to DTO
            var regionDto = mapper.Map<List<RegionDto>>(regionDomainModel);
            //return List
            return Ok(regionDto);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetRegionById([FromRoute] int id)
        {
            //get data from domain model
            var regionDomain = await _region.GetRegionbyIdAsync(id);

            if (regionDomain == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<RegionDto>(regionDomain));
        }

        [HttpPost]
        public async Task<IActionResult> AddRegion([FromBody] AddRegionRequestDto requestDto)
        {
            //convert DTO to domain
            var regionDomain = mapper.Map<Region>(requestDto);

            //Repo adds it and the Db generates the ID
            var addedRegion = await _region.AddRegionAsync(regionDomain);

            //Again Map domain to DTO
            var regiondto = mapper.Map<RegionDto>(addedRegion);

            return CreatedAtAction(nameof(GetRegionById), new { id = regiondto.Id }, regiondto);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateRegion([FromRoute] int id, [FromBody] UpdateRegionRequestDto updateregiondto)
        {
            //Map DTO to Domain
            var regionDomain = mapper.Map<Region>(updateregiondto);

            var updatedRegion = await _region.updateRegionAsync(id,regionDomain);
            if(updatedRegion == null)
            {
                return NotFound("Region with id : {id} not found ");
            }
            return Ok(mapper.Map<RegionDto>(updatedRegion));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteRegion([FromRoute] int id)
        {
            var region = await _region.DeleteRegionAsync(id);

            if (region == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<RegionDto>(region));
        }
    }
}
