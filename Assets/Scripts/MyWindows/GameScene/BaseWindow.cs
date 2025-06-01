using Richi;
using System;
using System.Collections;
using System.Collections.Generic;
using TastyCore._Examples.UiManager.Scripts;
using Unity.VisualScripting;
using UnityEngine;

//public abstract class BaseWindow : MonoBehaviour
//{
//    public abstract void PushThis();
//    public abstract void Enter(bool inignoreAnim = false);
//    public abstract void Exit(bool inignoreAnim = false);
//}

[RequireComponent(typeof(CanvasGroup))]
[DisallowMultipleComponent]
public abstract class BaseWindow : MonoBehaviour //<T> : BaseWindow where T : BaseWindow<T>
{
    public event Action PrePushAction;
    public event Action PostPushAction;
    public event Action PrePopAction;
    public event Action PostPopAction;

    private RectTransform RectTransform;
    private CanvasGroup CanvasGroup;

    [SerializeField] protected GameObject _bluredBG;

    [Header("Animations")]
    [SerializeField] protected float _animationSpeed = 1f;

    [SerializeField] protected PageEntryMode _entryMode = PageEntryMode.NONE;
    [SerializeField] protected Direction _entryDirection = Direction.LEFT;
    [SerializeField] protected PageEntryMode _exitMode = PageEntryMode.NONE;
    [SerializeField] protected Direction _exitDirection = Direction.LEFT;

    private Coroutine _animationCoroutine;

    private Direction _baseEntryDirection;
    private Direction _baseExitDirection;
    private PageEntryMode _baseEntryMode;
    private PageEntryMode _baseExitMode;


    public void ResetWidnow()
    {
        //_entryDirection = _baseEntryDirection;
        //_exitDirection = _baseExitDirection;
        //_entryMode = _baseEntryMode;
        //_exitMode = _baseExitMode;
    }

    #region Virtual Functions

    protected virtual void PrePush()
    {
        gameObject.SetActive(true);
        PrePushAction?.Invoke();
    }

    protected virtual void PostPush()
    {
        PostPushAction?.Invoke();
    }

    protected virtual void PrePop()
    {
        PrePopAction?.Invoke();
    }

    protected virtual void PostPop()
    {
        PostPopAction?.Invoke();
        gameObject.SetActive(false);
    }

    #endregion

    protected virtual void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
        CanvasGroup = GetComponent<CanvasGroup>();

        _baseEntryDirection = _entryDirection;
        _baseExitDirection = _exitDirection;
        _baseEntryMode = _entryMode;
        _baseExitMode = _exitMode;
    }

    //public override void PushThis()
    //{
    //    WindowController.Instance.PushWindow<T>();
    //}

    public void Enter(bool ignoreAnim = false)
    {
        PrePush();

        if (_bluredBG != null)
        {
            _bluredBG.SetActive(true);
        }

        if (ignoreAnim)
        {
            PostPush();
            return;
        }

        switch (_entryMode)
        {
            case PageEntryMode.NONE:
                PostPush();
                break;
            case PageEntryMode.SLIDE:
                SlideIn();
                break;
            case PageEntryMode.ZOOM:
                ZoomIn();
                break;
            case PageEntryMode.FADE:
                FadeIn();
                break;
        }
    }

    public void Exit(bool ignoreAnim = false)
    {
        PrePop();

        if (_bluredBG != null)
        {
            _bluredBG.SetActive(false);
        }

        if (ignoreAnim)
        {
            PostPop();
            return;
        }

        switch (_exitMode)
        {
            case PageEntryMode.NONE:
                PostPop();
                break;
            case PageEntryMode.SLIDE:
                SlideOut();
                break;
            case PageEntryMode.ZOOM:
                ZoomOut();
                break;
            case PageEntryMode.FADE:
                FadeOut();
                break;
        }
    }

    #region Animations

    private void SlideIn()
    {
        StopRoutineIfActive(_animationCoroutine);
        _animationCoroutine =
            StartCoroutine(PageAnimationHelper.SlideIn(RectTransform, _entryDirection, _animationSpeed, PostPush));
    }

    private void SlideOut()
    {
        StopRoutineIfActive(_animationCoroutine);
        _animationCoroutine =
            StartCoroutine(PageAnimationHelper.SlideOut(RectTransform, _exitDirection, _animationSpeed, PostPop));
    }

    private void ZoomIn()
    {
        StopRoutineIfActive(_animationCoroutine);
        _animationCoroutine = StartCoroutine(PageAnimationHelper.ZoomIn(RectTransform, _animationSpeed, PostPush));
    }

    private void ZoomOut()
    {
        StopRoutineIfActive(_animationCoroutine);
        _animationCoroutine = StartCoroutine(PageAnimationHelper.ZoomOut(RectTransform, _animationSpeed, PostPop));
    }

    private void FadeIn()
    {
        StopRoutineIfActive(_animationCoroutine);
        _animationCoroutine = StartCoroutine(PageAnimationHelper.FadeIn(CanvasGroup, _animationSpeed, PostPush));
    }

    private void FadeOut()
    {
        StopRoutineIfActive(_animationCoroutine);
        _animationCoroutine = StartCoroutine(PageAnimationHelper.FadeOut(CanvasGroup, _animationSpeed, PostPop));
    }

    #endregion

    private void StopRoutineIfActive(Coroutine routine)
    {
        if (routine != null)
            StopCoroutine(routine);
    }
}
