using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneProperties : MonoBehaviour
{
    public int number;
    // Start is called before the first frame update
    
    public void LoadScene()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        // Load the scene with the specified number
        SceneManager.LoadScene(number);
    }
    
}
