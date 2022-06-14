using System;
using System.Collections.Generic;
using board;


namespace chess
{
    internal class ChessMatch
    {
        public Board Board { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool Terminated { get; private set; }
        private HashSet<Piece> Pieces;
        private HashSet<Piece> Captured;
        public bool Check { get; private set; }

        public ChessMatch()
        {
            Board = new Board(8 , 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            Terminated = false;
            Check = false;
            Pieces = new HashSet<Piece>();
            Captured = new HashSet<Piece>();
            InsertPieces();
        }

        public Piece ExecuteMovement(Position origin, Position destiny)
        {
            Piece p = Board.RemovePiece(origin);
            p.IncrementMovementQuantity();
            Piece capturedPiece = Board.RemovePiece(destiny);
            Board.SetPiece(p, destiny);
            if(capturedPiece != null)
            {
                Captured.Add(capturedPiece);
            }
            return capturedPiece;
        }

        public void UndoMovement(Position origin, Position destiny, Piece capturedPiece)
        {
            Piece p = Board.RemovePiece(destiny);
            p.DecrementMovementQuantity();
            if(capturedPiece != null)
            {
                Board.SetPiece(capturedPiece, destiny);
                Captured.Remove(capturedPiece);
            }
            Board.SetPiece(p, origin);
        }

        public void makePlay(Position origin, Position destiny)
        {
            Piece capturedPiece = ExecuteMovement(origin, destiny);

            if (IsInCheck(CurrentPlayer))
            {
                UndoMovement(origin, destiny, capturedPiece);
                throw new BoardException("You can't put yourself in check!");
            }

            if (IsInCheck(Adversary(CurrentPlayer)))
            {
                Check = true;
            }
            else
            {
                Check = false;
            }
            if (TestCheckMate(Adversary(CurrentPlayer)))
            {
                Terminated = true;
            }
            else
            {
                Turn++;
                ChangePlayer();
            }
        }

        public void ValideOriginPosition(Position pos)
        {
            if(Board.Piece(pos) == null)
            {
                throw new BoardException("Theres no piece to be selected on the origin position!");
            }
            if(CurrentPlayer != Board.Piece(pos).Color)
            {
                throw new BoardException("This chess piece is not your color!");
            }
            if (!Board.Piece(pos).ExistsPossibleMovements())
            {
                throw new BoardException("Theres no possible movements for this piece!");
            }
        }

        public void ValidateDestinyPosition(Position origin, Position destiny)
        {
            if (!Board.Piece(origin).CanMoveTo(destiny))
            {
                throw new BoardException("Invalid destiny position!");
            }
        }
        public void ChangePlayer()
        {
            if(CurrentPlayer == Color.White)
            {
                CurrentPlayer = Color.Black;
            }
            else
            {
                CurrentPlayer = Color.White;
            }
        }

        public HashSet<Piece> CapturedPieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach(Piece x in Captured)
            {
                if(x.Color == color)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Piece> PiecesInGame(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in Pieces)
            {
                if (x.Color == color)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(CapturedPieces(color));
            return aux;
        }

        private Color Adversary(Color color)
        {
            if(color == Color.White)
            {
                return Color.Black;
            }
            return Color.White;
        }

        private Piece King(Color color)
        {
            foreach(Piece x in PiecesInGame(color))
            {
                if(x is King)
                {
                    return x;
                }
            }
            return null;
        }
        public bool IsInCheck(Color color)
        {
            Piece K = King(color);
            if(K == null)
            {
                throw new BoardException("There's no king of the color " + color + " on the board!");
            }
            foreach(Piece x in PiecesInGame(Adversary(color)))
            {
                bool[,] mat = x.PossibleMovements();
                if (mat[K.Position.Line, K.Position.Column])
                {
                    return true;
                }
            }
            return false;
        }

        public bool TestCheckMate(Color color)
        {
            if (!IsInCheck(color))
            {
                return false;
            }
            foreach(Piece x in PiecesInGame(color))
            {
                bool[,] mat = x.PossibleMovements();
                for (int i = 0; i < Board.Lines; i++)
                {
                    for (int j = 0; j < Board.Columns; j++)
                    {
                        if (mat[i, j])
                        {
                            Position origin = x.Position;
                            Position destiny = new Position(i, j);
                            Piece capturedPiece = ExecuteMovement(origin, destiny);
                            bool checkTest = IsInCheck(color);
                            UndoMovement(origin, destiny, capturedPiece);
                            if (!checkTest)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void InsertNewPiece(char column, int line, Piece piece)
        {
            Board.SetPiece(piece, new ChessPosition(column, line).ToPosition());
            Pieces.Add(piece);
        }
        private void InsertPieces()
        {
            InsertNewPiece('c', 1, new Tower(Board, Color.White));
            InsertNewPiece('d', 1, new King(Board, Color.White));
            InsertNewPiece('h', 7, new Tower(Board, Color.White));

            InsertNewPiece('a', 8, new King(Board, Color.Black));
            InsertNewPiece('b', 8, new Tower(Board, Color.Black));
            
        }
    }
}
