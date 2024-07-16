using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    const string CLASSIC_SCENE = "Classic";
    const string AI_SCENE = "AI";
    const string SPLASH_SCENE = "Splash";

    private void Awake()
    {
        instance = this;
    }
    public void LoadScene(string name)
    {
        switch (name)
        {
            case CLASSIC_SCENE:
                SceneManager.LoadScene(CLASSIC_SCENE);
                break;
            case AI_SCENE:
                SceneManager.LoadScene(AI_SCENE);
                break;
            case SPLASH_SCENE:
                SceneManager.LoadScene(SPLASH_SCENE);
                break;
            
        }
    }
}
