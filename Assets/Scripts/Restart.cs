using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public int sceneToLoad;

    public void RestartGame()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
