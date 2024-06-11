namespace SlidePuzzle.Pieces;

/// <summary>
/// Position 1: ⌈
/// Position 2: ⌉
/// Position 3: ⌋
/// Position 4: ⌊
/// </summary>
/// <param name="position"></param>
public class L : Piece
{
    private int _position;
    public L(int position)
    {
        _position = position;
    }
    public override bool[,] Shape
    {
        get
        {
            return _position switch
            {
                1 => new bool[,] {{true, true}, {true, false}},
                2 => new bool[,] {{true, true}, {false, true}},
                3 => new bool[,] {{true, false}, {true, true}},
                4 => new bool[,] {{false, true}, {true, true}},
                _ => new bool[,] {{true, true}, {true, false}}
            };
        }
    }
}