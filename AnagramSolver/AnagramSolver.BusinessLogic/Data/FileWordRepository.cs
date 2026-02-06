using AnagramSolver.BusinessLogic.Services;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;

namespace AnagramSolver.BusinessLogic.Data
{
    public class FileWordRepository : IWordRepository
    {
        private readonly AppSettings _settings;

        public FileWordRepository(AppSettings settings)
        {
            _settings = settings;
        }

        public async Task<IEnumerable<WordModel>> ReadAllLinesAsync(CancellationToken ct)
        {
            var words = new HashSet<WordModel>();

            string[] textLines = await File.ReadAllLinesAsync(_settings.FilePath, ct);

            int index = 0;

            foreach (string textLine in textLines)
            {
                if (string.IsNullOrEmpty(textLine)) continue;

                string[] textLineArray = textLine.Split("\t");

                if (textLineArray.Length < 4) continue;

                //zodynas.txt => 0     1        2    3
                //zodynas.txt => lemma wordForm word frequency
                int id = index;
                string lemma = textLineArray[0];
                string wordForm = textLineArray[1];
                string word = textLineArray[2];
                int frequency = Int32.Parse(textLineArray[3]);

                words.Add(new WordModel
                {
                    Id = id,
                    Lemma = lemma.ToLower(),
                    Form = wordForm.ToLower(),
                    Word = word.ToLower(),
                    Frequency = frequency
                });

                index++;
            }

            var result = words.ToList();

            return result;
        }

        public async Task WriteToFileAsync(WordModel wordModel, CancellationToken ct)
        {
            var lines = await ReadAllLinesAsync(ct);
            int newId = lines.Count() + 1;

            wordModel.Id = newId;

            var line = new List<string> { $"{wordModel.Lemma}\t{wordModel.Form}\t{wordModel.Word}\t{wordModel.Frequency}" };
            await File.AppendAllLinesAsync(_settings.FilePath, line, ct);
        }
    }
}