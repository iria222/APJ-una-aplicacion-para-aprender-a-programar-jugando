using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

/*
 * Gestiona lo relacionado con el aspecto del robot
 */
public class RobotSpriteManager : MonoBehaviour
{
    private bool isHitAnimationFinished;
    private SpriteRenderer spriteRenderer;
    private Animator robotAnimator;

    private const string hitAnimationTrigger = "hitObstacle";

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        robotAnimator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        isHitAnimationFinished = false;
    }

    public void RestartParameters() {
        isHitAnimationFinished = false;
        ChangeSpriteColor(Color.white);
    }

    public void ChangeSprite(GameObject sender, object data)
    {
        if(data is Sprite)
        {
            spriteRenderer.sprite = (Sprite)data;

        }
    }

    public void HitAnimationFinished()
    {
        this.isHitAnimationFinished = true;
    }

    public void ChangeSpriteColor(Color color)
    {
        spriteRenderer.color = color;
    }

    public IEnumerator PlayHitAnimation()
    {
        isHitAnimationFinished = false;
        ChangeSpriteColor(Color.red);
        robotAnimator.SetTrigger(hitAnimationTrigger);
        yield return new WaitUntil(()=>isHitAnimationFinished);
        ChangeSpriteColor(Color.white);
    }

    public void ChangeSpriteVisibility(bool visibility)
    {
        spriteRenderer.enabled = visibility;
    }

    public IEnumerator ColorFlickering(Color color)
    {
        ChangeSpriteColor(color);
        yield return new WaitForSeconds(0.3f);
        ChangeSpriteColor(Color.white);
    }
}
