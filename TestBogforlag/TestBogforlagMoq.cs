using Application.Dtos;
using Application.Interfaces.IBook;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using FluentAssertions;
using FluentValidation;
using Moq;
using System.ComponentModel.DataAnnotations;
using Xunit;
using static Infrastructure.Repositories.IRepository;
using ValidationException = FluentValidation.ValidationException;

namespace TestBogforlag
{
    public class TestBogforlagMoq
    {
        /*------------------------------------------------------------------------*
         *                                                                        *
         *                              TEST 1                                    *
         *                                                                        *
         * -----------------------------------------------------------------------*/
        /// <summary>
        /// Tests the <c>CreateBookAsync</c> method to ensure that a valid <c>CreateBookDto</c> is correctly mapped to a
        /// <c>Book</c>, added to the repository, and returned.
        /// </summary>
        /// <remarks>This test verifies the following behaviors: <list type="bullet"> <item><description>The
        /// <c>CreateBookDto</c> is mapped to a <c>Book</c> using the provided mapper.</description></item>
        /// <item><description>The mapped <c>Book</c> is added to the repository using <c>AddAsync</c>.</description></item>
        /// <item><description>The returned result matches the values of the mapped <c>Book</c>.</description></item>
        /// <item><description>The <c>AddAsync</c> method is called exactly once with the expected <c>Book</c>
        /// values.</description></item> </list></remarks>
        /// <returns></returns>
        [Fact]
        public async Task CreateBookAsync_ValidDto_AddsBookAndReturnsBook()
        {
            //---------------------------------------------------------------
            //                          Arrange  
            //---------------------------------------------------------------

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
                PublishDate = new DateOnly(2025, 1, 1),
                Price = 199
            };

            // Setup mapper  
            mockMapper.Setup(m => m.Map<Book>(bookDto)).Returns(mappedBook);

            // Setup repository AddAsync  
            mockRepo.Setup(r => r.AddAsync(mappedBook)).ReturnsAsync(mappedBook);


            var service = new BookService(mockRepo.Object, mockMapper.Object, mockBookRepo.Object);

            //---------------------------------------------------------------
            //                          Act  
            //---------------------------------------------------------------

            var result = await service.CreateBookAsync(bookDto);

            //---------------------------------------------------------------
            //                          Assert  
            //---------------------------------------------------------------

            result.Should().BeEquivalentTo(mappedBook);

            mockRepo.Verify(r => r.AddAsync(It.Is<Book>(b =>
                b.Title == "Testbog" &&
                b.PublishDate == new DateOnly(2025, 1, 1) &&
                b.Price == 199)), Times.Once);
        }


        /*------------------------------------------------------------------------*
         *                                                                        *
         *                              TEST 2                                    *
         *                                                                        *
         * -----------------------------------------------------------------------*/
        /// <summary>
        /// Tests the <c>CreateAuthorAsync</c> method to ensure that it successfully creates an author when provided
        /// with a valid <see cref="CreateAuthorDto"/>.
        /// </summary>
        /// <remarks>This test verifies that the service correctly validates the input DTO, maps it to an
        /// <see cref="Author"/> entity, and adds the entity to the repository. It also ensures that the returned result
        /// matches the created author.</remarks>
        /// <returns></returns>
        [Fact]
        public async Task CreateAuthorAsync_ValidDto_ReturnsCreatedAuthor()
        {
            //---------------------------------------------------------------
            //                          Arrange  
            //---------------------------------------------------------------
            var mockRepo = new Mock<IRepository<Author>>();
            var mockMapper = new Mock<IMapper>();
            var mockValidator = new Mock<IValidator<CreateAuthorDto>>();

            var createDto = new CreateAuthorDto
            {
                FirstName = "Jesper",
                LastName = "Kylling"
            };

            var mappedAuthor = new Author
            {
                Id = 1,
                FirstName = "Jesper",
                LastName = "Kylling"
            };

            mockValidator.Setup(v => v.ValidateAsync(createDto, default))
                         .ReturnsAsync(new FluentValidation.Results.ValidationResult());

            mockMapper.Setup(m => m.Map<Author>(createDto))
                      .Returns(mappedAuthor);

            mockRepo.Setup(r => r.AddAsync(mappedAuthor))
                    .ReturnsAsync(mappedAuthor);

            var service = new AuthorService(
                mockRepo.Object,
                mockValidator.Object,
                mockMapper.Object
            );

            //---------------------------------------------------------------
            //                          Act  
            //---------------------------------------------------------------
            var result = await service.CreateAuthorAsync(createDto);

            //---------------------------------------------------------------
            //                          Assert  
            //---------------------------------------------------------------
            result.Should().BeEquivalentTo(mappedAuthor);
            mockValidator.Verify(v => v.ValidateAsync(createDto, default), Times.Once);
            mockMapper.Verify(m => m.Map<Author>(createDto), Times.Once);
            mockRepo.Verify(r => r.AddAsync(mappedAuthor), Times.Once);
        }

