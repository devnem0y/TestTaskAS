using DG.Tweening;
using UnityEngine;

public class RunManager : MonoBehaviour
{
    //TODO: Возможно не лучшее решение, но лучше не придумал, чтобы запустить карутину из ScriptableObject
    public static RunManager instance;
    
    [SerializeField] private Session _session;

    private void Start()
    {
        instance = this;

        DOTween.Init();
        _session.Subscribe();
        _session.Run();
    }

    private void OnDestroy()
    {
        _session.Unsubscribe();
    }
}
