using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Labyrinth;

namespace Labyrinth.Tests
{
    [TestClass]
    public class LabyrinthMapperTests
    {
        [TestMethod]
        public void Map_ReturnCorrectMatrix()
        {
            // Arrange
            var converter = new LabyrinthMapper();
            var directPassing = "WRWWLWWLWWLWLWRRWRWWWRWWRWLW";
            var reversePassing = "WWRRWLWLWWLWWLWWRWWRWWLW";
            var correctResult = new List<List<int>>
            {
                new List<int>(new[] { 0xa, 0xc, 5 }),
                new List<int>(new[] { 3, 8, 6 }),
                new List<int>(new[] { 9, 0xc, 7 }),
                new List<int>(new[] { 0xe, 4, 3 }),
                new List<int>(new[] { 9, 0xc, 5 })
            };

            // Act
            var result = converter.Map(directPassing, reversePassing);

            // Assert
            for (int i = 0; i < result.Count; i++)
                for (int j = 0; j < result[0].Count; j++)
                    Assert.IsTrue(result[i][j] == correctResult[i][j]);
        }
    }
}
