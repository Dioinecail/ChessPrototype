using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPiece : MonoBehaviour
{
    public int CurrentX;
    public int CurrentY;
    public bool isWhite;

    // Sets the internal position of this piece
    public void SetPosition(int x, int y)
    {
        CurrentX = x;
        CurrentY = y;
    }

    // Overridable PossibleMoves method, see other scripts for implementation
    public virtual bool[,] PossibleMove()
    {
        return new bool[8,8];
    }
}