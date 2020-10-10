using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScreenManager : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = false;
    }

    public void LocalGameSelected()
    {
        SceneManager.LoadScene(1);
    }

    public void OnlineGameSelected()
    {
        Debug.Log("TODO");
    }

    public void Quit()
    {
        Debug.Log(2);
        Application.Quit();
    }
}
