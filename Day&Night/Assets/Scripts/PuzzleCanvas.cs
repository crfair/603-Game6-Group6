using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCanvas : MonoBehaviour
{

    public GameObject testObject;

    // Magic Number
    public int[,] savedMap = new int[10, 10];

    void setEmptyMap(int row, int column) {
        for (int i = 0; i < row; i++)
            for (int j = 0; j < column; j++)
                savedMap[i, j] = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        setEmptyMap(Internals.gridDimension.x, Internals.gridDimension.y);
    }

    // Update is called once per frame
    void Update()
    {


    }

    public bool AllowPlacement(Vector2Int position,Vector2Int gridDimension) {
        if (position.x < 0 || position.y < 0) {
            return false;
        }
        if (position.x >= gridDimension.x || position.y >= gridDimension.y) {
            return false;
        }
        return savedMap[position.x, position.y] == 0;
    }

    public void PlaceSingleBlock(Vector2Int position)
    {
        savedMap[position.x, position.y] = 1;
    }
    public void ResetSingleBlock(Vector2Int position) {
        savedMap[position.x, position.y] = 0;
    }
}
