using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCanvasHelper : MonoBehaviour
{
    public Vector2Int gridDimension;
    public Vector2 gridInitPosition;
    public Vector2 standardCanvasSize;
    public float singleGridSize;

    // Start is called before the first frame update
    void Start()
    {
        Internals.gridDimension = gridDimension;
        Internals.gridInitPosition = gridInitPosition;
        Internals.standardCanvasSize = standardCanvasSize;
        Internals.singleGridSize = singleGridSize;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static Vector2Int getGridPosition(Vector2 mousePosition,Vector2Int gridSize)
    {
        float scaler = Screen.width / Internals.standardCanvasSize.x;
        //Debug.Log(scaler);
        // 256,216
        int x = (int)((mousePosition.x - Internals.gridInitPosition.x * scaler) / Internals.singleGridSize / scaler);
        int y = (int)((mousePosition.y - Internals.gridInitPosition.y * scaler) / Internals.singleGridSize / scaler);
        if (x >= 0 && x < gridSize.x && y >= 0 && y < gridSize.y)
        {
            return new Vector2Int(x, y);
        }
        else {
            return new Vector2Int(-1, -1);
        }
    }

    public static Vector2 getPositionFromGrid(Vector2Int gridPostion) {
        float scaler = Screen.width / Internals.standardCanvasSize.x;
        float x = (Internals.gridInitPosition.x + (gridPostion.x + 0.5f) * Internals.singleGridSize) * scaler;
        float y = (Internals.gridInitPosition.y + (gridPostion.y + 0.5f) * Internals.singleGridSize) * scaler;
        return new Vector2(x, y);
    }
}

public static class Internals {
    public static bool startMovingPieces = false;
    public static Vector2Int gridDimension;
    public static Vector2 gridInitPosition;
    public static Vector2 standardCanvasSize;
    public static float singleGridSize;
}