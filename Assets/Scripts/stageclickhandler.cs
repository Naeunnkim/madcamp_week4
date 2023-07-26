using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class stagclickhanler : MonoBehaviour
{
    public string mapName = "Map";

    private void OnMouseDown() {
        SceneManager.LoadScene(mapName);
    }
}
