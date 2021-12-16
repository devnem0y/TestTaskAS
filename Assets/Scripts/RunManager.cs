using DG.Tweening;
using UnityEngine;

public class RunManager : MonoBehaviour
{
    [SerializeField] private Session _session;

    private void Start()
    {
        DOTween.Init();
        _session.Run();
    }

    private void OnDestroy()
    {
        _session.Unsubscribe();
    }
}
