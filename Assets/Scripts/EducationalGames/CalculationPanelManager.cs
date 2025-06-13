using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
 * Controla el panel en el que se muestra el número objetivo y la suma actual
 */
public class CalculationPanelManager : MonoBehaviour
{
    private TextMeshProUGUI goalText;
    private TextMeshProUGUI currentNumberText;
    private int goalNumber;
    private int currentNumber;

    [SerializeField] private GameEvent onVictoryEvent;

    private void Awake()
    {
        goalText = this.transform.Find("Objetivo").GetComponent<TextMeshProUGUI>();
        currentNumberText = this.transform.Find("Suma").GetComponent<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        goalNumber = 0;
        currentNumber = 0;
    }

    public void ChangeGoalNumber(GameObject sender, object data)
    {
        if(data is int)
        {
            goalNumber = (int)data;
            goalText.text = goalNumber.ToString();
        }
    }

    public void AddNumber(GameObject sender, object data)
    {
        if(data is int)
        {
            currentNumber += (int)data;
            currentNumberText.text = currentNumber.ToString();
            
        }
    }

    public void OnFinishExecutionEvent(GameObject sender, object data)
    {
        if (currentNumber == goalNumber)
        {
            onVictoryEvent.RaiseEvent(this.gameObject, null);
        }
    }

    public void ResetCurrentNumber(GameObject sender, object data)
    {
        currentNumber = 0;
        currentNumberText.text = currentNumber.ToString();
    }

    
}
