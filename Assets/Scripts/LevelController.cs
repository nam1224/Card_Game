using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelController : MonoBehaviour
{
    void Start()
    {
        
    }
    public void OpenScene_Easy()
    {
        SceneManager.LoadScene("1.Easy");
    }
    public void OpenScene_Normal()
    {
        SceneManager.LoadScene("2.Normal");
    }
    public void OpenScene_Hard()
    {
        SceneManager.LoadScene("3.Hard");
    }

    public void OpenScene_GameMain()
    {
        SceneManager.LoadScene("GameMain");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
