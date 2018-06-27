using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardHighlights : MonoBehaviour
{
    // Static accessible instance
    public static BoardHighlights Instance;

    // Prefab
    public GameObject highlightPrefab;

    // Reusable List
    private List<GameObject> highlights;

    private void Start()
    {
        Instance = this;
        highlights = new List<GameObject>();
    }

    private GameObject GetHighlightObject()
    {
        // Find a reusable object
        GameObject go = highlights.Find(g => !g.activeSelf);

        // If nothing found, spawn another one
        if (go == null)
        {
            go = Instantiate(highlightPrefab, transform);
            highlights.Add(go);
        }

        return go;
    }

    public void HighlightAllowedMoves(bool[,] moves)
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (moves[i, j])
                {
                    // For any possible move, spawn/show a highlight on that position
                    GameObject go = GetHighlightObject();
                    go.SetActive(true);
                    go.transform.localPosition = BoardManager.GetPosition(i, j);
                }
            }
        }
    }
    
    // Hide all the highlights to reuse them later
    public void Hidehighlights()
    {
        foreach (GameObject go in highlights)
            go.SetActive(false);
    }
}