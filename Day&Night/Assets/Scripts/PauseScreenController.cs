using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreenController : MonoBehaviour
{

    public GameObject pauseCanvas;

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
        if (Input.GetKeyDown(KeyCode.Escape)) {
            pauseCanvas.SetActive(!pauseCanvas.activeSelf);
        }
    }

    public void btnYesClicked() {
        SceneManager.LoadScene("TitleScreen");
    }
    public void btnNoClicked() {
        pauseCanvas.SetActive(!pauseCanvas.activeSelf);
    }
}
