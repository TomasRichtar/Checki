using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Localization.Components;

public class SelfDestructInformationWindow : BaseWindow
{

    [Header("Texts")]
    [SerializeField] protected LocalizeStringEvent _titleTextLocalizer;


    public virtual void InitFunction(Action action)
    {
        StartCoroutine(DestroyPopUp(action));
    }

    private IEnumerator DestroyPopUp(Action action)
    {
        yield return new WaitForSeconds(2);
        action?.Invoke();
        Exit();
        Destroy(gameObject, _animationSpeed);
    }

    public void SetTexts(string title)
    {
        _titleTextLocalizer.StringReference.TableReference = "PopUpWindow";
        _titleTextLocalizer.StringReference.TableEntryReference = title;
    }
}
