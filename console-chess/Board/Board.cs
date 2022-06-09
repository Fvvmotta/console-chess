using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace board
{
    internal class Board
    {
        public int Lines { get; set; }
        public int Columns { get; set; }
        private Piece[,] Pieces;

        public Board(int lines, int columns)
        {
            Lines = lines;
            Columns = columns;
            Pieces = new Piece[lines, columns];
        }

        public Piece Piece(int line, int column)
        {
            return Pieces[line, column];
        }

        public Piece Piece(Position pos)
        {
            return Pieces[pos.Lines, pos.Columns];
        }
        public bool PieceExist(Position pos)
        {
            ValidatePosition(pos);
            return Piece(pos) != null;
        }

        public void SetPiece(Piece p, Position pos)
        {
            if (PieceExist(pos))
            {
                throw new BoardException("A piece already exists in this position");
            }
            Pieces[pos.Lines, pos.Columns] = p;
            p.Position = pos;
        }

        public Piece RemovePiece(Position pos)
        {
            if(Piece(pos) == null)
            {
                return null;
            }
            Piece aux = Piece(pos);
            aux.Position = null;
            Pieces[pos.Lines, pos.Columns] = null;
            return aux;
        }

        public bool ValidPosition(Position pos)
        {
            if(pos.Lines < 0 || pos.Columns >= Lines || pos.Columns < 0 || pos.Columns >= Columns)
            {
                return false;
            }
            return true;
        }

        public void ValidatePosition(Position pos)
        {
            if (!ValidPosition(pos))
            {
                throw new BoardException("Invalid Position!");
            }
        }

    }
}
