using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Clase que se encarga de cargar las escenas
 */
public class SceneLoader : MonoBehaviour
{

    [SerializeField] private GameEvent unloadSceneEvent;
    private string unloadAnimationTrigger = "UnloadScene";
    private float unloadTime = 0.6f;
    /*
     *@param    sceneName   nombre de la escena a cargar
     */
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    public void LoadNextScene()
    {
        StartCoroutine(LoadSceneCoroutine(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public IEnumerator LoadSceneCoroutine(object scene)
    {
        
        if(scene is string)
        {
            string newScene = (string)scene;
            yield return StartCoroutine(PlayUnloadSceneAnimation());
            SceneManager.LoadScene(newScene);
        }
        else if(scene is int)
        {
            int newScene = (int)scene;
            yield return StartCoroutine(PlayUnloadSceneAnimation());
            SceneManager.LoadScene(newScene);
        }
    }

   
    public IEnumerator PlayUnloadSceneAnimation()
    {
        unloadSceneEvent.RaiseEvent(this.gameObject, unloadAnimationTrigger);
        yield return new WaitForSeconds(unloadTime);
    }

    
}
