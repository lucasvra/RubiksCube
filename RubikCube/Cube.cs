using System;

namespace RubikCube
{
    
    public class Cube<T>
    {
        private readonly T[,,] _cube;
        
        public Cube(T[,,] cube)
        {
            if(cube.GetLength(0) != 3
            || cube.GetLength(1) != 3
            || cube.GetLength(2) != 6) throw new Exception("Cube must have dimensions 3x3x6");
            
            _cube = cube;
        }

        
        public Cube(T[] faces)
        {
            if(faces.Length != 6) throw new Exception("The array faces should have 6 elements");
            _cube = new T[3,3,6];
            
            for (byte i = 0; i < 3; i++)
            for (byte j = 0; j < 3; j++)
            for (byte k = 0; k < 6; k++)
                _cube[i, j, k] = faces[k];
            
        }

        public Cube<T> Move(string m)
        {
            var cube = (T[,,]) _cube.Clone();
            
            for (byte i = 0; i < 3; i++)
            for (byte j = 0; j < 3; j++)
            for (byte k = 0; k < 6; k++)
            {
                var (i2, j2, k2) = Move((i, j, k), m);
                cube[i2, j2, k2] = _cube[i, j, k];
            }
            
            return new Cube<T>(cube);
        }

        public T this[int i, int j, int k] => _cube[i, j, k];
        
        private (byte i, byte j, byte k) Move((byte i, byte j, byte k) pos1, string m)
        {
            var pos2 = m switch
            {
                "R" => pos1 switch
                {
                    (byte i, 2, 0) => (i, 2, 4),
                    (byte i, 2, 4) => (i, 2, 2),
                    (byte i, 2, 2) => (i, 2, 5),
                    (byte i, 2, 5) => (i, 2, 0),
                    (0, 0, 1) => (0, 2, 1),
                    (0, 1, 1) => (1, 2, 1),
                    (0, 2, 1) => (2, 2, 1),
                    (1, 0, 1) => (0, 1, 1), //(1, 1, 3) => (1, 1, 3),
                    (1, 2, 1) => (2, 1, 1),
                    (2, 0, 1) => (0, 0, 1),
                    (2, 1, 1) => (1, 0, 1),
                    (2, 2, 1) => (2, 0, 1),
                    _ => pos1,
                },
                "R'" => Move(Move(Move(pos1, "R"), "R"), "R"),
                "R2" => Move(Move(pos1, "R"), "R"),
                "L'" => pos1 switch
                {
                    (byte i, 0, 0) => (i, 0, 4),
                    (byte i, 0, 4) => (i, 0, 2),
                    (byte i, 0, 2) => (i, 0, 5),
                    (byte i, 0, 5) => (i, 0, 0),
                    (0, 0, 3) => (2, 0, 3),
                    (0, 1, 3) => (1, 0, 3),
                    (0, 2, 3) => (0, 0, 3),
                    (1, 0, 3) => (2, 1, 3), //(1, 1, 3) => (1, 1, 3),
                    (1, 2, 3) => (0, 1, 3),
                    (2, 0, 3) => (2, 2, 3),
                    (2, 1, 3) => (1, 2, 3),
                    (2, 2, 3) => (0, 2, 3),
                    _ => pos1,
                },
                "L" => Move(Move(Move(pos1, "L'"), "L'"), "L'"),
                "L2" => Move(Move(pos1, "L"), "L"),
                "D" => pos1 switch
                {
                    (2, byte j, 0) => (2, j, 1),
                    (2, byte j, 1) => (0, 2 - j, 2),
                    (0, byte j, 2) => (2, 2 - j, 3),
                    (2, byte j, 3) => (2, j, 0),
                    (0, 0, 5) => (0, 2, 5),
                    (0, 1, 5) => (1, 2, 5),
                    (0, 2, 5) => (2, 2, 5),
                    (1, 0, 5) => (0, 1, 5), //(1, 1, 3) => (1, 1, 3),
                    (1, 2, 5) => (2, 1, 5),
                    (2, 0, 5) => (0, 0, 5),
                    (2, 1, 5) => (1, 0, 5),
                    (2, 2, 5) => (2, 0, 5),
                    _ => pos1,
                },
                "D'" => Move(Move(Move(pos1, "D"), "D"), "D"),
                "D2" => Move(Move(pos1, "D"), "D"),
                "U'" => pos1 switch
                {
                    (0, byte j, 0) => (0, j, 1),
                    (0, byte j, 1) => (2, 2 - j, 2),
                    (2, byte j, 2) => (0, 2 - j, 3),
                    (0, byte j, 3) => (0, j, 0),
                    (0, 0, 4) => (2, 0, 4),
                    (0, 1, 4) => (1, 0, 4),
                    (0, 2, 4) => (0, 0, 4),
                    (1, 0, 4) => (2, 1, 4), //(1, 1, 3) => (1, 1, 3),
                    (1, 2, 4) => (0, 1, 4),
                    (2, 0, 4) => (2, 2, 4),
                    (2, 1, 4) => (1, 2, 4),
                    (2, 2, 4) => (0, 2, 4),
                    _ => pos1,
                },
                "U" => Move(Move(Move(pos1, "U'"), "U'"), "U'"),
                "U2" => Move(Move(pos1, "U"), "U"),
                "F'" => pos1 switch
                {
                    (byte i, 0, 1) => (2, i, 4),
                    (2, byte j, 4) => (2 - j, 2, 3),
                    (byte i, 2, 3) => (0, i, 5),
                    (0, byte j, 5) => (2 - j, 0, 1),
                    (0, 0, 0) => (2, 0, 0),
                    (0, 1, 0) => (1, 0, 0),
                    (0, 2, 0) => (0, 0, 0),
                    (1, 0, 0) => (2, 1, 0), //(1, 1, 3) => (1, 1, 3),
                    (1, 2, 0) => (0, 1, 0),
                    (2, 0, 0) => (2, 2, 0),
                    (2, 1, 0) => (1, 2, 0),
                    (2, 2, 0) => (0, 2, 0),
                    _ => pos1,
                },
                "F2" => Move(Move(pos1, "F"), "F"),
                "F" => Move(Move(Move(pos1, "F'"), "F'"), "F'"),
                "B" => pos1 switch
                {
                    (byte i, 2, 1) => (0, i, 4),
                    (0, byte j, 4) => (2 - j, 0, 3),
                    (byte i, 0, 3) => (2, i, 5),
                    (2, byte j, 5) => (2 - j, 2, 1),
                    (0, 0, 2) => (0, 2, 2),
                    (0, 1, 2) => (1, 2, 2),
                    (0, 2, 2) => (2, 2, 2),
                    (1, 0, 2) => (0, 1, 2), //(1, 1, 3) => (1, 1, 3),
                    (1, 2, 2) => (2, 1, 2),
                    (2, 0, 2) => (0, 0, 2),
                    (2, 1, 2) => (1, 0, 2),
                    (2, 2, 2) => (2, 0, 2),
                    _ => pos1,
                },
                "B'" => Move(Move(Move(pos1, "B"), "B"), "B"),
                "B2" => Move(Move(pos1, "B"), "B"), 
                _ => throw new Exception("Move does not exist")
            };

            return ((byte)pos2.Item1, (byte)pos2.Item2, (byte)pos2.Item3);
        }
        

    }
    
}