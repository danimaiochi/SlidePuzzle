using SlidePuzzle.Pieces;

namespace SlidePuzzle;

public class Game
{
    private Board _board;
    public int NumberOfMovements;
    private List<(Piece piece, (int x, int y) position)> _pieces = new List<(Piece, (int x, int y))>();
    private List<(Piece piece, (int x, int y) position)> _originalState = new List<(Piece, (int x, int y))>();
    private int _pieceId = 0;
    private const ConsoleColor DefaultBackgroundColour = ConsoleColor.Black;
    private const ConsoleColor DefaultForegroundColour = ConsoleColor.White;
    private const ConsoleColor SelectedBackgroundColour = ConsoleColor.Yellow;
    private const ConsoleColor SelectedForegroundColour = ConsoleColor.Black;


    public int NumberOfPieces => _pieceId;
    public int SelectedPiece { get; set; }
    public Game(Board board)
    {
        _board = board;
    }
    public void AddPiece(Piece piece, int x, int y)
    {
        _pieceId++;
        piece.Id = _pieceId;
        _board.AddPiece(piece, x, y);
        _pieces.Add((piece, (x, y)));
    }

    public Piece GetPieceById(int pieceId)
    {
        return _pieces.Find(x => x.piece.Id == pieceId).piece;
    }
    public (int x, int y) GetPositionByPieceId(int pieceId)
    {
        return _pieces.Find(x => x.piece.Id == pieceId).position;
    }

    public void MovePiece(Direction direction)
    {
        var piece = GetPieceById(SelectedPiece);
        var position = GetPositionByPieceId(SelectedPiece);
        var newPosition = _board.MovePiece(piece, position, direction);
        
        if (position != newPosition)
        {
            NumberOfMovements++;
            _pieces.RemoveAll(x => x.piece.Id == SelectedPiece);
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
                    if (int.Parse(currentPosition) == SelectedPiece)
                    {
                        Console.BackgroundColor = SelectedBackgroundColour;
                        Console.ForegroundColor = SelectedForegroundColour;

                    }
                    else
                    {
                        Console.BackgroundColor = GetPieceById(int.Parse(currentPosition)).Colour;                        
                    }
                }
                Console.Write($" {currentPosition} ");
                Console.BackgroundColor = DefaultBackgroundColour;
                Console.ForegroundColor = DefaultForegroundColour;
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