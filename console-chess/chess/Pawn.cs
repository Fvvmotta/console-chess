using board;
namespace chess
{
    internal class Pawn : Piece
    {
        private ChessMatch Match;

        public Pawn(Board board, Color color, ChessMatch match) : base(board, color)
        {
            Match = match;
        }

        public override string ToString()
        {
            return "P";
        }

        private bool EnemyExists(Position pos)
        {
            Piece p = Board.Piece(pos);
            return p != null && p.Color != Color;
        }

        private bool Free(Position pos)
        {
            return Board.Piece(pos) == null;
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];

            Position pos = new Position(0, 0);

            if (Color == Color.White)
            {
                pos.DefineValues(Position.Line - 1, Position.Column);
                if (Board.ValidPosition(pos) && Free(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.DefineValues(Position.Line - 2, Position.Column);
                Position p2 = new Position(Position.Line - 1, Position.Column);
                if (Board.ValidPosition(p2) && Free(p2) && Board.ValidPosition(pos) && Free(pos) && MovementQty == 0)
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.DefineValues(Position.Line - 1, Position.Column - 1);
                if (Board.ValidPosition(pos) && EnemyExists(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.DefineValues(Position.Line - 1, Position.Column + 1);
                if (Board.ValidPosition(pos) && EnemyExists(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                // #jogadaespecial en passant white
                if (Position.Line == 3)
                {
                    Position left = new Position(Position.Line, Position.Column - 1);
                    if (Board.ValidPosition(left) && EnemyExists(left) && Board.Piece(left) == Match.VulnerableEnPassant)
                    {
                        mat[left.Line - 1, left.Column] = true;
                    }
                    Position direita = new Position(Position.Line, Position.Column + 1);
                    if (Board.ValidPosition(direita) && EnemyExists(direita) && Board.Piece(direita) == Match.VulnerableEnPassant)    {
                        mat[direita.Line - 1, direita.Column] = true;
                    }
                }
            }
            else
            {
                pos.DefineValues(Position.Line + 1, Position.Column);
                if (Board.ValidPosition(pos) && Free(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.DefineValues(Position.Line + 2, Position.Column);
                Position p2 = new Position(Position.Line + 1, Position.Column);
                if (Board.ValidPosition(p2) && Free(p2) && Board.ValidPosition(pos) && Free(pos) && MovementQty == 0)
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.DefineValues(Position.Line + 1, Position.Column - 1);
                if (Board.ValidPosition(pos) && EnemyExists(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.DefineValues(Position.Line + 1, Position.Column + 1);
                if (Board.ValidPosition(pos) && EnemyExists(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                // #jogadaespecial en passant black
                if (Position.Line == 4)
                {
                    Position left = new Position(Position.Line, Position.Column - 1);
                    if (Board.ValidPosition(left) && EnemyExists(left) && Board.Piece(left) == Match.VulnerableEnPassant)
                    {
                        mat[left.Line + 1, left.Column] = true;
                    }
                    Position direita = new Position(Position.Line, Position.Column + 1);
                    if (Board.ValidPosition(direita) && EnemyExists(direita) && Board.Piece(direita) == Match.VulnerableEnPassant)
                    {
                        mat[direita.Line + 1, direita.Column] = true;
                    }
                }
            }
            return mat;
        }
    }
}
