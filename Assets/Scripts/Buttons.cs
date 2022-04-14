using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    [SerializeField] GameObject resumeButton;

    public void PauseGame()
    {
        resumeButton.SetActive(true);
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        resumeButton.SetActive(false);
        Time.timeScale = 1;
    }

    public void Return()
    {
        SceneManager.LoadScene(0);
    }

    public void HelpRogozin()
    {
        SceneManager.LoadScene(1);
    }
}
