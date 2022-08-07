using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionsController : Controller
    {
        private IRegionRepository regionRepository;
        private IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            var regions = await regionRepository.GetAllAsync();
            var mappedregions = mapper.Map<List<DTO.Region>>(regions);

            return Ok(mappedregions);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegions")]
        public async Task<IActionResult> GetRegions(Guid id)
        {
            var region = await regionRepository.GetAsync(id);
            if(region == null)
            {
                return NotFound();
            }

            var mappedregions = mapper.Map<DTO.Region>(region);

            return Ok(mappedregions);
        }

        [HttpPost]
        public async Task<IActionResult> AddRegionsAsync(AddRegionRequest addRegionRequest)
        {
            var region = new Models.Region
            {
                Code = addRegionRequest.Code,
                Area = addRegionRequest.Area,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Name = addRegionRequest.Name,
                Population = addRegionRequest.Population
            };

            region = await regionRepository.AddAsync(region);

            var regionDTO = new DTO.Region
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };
            return CreatedAtAction(nameof(GetRegions), new {Id = regionDTO.Id}, regionDTO);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteRegion(Guid id)
        {
            var region = await regionRepository.DeletAsync(id);
            if (region == null)
            {
                return NotFound();
            }

            var regionDTO = new DTO.Region
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };

            return Ok(regionDTO);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateregionAsync([FromRoute] Guid id, [FromBody] UpdateRegionRequest addRegionRequest)
        {
            var region = new Models.Region
            {
                Code = addRegionRequest.Code,
                Area = addRegionRequest.Area,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Name = addRegionRequest.Name,
                Population = addRegionRequest.Population
            };

            region = await regionRepository.UpdateRegionAsync(id, region);
            if(region == null)
            {
                return NotFound();
            }


            var updatedRegionDTO = new DTO.Region
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };

            return Ok(updatedRegionDTO);

        }

    }
}
