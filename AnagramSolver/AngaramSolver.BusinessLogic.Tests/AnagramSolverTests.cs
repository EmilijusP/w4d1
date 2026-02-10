using AnagramSolver.BusinessLogic.Data;
using AnagramSolver.BusinessLogic.Services;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using Moq;
using FluentAssertions;

namespace AnagramSolver.Tests
{
    public class AnagramSolverTests
    {
        private readonly Mock<IWordProcessor> _mockWordProcessor;
        private readonly Mock<IAnagramDictionaryService> _mockDictionaryService;
        private readonly Mock<IAnagramAlgorithm> _mockAnagramAlgorithm;
        private readonly Mock<IWordRepository> _mockWordRepository;
        private readonly Mock<IAppSettings> _mockAppSettings;
        private readonly AnagramSolverService _systemUnderTest;

        public AnagramSolverTests()
        {
            _mockWordProcessor = new Mock<IWordProcessor>();
            _mockDictionaryService = new Mock<IAnagramDictionaryService>();
            _mockAnagramAlgorithm = new Mock<IAnagramAlgorithm>();
            _mockWordRepository = new Mock<IWordRepository>();
            _mockAppSettings = new Mock<IAppSettings>();

            _systemUnderTest = new AnagramSolverService(
                _mockWordProcessor.Object,
                _mockDictionaryService.Object,
                _mockAnagramAlgorithm.Object,
                _mockWordRepository.Object,
                _mockAppSettings.Object
                );
        }

        [Theory]
        [InlineData("labas", "balas", 1, 4)]
        public async Task GetAnagramsAsync_ValidSingleWord_ReturnsExpectedAnagram(string inputWord, string expectedAnagram, int anagramCount, int minOutputWordsLength)
        {
            //arrange
            var charCount = new Dictionary<char, int> { { 'a', 2 }, { 'b', 1 }, { 'l', 1 }, { 's', 1 } };
            var anagrams = new List<Anagram> { new Anagram { Words = new List<string> { expectedAnagram }, KeyCharCount = charCount } };

            _mockAppSettings.Setup(s => s.AnagramCount).Returns(anagramCount);
            _mockAppSettings.Setup(s => s.MinOutputWordsLength).Returns(minOutputWordsLength);
            _mockWordProcessor.Setup(p => p.RemoveWhitespace(inputWord)).Returns(inputWord);
            _mockWordProcessor.Setup(p => p.CreateCharCount(inputWord)).Returns(charCount);
            _mockWordRepository.Setup(r => r.ReadAllLinesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(new List<WordModel>());
            _mockDictionaryService.Setup(d => d.CreateAnagrams(It.IsAny<HashSet<WordModel>>())).Returns(anagrams);
            _mockAnagramAlgorithm.Setup(a => a.IsValidOutputLength(It.IsAny<string>(), _mockAppSettings.Object.MinOutputWordsLength)).Returns(true);
            _mockAnagramAlgorithm.Setup(a => a.CanFitWithin(It.IsAny<Dictionary<char, int>>(), It.IsAny<Dictionary<char, int>>())).Returns(true);
            _mockAnagramAlgorithm.Setup(a => a.FindKeyCombinations(It.IsAny<Dictionary<char, int>>(), _mockAppSettings.Object.AnagramCount, It.IsAny<List<Anagram>>()))
                .Returns(new List<List<string>> { new List<string> { "key" } });
            _mockAnagramAlgorithm.Setup(a => a.CreateCombinations(It.IsAny<List<List<string>>>(), It.IsAny<List<Anagram>>()))
                .Returns(new List<string> { expectedAnagram });

            //act
            var result = await _systemUnderTest.GetAnagramsAsync(inputWord, CancellationToken.None);

            //assert
            result.Should().Contain(expectedAnagram);
        }
    }
}