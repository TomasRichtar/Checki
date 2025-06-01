using MedaWars.Combat.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class InformationWindow : BaseWindow
{
    [Header("Buttons")]
    [SerializeField] protected Button _yesBtn;

    [Header("Texts")]
    [SerializeField] protected LocalizeStringEvent _titleTextLocalizer;
    [SerializeField] protected LocalizeStringEvent _baseTextLocalizer;
    [SerializeField] protected LocalizeStringEvent _yesTextLocalizer;


    public virtual void InitFunction(Action yesAction)
    {
        _yesBtn.onClick.AddListener(() =>
        {
            yesAction?.Invoke();
            Exit();
            Destroy(gameObject, _animationSpeed);
        });
    }

    public void SetTexts(string title, string body, string yesText)
    {
        _titleTextLocalizer.StringReference.TableReference = "PopUpWindow";
        _titleTextLocalizer.StringReference.TableEntryReference = title;


        _baseTextLocalizer.StringReference.TableReference = "PopUpWindow";
        _baseTextLocalizer.StringReference.TableEntryReference = body;

        _yesTextLocalizer.StringReference.TableReference = "PopUpWindow";
        _yesTextLocalizer.StringReference.TableEntryReference = yesText;
    }
}
