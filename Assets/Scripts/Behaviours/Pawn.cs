using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : ChessPiece
{

    public override bool[,] PossibleMove()
    {
        bool[,] moves = new bool[8, 8];
        ChessPiece c, c2;
        int[] epm = BoardManager.Instance.EnPassantMove;

        // White team move
        if (isWhite)
        {
            // Diagonal Left
            if (CurrentX != 0 && CurrentY != 7)
            {
                if (epm[0] == CurrentX - 1 && epm[1] == CurrentY + 1)
                    moves[CurrentX - 1, CurrentY + 1] = true;

                c = BoardManager.Instance.Board[CurrentX - 1, CurrentY + 1];
                if (c != null && !c.isWhite)
                    moves[CurrentX - 1, CurrentY + 1] = true;
            }

            // Diagonal Right
            if (CurrentX != 7 && CurrentY != 7)
            {
                if (epm[0] == CurrentX + 1 && epm[1] == CurrentY + 1)
                    moves[CurrentX + 1, CurrentY + 1] = true;

                c = BoardManager.Instance.Board[CurrentX + 1, CurrentY + 1];
                if (c != null && !c.isWhite)
                    moves[CurrentX + 1, CurrentY + 1] = true;
            }

            // Middle
            if (CurrentY != 7)
            {
                c = BoardManager.Instance.Board[CurrentX, CurrentY + 1];
                if (c == null)
                    moves[CurrentX, CurrentY + 1] = true;
            }

            // Middle on first move
            if (CurrentY == 1)
            {
                c = BoardManager.Instance.Board[CurrentX, CurrentY + 1];
                c2 = BoardManager.Instance.Board[CurrentX, CurrentY + 2];
                if (c == null & c2 == null)
                    moves[CurrentX, CurrentY + 2] = true;
            }
        }
        else
        {
            // Diagonal Left
            if (CurrentX != 0 && CurrentY != 0)
            {
                if (epm[0] == CurrentX - 1 && epm[1] == CurrentY - 1)
                    moves[CurrentX - 1, CurrentY - 1] = true;

                c = BoardManager.Instance.Board[CurrentX - 1, CurrentY - 1];
                if (c != null && c.isWhite)
                    moves[CurrentX - 1, CurrentY - 1] = true;
            }

            // Diagonal Right
            if (CurrentX != 7 && CurrentY != 0)
            {
                if (epm[0] == CurrentX + 1 && epm[1] == CurrentY - 1)
                    moves[CurrentX + 1, CurrentY - 1] = true;

                c = BoardManager.Instance.Board[CurrentX + 1, CurrentY - 1];
                if (c != null && c.isWhite)
                    moves[CurrentX + 1, CurrentY - 1] = true;
            }

            // Middle
            if (CurrentY != 0)
            {
                c = BoardManager.Instance.Board[CurrentX, CurrentY - 1];
                if (c == null)
                    moves[CurrentX, CurrentY - 1] = true;
            }

            // Middle on first move
            if (CurrentY == 6)
            {
                c = BoardManager.Instance.Board[CurrentX, CurrentY - 1];
                c2 = BoardManager.Instance.Board[CurrentX, CurrentY - 2];
                if (c == null & c2 == null)
                    moves[CurrentX, CurrentY - 2] = true;
            }
        }

        return moves;
    }
}
