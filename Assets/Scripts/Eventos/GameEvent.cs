using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(menuName = "GameEvent")]
public class GameEvent : ScriptableObject
{
    [SerializeField] private List<GameEventListener> listenersList = new List<GameEventListener> ();

    /*
     * Necesario para poder iniciar el evento de comenzar ejecuci�n desde un bot�n
     * Los botones no pueden llamar a m�todos con m�s de un par�metro
     */
    public void onClick()
    {
        RaiseEvent(null, null);
    }

    /*
     * M�todo que se llama cuando el evento ha ocurrido para inciar la respuesta
     * @param   sender  gameObject que incia el evento
     * @param   data    informacion necesaria para realizar la respuesta
     */
    public void RaiseEvent(GameObject sender, object data)
    {
        for(int i = 0; i < listenersList.Count; i++)
        {
            listenersList[i].OnEventRaised(sender, data);
        }
    }


    public void RegisterListener(GameEventListener listener)
    {
        if (!listenersList.Contains(listener))
        {
            listenersList.Add(listener);
        }
    }

    public void UnregisterListener(GameEventListener listener)
    {
        if (listenersList.Contains(listener))
        {
            listenersList.Remove(listener);
        }
    }
}
