using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckButton : MonoBehaviour
{

    private PuzzleCanvas puzzleCanvas;

    // Start is called before the first frame update
    void Start()
    {
        puzzleCanvas = GameObject.FindGameObjectWithTag("PuzzleCanvasGrid").GetComponent<PuzzleCanvas>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonClicked() {
        puzzleCanvas.ValidatePath();
    }
}
