using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject Restartscreen;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        Restartscreen.SetActive(false);
    }
    public void ShowRestartScreen()
    {
        Restartscreen.SetActive(true);
    }
    public void Restart() 
    {
        SceneManager.LoadScene(0);
    }
}
