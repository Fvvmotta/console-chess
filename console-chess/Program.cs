﻿using System;
using board;
using chess;

namespace console_chess
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ChessPosition pos = new ChessPosition('c', 7);
            Console.WriteLine(pos);

            Console.WriteLine(pos.toPosition());

            Console.ReadLine();
        }
    }
}