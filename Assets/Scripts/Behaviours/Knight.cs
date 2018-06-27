using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : ChessPiece
{
    public override bool[,] PossibleMove()
    {
        bool[,] moves = new bool[8, 8];

        //UpLeft
        KnightMove(CurrentX - 1, CurrentY + 2, ref moves);

        //UpRight
        KnightMove(CurrentX + 1, CurrentY + 2, ref moves);

        //RightUp
        KnightMove(CurrentX + 2, CurrentY + 1, ref moves);

        //RightDown
        KnightMove(CurrentX + 2, CurrentY - 1, ref moves);

        //DownLeft
        KnightMove(CurrentX - 1, CurrentY - 2, ref moves);

        //DownRight
        KnightMove(CurrentX + 1, CurrentY - 2, ref moves);

        //LeftUp
        KnightMove(CurrentX - 2, CurrentY + 1, ref moves);

        //LeftDown
        KnightMove(CurrentX - 2, CurrentY - 1, ref moves);

        return moves;
    }

    public void KnightMove(int x, int y, ref bool[,] r)
    {
        ChessPiece c;
        if (x >= 0 && x < 8 && y >= 0 && y < 8)
        {
            c = BoardManager.Instance.Board[x, y];
            if (c == null)
                r[x, y] = true;
            else if (isWhite != c.isWhite)
                r[x, y] = true;
        }
    }

}
