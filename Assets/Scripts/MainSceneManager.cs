using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    public void OnClickReload()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void OnClickToTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }
}