using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSpriteButton : MonoBehaviour
{

    [SerializeField] private Sprite sprite;
    [SerializeField] private GameEvent changeSpriteEvent;

    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        image.sprite = sprite;
    }


    public void ChangeRobotSprite()
    {
        changeSpriteEvent.RaiseEvent(this.gameObject, sprite);
    }

    public void SetSprite(Sprite sprite)
    {
        this.sprite = sprite;
    }
}
