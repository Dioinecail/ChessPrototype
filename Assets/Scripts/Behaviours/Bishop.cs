using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : ChessPiece
{
    public override bool[,] PossibleMove()
    {
        bool[,] moves = new bool[8, 8];

        ChessPiece c;
        int i, j;

        // Top Left
        i = CurrentX;
        j = CurrentY;
        while (true)
        {
            i--;
            j++;
            if (i < 0 || j >= 8)
                break;

            c = BoardManager.Instance.Board[i, j];
            if (c == null)
                moves[i, j] = true;
            else
            {
                if (isWhite != c.isWhite)
                    moves[i, j] = true;

                break;
            }
        }

        // Top Right
        i = CurrentX;
        j = CurrentY;
        while (true)
        {
            i++;
            j++;
            if (i >= 8 || j >= 8)
                break;

            c = BoardManager.Instance.Board[i, j];
            if (c == null)
                moves[i, j] = true;
            else
            {
                if (isWhite != c.isWhite)
                    moves[i, j] = true;

                break;
            }
        }

        // Down Left
        i = CurrentX;
        j = CurrentY;
        while (true)
        {
            i--;
            j--;
            if (i < 0 || j < 0)
                break;

            c = BoardManager.Instance.Board[i, j];
            if (c == null)
                moves[i, j] = true;
            else
            {
                if (isWhite != c.isWhite)
                    moves[i, j] = true;

                break;
            }
        }

        // Down Right
        i = CurrentX;
        j = CurrentY;
        while (true)
        {
            i++;
            j--;
            if (i >= 8 || j < 0)
                break;

            c = BoardManager.Instance.Board[i, j];
            if (c == null)
                moves[i, j] = true;
            else
            {
                if (isWhite != c.isWhite)
                    moves[i, j] = true;

                break;
            }
        }

        return moves;
    }
}
