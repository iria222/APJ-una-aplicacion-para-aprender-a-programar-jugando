using System.Collections;
using TMPro;
using UnityEngine;

/*
 * Clase que describe el comando Establecer Velocidad
 */
public class CommandEstablecerVel : Command
{
    [Header("Input field")]
    [SerializeField] private TMP_InputField inputField;

    private float speed;
    private float minSpeed = 0.1f;
    private float maxSpeed = 10f;
    
    // Start is called before the first frame update
    void Start()
    {
        speed = 5f;
    }

    public override IEnumerator ExecuteCommand()
    {
        onExecution.RaiseEvent(this.gameObject, speed);
        yield break;
    }

    public void SetSpeed()
    {
        if(string.IsNullOrEmpty(inputField.text))
        {
            inputField.text = 5.ToString();
            return;
        }

        float newSpeed = float.Parse(inputField.text);
        if(newSpeed < minSpeed)
        {
            this.speed = minSpeed;
            inputField.text = minSpeed.ToString();
        }
        else if(newSpeed > maxSpeed)
        {
            this.speed = maxSpeed;
            inputField.text = maxSpeed.ToString();
        }
        else
        {
            this.speed = newSpeed;
        }
    }

}
