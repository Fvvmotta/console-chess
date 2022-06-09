using System;
using board;
using chess;

namespace console_chess
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(8, 8);

            board.setPiece(new Tower(board, Color.Black), new Position(0, 0));
            board.setPiece(new Tower(board, Color.Black), new Position(1, 3));
            board.setPiece(new King(board, Color.Black), new Position(2, 4));

            Screen.printBoard(board);
            Console.ReadLine();
        }
    }
}