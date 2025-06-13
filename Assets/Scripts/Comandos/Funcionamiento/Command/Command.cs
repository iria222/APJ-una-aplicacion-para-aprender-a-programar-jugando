using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Clase padre de los comandos
 */
public abstract class Command : MonoBehaviour, IExecutableCommand
{
    [Header("Evento")]
    [SerializeField] protected GameEvent onExecution;
    [SerializeField] protected GameEvent addCommandInstance;
    private bool stopCoroutine;
    private Outline outline;

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }

    public void SetStopCoroutine(bool stopCoroutine)
    {
        this.stopCoroutine = stopCoroutine;
    }

    public bool GetStopCoroutine()
    {
        return stopCoroutine;
    }

    private void OnDestroy()
    {
        if (this.gameObject !=null)
        {
            addCommandInstance.RaiseEvent(this.gameObject, null);

        }
    }

    public IEnumerator Execute()
    {
        SetStopCoroutine(false);
        outline.enabled = true;
        yield return StartCoroutine(ExecuteCommand());
        outline.enabled = false;
    }

    public abstract IEnumerator ExecuteCommand();
}
