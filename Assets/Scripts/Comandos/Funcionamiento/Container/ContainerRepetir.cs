using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class ContainerRepetir : Container
{
    private int repetitions;

    [SerializeField] private TMP_InputField inputField;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        repetitions = 0;
    }  
    

    public void SetRepetitions()
    {
        //Si el inputField está vacío, se le asigna un 0 como valor predeterminado
        if (string.IsNullOrEmpty(inputField.text))
        {
            inputField.text = 0.ToString();
        }
        repetitions = int.Parse(inputField.text);
    }

    public override IEnumerator StartExecution()
    {
        List<GameObject> commandList = GetCommands(containerPanel.transform);
        for (int i = 0; i < repetitions; i++)
        {
            if (GetStopCoroutine())
            {
                yield break;
            }
            yield return StartCoroutine(ExecuteChildCommands(commandList));
        }
    }

}
