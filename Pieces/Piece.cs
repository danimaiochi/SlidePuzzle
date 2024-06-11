namespace SlidePuzzle.Pieces;

public abstract class Piece
{
    public abstract bool[,] Shape { get; }
    public ConsoleColor Colour { get; set; } = ConsoleColor.Blue;

    public int Id { get; set; }
}