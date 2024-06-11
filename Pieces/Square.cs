namespace SlidePuzzle.Pieces;

public class Square: Piece
{
    public override bool[,] Shape => new bool[,]
    {
        {true, true},
        {true, true}
    };
}