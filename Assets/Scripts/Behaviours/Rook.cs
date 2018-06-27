using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : ChessPiece
{
    public override bool[,] PossibleMove()
    {
        bool[,] moves = new bool[8, 8];

        ChessPiece c;
        int i;

        // Right
        i = CurrentX;
        while (true)
        {
            i++;
            if (i >= 8)
                break;

            c = BoardManager.Instance.Board[i, CurrentY];
            if (c == null)
                moves[i, CurrentY] = true;
            else
            {
                if (c.isWhite != isWhite)
                    moves[i, CurrentY] = true;

                break;
            }
        }

        // Left
        i = CurrentX;
        while (true)
        {
            i--;
            if (i < 0)
                break;

            c = BoardManager.Instance.Board[i, CurrentY];
            if (c == null)
                moves[i, CurrentY] = true;
            else
            {
                if (c.isWhite != isWhite)
                    moves[i, CurrentY] = true;

                break;
            }
        }

        // Up
        i = CurrentY;
        while (true)
        {
            i++;
            if (i >= 8)
                break;

            c = BoardManager.Instance.Board[CurrentX, i];
            if (c == null)
                moves[CurrentX, i] = true;
            else
            {
                if (c.isWhite != isWhite)
                    moves[CurrentX, i] = true;

                break;
            }
        }

        // Down
        i = CurrentY;
        while (true)
        {
            i--;
            if (i < 0)
                break;

            c = BoardManager.Instance.Board[CurrentX, i];
            if (c == null)
                moves[CurrentX, i] = true;
            else
            {
                if (c.isWhite != isWhite)
                    moves[CurrentX, i] = true;

                break;
            }
        }

        return moves;
    }
}
