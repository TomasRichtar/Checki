using MedaWars.Combat.UI;
using Richi;
using System;
using System.Collections.Generic;
using TastyCore.Utils;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class WindowController : SingletonMonoBehaviour<WindowController>
{
    [SerializeField] private List<BaseWindow> _allWindows;

    private Stack<BaseWindow> _windowStack = new Stack<BaseWindow>();

    [SerializeField] private BaseWindow _initWindow;
    [SerializeField] private Transform _mainCanvas;

    [Header("PopUps")]
    [SerializeField] private ConfirmationWindow _confirmationWindow;
    [SerializeField] private InformationWindow _informationWindow;
    [SerializeField] private SelfDestructInformationWindow _felfDestructInformationWindow;

    [Header("Buttons")]
    [SerializeField] private Button _backButton;

    private void OnEnable()
    {
        if (_backButton)
        {
            _backButton.onClick.AddListener(PopWindow);
        }
    }
    private void OnDisable()
    {
        if (_backButton)
        {
            _backButton.onClick.RemoveListener(PopWindow);
        }
    }

    protected override void Awake()
    {
        base.Awake();

       
    }

    private void Start()
    {
        foreach (var item in _allWindows)
        {
            item.gameObject.SetActive(true);
        }

        _allWindows.ForEach(page => page.Exit(true));

        _initWindow.Enter(true);
    }

    public void ResetWindow<T>() where T : BaseWindow
    {
        BaseWindow window = _allWindows.Find(w => w is T);

        if (window == null)
        {
            Debug.LogError($"Window of type {typeof(T).Name} not found!");
            return;
        }

        window.ResetWidnow();
    }

    public void ForceEnter<T>() where T : BaseWindow
    {
        BaseWindow window = _allWindows.Find(w => w is T);

        if (window == null)
        {
            Debug.LogError($"Window of type {typeof(T).Name} not found!");
            return;
        }

        window.Enter();
    }

    public void ForceExit<T>() where T : BaseWindow
    {
        BaseWindow window = _allWindows.Find(w => w is T);

        if (window == null)
        {
            Debug.LogError($"Window of type {typeof(T).Name} not found!");
            return;
        }

        window.Exit();
    }

    public T GetWindow<T>() where T : BaseWindow
    {
        BaseWindow window = _allWindows.Find(w => w is T);

        if (window == null)
        {
            Debug.LogError($"Window of type {typeof(T).Name} not found!");
            return null;
        }

        return window as T;
    }


    public void PushWindow<T>() where T : BaseWindow
    {
        if (_windowStack.Count == 0)
        {
            _initWindow.Exit();
        }

        BaseWindow window = _allWindows.Find(w => w is T);

        if (window == null)
        {
            Debug.LogError($"Window of type {typeof(T).Name} not found!");
            return;
        }

        if (_windowStack.Count > 0)
        {
            _windowStack.Peek().Exit();
        }

        _windowStack.Push(window);
        window.Enter();
    }

    public void PopWindow()
    {
        if (_windowStack.Count == 0)
        {
            return;
        }

        BaseWindow window = _windowStack.Pop();
        window.Exit();

        if (_windowStack.Count > 0)
        {
            _windowStack.Peek().Enter();
        }

        if (_windowStack.Count == 0)
        {
            _initWindow.Enter();
            return;
        }
    }
    public void PushPopUpWindow(
       string title,
        Action action)
    {
        SelfDestructInformationWindow confirmationWindow = Instantiate(_felfDestructInformationWindow, _mainCanvas);
        confirmationWindow.InitFunction(action);
        confirmationWindow.SetTexts(title);
    }
    public void PushPopUpWindow(
        string title,
        string bodyText,
        string yesText,
        Action yesAction)
    {
        InformationWindow informationWindow = Instantiate(_informationWindow, _mainCanvas);
        informationWindow.InitFunction(yesAction);
        informationWindow.SetTexts(title, bodyText, yesText);
    }
    public void PushPopUpWindow(
        string title,
        string bodyText,
        string yesText,
        Action yesAction,
        string noText,
        Action noAction)
    {
        ConfirmationWindow confirmationWindow = Instantiate(_confirmationWindow, _mainCanvas);
        confirmationWindow.InitFunction(yesAction, noAction);
        confirmationWindow.SetTexts(title, bodyText, yesText, noText);
    }

    public void PopAllPages()
    {
        for (int i = 1; i < _windowStack.Count; i++)
        {
            PopWindow();
        }
    }
}
