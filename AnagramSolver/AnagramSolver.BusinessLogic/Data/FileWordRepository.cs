using AnagramSolver.BusinessLogic.Services;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;

namespace AnagramSolver.BusinessLogic.Data
{
    public class FileWordRepository : IWordRepository
    {
        private readonly IAppSettings _settings;
        private readonly string _filePath;

        public FileWordRepository(IAppSettings settings)
        {
            _settings = settings;
            _filePath = string.Concat(_settings.FilesPath, "/zodynas.txt");
        }

        public async Task<IEnumerable<WordModel>> ReadAllLinesAsync(CancellationToken ct)
        {
            var words = new HashSet<WordModel>();


            string[] textLines = await File.ReadAllLinesAsync(_filePath, ct);

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
            var line = new List<string> { $"{wordModel.Lemma?.ToLower()}\t{wordModel.Form?.ToLower()}\t{wordModel.Word?.ToLower()}\t{wordModel.Frequency}" };

            await File.AppendAllLinesAsync(_filePath, line, ct);
        }

        public async Task<bool> DeleteById(int id, CancellationToken ct)
        {
            var lines = await ReadAllLinesAsync(ct);
            var words = lines.ToList();

            if (id < 0 || id >= words.Count())
            {
                return false;
            }

            words.RemoveAt(id);

            var newLines = words.Select(w => $"{w.Lemma}\t{w.Form}\t{w.Word}\t{w.Frequency}");

            await File.WriteAllLinesAsync(_filePath, newLines, ct);

            return true;
        }
    }
}