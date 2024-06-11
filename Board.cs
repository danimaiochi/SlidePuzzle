using SlidePuzzle.Pieces;

namespace SlidePuzzle;

public class Board
{
    public string[,]  BoardCoordinates;
    public const string EmptySpace = "_";
    
    public Board(int x, int y)
    {
        BoardCoordinates = new string[x, y];
        ClearBoard();
    }

    public void AddPiece(Piece piece, int x, int y)
    {
        if (x > BoardCoordinates.GetLength(0))
        {
            throw new Exception("X is out of bounds");
        }
        
        if (y > BoardCoordinates.GetLength(1))
        {
            throw new Exception("Y is out of bounds");
        }

        for (int k = 0; k < piece.Shape.GetLength(0); k++)
        {
            for (int l = 0; l < piece.Shape.GetLength(1); l++)
            {
                if (piece.Shape[k, l] && BoardCoordinates[k+x, l+y] == EmptySpace)
                {
                    BoardCoordinates[k+x, l+y] = piece.Id.ToString();
                }
            }
        }
    }
    public void RemovePiece(Piece piece)
    {
        for (int k = 0; k < BoardCoordinates.GetLength(0); k++)
        {
            for (int l = 0; l < BoardCoordinates.GetLength(1); l++)
            {
                if (BoardCoordinates[k, l] == piece.Id.ToString())
                {
                    BoardCoordinates[k, l] = EmptySpace;
                }
            }
        }
    }
    public void ClearBoard()
    {
        for (int k = 0; k < BoardCoordinates.GetLength(0); k++)
        {
            for (int l = 0; l < BoardCoordinates.GetLength(1); l++)
            {
                BoardCoordinates[k, l] = EmptySpace;
            }
        }
    }

    private bool CanMovePiece(Piece piece, int destinationX, int destinationY)
    {
        var maxBoardX = BoardCoordinates.GetLength(0) - 1;
        var maxBoardY = BoardCoordinates.GetLength(1) - 1;
        
        if (destinationX < 0 ||
            destinationY < 0 ||
            maxBoardX < destinationX || 
            maxBoardY < destinationY)
        {
            return false;
        }
        for (int k = 0; k < piece.Shape.GetLength(0); k++)
        {
            for (int l = 0; l < piece.Shape.GetLength(1); l++)
            {
                if (piece.Shape[k, l])
                {
                    if (k + destinationX > maxBoardX || l + destinationY > maxBoardY)
                    {
                        return false;
                    }
                    if (BoardCoordinates[k+destinationX, l+destinationY] != EmptySpace && 
                        BoardCoordinates[k+destinationX, l+destinationY] != piece.Id.ToString())
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }

    public (int x, int y) MovePiece(Piece piece, (int x, int y) position, Direction direction)
    {
        var currentX = position.x;
        var currentY = position.y;
        var destinationX = position.x;
        var destinationY = position.y;

        switch (direction)
        {
            case Direction.Up:
                destinationX--;
                break;
            case Direction.Right:
                destinationY++;
                break;
            case Direction.Down:
                destinationX++;
                break;
            case Direction.Left:
                destinationY--;
                break;
        }
        
        if (!CanMovePiece(piece, destinationX, destinationY))
        {
            return (currentX, currentY);
        }
        
        RemovePiece(piece);
        AddPiece(piece, destinationX, destinationY);
        return (destinationX, destinationY);
    }
}