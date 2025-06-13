using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
 * Controla el panel en el que se muestra la palabra objetivo
 */
public class WordObjectivePanelManager : MonoBehaviour
{
    private TextMeshProUGUI goalWordText;

    [SerializeField] private GameEvent showVictoryScreen;
    
    private string goalWord;
    private int currentLetter;

    private void Awake()
    {
        goalWordText = this.transform.Find("GoalWord").GetComponent<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        goalWord = ""; 
        currentLetter = 0;
    }

    public void SetGoalWord(GameObject sender, object data)
    {
        if(data is string)
        {
            goalWord = (string)data;
            goalWordText.text = goalWord;
            currentLetter = 0;
        }
    }


    /*Comprueba si una letra es la siguiente a escribir
     * @param   letter  letra que comprobar
     * @return          es o no la siguiente
     */
    public bool IsNextLetter(char letter, int currentLetter) 
    {
      
        if (currentLetter < goalWord.Length && letter.Equals(goalWord[currentLetter]))
        {
            return true;
        }
        return false;
    }

    public string ChangeTextColor(char letter)
    {
        return "<color=green>"+letter+"</color>";
    }

    /*
     * Cambia el color de las letras seleccionadas por el usuario que se encuentran en la palabra objetivo
     * @param   data    letra seleccionada
     */
    public void UpdateGoalWord(GameObject sender, object data)
    {
        bool isTag = false;
        int auxCurrentLetter=0;
        string auxGoalWord = "";
        if(data is char)
        {
            char letter = (char)data;
            if(IsNextLetter(letter, currentLetter))
            {
                string aux = goalWordText.text;
                foreach(char c in aux)
                {
                    if (c.Equals('<'))
                    {
                        isTag = true;
                    }

                    if(!isTag && currentLetter == auxCurrentLetter)
                    {
                        auxGoalWord += ChangeTextColor(c);
                    }
                    else
                    {
                        auxGoalWord += c;
                    }

                    if(!isTag)
                    {
                        auxCurrentLetter++;
                    }
                    
                    if (c.Equals('>'))
                    {
                        isTag = false;
                    }
                }
                goalWordText.text = auxGoalWord;
                currentLetter++;
            }
            else
            {
                ResetGoal(this.gameObject, null);
            }
        }
    }

    public void OnFinishExecutionEvent(GameObject sender, object data)
    {
        if(currentLetter == goalWord.Length)
        {
            showVictoryScreen.RaiseEvent(this.gameObject, null);
        }
    }

    public void ResetGoal(GameObject sender, object data)
    {
        goalWordText.text = goalWord.ToString();
        currentLetter = 0;
    }
}
