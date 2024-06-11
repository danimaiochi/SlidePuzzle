using SlidePuzzle.Pieces;

namespace SlidePuzzle;

public class Game
{
    private Board _board;
    public int NumberOfMovements;
    private List<(Piece piece, (int x, int y) position)> _pieces = new List<(Piece, (int x, int y))>();
    private List<(Piece piece, (int x, int y) position)> _originalState = new List<(Piece, (int x, int y))>();
    private int _pieceId = 1;
    private const ConsoleColor DefaultColour = ConsoleColor.Black;
    public Game(Board board)
    {
        _board = board;
    }
    public void AddPiece(Piece piece, int x, int y)
    {
        piece.Id = _pieceId;
        _board.AddPiece(piece, x, y);
        _pieces.Add((piece, (x, y)));
        _pieceId++;
    }

    public Piece GetPieceById(int pieceId)
    {
        return _pieces.Find(x => x.piece.Id == pieceId).piece;
    }
    public (int x, int y) GetPositionByPieceId(int pieceId)
    {
        return _pieces.Find(x => x.piece.Id == pieceId).position;
    }

    public void MovePiece(int pieceId, Direction direction)
    {
        var piece = GetPieceById(pieceId);
        var position = GetPositionByPieceId(pieceId);
        var newPosition = _board.MovePiece(piece, position, direction);
        
        if (position != newPosition)
        {
            NumberOfMovements++;
            _pieces.RemoveAll(x => x.piece.Id == pieceId);
            _pieces.Add((piece, newPosition));
        }
        Print();
    }

    public void Print()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.White;
        for (int k = 0; k < _board.BoardCoordinates.GetLength(0); k++)
        {
            for (int l = 0; l < _board.BoardCoordinates.GetLength(1); l++)
            {
                var currentPosition = _board.BoardCoordinates[k, l];
                if (currentPosition != Board.EmptySpace)
                {
                    Console.BackgroundColor = GetPieceById(int.Parse(currentPosition)).Colour;
                }
                Console.Write($" {currentPosition} ");
                Console.BackgroundColor = DefaultColour;
            }
            Console.WriteLine();
        }
    }

    public void SaveState()
    {
        _originalState.AddRange(_pieces);
    }

    public void Reset()
    {
        _pieces.Clear();
        _pieces.AddRange(_originalState);
        _board.ClearBoard();
        _pieces.ForEach(x => _board.AddPiece(x.piece, x.position.x, x.position.y));
        NumberOfMovements = 0;
        Print();
    }
}