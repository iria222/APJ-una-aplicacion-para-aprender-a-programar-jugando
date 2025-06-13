using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Clase padre de los contenedores
 */
public abstract class Container : MonoBehaviour, IExecutableCommand
{

    protected GameObject containerPanel;
    private Outline outline;
    private bool stopCoroutine;

    [SerializeField] protected GameEvent addCommandInstance;
    private void Awake()
    {
        outline = GetComponent<Outline>();   
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        containerPanel = this.transform.GetChild(1).gameObject;
        stopCoroutine = false;
    }

    private void OnDestroy()
    {
        addCommandInstance.RaiseEvent(this.gameObject, null);
    }

    public GameObject GetContainerPanel() { return containerPanel; }

    public abstract IEnumerator StartExecution();

    public void StopExecution(GameObject sender, object data)
    {
        outline.enabled = false;
        stopCoroutine = true;
        //StopAllCoroutines();
    }

    public IEnumerator Execute()
    {
        SetStopCoroutine(false);
        outline.enabled = true;
        yield return StartCoroutine(StartExecution());
        outline.enabled = false;
    }

    public void SetStopCoroutine(bool stopCoroutine)
    {
        this.stopCoroutine = stopCoroutine;
    }

    public bool GetStopCoroutine()
    {
        return stopCoroutine;
    }

    /*
     * Ejecuta los comandos pasados
     * @param   commandList     lista de comandos a ejecutar
     */
    public IEnumerator ExecuteChildCommands(List<GameObject> commandList)
    {
        SetStopCoroutine(false);
        foreach (GameObject command in commandList)
        {
            if(GetStopCoroutine())
            {
                yield break;
            }
            yield return StartCoroutine(command.GetComponent<IExecutableCommand>().Execute());
        }
    }

    /*
     * Obtiene una lista con todos los comandos dentro del panel del contenedor
     * @param       parent          Transform del panel del contenedor padre
     * @return                      Lista con los comandos hijo
     */
    public List<GameObject> GetCommands(Transform parent)
    {
        List<GameObject> commandList = new List<GameObject>();
        foreach(Transform child in parent)
        {
            bool isPlaceHolder = child.gameObject.CompareTag("PlaceHolder");

            if (!isPlaceHolder)
            {
                commandList.Add(child.gameObject);
            }
            
        }
        return commandList;
    }
}
