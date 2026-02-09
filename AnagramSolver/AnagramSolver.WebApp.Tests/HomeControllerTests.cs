//using AnagramSolver.Contracts.Interfaces;
//using AnagramSolver.WebApp;
//using AnagramSolver.WebApp.Controllers;
//using AnagramSolver.WebApp.Models;
//using FluentAssertions;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using Moq;

//namespace AnagramSolver.WebApp.Tests
//{
//    public class HomeControllerTests
//    {
//        private readonly Mock<ILogger<HomeController>> _mockLogger;
//        private readonly Mock<IAnagramSolver> _mockAnagramSolver;
//        private readonly HomeController _homeController;

//        public HomeControllerTests()
//        {
//            _mockLogger = new Mock<ILogger<HomeController>>();
//            _mockAnagramSolver = new Mock<IAnagramSolver>();
//            _homeController = new HomeController(_mockLogger.Object, _mockAnagramSolver.Object);
//        }

//        [Theory]
//        [InlineData("test", "sett")]
//        [InlineData("testing", null)]
//        public async Task Index_VariousWords_ReturnsPossibleAnagrams(string id, string expectedAnagram)
//        {
//            //arrange
//            var expectedResult = expectedAnagram == null 
//                ? new List<string>() 
//                : new List<string> { expectedAnagram };

//            _mockAnagramSolver.Setup(s => s.GetAnagramsAsync(id, It.IsAny<CancellationToken>())).ReturnsAsync(expectedResult);

//            //act
//            var result = await _homeController.Index(id, CancellationToken.None);

//            //assert
//            var viewResult = Assert.IsType<ViewResult>(result);

//            var model = Assert.IsType<AnagramViewModel>(viewResult.Model);

//            model.AnagramLines.Should().BeEquivalentTo(expectedResult);
            
//        }

//        [Fact]
//        public async Task Index_EmptyId_DoesNotCallService()
//        {
//            //arrange

//            //act
//            var result = await _homeController.Index("", CancellationToken.None);

//            //assert
//            _mockAnagramSolver.Verify(s => s.GetAnagramsAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);

//            var viewResult = Assert.IsType<ViewResult>(result);

//            var model = Assert.IsType<AnagramViewModel>(viewResult.Model);

//            model.AnagramLines.Should().BeNullOrEmpty();
//        }
//    }
//}