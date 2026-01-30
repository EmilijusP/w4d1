using AnagramSolver.BusinessLogic.Services;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using FluentAssertions;
using Moq;

namespace AnagramSolver.BusinessLogic.Tests;

public class AnagramDictionaryServiceTests
{
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

        var mockWordProcessor = new Mock<IWordProcessor>();

        mockWordProcessor.Setup(p => p.SortString(fakeWord1)).Returns(sortedFakeWord);
        mockWordProcessor.Setup(p => p.SortString(fakeWord2)).Returns(sortedFakeWord);

        mockWordProcessor.Setup(p => p.CreateCharCount(sortedFakeWord)).Returns(dummyCharCount);

        var expectedResult = new List<Anagram>
        {
            new Anagram
            {
                Key = sortedFakeWord,
                KeyCharCount = dummyCharCount,
                Words = new List<string> { fakeWord1, fakeWord2 }
            }
        };

        var anagramDictionaryService = new AnagramDictionaryService(mockWordProcessor.Object);

        //act
        var result = anagramDictionaryService.CreateAnagrams(dummyWordModels);

        //assert
        result.Should().BeEquivalentTo(expectedResult);
    }
}
