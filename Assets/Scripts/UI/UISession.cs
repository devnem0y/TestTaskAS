using UnityEngine;
using UnityEngine.UI;

public class UISession : MonoBehaviour
{
    [SerializeField] private Text _labelTask;
    [SerializeField] private Image _imageFade;
    [SerializeField] private Button _btnRestart;

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
        _btnRestart.gameObject.SetActive(true);
    }
    
    private void Launch()
    {
        _imageFade.gameObject.SetActive(false);
    }
    
    private void Restart()
    {
        _imageFade.gameObject.SetActive(false);
        _btnRestart.gameObject.SetActive(false);
        Dispatcher.Send(Event.ON_RESTART);
    }

    private void OnDestroy()
    {
        Dispatcher.OnGenerationDone -= SetLabelTask;
        Dispatcher.OnWin -= Win;
        Dispatcher.OnStart -= Launch;
    }
}
