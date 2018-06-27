using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : ChessPiece
{
    public override bool[,] PossibleMove()
    {
        bool[,] moves = new bool[8, 8];

        ChessPiece c;
        int i, j;

        // Top Side
        i = CurrentX - 1;
        j = CurrentY + 1;
        if (CurrentY != 7)
        {
            for (int k = 0; k < 3; k++)
            {
                if (i >= 0 || i < 8)
                {
                    c = BoardManager.Instance.Board[i, j];
                    if (c == null)
                        moves[i, j] = true;
                    else if (isWhite != c.isWhite)
                        moves[i, j] = true;
                }

                i++;
            }
        }

        // Down Side
        i = CurrentX - 1;
        j = CurrentY - 1;
        if (CurrentY != 0)
        {
            for (int k = 0; k < 3; k++)
            {
                if (i >= 0 || i < 8)
                {
                    c = BoardManager.Instance.Board[i, j];
                    if (c == null)
                        moves[i, j] = true;
                    else if (isWhite != c.isWhite)
                        moves[i, j] = true;
                }

                i++;
            }
        }

        // Middle Left
        if (CurrentX != 0)
        {
            c = BoardManager.Instance.Board[CurrentX - 1, CurrentY];
            if (c == null)
                moves[CurrentX - 1, CurrentY] = true;
            else if (isWhite != c.isWhite)
                moves[CurrentX - 1, CurrentY] = true;
        }

        // Middle Right
        if (CurrentX != 7)
        {
            c = BoardManager.Instance.Board[CurrentX + 1, CurrentY];
            if (c == null)
                moves[CurrentX + 1, CurrentY] = true;
            else if (isWhite != c.isWhite)
                moves[CurrentX + 1, CurrentY] = true;
        }

        return moves;
    }

}
