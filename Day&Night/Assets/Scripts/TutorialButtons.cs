using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TutorialButtonTypes {
    Previous,
    Next,
    Close
}

public class TutorialButtons : MonoBehaviour
{
    public TutorialButtonTypes type;
    public GameObject thisCanvas;
    public GameObject previousCanvas;
    public GameObject nextCanvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonClicked() {
        switch (type) {
            case TutorialButtonTypes.Previous:
                previousCanvas.SetActive(true);
                thisCanvas.SetActive(false);
                break;
            case TutorialButtonTypes.Next:
                nextCanvas.SetActive(true);
                thisCanvas.SetActive(false);
                break;
            case TutorialButtonTypes.Close:
                thisCanvas.SetActive(false);
                break;
        }
    }
}
