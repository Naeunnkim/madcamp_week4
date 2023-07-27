using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetrySceneChange : MonoBehaviour
{
    public void RetrySceneBtn () {
        string sceneToReload = PlayerPrefs.GetString("RetryScene", "Map");
        SceneManager.LoadScene(sceneToReload);
    }
}
