using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
 * Clase que describe el funcionamiento del comando Mover
 */
public class CommandMover : Command
{
    [SerializeField] private int steps;
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
        onExecution.RaiseEvent(this.gameObject, steps);
        robotStopped = false;
        yield return new WaitUntil(() => robotStopped);
    }

}
