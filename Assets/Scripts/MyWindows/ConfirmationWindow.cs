using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization.Components;
using Richi;

namespace MedaWars.Combat.UI
{
    public class ConfirmationWindow : BaseWindow
    {
        [Header("Buttons")]
        [SerializeField] protected Button _yesBtn;
        [SerializeField] protected Button _noBtn;

        [Header("Texts")]
        [SerializeField] protected LocalizeStringEvent _titleTextLocalizer;
        [SerializeField] protected LocalizeStringEvent _baseTextLocalizer;
        [SerializeField] protected LocalizeStringEvent _yesTextLocalizer;
        [SerializeField] protected LocalizeStringEvent _noTextLocalizer;

        public virtual void InitFunction(Action yesAction, Action noAction)
        {
            _yesBtn.onClick.AddListener(() =>
            {
                yesAction?.Invoke();
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

        public void SetTexts(string title, string body, string yesText, string noText)
        {
            _titleTextLocalizer.StringReference.TableReference = "PopUpWindow";
            _titleTextLocalizer.StringReference.TableEntryReference = title;


            _baseTextLocalizer.StringReference.TableReference = "PopUpWindow";
            _baseTextLocalizer.StringReference.TableEntryReference = body;

            _yesTextLocalizer.StringReference.TableReference = "PopUpWindow";
            _yesTextLocalizer.StringReference.TableEntryReference = yesText;

            _yesTextLocalizer.StringReference.TableReference = "PopUpWindow";
            _noTextLocalizer.StringReference.TableEntryReference = noText;
        }
    }
}
