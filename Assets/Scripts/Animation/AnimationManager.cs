using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Clase que controla las animaciones de cambio de escena
 */
public class AnimationManager : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayAnimation(GameObject sender, object data)
    {
        if(data is string)
        {
            animator.SetTrigger((string)data);
        }
    }
    
}
