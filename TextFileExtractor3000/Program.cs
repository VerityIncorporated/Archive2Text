using SharpCompress.Archives;
using SharpCompress.Common;
using SharpCompress.Readers;

namespace TextFileExtractor3000
{
    internal class Program
    {
        private static void Main()
        {
            Directory.CreateDirectory("Extracted");
            Console.Title = $"Verity Sexy | TextFileExtractor3000";

            string[] allowedExtensions = { ".zip", ".rar" };
            var archiveFiles = Directory.GetFiles(Environment.CurrentDirectory, "*.*", SearchOption.AllDirectories).Where(file => allowedExtensions.Contains(Path.GetExtension(file))).ToList();
            foreach (var filePath in archiveFiles)
            {
                Extract(filePath);
            }

            Console.WriteLine("\nDone!");
            Console.Beep();
            Console.Beep();
            Console.ReadLine();
        }

        private static void Extract(string filePath)
        {
            try
            {
                var entries = ArchiveFactory.Open(filePath).Entries;
                Console.WriteLine($"[Extractor] - Extracting text files from: {filePath}");
                foreach (var entry in entries)
                {
                    if (Path.GetExtension(entry.Key).ToLower() != ".txt") continue;
                    entry.WriteToDirectory("Extracted", new ExtractionOptions { Overwrite = true, ExtractFullPath = true });
                }
            }
            catch (CryptographicException)
            {
                Console.WriteLine($"[Extractor] - Extraction Failed: {filePath}");
                return;
            }
            Console.WriteLine($"[Extractor] - Extraction Finished: {filePath}");
        }
    }
}