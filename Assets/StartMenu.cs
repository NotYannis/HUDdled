using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StartMenu : MonoBehaviour {
    public GameObject UI;
    public GameObject wizard;
    public GameObject soundManager;
    public GameObject waveManager;

    public void StartGame()
    {
        UI.SetActive(true);
        wizard.SetActive(true);
        soundManager.SetActive(true);
        waveManager.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
