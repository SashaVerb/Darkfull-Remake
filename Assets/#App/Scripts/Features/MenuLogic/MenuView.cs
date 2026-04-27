using System;
using UIManagement;
using UnityEngine;
using UnityEngine.UI;

public class MenuView : UIPanel
{
    public event Action OnPlay;
    public event Action OnContinue;
    public event Action OnExit;

    [SerializeField] private Button _startButton;
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _exitButton;

    private void OnEnable()
    {
        _startButton.onClick.AddListener(() => OnPlay?.Invoke());
        _continueButton.onClick.AddListener(() => OnContinue?.Invoke());
        _exitButton.onClick.AddListener(() => OnExit?.Invoke());
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveAllListeners();
        _continueButton.onClick.RemoveAllListeners();
        _exitButton.onClick.RemoveAllListeners();
    }
}
