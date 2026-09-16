using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadAllScenes : MonoBehaviour
{
    [SerializeField] ScenesList[] scenesList;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (ScenesList scene in scenesList)
        {
            if (scene.isLoaded)
            {
                SceneManager.LoadScene(scene.sceneName, LoadSceneMode.Additive);
            }
        }
    }

    [System.Serializable]

    public class ScenesList
    {
        public string sceneName;
        public bool isLoaded;
    }

    
}
