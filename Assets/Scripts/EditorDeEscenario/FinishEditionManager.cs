using UnityEngine;

/*
 * Clase que maneja el fin de la edición del robot y escenario
 */

public class FinishEditionManager : MonoBehaviour
{


    [SerializeField] private GameEvent changeStartColorEvent;
    [SerializeField] private PlaceableManager placeableManager;
    

    public void OnClick()
    {
        changeStartColorEvent.RaiseEvent(this.gameObject, null);
        placeableManager.SetSelectedObject(null);
    }

}
