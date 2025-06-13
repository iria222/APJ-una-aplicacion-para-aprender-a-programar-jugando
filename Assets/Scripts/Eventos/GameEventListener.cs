using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class MyGameEvent : UnityEvent<GameObject, object> { }

/*
 * Clase que maneja los listeners de los GameEvent
 */
public class GameEventListener : MonoBehaviour
{
    [SerializeField] private GameEvent gameEvent;

    [SerializeField] private MyGameEvent response;

    private void OnEnable()
    {
        gameEvent.RegisterListener(this);
    }
    private void OnDisable()
    {
        gameEvent.UnregisterListener(this);
    }

    /*
     * Invoca la respuesta a un evento
     * @param   sender  GameObject que incia el evento
     * @param   data    informacion necesaria para realizar la respuesta
     */
    public void OnEventRaised(GameObject sender, object data)
    {
        response.Invoke(sender, data);
    }
}
