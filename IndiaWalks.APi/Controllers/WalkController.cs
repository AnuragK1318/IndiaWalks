using AutoMapper;
using IndiaWalks.APi.Abstract;
using IndiaWalks.APi.Concrete;
using IndiaWalks.APi.CustomActionFilters;
using IndiaWalks.APi.Domain;
using IndiaWalks.APi.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IndiaWalks.APi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalkController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWalksRepo _walksRepo;
        public WalkController(IMapper mapper, IWalksRepo walksRepo)
        {
            _mapper = mapper;
            _walksRepo = walksRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalks()
        {
            //call repository method and get the walks domain
            var walksDomain = await _walksRepo.getAllWalksAsync();
            //convert the walks domain to dto and return to client
            return Ok(_mapper.Map<List<WalksDto>>(walksDomain));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetWalkById([FromRoute] int id)
        {
            //get walks domain from repo
            var walkDomain = await _walksRepo.getWalksByIdAsync(id);
            if (walkDomain == null)
            {
                return NotFound($"Walk with id : {id} not found");
            }
            //map domain to dto
            return Ok(_mapper.Map<WalksDto>(walkDomain));
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateWalk([FromBody] addWalkRequestDto walksReqdto)
        {
            //Map DTo to domain model
            var walksDomain = _mapper.Map<Walks>(walksReqdto);

            //Pass that to repository
            await _walksRepo.createAsync(walksDomain);

            //map Domain Model to DTo
            return Ok(_mapper.Map<WalksDto>(walksDomain));
        }

        [HttpPut]
        [Route("{id:int}")]
        [ValidateModel]
        public async Task<IActionResult> updateWalk([FromRoute] int id, UpdateWalkRequstDto updateWalkDto)
        {
            //map DTO to domain Model
            var existingWalkDomain = _mapper.Map<Walks>(updateWalkDto);

            var updatedWalk = await _walksRepo.updateWalkAync(id, existingWalkDomain);
            //Check if null
            if (updatedWalk == null)
            {
                return BadRequest("Something went wrong");
            }

            //Map the returned domain to DTO
            var walkDTO = _mapper.Map<WalksDto>(updatedWalk);
            //return the DTO
            return Ok(walkDTO);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> deleteWalk([FromRoute] int id)
        {
            var deletedDomainWalk = await _walksRepo.deleteWalkAsync(id);
            if (deletedDomainWalk == null)
            {
                return NotFound($"Walk with id {id} not found ");
            }

            return Ok(_mapper.Map<WalksDto>(deletedDomainWalk));
        }
    }
}
