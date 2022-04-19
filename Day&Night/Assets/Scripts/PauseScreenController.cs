using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreenController : MonoBehaviour
{

    public GameObject pauseCanvas;

    // Start is called before the first frame update
    void Start()
    {
        
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
