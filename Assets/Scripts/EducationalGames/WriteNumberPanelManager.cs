using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WriteNumberPanelManager : MonoBehaviour
{
    [SerializeField] private GameEvent changeGoalNumber;
    [SerializeField] private TMP_InputField inputField;
    private Animator animator;

    private Canvas canvas;

    private int goalNumber;
    private string showCanvasTrigger= "ShowCanvas";

    private void Awake()
    {
        animator = GetComponent<Animator>();
        canvas = GetComponent<Canvas>();
    }

    private void Start()
    {
        animator.SetTrigger(showCanvasTrigger);
        goalNumber = 1;
    }

    public void SetGoalNumber(string number)
    {
        if (number.Length > 0)
        {
            this.goalNumber = MakeNumberValid(int.Parse(number));
            inputField.text = goalNumber.ToString();
        }
        
    }

    public int MakeNumberValid(int number)
    {
        if(number < 1)
        {
            return 1;
        }
        if(number > 99)
        {
            return 99;
        }
        return number;
    }

    public void DisableCanvas()
    {
        canvas.enabled = false;
    }

    public void SendGoalNumber()
    {
        changeGoalNumber.RaiseEvent(this.gameObject, goalNumber);
    }
}
