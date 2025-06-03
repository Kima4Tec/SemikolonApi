using Application.Dtos;
using AutoMapper;
using Domain.Entities;
using FluentAssertions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;
using static Infrastructure.Repositories.IRepository;

public class TestBogforlagInMemory
{
    private IMapper _mapper;

    public TestBogforlagInMemory()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<CreateArtistDto, Artist>();
        });
        _mapper = config.CreateMapper();
    }

    [Fact]
    public async Task CreateArtistAsync_WithValidDto_SavesArtistToDatabase()
    {
        //---------------------------------------------------------------
        //                          Arrange  
        //---------------------------------------------------------------
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "ArtistDb")
            .Options;

        var dto = new CreateArtistDto
        {
            FirstName = "Ida",
            LastName = "Lauritzen"
        };

        //---------------------------------------------------------------
        //                          Act  
        //---------------------------------------------------------------
        using (var context = new ApplicationDbContext(options))
        {
            var repository = new Repository<Artist>(context);
            var service = new ArtistService(repository, _mapper);

            var createdArtist = await service.CreateArtistAsync(dto);
        }

        //---------------------------------------------------------------
        //                         Assert  
        //---------------------------------------------------------------
        using (var context = new ApplicationDbContext(options))
        {
            var artist = await context.Artists.FirstOrDefaultAsync();
            artist.Should().NotBeNull();
            artist!.FirstName.Should().Be("Ida");
            artist.LastName.Should().Be("Lauritzen");
        }
    }
}
