using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
#if UNITY_EDITOR
using System.Reflection;
using UnityEditor;
#endif
public class GameController : ControllerBase
{
    public bool IsPlayerWin { get; set; }
    public GameStates CurrentState => _currentState;
    [SerializeField] List<ControllerBase> controllers;
    [SerializeField] UnityEvent<GameStates> onStateChanged;
    [SerializeField] private bool autoMapControllers;
    
    private GameStates _currentState;

    public override void Initialize()
    {
        base.Initialize();
        _currentState = GameStates.Main;
    }

    public void ChangeState(GameStates state)
    {
        _currentState = state;
        onStateChanged?.Invoke(_currentState);
        OnStateChanged(_currentState);
    }
    public void ChangeState(int stateIndex)
    {
        _currentState = (GameStates)stateIndex;
        onStateChanged?.Invoke(_currentState);
        OnStateChanged(_currentState);
    }

    public void EndState(bool isPlayerWin)
    {
        IsPlayerWin = isPlayerWin;
        ChangeState(GameStates.End);
    }
    
    private void Update()
    {
        foreach (var item in controllers)
        {
            item.ControllerUpdate(_currentState);
        }
    }

    private void FixedUpdate()
    {
        foreach (var item in controllers)
        {
            item.ControllerFixedUpdate(_currentState);
        }
    }
    private void LateUpdate()
    {
        foreach (var item in controllers)
        {
            item.ControllerLateUpdate(_currentState);
        }
    }

    public override void OnStateChanged(GameStates state)
    {
        base.OnStateChanged(state);
        foreach (ControllerBase controllerBase in controllers)
        {
            controllerBase.OnStateChanged(state);
        }
    }

#if UNITY_EDITOR
    public void OnValidate()
    {
    if(!autoMapControllers) return;
        AssemblyReloadEvents.afterAssemblyReload -= MapControllersInScene;
        AssemblyReloadEvents.afterAssemblyReload += MapControllersInScene;
    }
    [EditorButton]
    public void MapControllersInScene()
    {
        controllers.Clear();
        var controllersInScene = FindObjectsOfType<ControllerBase>();
        controllers.AddRange(controllersInScene);
        controllers.Remove(this);
    }
#endif

}
