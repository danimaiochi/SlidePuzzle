﻿// See https://aka.ms/new-console-template for more information

using SlidePuzzle;
using SlidePuzzle.Pieces;

var board = new Board(4, 6);

var game = new Game(board);
game.AddPiece(new Square() {Colour = ConsoleColor.Red}, 1, 0);

game.AddPiece(new L(1), 0, 2);
game.AddPiece(new L(2), 0, 4);
game.AddPiece(new L(3), 2, 2);
game.AddPiece(new L(4), 2, 4);
game.SaveState();

game.Print();
var selectedPiece = 1;
var numberOfMovements = 0;
Console.WriteLine($"Selected piece {selectedPiece}");
Console.WriteLine($"Number of movements {numberOfMovements}");
while (true)
{
    var key = Console.ReadKey();
    
    switch (key.Key)
    {
        case (ConsoleKey.UpArrow):
            game.MovePiece(selectedPiece, Direction.Up);
            break;
        case (ConsoleKey.DownArrow):
            game.MovePiece(selectedPiece, Direction.Down);
            break;
        case (ConsoleKey.LeftArrow):
            game.MovePiece(selectedPiece, Direction.Left);
            break;
        case (ConsoleKey.RightArrow):
            game.MovePiece(selectedPiece, Direction.Right);
            break;
        case ConsoleKey.R:
            game.Reset();
            selectedPiece = 1;
            break;
        default:
            selectedPiece = Int32.TryParse(key.KeyChar.ToString(), out var number) ? number : selectedPiece;
            break;
    }
    game.Print();
    Console.WriteLine($"Selected piece {selectedPiece}");
    Console.WriteLine($"Number of movements {game.NumberOfMovements}");
}