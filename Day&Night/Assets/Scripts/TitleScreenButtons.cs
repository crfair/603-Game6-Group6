using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TitleScreenButtons : MonoBehaviour
{

    public GameObject optionImage;
    public GameObject creditImage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BtnStartClicked() {
        SceneManager.LoadScene("SampleScene");
    }
    public void BtnOptionClicked()
    {
        optionImage.SetActive(!optionImage.activeSelf);

    }
    public void BtnCreditClicked()
    {
        creditImage.SetActive(!creditImage.activeSelf);
    }
    public void BtnExitClicked()
    {
        Application.Quit();
    }
}
