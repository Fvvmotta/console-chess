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

        public ChessMatch()
        {
            Board = new Board(8 , 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            Terminated = false;
            Pieces = new HashSet<Piece>();
            Captured = new HashSet<Piece>();
            InsertPieces();
        }

        public void ExecuteMovement(Position origin, Position destiny)
        {
            Piece p = Board.RemovePiece(origin);
            p.IncrementMovementQuantity();
            Piece capturedPiece = Board.RemovePiece(destiny);
            Board.SetPiece(p, destiny);
            if(capturedPiece != null)
            {
                Captured.Add(capturedPiece);
            }

        }
        public void makePlay(Position origin, Position destiny)
        {
            ExecuteMovement(origin, destiny);
            Turn++;
            ChangePlayer();
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
        public void InsertNewPiece(char column, int line, Piece piece)
        {
            Board.SetPiece(piece, new ChessPosition(column, line).ToPosition());
            Pieces.Add(piece);
        }
        private void InsertPieces()
        {
            InsertNewPiece('c', 1, new Tower(Board, Color.White));
            InsertNewPiece('c', 2, new Tower(Board, Color.White));
            InsertNewPiece('d', 2, new Tower(Board, Color.White));
            InsertNewPiece('e', 2, new Tower(Board, Color.White));
            InsertNewPiece('e', 1, new Tower(Board, Color.White));
            InsertNewPiece('d', 1, new King(Board, Color.White));

            InsertNewPiece('c', 7, new Tower(Board, Color.Black));
            InsertNewPiece('c', 8, new Tower(Board, Color.Black));
            InsertNewPiece('d', 7, new Tower(Board, Color.Black));
            InsertNewPiece('e', 7, new Tower(Board, Color.Black));
            InsertNewPiece('e', 8, new Tower(Board, Color.Black));
            InsertNewPiece('d', 8, new King(Board, Color.Black));
        }
    }
}
