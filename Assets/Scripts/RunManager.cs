using DG.Tweening;
using UnityEngine;

public class RunManager : MonoBehaviour
{
    public static RunManager instance;
    
    [SerializeField] private Session _session;

    private void Start()
    {
        RunManager.instance = this;

        DOTween.Init();
        _session.Subscribe();
        _session.Run();
    }

    private void OnDestroy()
    {
        _session.Unsubscribe();
    }
}
