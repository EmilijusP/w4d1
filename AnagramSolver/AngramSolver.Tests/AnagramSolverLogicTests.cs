using AnagramSolver.BusinessLogic.Data;
using AnagramSolver.BusinessLogic.Services;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using Moq;
using FluentAssertions;

namespace AngramSolver.Tests
{
    public class AnagramSolverLogicTests
    {
        [Theory]
        [InlineData ("labas", "labas", "aabls", "balas")]
        [InlineData ("visma praktika", "vismapraktika", "aaaiikkmprstv", "praktikavimas")]
        public void GetAnagrams_ValidInput_ReturnsOneWordAnagram(string inputWord, string cleanInput, string keyCombination, string expectedAnagram)
        {
            //arrange
            var wordSet = new HashSet<WordModel>();
            wordSet.Add(new WordModel { Word = expectedAnagram });

            var mockWordProcessor = new Mock<IWordProcessor>();
            var mockAnagramDictionaryService = new Mock<IAnagramDictionaryService>();
            var mockAnagramAlgorithm = new Mock<IAnagramAlgorithm>();
            var mockWordRepository = new Mock<IWordRepository>();

            mockWordProcessor.Setup(processor => processor.RemoveWhitespace(It.IsAny<string>())).Returns(cleanInput);

            mockWordProcessor.Setup(processor => processor.CreateCharCount(It.IsAny<string>())).Returns(new Dictionary<char, int>());

            mockWordRepository.Setup(repository => repository.GetWords()).Returns(wordSet);

            var allAnagrams = new List<Anagram> { new Anagram { Key = "a", KeyCharCount = new Dictionary<char, int> { ['a'] = 1}, Words = new List<string> { "a" } } };
            mockAnagramDictionaryService.Setup(dictionaryService => dictionaryService.CreateAnagrams(It.IsAny<HashSet<WordModel>>())).Returns(allAnagrams);

            mockAnagramAlgorithm.Setup(algortihm => algortihm.CanFitWithin(It.IsAny<Dictionary<char, int>>(), It.IsAny<Dictionary<char, int>>())).Returns(true);

            var keyCombinations = new List<List<string>> { new List<string> { keyCombination } };
            mockAnagramAlgorithm.Setup(algortihm => algortihm.FindKeyCombinations(It.IsAny<Dictionary<char, int>>(), 1, It.IsAny<List<Anagram>>())).Returns(keyCombinations);
            var anagramList = new List<string> { expectedAnagram };
            mockAnagramAlgorithm.Setup(algorithm => algorithm.CreateCombinations(It.IsAny<List<List<string>>>(), It.IsAny<List<Anagram>>())).Returns(anagramList);

            var anagramSolver = new AnagramSolverLogic(mockWordProcessor.Object, mockAnagramDictionaryService.Object, mockAnagramAlgorithm.Object, mockWordRepository.Object, 1);

            //act
            var result = anagramSolver.GetAnagrams(inputWord);

            //assert
            result.Should().Contain(expectedAnagram);
        }
    }
}