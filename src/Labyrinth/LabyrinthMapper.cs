using System.Collections.Generic;
using System.Linq;

namespace Labyrinth
{
    /// <summary>
    /// По двум маршрутам выхода из лабиринта методом "левая рука к стене" строит карту лабиринта
    /// </summary>
    public class LabyrinthMapper
    {
        private const int ADD_NORTH = 0b0001;
        private const int ADD_SOUTH = 0b0010;
        private const int ADD_WEST = 0b0100;
        private const int ADD_EAST = 0b1000;

        private readonly Dictionary<int, int> DirectionDictionary;

        // directions: 0 - S, 1 - W, 2 - N, 3 - E
        // операция +: поворот направо. операция -: поворот налево
        private int _currentDirection;
        // x:0 y:0 всегда соответствует [0][0] элементу матрицы
        private int _x;
        private int _y;
        private List<List<int>> _labyrinth;

        /// <summary>
        /// Создает инстанс <see cref="LabyrinthMapper"/>
        /// </summary>
        public LabyrinthMapper()
        {
            DirectionDictionary = new Dictionary<int, int>
            {
                { 0, ADD_SOUTH },
                { 1, ADD_WEST },
                { 2, ADD_NORTH },
                { 3, ADD_EAST }
            };
        }

        /// <summary>
        /// Строит карту лабиринта по двум маршрутам
        /// </summary>
        /// <param name="directPassing">Маршрут из входа до выхода</param>
        /// <param name="reversePassing">Маршрут из выхода до входа</param>
        /// <returns>Матрица-карта лабиринта</returns>
        public List<List<int>> Map(string directPassing, string reversePassing)
        {
            _currentDirection = _x = _y = 0;
            _labyrinth = new List<List<int>>();

            // удаляем действия входа в лабиринт и выхода из лабиринта
            directPassing = RemoveFirstAndLastCharacters(directPassing);
            reversePassing = RemoveFirstAndLastCharacters(reversePassing);

            // добавляем клетку с открытым проходом на север
            _labyrinth.Add(new List<int>());
            _labyrinth[0].Add(ADD_NORTH);

            FollowDirections(directPassing);

            // добавляем на выходе проход на в текущем направлении
            _labyrinth[_y][_x] |= DirectionDictionary.GetValueOrDefault(_currentDirection);

            // разворачиваемся и проходим обратный путь
            TurnRight(); TurnRight();
            FollowDirections(reversePassing);

            return _labyrinth;
        }

        private void FollowDirections(string directions)
        {
            foreach (var symbol in directions)
            {
                switch (symbol)
                {
                    case 'W':
                        Move();
                        break;
                    case 'L':
                        TurnLeft();
                        break;
                    case 'R':
                        TurnRight();
                        break;
                }
            }
        }

        private string RemoveFirstAndLastCharacters(string input) =>
            input.Substring(1, input.Length - 2);

        private void TurnLeft() 
        {
            --_currentDirection;
            if (_currentDirection < 0)
                _currentDirection = 3;
        }

        private void TurnRight() =>
            _currentDirection = (_currentDirection + 1) % 4;

        private void Move()
        {
            switch (_currentDirection)
            {
                case 0:
                    if (_y == _labyrinth.Count - 1)
                        AddRow();
                    _labyrinth[_y][_x] |= ADD_SOUTH;
                    _y++;
                    _labyrinth[_y][_x] |= ADD_NORTH;
                    break;
                case 1:
                    if (_x == 0)
                    {
                        AddColumn(true);
                        _x++;
                    }
                    _labyrinth[_y][_x] |= ADD_WEST;
                    _x--;
                    _labyrinth[_y][_x] |= ADD_EAST;
                    break;
                case 2:
                    _labyrinth[_y][_x] |= ADD_NORTH;
                    _y--;
                    _labyrinth[_y][_x] |= ADD_SOUTH;
                    break;
                case 3:
                    if (_x == _labyrinth[0].Count - 1)
                        AddColumn(false);
                    _labyrinth[_y][_x] |= ADD_EAST;
                    _x++;
                    _labyrinth[_y][_x] |= ADD_WEST;
                    break;
            }
        }

        private void AddRow() =>
            _labyrinth.Add(Enumerable.Repeat(0, _labyrinth[0].Count).ToList());

        private void AddColumn(bool left) 
        {
            if (left)
                _labyrinth.ForEach(l => l.Insert(0, 0));
            else
                _labyrinth.ForEach(l => l.Add(0));
        }
    }
}
