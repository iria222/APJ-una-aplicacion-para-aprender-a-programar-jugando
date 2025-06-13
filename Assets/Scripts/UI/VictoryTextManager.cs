using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
 * Clase que maneja el texto mostrado en la pantalla de victoria
 */
public class VictoryTextManager : MonoBehaviour
{
    [SerializeField] private GameEvent onVictory;
    [SerializeField] private int minNumberCommands = 0;
    [SerializeField] private TMP_Text extraTextField;
    private TMP_Text commandNumberText;

    private bool extraTextEnable;

    private void Awake()
    {
        commandNumberText = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        if(minNumberCommands > 0)
        {
            extraTextEnable = true;
        }
    }

    public void ChangeText(GameObject sender, object data)
    {
        if (data is int)
        {
            int numberOfCommands = (int)data;
            commandNumberText.text = numberOfCommands.ToString();

            if(extraTextField  != null)
            {
                ShowExtraText(numberOfCommands);

            }
        }
        
    }

    public void ShowExtraText(int numberOfCommands)
    {
        string extraText = "";
        if (extraTextEnable && numberOfCommands > minNumberCommands)
        {
            extraText = "Buen trabajo pero...\n ¿Crees que podrías hacerlo usando menos bloques?";
        }
        else
        {
            extraText = "¡Bien hecho!";
        }

        extraTextField.text = extraText;
    }
}
