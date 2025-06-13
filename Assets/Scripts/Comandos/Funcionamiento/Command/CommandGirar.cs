using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 * Clase que describe el funcionamiento del comando Girar
 */
public class CommandGirar : Command
{
    [SerializeField] private int angle;
    private bool robotStopped;

    private void Start()
    {
        robotStopped = true;
    }

    public void SetRobotStopped(GameObject sender, object data)
    {
        if(data is bool)
        {
            robotStopped = (bool)data;
        }
    }

    public override IEnumerator ExecuteCommand()
    {
        onExecution.RaiseEvent(this.gameObject, angle);
        robotStopped = false;
        yield return new WaitUntil(() => robotStopped);
    }

}
