using System;
using _App.Scripts.Modules.LevelManagement;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer.Unity;

public class MenuController : IInitializable, IDisposable
{
    private readonly MenuView _view;
    private readonly LevelManager _levelManager;

    public MenuController(MenuView view, LevelManager levelManager)
    {
        _view = view;
        _levelManager = levelManager;
    }

    public void Initialize()
    {
        _view.OnPlay += OnPlay;
        _view.OnContinue += OnContinue;
        _view.OnExit += OnExit;
        
        _view.Show();
    }

    public void Dispose()
    {
        _view.OnPlay -= OnPlay;
        _view.OnContinue -= OnContinue;
        _view.OnExit -= OnExit;
        
        GameObject.Destroy(_view.gameObject);
    }

    private void OnPlay() => _levelManager.LoadLevel(0).Forget();

    private void OnContinue() => _levelManager.LoadLastLevel().Forget();

    private void OnExit() => Application.Quit();
}
