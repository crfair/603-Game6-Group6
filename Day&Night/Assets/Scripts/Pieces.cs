using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum RotationStatus {
    Normal,
    RotateLeft,
    UpsideDown,
    RotateRight,
    
}

public enum PiecesTypes {
    T,
    J,
    L
}


static class Extension
{
    
    public static T Next<T>(this T option) where T : struct, Enum
    {
        int newValue = (int)(object)option + 1;
        if (newValue >= Enum.GetNames(typeof(T)).Length) { newValue = 0; }
        return (T)(object)newValue;
    }

    public static T Previous<T>(this T option) where T : struct, Enum
    {
        int newValue = (int)(object)option - 1;
        if (newValue < 0) { newValue = Enum.GetNames(typeof(T)).Length - 1; }
        return (T)(object)newValue;
    }

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

    public static bool ifInGrid(this int num, int gridSize) {
        return num >= 0 && num < gridSize;
    }
}


public class Pieces : MonoBehaviour
{
    public RotationStatus rotationStatus = RotationStatus.Normal;
    [SerializeField]
    int pieces;
    public Vector2Int[] piecesBlocks;


    private bool startMovingPiece = false;
    public PuzzleCanvas puzzleCanvas;
    private List<Vector2Int> placedPositions = new List<Vector2Int>();
    private bool allowRotation = true;
    private Vector2 originalPosition;


    private void clearMap() {
        foreach (Vector2Int position in placedPositions)
        {
            puzzleCanvas.ResetSingleBlock(position);
        }
        placedPositions.Clear();
    }


    public void resetToDefaultPosition() {
        clearMap();
        transform.position = originalPosition;
    }


    public void piecesClicked() {
        if (!Input.GetMouseButtonDown(0))
        {
            return;
        }
            allowRotation = false;
        if (!Internals.startMovingPieces)
        {
            clearMap();
            startMovingPiece = true;
            Internals.startMovingPieces = true;
        }
        else {
            bool allowPlacement = true;
            Vector2Int currentGridPosition = PuzzleCanvasHelper.getGridPosition(Input.mousePosition, Internals.gridDimension);
            for (int i = 0; i < pieces; i++) {
                allowPlacement = allowPlacement && puzzleCanvas.AllowPlacement(getBlockPosition(rotationStatus, i) + currentGridPosition,
                                                                                Internals.gridDimension);
            }
            if (allowPlacement)
            {
                startMovingPiece = false;
                Internals.startMovingPieces = false;

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
    }

    // Update is called once per frame
    void Update()
    {

        if (startMovingPiece) {
            Vector2Int gridPosition = PuzzleCanvasHelper.getGridPosition(Input.mousePosition, Internals.gridDimension);
            if (gridPosition.x != -1)
            {
                transform.position = PuzzleCanvasHelper.getPositionFromGrid(gridPosition);
            }
            else
            {
                transform.position = Input.mousePosition;
            }

            if (Input.GetMouseButtonDown(1))
            {
                
                resetToDefaultPosition();
                startMovingPiece = false;
                Internals.startMovingPieces = false;
                //Debug.Log(Input.mousePosition);
                //Debug.Log(PuzzleCanvasHelper.getGridPosition(Input.mousePosition, Internals.gridDimension));
            }


            if (Input.GetKeyDown(KeyCode.Space) && allowRotation)
            {
                transform.Rotate(Vector3.forward * -90);
                rotationStatus = rotationStatus.Next();
            }
        }

    }

    Vector2Int getBlockPosition(RotationStatus rotationStatus, int index) {

        return piecesBlocks[(int)rotationStatus * pieces + index];
    }
}
