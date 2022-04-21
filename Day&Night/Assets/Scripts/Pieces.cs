using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


// Rotation types
public enum RotationStatus {
    Normal,
    RotateLeft,
    UpsideDown,
    RotateRight,
    
}


static class Extension
{
    /// <summary>
    /// Rollover to the next option in an enum
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="option"></param>
    /// <returns></returns>
    public static T Next<T>(this T option) where T : struct, Enum
    {
        int newValue = (int)(object)option + 1;
        if (newValue >= Enum.GetNames(typeof(T)).Length) { newValue = 0; }
        return (T)(object)newValue;
    }

    /// <summary>
    /// Rollover to the previous option in an enum
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="option"></param>
    /// <returns></returns>
    public static T Previous<T>(this T option) where T : struct, Enum
    {
        int newValue = (int)(object)option - 1;
        if (newValue < 0) { newValue = Enum.GetNames(typeof(T)).Length - 1; }
        return (T)(object)newValue;
    }

    /// <summary>
    /// If the value is out of bounds, it will return the minimum or the maximum value instead.
    /// </summary>
    /// <param name="num"></param>
    /// <param name="min">The minimum value</param>
    /// <param name="max">The maximum value</param>
    /// <returns>The value in the bound</returns>
    public static float Bounds(this float num, float min, float max)
    {
        float output = num;
        if (num < min)
        {
            output = min;
        }
        if (num > max)
        {
            output = max;
        }
        return output;
    }

    /// <summary>
    /// Check if the number is in the bound
    /// </summary>
    /// <param name="num"></param>
    /// <param name="gridSize">The size of a grid</param>
    /// <returns></returns>
    public static bool ifInGrid(this int num, int gridSize) {
        return num >= 0 && num < gridSize;
    }
}


public class Pieces : MonoBehaviour
{

    public Texture2D cursorTexture;

    public RotationStatus rotationStatus = RotationStatus.Normal;
    [SerializeField]
    int pieces; // The number of blocks in single piece
    // The collision for the piece in 4 direction
    // The size of this should be 4 * pieces, the order in this list is Normal, RotateLeft, UpsideDown, RotateRight
    public Vector2Int[] piecesBlocks; 

    // If the current piece is being moved by the player
    private bool startMovingPiece = false;

    // The canvas attached
    public PuzzleCanvas puzzleCanvas;
    // The current position of the piece
    private List<Vector2Int> placedPositions = new List<Vector2Int>();
    private bool allowRotation = true;

    // The initial position in the canvas
    private Vector2 originalPosition;

    /// <summary>
    /// Deregister the piece from the grid map
    /// </summary>
    private void clearMap() {
        foreach (Vector2Int position in placedPositions)
        {
            puzzleCanvas.ResetSingleBlock(position);
        }
        placedPositions.Clear();
    }

    /// <summary>
    /// Remove current piece from the grid
    /// </summary>
    public void resetToDefaultPosition() {
        clearMap();
        transform.position = originalPosition;
    }

    /// <summary>
    /// Placing the piece
    /// </summary>
    public void piecesClicked() {

        // Only accept left click
        if (!Input.GetMouseButtonDown(0))
        {
            return;
        }

        // disable rotation during the placement process
        // this is set to avoid multithreading problem
        // e.g placing the object and rotating the piece at the same time
        allowRotation = false;

        // The player has clicked on a piece
        if (!Internals.startMovingPieces)
        {
            clearMap();
            startMovingPiece = true;
            Internals.startMovingPieces = true;
        }
        // The player tries to set the piece
        else {
            // Check if all the blocks in the piece do not overlap with other blocks in the grid
            bool allowPlacement = true;
            Vector2Int currentGridPosition = PuzzleCanvasHelper.getGridPosition(Input.mousePosition, Internals.gridDimension);
            for (int i = 0; i < pieces; i++) {
                allowPlacement = allowPlacement && puzzleCanvas.AllowPlacement(getBlockPosition(rotationStatus, i) + currentGridPosition,
                                                                                Internals.gridDimension);
            }
            if (allowPlacement)
            {
                // The grid is ok to set the piece
                startMovingPiece = false;
                Internals.startMovingPieces = false;

                // Register the block to the grid
                for (int i = 0; i < pieces; i++)
                {
                    placedPositions.Add(getBlockPosition(rotationStatus, i) + currentGridPosition);
                    puzzleCanvas.PlaceSingleBlock(getBlockPosition(rotationStatus, i) + currentGridPosition);
                }
            }
            else {
                Debug.Log("Not Allowed");
                
            }
        }
        allowRotation = true;
    }

    void Start()
    {
        //puzzleCanvas = GameObject.FindGameObjectWithTag("PuzzleCanvasGrid").GetComponent<PuzzleCanvas>();
        originalPosition = transform.position;
        //puzzleCanvas.Delegate = this;
    }

    // Update is called once per frame
    void Update()
    {

        if (startMovingPiece) {
            // get the grid coordinate using the mouse position
            Vector2Int gridPosition = PuzzleCanvasHelper.getGridPosition(Input.mousePosition, Internals.gridDimension);
            if (gridPosition.x != -1)
            {
                // The mouse is in the grid, the piece should snap to the grid
                transform.position = PuzzleCanvasHelper.getPositionFromGrid(gridPosition);
            }
            else
            {
                // The mouse is not in the grid, the piece should follow the mouse
                transform.position = Input.mousePosition;
            }

            if (Input.GetMouseButtonDown(1))
            {
                // Undo the placement
                resetToDefaultPosition();
                startMovingPiece = false;
                Internals.startMovingPieces = false;
            }

            // Rotation
            if (Input.GetKeyDown(KeyCode.Space) && allowRotation)
            {
                transform.Rotate(Vector3.forward * -90);
                rotationStatus = rotationStatus.Next();
            }
        }

    }
    /// <summary>
    /// Get the index in the list
    /// </summary>
    /// <param name="rotationStatus"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    Vector2Int getBlockPosition(RotationStatus rotationStatus, int index) {
        return piecesBlocks[(int)rotationStatus * pieces + index];
    }

    //public void PuzzleCanvasDidPassVerification()
    //{
    //    throw new NotImplementedException();
    //}

    public void PointerEnter() {
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }
    public void PointerExit() {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
