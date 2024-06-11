// See https://aka.ms/new-console-template for more information

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
var numberOfMovements = 0;
Console.WriteLine($"Selected piece {game.SelectedPiece}");
Console.WriteLine($"Number of movements {numberOfMovements}");
while (true)
{
    var key = Console.ReadKey();
    
    switch (key.Key)
    {
        case (ConsoleKey.UpArrow):
            game.MovePiece(Direction.Up);
            break;
        case (ConsoleKey.DownArrow):
            game.MovePiece(Direction.Down);
            break;
        case (ConsoleKey.LeftArrow):
            game.MovePiece(Direction.Left);
            break;
        case (ConsoleKey.RightArrow):
            game.MovePiece(Direction.Right);
            break;
        case ConsoleKey.Tab:
            game.SelectedPiece = game.SelectedPiece == game.NumberOfPieces ? 1 : game.SelectedPiece+1;
            break;
        case ConsoleKey.R:
            game.Reset();
            game.SelectedPiece = 1;
            break;
        default:
            game.SelectedPiece = Int32.TryParse(key.KeyChar.ToString(), out var number) ? number : game.SelectedPiece;
            break;
    }
    game.Print();
    Console.WriteLine($"Selected piece {game.SelectedPiece}");
    Console.WriteLine($"Number of movements {game.NumberOfMovements}");
}