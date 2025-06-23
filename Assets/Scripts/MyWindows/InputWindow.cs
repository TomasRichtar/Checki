using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class InputWindow : BaseWindow
{
    [Header("Buttons")]
    [SerializeField] private Button _yesBtn;
    [SerializeField] private Button _noBtn;

    [Header("Texts")]
    [SerializeField] private LocalizeStringEvent _titleTextLocalizer;
    [SerializeField] private LocalizeStringEvent _yesTextLocalizer;
    [SerializeField] private LocalizeStringEvent _noTextLocalizer;
    [SerializeField] private LocalizeStringEvent _inputFieldOneTextLocalizer;
    [SerializeField] private LocalizeStringEvent _inputFieldTwoTextLocalizer;

    [Header("Inputs")]
    [SerializeField] private TMP_InputField _inputFieldOne;
    [SerializeField] private TMP_InputField _inputFieldTwo;

    public void InitFunction(Action<string, string> yesAction, Action noAction)
    {
        _yesBtn.onClick.AddListener(() =>
        {
            string inputOne = _inputFieldOne.text;
            string inputTwo = "";
            if (_inputFieldTwo != null)
            {
                inputTwo = _inputFieldTwo.text;
            }

            yesAction?.Invoke(inputOne, inputTwo);
            Exit();
            Destroy(gameObject, _animationSpeed);
        });

        _noBtn.onClick.AddListener(() =>
        {
            noAction?.Invoke();
            Exit();
            Destroy(gameObject, _animationSpeed);
        });
    }

    public void SetTexts(string title, string yesText, string noText, string InputOne, string InputTwo = null)
    {
        _titleTextLocalizer.StringReference.TableReference = "PopUpWindow";
        _titleTextLocalizer.StringReference.TableEntryReference = title;

        _yesTextLocalizer.StringReference.TableReference = "PopUpWindow";
        _yesTextLocalizer.StringReference.TableEntryReference = yesText;

        _noTextLocalizer.StringReference.TableReference = "PopUpWindow";
        _noTextLocalizer.StringReference.TableEntryReference = noText;

        _inputFieldOneTextLocalizer.StringReference.TableReference = "PopUpWindow";
        _inputFieldOneTextLocalizer.StringReference.TableEntryReference = InputOne;

        if (InputTwo != null)
        {
            _inputFieldTwoTextLocalizer.StringReference.TableReference = "PopUpWindow";
            _inputFieldTwoTextLocalizer.StringReference.TableEntryReference = InputTwo;
        }
    }
}