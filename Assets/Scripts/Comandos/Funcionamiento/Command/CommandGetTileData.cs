using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandGetTileData : Command
{

    public override IEnumerator ExecuteCommand()
    {
        onExecution.RaiseEvent(this.gameObject, null);
        yield return new WaitForSeconds(0.3f);
    }

}
