using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class Element : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private SpriteRenderer _image;
    [SerializeField] private SpriteRenderer _background;
    [SerializeField] private Color[] _backgroundColors;
    
    private String _identifier;

    private void Awake()
    {
        Dispatcher.OnStart += SessionStart;
        Dispatcher.OnLevelCompleted += LevelCompleted;
        Dispatcher.OnFailed += Failed;
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

    private void SessionStart()
    {
        PlayAnimationBounce(transform, 0.3f);
    }
    
    private void LevelCompleted(object arg)
    {
        if (_identifier.Contains((string) arg))
        {
            PlayAnimationBounce(_image.transform, 0.15f);
            // add particls
        }
    }
    
    private void Failed(object arg)
    {
        if (_identifier.Contains((string) arg))
            _image.transform.DOShakePosition(0.8f, 0.15f, 10, 0f);
    }

    private void PlayAnimationBounce(Transform t, float strength)
    {
        t.DOShakeScale(1f, strength, 10, 0f);
    }
    
    private void OnDestroy()
    {
        Dispatcher.OnStart -= SessionStart;
        Dispatcher.OnLevelCompleted -= LevelCompleted;
        Dispatcher.OnFailed -= Failed;
    }
}
