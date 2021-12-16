using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UISession : MonoBehaviour
{
    [SerializeField] private Text _labelTask;
    [SerializeField] private Image _imageFade;
    [SerializeField] private Button _btnRestart;
    
    private Tween _fadeTween;

    private void Awake()
    {
        Dispatcher.OnGenerationDone += SetLabelTask;
        Dispatcher.OnWin += Win;
        Dispatcher.OnStart += Launch;
        
        _btnRestart.onClick.AddListener(Restart);
    }

    private void SetLabelTask(object arg)
    {
        _labelTask.text = $"Find {(string) arg}";
    }

    private void Win()
    {
        _imageFade.gameObject.SetActive(true);
        Fade(_imageFade, 0.75f, 0.6f, () =>
        {
            _btnRestart.gameObject.SetActive(true); 
        });
    }
    
    private void Launch()
    {
        _imageFade.gameObject.SetActive(false);
        Fade(_imageFade, 0f, 1.5f, () => {});
    }
    
    private void Restart()
    {
        _btnRestart.gameObject.SetActive(false);
        Fade(_imageFade, 1f, 0.3f, () =>
        {
            _imageFade.gameObject.SetActive(false);
            Dispatcher.Send(Event.ON_RESTART);
        });
    }

    private void Fade(Image image, float endValue, float duration, TweenCallback onEnd)
    {
        _fadeTween?.Kill();

        _fadeTween = image.DOFade(endValue, duration);
        _fadeTween.onComplete += onEnd;
    }

    private void OnDestroy()
    {
        Dispatcher.OnGenerationDone -= SetLabelTask;
        Dispatcher.OnWin -= Win;
        Dispatcher.OnStart -= Launch;
    }
}
