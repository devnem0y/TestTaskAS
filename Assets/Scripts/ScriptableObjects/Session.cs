using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New Session", menuName = "MyScriptableObject/Session", order = 10)]
public class Session : ScriptableObject
{
    [SerializeField] private Level[] _levels;

    private int _currentLevel;
    private int _maxLevel => _levels.Length - 1;
    
    private Spawner _spawner;
    
    public void Subscribe()
    {
        _spawner = FindObjectOfType<Spawner>();

        Dispatcher.OnElementClick += ElementClick;
        Dispatcher.OnRestart += Restart;
    }
    
    public void Unsubscribe()
    {
        Dispatcher.OnElementClick -= ElementClick;
        Dispatcher.OnRestart -= Restart;
    }

    public void Run()
    {
        _currentLevel = 0;
        CreateLevel(_currentLevel);
        Dispatcher.Send(Event.ON_START);
    }
    
    private void CreateLevel(int id)
    {
        _spawner.Create(_levels[id]);
        _spawner.Generate();
    }

    private void ElementClick(object arg)
    {
        if (_spawner.TargetTask.Contains((string) arg))
        {
            _currentLevel++;
            RunManager.instance.StartCoroutine(LevelCompleted());
        }
        else
        {
            Dispatcher.Send(Event.ON_FAILED, (string) arg);
        }
    }
    
    private IEnumerator LevelCompleted()
    {
        Dispatcher.Send(Event.ON_LEVEL_COMPLETED, _spawner.TargetTask);
        yield return new WaitForSeconds(0.8f);
        if (_currentLevel > _maxLevel)
        {
            Dispatcher.Send(Event.ON_WIN);
        }
        else
        {
            CreateLevel(_currentLevel);
        }
    }
    
    private void Restart()
    {
        _currentLevel = 0;
        _spawner.Rebut();
        Run();
    }
}
