using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Clase que guarda el objeto seleccionado en el momento actual junto con sus características.
 */
[CreateAssetMenu(menuName = "SelectedCommand", fileName = "SelectedCommand")]
public class SelectedObject : ScriptableObject
{

    [SerializeField] private GameObject selectedCommand;
    private bool droppable;
    private int index;
    // Start is called before the first frame update
    void Start()
    {
        selectedCommand = null;
        droppable = false;
        index = -1;
    }

    public int GetIndex() {  return index; }
    public void SetIndex(int index) {  this.index = index; }
    public bool IsDroppable() { return droppable; }
    public void SetDroppable(bool droppable) { this.droppable=droppable;}
    public GameObject GetSelectedCommand() { return selectedCommand; }
    public void SetSelectedCommand(GameObject selected) { this.selectedCommand = selected; }

}
