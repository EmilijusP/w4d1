using AnagramSolver.BusinessLogic.Services;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using FluentAssertions;
using Moq;
using System.Security.AccessControl;

namespace AnagramSolver.BusinessLogic.Tests;

public class AnagramDictionaryServiceTests
{
    private readonly Mock<IWordProcessor> _mockWordProcessor;
    private readonly Mock<IWordRepository> _mockWordRepository;
    private readonly Mock<IInputValidation> _mockInputValidation;
    private readonly IAnagramDictionaryService _anagramDictionaryService;

    public AnagramDictionaryServiceTests()
    {
        _mockWordProcessor = new Mock<IWordProcessor>();
        _mockWordRepository = new Mock<IWordRepository>();
        _mockInputValidation = new Mock<IInputValidation>();
        _anagramDictionaryService = new AnagramDictionaryService(_mockWordProcessor.Object, _mockWordRepository.Object, _mockInputValidation.Object);
    }

    [Theory]
    [InlineData ("test", "sett", "estt")]
    public void CreateAnagrams_GivenTwoAnagrams_ReturnsThemUnderTheSame(string fakeWord1, string fakeWord2, string sortedFakeWord)
    {
        //arrange
        var dummyWordModels = new HashSet<WordModel>
        {
            new WordModel { Word = fakeWord1 },
            new WordModel { Word = fakeWord2 }
        };

        var dummyCharCount = new Dictionary<char, int>
        {
            ['e'] = 1,
            ['s'] = 1,
            ['t'] = 2
        };

        _mockWordProcessor.Setup(p => p.SortString(fakeWord1)).Returns(sortedFakeWord);
        _mockWordProcessor.Setup(p => p.SortString(fakeWord2)).Returns(sortedFakeWord);

        _mockWordProcessor.Setup(p => p.CreateCharCount(sortedFakeWord)).Returns(dummyCharCount);

        var expectedResult = new List<Anagram>
        {
            new Anagram
            {
                Key = sortedFakeWord,
                KeyCharCount = dummyCharCount,
                Words = new List<string> { fakeWord1, fakeWord2 }
            }
        };

        //act
        var result = _anagramDictionaryService.CreateAnagrams(dummyWordModels);

        //assert
        result.Should().BeEquivalentTo(expectedResult);
    }
}
