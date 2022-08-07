using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models;

namespace NZWalks.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private NZWalksDbContext nZWalksDbContext;

        public RegionRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<Region> AddAsync(Region region)
        {
            region.Id = new Guid();
            await nZWalksDbContext.Regions.AddAsync(region);
            await nZWalksDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeletAsync(Guid id)
        {
            var region = await nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if(region == null)
            {
                return null;
            }
            nZWalksDbContext.Regions.Remove(region);
            await nZWalksDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await nZWalksDbContext.Regions.ToListAsync();
        }

        public async Task<Region> GetAsync(Guid id)
        {
            return await nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region> UpdateRegionAsync(Guid id, Region region)
        {
            var regionFound = await nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if(regionFound == null)
            {
                return null;
            }
            regionFound.Area = region.Area;
            regionFound.Population = region.Population;
            regionFound.Lat = region.Lat;
            regionFound.Long = region.Long;
            regionFound.Name = region.Name;

            await nZWalksDbContext.SaveChangesAsync();

            return regionFound;
        }
    }
}
