using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Clase que gestiona las pestañas de comandos
 */
public class TabGroup : MonoBehaviour
{
    [SerializeField] private GameObject pageGroup;

    /*
    private void OnEnable()
    {
        GameObject robotPanel = pageGroup.transform.GetChild(0).gameObject;
        robotPanel.SetActive(true);
        List<GameObject> list = GetPagesList();
        for(int i = 1; i < list.Count; i++)
        {
            pageGroup.transform.GetChild(i).gameObject.SetActive(false);
        }
        
    }*/

    public List<GameObject> GetPagesList()
    {
        List<GameObject> pagesList = new List<GameObject>();
        int childCount = pageGroup.transform.childCount;
        for(int i = 0; i < childCount; i++)
        {
            pagesList.Add(pageGroup.transform.GetChild(i).gameObject);
        }
        return pagesList;
    }
 
    /*
     * Activa la pagina seleccionada y desactiva las demas
     * @param   tab pestaña de la pagina seleccionada
     */
    public void OnTabSelected(TabButton tab)
    {   
        GameObject tabPage = tab.GetPage();

        foreach (GameObject page in GetPagesList())
        {           
            if (page.Equals(tabPage))
            {
                page.transform.SetAsLastSibling();
            }
        }

        //Pongo la pestaña seleccionada sobre las demas
        tab.gameObject.transform.SetAsLastSibling();
    }
}
