using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class Element : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private SpriteRenderer _image;
    public SpriteRenderer Image => _image;
    [SerializeField] private SpriteRenderer _background;
    [SerializeField] private Color[] _backgroundColors;
    
    private String _identifier;

    private void Awake()
    {
        Dispatcher.OnStart += PlayAnimationBounce;
        Dispatcher.OnLevelCompleted += PlayAnimationBounce;
        Dispatcher.OnFailed += PlayAnimationBounce;
    }

    public void Init(string identifier, Sprite sprite)
    {
        _identifier = identifier;
        _image.sprite = sprite;
        var randomColor = Random.Range(0, _backgroundColors.Length - 1);
        _background.color = _backgroundColors[randomColor];
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Dispatcher.Send(Event.ON_ELEMENT_CLICK, _identifier);
    }

    private void PlayAnimationBounce()
    {
        
    }
    
    private void OnDestroy()
    {
        Dispatcher.OnStart -= PlayAnimationBounce;
        Dispatcher.OnLevelCompleted -= PlayAnimationBounce;
        Dispatcher.OnFailed -= PlayAnimationBounce;
    }
}
