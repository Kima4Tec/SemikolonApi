using Xunit;
using Moq;
using AutoMapper;
using Application.Services;
using Application.Dtos;
using Domain.Entities;
using Application.Interfaces.IBook;
using FluentAssertions;
using Infrastructure.Repositories;
using static Infrastructure.Repositories.IRepository;

namespace TestBogforlag
{
    public class BookServiceTests
    {
        [Fact]
        public async Task CreateBookAsync_ValidDto_AddsBookAndReturnsBook()
        {
            // Arrange  
            var mockRepo = new Mock<IRepository<Book>>();
            var mockMapper = new Mock<IMapper>();
            var mockBookRepo = new Mock<IBookRepository>();

            var bookDto = new CreateBookDto
            {
                Title = "Testbog",
                PublishDate = 2025,
                Price = 199
            };

            var mappedBook = new Book
            {
                Title = "Testbog",
                PublishDate = new DateOnly(2025, 1, 1), // Fixed: Use DateOnly constructor  
                Price = 199
            };

            // Setup mapper  
            mockMapper.Setup(m => m.Map<Book>(bookDto)).Returns(mappedBook);

            // Setup repository AddAsync  
            mockRepo.Setup(r => r.AddAsync(mappedBook)).Returns((Task<Book>)Task.CompletedTask);

            var service = new BookService(mockRepo.Object, mockMapper.Object, mockBookRepo.Object);

            // Act  
            var result = await service.CreateBookAsync(bookDto);

            // Assert  
            result.Should().BeEquivalentTo(mappedBook);
            mockRepo.Verify(r => r.AddAsync(It.Is<Book>(b =>
                b.Title == "Testbog" &&
                b.PublishDate == new DateOnly(2025, 1, 1) && // Fixed: Use DateOnly constructor  
                b.Price == 199)), Times.Once);
        }
    }
}
