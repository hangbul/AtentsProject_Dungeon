using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    public GameObject StartPanel;
    public GameObject IntroPanel;
    public GameObject MapPanel;
    public GameObject CharacterPanel;
    public GameObject Settings;
    void Start()
    {
        StartCoroutine(DelayTime(4));
    }

    IEnumerator DelayTime(float time)
    {
        yield return new WaitForSeconds(time);
        IntroPanel.SetActive(false);
        StartPanel.SetActive(true);
    }

    public void OpenSettingsMenu() //Settings
    {
        Settings.SetActive(true);
    }

    public void CloseSettingsMenu()
    {
        Settings.SetActive(false);
    }

    public void OpenStartMenu() //Start
    {
        MapPanel.SetActive(true);
        //SceneManager.LoadScene("MainGameScene");
    }

    public void QuitStartMenu()
    {
        MapPanel.SetActive(true);
    }
    public void CloseGame() //Quit
    {
        Application.Quit();
    }
}
