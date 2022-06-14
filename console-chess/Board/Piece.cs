using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace board
{
    internal abstract class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int MovementQty { get; protected set; }
        public Board Board { get; protected set; }

        public Piece(Board board, Color color)
        {
            Position = null;
            Color = color;
            Board = board;
            MovementQty = 0;
        }

        public void IncrementMovementQuantity()
        {
            MovementQty++;
        }
        public void DecrementMovementQuantity()
        {
            MovementQty--;
        }

        public bool ExistsPossibleMovements()
        {
            bool[,] mat = PossibleMovements();
            for (int i = 0; i < Board.Lines; i++)
            {
                for (int j = 0; j < Board.Columns; j++)
                {
                    if (mat[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool CanMoveTo(Position pos)
        {
            return PossibleMovements()[pos.Line, pos.Column];
        }

        public abstract bool[,] PossibleMovements();
    }
}
