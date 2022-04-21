using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckButton : MonoBehaviour
{

    public PuzzleCanvas puzzleCanvas;

    public TMP_Text indicatorText;

    // Start is called before the first frame update
    void Start()
    {
        //puzzleCanvas = GameObject.FindGameObjectWithTag("PuzzleCanvasGrid").GetComponent<PuzzleCanvas>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DisableText()
    {
        yield return new WaitForSeconds(2);
        indicatorText.text = "";
    }


    public void ButtonClicked() {

        if (!puzzleCanvas.ValidatePath()) {
            indicatorText.text = "Not Connected";
            StartCoroutine(DisableText());
        }
    }
}
