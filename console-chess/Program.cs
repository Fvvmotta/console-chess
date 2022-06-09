using System;
using board;
using chess;

namespace console_chess
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Board board = new Board(8, 8);

                board.setPiece(new Tower(board, Color.Black), new Position(0, 0));
                board.setPiece(new Tower(board, Color.Black), new Position(1, 3));
                board.setPiece(new King(board, Color.Black), new Position(0, 2));

                board.setPiece(new Tower(board, Color.White), new Position(3, 4));

                Screen.printBoard(board);
            }catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }
            
            Console.ReadLine();
        }
    }
}