        /*------------------------------------------------------------------------*
         *                                                                        *
         *                              TEST 3                                    *
         *                                                                        *
         * -----------------------------------------------------------------------*/
        /// <summary>
        /// Tests that the <see cref="AuthorService.CreateAuthorAsync(CreateAuthorDto)"/> method throws a  <see
        /// cref="ValidationException"/> when provided with an invalid <see cref="CreateAuthorDto"/>.
        /// </summary>
        /// <remarks>This test verifies that the service correctly validates the input DTO and does not
        /// proceed with  mapping or repository operations when validation fails. It ensures that invalid data is
        /// rejected  and appropriate exceptions are thrown.</remarks>
        /// <returns></returns>
        [Fact]
        public async Task CreateAuthorAsync_InvalidDto_ThrowsValidationException()
        {
            //---------------------------------------------------------------
            //                          Arrange  
            //---------------------------------------------------------------
            var mockRepo = new Mock<IRepository<Author>>();
            var mockMapper = new Mock<IMapper>();
            var mockValidator = new Mock<IValidator<CreateAuthorDto>>();

            var createDto = new CreateAuthorDto
            {
                FirstName = "", // Invalid (empty)
                LastName = "Kylling"
            };

            var validationFailures = new List<FluentValidation.Results.ValidationFailure>
            {
                new FluentValidation.Results.ValidationFailure("FirstName", "First name is required")
            };

            var validationResult = new FluentValidation.Results.ValidationResult(validationFailures);

            mockValidator.Setup(v => v.ValidateAsync(createDto, default))
                         .ReturnsAsync(validationResult);

            var service = new AuthorService(
                mockRepo.Object,
                mockValidator.Object,
                mockMapper.Object
            );

            //---------------------------------------------------------------
            //                         Act & Assert 
            //---------------------------------------------------------------
            await Assert.ThrowsAsync<ValidationException>(() => service.CreateAuthorAsync(createDto));

            mockValidator.Verify(v => v.ValidateAsync(createDto, default), Times.Once);
            mockMapper.Verify(m => m.Map<Author>(It.IsAny<CreateAuthorDto>()), Times.Never);
            mockRepo.Verify(r => r.AddAsync(It.IsAny<Author>()), Times.Never);
        }

        /*------------------------------------------------------------------------*
         *                                                                        *
         *                              TEST 4                                    *
         *                                                                        *
         * -----------------------------------------------------------------------*/
        [Fact]
        public async Task CreateArtistAsync_ValidDto_CreatesArtist()
        {
            //---------------------------------------------------------------
            //                          Arrange  
            //---------------------------------------------------------------
            var dto = new CreateArtistDto
            {
                FirstName = "Julie",
                LastName = "Andersen"
            };

            var expectedArtist = new Artist
            {
                ArtistId = 0, // not set by the service, will be generated by the database
                FirstName = "Julie",
                LastName = "Andersen"
            };

            var mockRepo = new Mock<IRepository<Artist>>();
            mockRepo.Setup(r => r.AddAsync(It.IsAny<Artist>())).ReturnsAsync((Artist a) => a);

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateArtistDto, Artist>();
            });
            var mapper = mapperConfig.CreateMapper();

            var service = new ArtistService(mockRepo.Object, mapper);

            //---------------------------------------------------------------
            //                          Act  
            //---------------------------------------------------------------
            var result = await service.CreateArtistAsync(dto);

            //---------------------------------------------------------------
            //                          Assert  
            //---------------------------------------------------------------
            result.FirstName.Should().Be("Julie");
            result.LastName.Should().Be("Andersen");

            mockRepo.Verify(r => r.AddAsync(It.Is<Artist>(a =>
                a.FirstName == "Julie" && a.LastName == "Andersen")), Times.Once);
        }
    


    /*------------------------------------------------------------------------*
     *                                                                        *
     *                              TEST 5                                    *
     *    Unit test for validation of CreateArtistDto with dataannotations    *
     * -----------------------------------------------------------------------*/

        [Fact]
        public void CreateArtistDto_WhenMissingFirstName_ShouldFailValidation()
        {
            // Arrange
            var dto = new CreateArtistDto
            {
                FirstName = "",
                LastName = "Hansen"
            };

            // Act
            var context = new ValidationContext(dto, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(dto, context, results, true);

            // Assert
            isValid.Should().BeFalse();
            results.Should().ContainSingle(r => r.ErrorMessage.Contains("Fornavn skal udfyldes"));
        }

        [Fact]
        public void CreateArtistDto_WhenValid_ShouldPassValidation()
        {
            // Arrange
            var dto = new CreateArtistDto
            {
                FirstName = "Kurt",
                LastName = "Hansen"
            };

            // Act
            var context = new ValidationContext(dto, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(dto, context, results, true);

            // Assert
            isValid.Should().BeTrue();
            results.Should().BeEmpty();
        }
    }


}

