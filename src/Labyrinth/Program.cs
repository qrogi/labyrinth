using System;
using System.Collections.Generic;
using System.IO;

namespace Labyrinth
{
    class Program
    {
        static void Main(string[] args)
        {
            var repository = new LabyrinthRepository();
            var mapper = new LabyrinthMapper();
            
            var labyrinths = repository.GetLabyrinthsFromAssembly("Labyrinth.Data.small-test.in.txt");
            var labyrinthMaps = new List<List<List<int>>>();
            foreach (var labyrinth in labyrinths) 
                labyrinthMaps.Add(mapper.Map(labyrinth.Item1, labyrinth.Item2));
            repository.WriteLabyrinthMapsToFile("small-test", labyrinthMaps);
            Console.WriteLine($"Created file small-test.txt in directory {Directory.GetCurrentDirectory()}");

            labyrinths = repository.GetLabyrinthsFromAssembly("Labyrinth.Data.large-test.in.txt");
            labyrinthMaps.Clear();
            foreach (var labyrinth in labyrinths)
                labyrinthMaps.Add(mapper.Map(labyrinth.Item1, labyrinth.Item2));
            repository.WriteLabyrinthMapsToFile("large-test", labyrinthMaps);
            Console.WriteLine($"Created file large-test.txt in directory {Directory.GetCurrentDirectory()}");

            Console.ReadKey();
        }
    }
}
