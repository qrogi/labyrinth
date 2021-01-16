using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Labyrinth
{
    /// <summary>
    /// Репозиторий чтения и записи лабиринтов
    /// </summary>
    class LabyrinthRepository
    {
        /// <summary>
        /// Получает строки прохождения лабиринта из сборки
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <returns>Коллекция кортежей (прямой маршрут/обратный маршрут)</returns>
        public IList<(string, string)> GetLabyrinthsFromAssembly(string path)
        {
            var list = new List<(string, string)>();

            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path))
            {
                using (var streamReader = new StreamReader(stream))
                {
                    var count = int.Parse(streamReader.ReadLine());
                    for (var i = 0; i < count; i++)
                    {
                        var @string = streamReader.ReadLine();
                        var spaceIndex = @string.IndexOf(' ');
                        var directPassing = @string.Substring(0, spaceIndex);
                        var reversePassing = @string.Substring(spaceIndex + 1);
                        list.Add((directPassing, reversePassing));
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// Записывает карты лабиринтов в файл
        /// </summary>
        /// <param name="fileName">Имя файла для записи</param>
        /// <param name="labyrinthMaps">Коллекция матриц-карт лабиринтов</param>
        public void WriteLabyrinthMapsToFile(string fileName, List<List<List<int>>> labyrinthMaps)
        {
            using (var streamWriter = new StreamWriter($"{fileName}.txt"))
            {
                for (int i = 0; i < labyrinthMaps.Count; i++)
                {
                    streamWriter.WriteLine($"Case #{i + 1}");
                    foreach (var line in labyrinthMaps[i])
                    {
                        foreach (int number in line)
                        {
                            streamWriter.Write(number.ToString("X").ToLower());
                        }
                        streamWriter.WriteLine();
                    }
                }
            }
        }
    }
}
