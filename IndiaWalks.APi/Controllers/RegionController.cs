using AutoMapper;
using IndiaWalks.APi.Abstract;
using IndiaWalks.APi.Context;
using IndiaWalks.APi.CustomActionFilters;
using IndiaWalks.APi.Domain;
using IndiaWalks.APi.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System.Text.Json;
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
        private readonly ILogger<RegionController> _logger;

        public RegionController(IndiaWalksDbContext context,
            IMapper mapper,
            IRegion region,
            ILogger<RegionController> logger)
        {
            this.context = context;
            this.mapper = mapper;
            _region = region;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles ="Reader")]
        public async Task<IActionResult> GetAllRegions([FromQuery] RegionListRequestDto filter)
        {
            _logger.LogInformation("GetAll Regions action method invoked");
            //call repo method
            var regionDomainModel = await _region.GetAllRegionsAsync(filter);
            //Map Domain to DTO
            var regionDto = mapper.Map<List<RegionDto>>(regionDomainModel);

            _logger.LogInformation($"Finished getAll regions req " +
                $"with data{JsonSerializer.Serialize(regionDto)}");
            //return List
            return Ok(regionDto);
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles ="Reader")]
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
        [ValidateModel]
        [Authorize(Roles ="Writer")]
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
        [ValidateModel]
        [Authorize(Roles ="Writer")]
        public async Task<IActionResult> UpdateRegion([FromRoute] int id, [FromBody] UpdateRegionRequestDto updateregiondto)
        {
                //Map DTO to Domain
                var regionDomain = mapper.Map<Region>(updateregiondto);

                var updatedRegion = await _region.updateRegionAsync(id, regionDomain);
                if (updatedRegion == null)
                {
                    return NotFound("Region with id : {id} not found ");
                }
                return Ok(mapper.Map<RegionDto>(updatedRegion));
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles ="Writer")]
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
