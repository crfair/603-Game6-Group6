using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCanvas : MonoBehaviour
{

    public GameObject testObject;
    public GameObject specialBlockPrefab;
    public GameObject placeHolderBlockPrefab;
    //public GameObject puzzle1Canvas;
    //public GameObject puzzle2Canvas;
    public Vector2Int[] fixedBlockPositions;
    public Vector2Int[] specialBlockPositions;


    // Magic Number
    public int[,] savedMap = new int[10, 10];

    private List<GameObject> fixedBlocks = new List<GameObject>();
    private List<GameObject> specialBlocks = new List<GameObject>();

    void setEmptyMap(int row, int column) {
        for (int i = 0; i < row; i++)
            for (int j = 0; j < column; j++)
                savedMap[i, j] = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        setEmptyMap(Internals.gridDimension.x, Internals.gridDimension.y);
        GenerateBlocks(fixedBlockPositions, placeHolderBlockPrefab, fixedBlocks);
        GenerateBlocks(specialBlockPositions, specialBlockPrefab, specialBlocks);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            string output = "";
            for (int i = 0; i < 10; i++) {
                for (int j = 0; j < 10; j++)
                {
                    if (savedMap[j, 9 - i] == 1)
                    {
                        output += "x";
                    }
                    else {
                        output += "o";
                    }
                   
                }
                output += "\n";
            }
            Debug.Log(output);
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            if (!Internals.startMovingPieces) {
                ResetAllPieces();
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            List<Vector2Int> exploredBlocks = new List<Vector2Int>();
            //bool result= false;
            bool result = SearchPath(specialBlockPositions[0], specialBlockPositions[specialBlockPositions.Length - 1], savedMap, Internals.gridDimension, exploredBlocks);

            if (result)
            {
                Debug.Log("Found");
            }
            else {
                Debug.Log("Not Found");
            }
        }
    }


    public void ValidatePath() {
        List<Vector2Int> exploredBlocks = new List<Vector2Int>();
        //bool result = false;
        bool result = SearchPath(specialBlockPositions[0], specialBlockPositions[specialBlockPositions.Length - 1], savedMap, Internals.gridDimension, exploredBlocks);

        if (result)
        {
            Debug.Log("Found");
            //puzzle1Canvas.gameObject.SetActive(false);
            //puzzle2Canvas.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Not Found");
        }
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

    public void ResetAllPieces() {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Pieces");
        foreach (GameObject obj in objects) {
            Pieces singlePiece = obj.GetComponent<Pieces>();
            singlePiece.resetToDefaultPosition();
        }
    }

    public void ResetAllBlocks(List<GameObject> storedData)
    {
        foreach (GameObject block in storedData)
        {
            ResetSingleBlock(block.GetComponent<Blocks>().gridPosition);
            Destroy(block);
        }
        storedData.Clear();
    }

    public void ResetStartEndBlocks() {

    }

    public void GenerateBlocks(Vector2Int[] positions,GameObject prefab,List<GameObject> storedData) {
        foreach (Vector2Int position in positions) {
            Vector2 canvasPosition = PuzzleCanvasHelper.getPositionFromGrid(position);
            GameObject singleBlock = Instantiate(prefab);
            singleBlock.transform.position = canvasPosition;
            singleBlock.transform.SetParent(transform);
            singleBlock.transform.localScale = new Vector3(1, 1, 1);
            singleBlock.GetComponent<Blocks>().gridPosition = position;
            PlaceSingleBlock(position);
            storedData.Add(singleBlock);
        }
    }

    private void AddNeighbor(int[,] map, int x, int y, Vector2Int gridDimension,List<Vector2Int> data, List<Vector2Int> exploredBlocks) {
        if (x.ifInGrid(gridDimension.x) && y.ifInGrid(gridDimension.y) && !exploredBlocks.Exists(block => block.x == x && block.y == y))
        {
            if (map[x, y] == 1) data.Add(new Vector2Int(x, y));
        }
    }

    private List<Vector2Int> SearchRectNeighbors(int[,] map, Vector2Int position, Vector2Int gridDimension, List<Vector2Int> exploredBlocks) {
        List<Vector2Int> output = new List<Vector2Int>();
        if (!position.x.ifInGrid(gridDimension.x) || !position.y.ifInGrid(gridDimension.y))
        {
            return output;
        }
        AddNeighbor(map, position.x - 1, position.y, gridDimension, output, exploredBlocks);
        AddNeighbor(map, position.x + 1, position.y, gridDimension, output, exploredBlocks);
        AddNeighbor(map, position.x, position.y + 1, gridDimension, output, exploredBlocks);
        AddNeighbor(map, position.x, position.y - 1, gridDimension, output, exploredBlocks);
        return output;
    }

    public bool SearchPath(Vector2Int start, Vector2Int end, int[,] map, Vector2Int gridDimension,List<Vector2Int> exploredBlocks) {

        exploredBlocks.Add(start);
        //Vector2 canvasPosition = PuzzleCanvasHelper.getPositionFromGrid(start);
        //GameObject singleBlock = Instantiate(testObject);
        //singleBlock.transform.position = canvasPosition;
        //singleBlock.transform.SetParent(transform);
        //singleBlock.transform.localScale = new Vector3(1, 1, 1);

        List<Vector2Int> neighbors = SearchRectNeighbors(map, start, gridDimension, exploredBlocks);
        bool result = false;
        foreach (Vector2Int position in neighbors) {
            if (position == end)
            {
                return true;
            }
            else{
                result = result || SearchPath(position, end, map, gridDimension, exploredBlocks);
            }
        }
        return result;
    }
}
