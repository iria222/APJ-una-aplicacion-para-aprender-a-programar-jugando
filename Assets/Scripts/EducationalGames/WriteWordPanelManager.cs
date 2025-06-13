using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using TMPro;
using UnityEngine;

public class WriteWordPanelManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    private Animator animator;
    private Canvas canvas;
    private string showCanvasTrigger = "ShowCanvas";
    private string word;

    [SerializeField] GameEvent setGoalWord;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        canvas = GetComponent<Canvas>();
    }

    // Start is called before the first frame update
    void Start()
    {
        word = "PALABRA";
        animator.SetTrigger(showCanvasTrigger);
    }

    public void SetWord()
    {
        if(inputField.text.Length > 0)
        {
            word = MakeWordValid(inputField.text.ToUpper());
        }
        setGoalWord.RaiseEvent(this.gameObject, word);
    }

    public void DisableCanvas()
    {
        canvas.enabled = false;
    }

    
    /*
     * Elimino todos los caracteres que no sean letras y quito los acentos
     * @param   word    palabra que corregir
     * @return          palabra corregida
     */
    public string MakeWordValid(string word)
    {
        string aux ="";
        foreach(char c in word)
        {
            if (char.IsLetter(c))
            {
                //string letterAux = c.ToString().Normalize(NormalizationForm.FormD);
                //aux += letterAux[0];
                aux += c;
            }
        }
        return aux;
    }
}
