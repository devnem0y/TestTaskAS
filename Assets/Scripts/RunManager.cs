using UnityEngine;

public class RunManager : MonoBehaviour
{
    [SerializeField] private Session _session;

    private void Start()
    {
        _session.Subscribe();
        _session.Run();
    }

    private void OnDestroy()
    {
        _session.Unsubscribe();
    }
}
