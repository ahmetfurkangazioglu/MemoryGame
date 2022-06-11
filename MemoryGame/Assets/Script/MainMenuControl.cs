using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuControl : MonoBehaviour
{
    public GameObject SurePanel;
  public void StartLevel()
    {
        SceneManager.LoadScene(1);
    }
    public void Quit(string respond)
    {
        switch (respond)
        {
            case "Sure":
                SurePanel.SetActive(true);
                break;
            case "Yes":
                Application.Quit();
                break;
             case "No":
                SurePanel.SetActive(false);
                break;

        }

    }
}
