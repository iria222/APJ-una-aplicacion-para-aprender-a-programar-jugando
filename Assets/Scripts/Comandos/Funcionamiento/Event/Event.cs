using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Clase padre de los eventos
 */
public abstract class Event : MonoBehaviour
{
    [SerializeField] protected GameEvent addCommandInstance;

    private void OnDestroy()
    {
        addCommandInstance.RaiseEvent(this.gameObject, null);
    }

    public abstract bool IsEventHappening();
}
