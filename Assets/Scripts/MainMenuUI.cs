using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public GameObject creditScreen;
    public GameObject logo;
    public GameObject aboutScreen;

    public GameObject menuButtons;

    // Start is called before the first frame update
    void Start()
    {
        creditScreen.SetActive(false);
        aboutScreen.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ShowCreditScreen()
    {
        logo.SetActive(false);
        menuButtons.SetActive(false);
        creditScreen.SetActive(true);
    }

    public void HideCreditScreen()
    {
        logo.SetActive(true);
        menuButtons.SetActive(true);
        creditScreen.SetActive(false);
    }

    public void ShowAboutScreen()
    {
        logo.SetActive(false);
        menuButtons.SetActive(false);
        aboutScreen.SetActive(true);
    }

    public void HideAboutScreen()
    {
        logo.SetActive(true);
        menuButtons.SetActive(true);
        aboutScreen.SetActive(false);
    }
}
