using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance;

    private bool[,] allowedMoves;

    // Board offset
    public static Vector2 boardStartOffset = new Vector2(1.423f, 1.423f);

    // Active selectable pieces
    public ChessPiece[,] Board;
    private ChessPiece selectedPiece;

    // Current selection
    private int selectionX = -1;
    private int selectionY = -1;

    // Spawnable prefabs
    public List<GameObject> chessPiecesPrefabs;
    private List<GameObject> activeChessPieces;

    // EnPassantMove
    public int[] EnPassantMove;

    // Is it white turn right now?
    private bool isWhiteTurn = true;

    private bool isMoving = false;
    public int MovingTime = 15;

    #region Unity Life Cycle


    private void Start()
    {
        // Set the static accessible instance
        Instance = this;
        // Spawn all the pieces for the first time
        SpawnAllPieces();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Update selection only on mouse click
            UpdateSelection();
        }
    }


    #endregion

    #region Private Methods


    private void SelectPiece(int x, int y) 
    {
        // If piece is currently moving, cancel any selection
        if (isMoving)
            return;

        // If no piece is available at this position = return
        if (Board[x, y] == null)
            return;

        // If white piece is selected but it's not a white turn right now = return
        if (Board[x, y].isWhite != isWhiteTurn)
            return;

        bool hasAtLeastOneMove = false;
        allowedMoves = Board[x,y].PossibleMove();

        // Check if the piece has at least one turn available
        for (int i = 0; i < 8; i++)
            for (int j = 0; j < 8; j++)
                if (allowedMoves[i, j])
                    hasAtLeastOneMove = true;

        // If no moves available at all,  return
        if (!hasAtLeastOneMove)
            return;

        // else = select piece
        selectedPiece = Board[x, y];
        BoardHighlights.Instance.HighlightAllowedMoves(allowedMoves);
    }

    private void MovePiece(int x, int y)
    {
        if(allowedMoves[x, y])
        {
            // Get the piece in the destination tile
            ChessPiece piece = Board[x, y];

            // If there is a piece at this place and it is not on our team
            if(piece != null && piece.isWhite != isWhiteTurn)
            {
                // Capture the piece

                // If it's the king
                if(piece.GetType() == typeof(King))
                {
                    EndGame(false);
                    return;
                }

                activeChessPieces.Remove(piece.gameObject);
                Destroy(piece.gameObject);
            }

            // If it was an EnPassantMove
            if (x == EnPassantMove[0] && y == EnPassantMove[1])
            {
                if (isWhiteTurn)
                    piece = Board[x, y - 1];
                else
                    piece = Board[x, y + 1];

                activeChessPieces.Remove(piece.gameObject);
                Destroy(piece.gameObject);
            }

            // Reset the EnPassantMove before calculating it
            EnPassantMove[0] = -1;
            EnPassantMove[1] = -1;

            // If we are moving a pawn, calculate EnPassantMove
            if (selectedPiece.GetType() == typeof(Pawn))
            {
                if(y == 7)
                {
                    // If white has reached the top, promote it
                    activeChessPieces.Remove(selectedPiece.gameObject);
                    Destroy(selectedPiece.gameObject);
                    SpawnChessPiece(10, x, y);
                    selectedPiece = Board[x, y];
                } 
                else if (y == 0)
                {
                    // if black has reached the bottom, promote it
                    activeChessPieces.Remove(selectedPiece.gameObject);
                    Destroy(selectedPiece.gameObject);
                    SpawnChessPiece(4, x, y);
                    selectedPiece = Board[x, y];
                }

                if (selectedPiece.CurrentY == 1 && y == 3)
                {
                    // set the black enPassantMove
                    EnPassantMove[0] = x;
                    EnPassantMove[1] = y - 1;
                }
                else if (selectedPiece.CurrentY == 6 && y == 4)
                {
                    // set the white enPassantMove
                    EnPassantMove[0] = x;
                    EnPassantMove[1] = y + 1;
                }
            }

            // Remove the piece from the grid's current position
            Board[selectedPiece.CurrentX, selectedPiece.CurrentY] = null;
            // Move the object
            StartCoroutine(MovePiece(selectedPiece.transform.localPosition, GetPosition(x, y), selectedPiece.transform));
            selectedPiece.SetPosition(x, y);
            // Move the piece on the virtual board
            Board[x, y] = selectedPiece;
            // Swap turns
            isWhiteTurn = !isWhiteTurn;
        }

        // Hide the highlights after the movement
        BoardHighlights.Instance.Hidehighlights();
        // Deselect the piece after the movement
        selectedPiece = null;
    }

    private void UpdateSelection()
    {
        // Throw a raycast to see if we hit a board or not
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null)
        {
            // If we hit a board, offset a selection by -1 since arrays start at 0
            selectionX = Mathf.FloorToInt(hit.point.x - 1);
            selectionY = Mathf.FloorToInt(hit.point.y - 1);
        }
        else
        {
            // If nothing was hit
            selectionX = -1;
            selectionY = -1;
        }

        // If we were able to hit a selection activate this piece
        if (selectionX > -1)
        {
            // if nothing is currently selected, select it, else attempt to move the selected piece
            if (selectedPiece == null)
                SelectPiece(selectionX, selectionY);
            else
                MovePiece(selectionX, selectionY);
        }
    }

    private void SpawnAllPieces()
    {
        // Initialize the arrays and lists
        activeChessPieces = new List<GameObject>();
        Board = new ChessPiece[8, 8];
        EnPassantMove = new int[2] { -1, -1 };


        // spawn black pieces


        // 2 bishops
        SpawnChessPiece(0, 2, 7);
        SpawnChessPiece(0, 5, 7);
        // 2 knights
        SpawnChessPiece(2, 1, 7);
        SpawnChessPiece(2, 6, 7);
        // 2 rooks
        SpawnChessPiece(5, 0, 7);
        SpawnChessPiece(5, 7, 7);
        // 8 pawns
        for (int i = 0; i < 8; i++)
        {
            SpawnChessPiece(3, i, 6);
        }
        // 1 king
        SpawnChessPiece(1, 4, 7);
        // 1 queen
        SpawnChessPiece(4, 3, 7);


        // spawn white pieces


        // 2 bishops
        SpawnChessPiece(6, 2, 0);
        SpawnChessPiece(6, 5, 0);
        // 2 knights
        SpawnChessPiece(8, 1, 0);
        SpawnChessPiece(8, 6, 0);
        // 2 rooks
        SpawnChessPiece(11, 0, 0);
        SpawnChessPiece(11, 7, 0);
        // 8 pawns
        for (int i = 0; i < 8; i++)
        {
            SpawnChessPiece(9, i, 1);
        }
        // 1 king
        SpawnChessPiece(7, 4, 0);
        // 1 queen
        SpawnChessPiece(10, 3, 0);
    }

    private void SpawnChessPiece(int index, int xPosition, int yPosition)
    {
        // Spawn a prefab
        GameObject piece = Instantiate(chessPiecesPrefabs[index], transform);
        piece.transform.localPosition = GetPosition(xPosition, yPosition);

        // Set the position
        ChessPiece cp = piece.GetComponent<ChessPiece>();
        cp.SetPosition(xPosition, yPosition);

        // Assign it into the 2D array
        Board[xPosition, yPosition] = cp;

        // Add it to the list of active pieces
        activeChessPieces.Add(piece);
    }

    private void EndGame(bool hardReset)
    {
        if (!hardReset)
        {
            // If it's white turn right now = hurray!
            if (isWhiteTurn)
            {
                Debug.Log("White team wins");
            }
            else
            {
                Debug.Log("Black team wins");
            }
        }

        // Destroy all the piece to respawn them again
        foreach(GameObject go in activeChessPieces)
            Destroy(go);

        // Set the white turn to true, because whites start the game (so racist)
        isWhiteTurn = true;

        // Hide the highlights
        BoardHighlights.Instance.Hidehighlights();

        // Spawn the pieces again
        SpawnAllPieces();
    }

    private IEnumerator MovePiece(Vector2 currentPosition, Vector2 targetPosition, Transform piece)
    {
        // Block selection during movement
        isMoving = true;

        // Move the piece smoothly among the MovingTime (in frames)
        for (int i = 0; i < MovingTime && piece != null; i++)
        {
            piece.localPosition = Vector3.Lerp(currentPosition, targetPosition, (float)i / MovingTime);
            yield return null;
        }

        // Check if piece were destroyed in the process (in case of promotion)
        if(piece != null)
            piece.position = targetPosition;

        // Unblock selection
        isMoving = false;
    }

    #endregion

    #region Public Methods


    public static Vector2 GetPosition(int x, int y)
    {
        return new Vector2(x + boardStartOffset.x, y + boardStartOffset.y);
    }
    
    public void Reset()
    {
        EndGame(true);
    }

    #endregion

    #region DEBUG


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                if (selectionX == x && selectionY == y)
                    Gizmos.DrawCube(GetPosition(x, y), Vector2.one);
                else 
                    Gizmos.DrawWireCube(GetPosition(x, y) , Vector3.one);
            }
        }
    }


    #endregion
}
