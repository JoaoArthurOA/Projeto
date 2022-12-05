using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Instructions : MonoBehaviour
{
    public void LoadScenes(string cena)
    {
        SceneManager.LoadScene(cena);
    }
}
