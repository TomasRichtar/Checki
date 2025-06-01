using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace Richi
{
    public class Localization : MonoBehaviour
    {
        private bool _active = false;
        private int _locale = 0; // 0 == Czech

        private void Start()
        {
            _locale = PlayerPrefs.GetInt("LocaleKey");
        }

        public void ChangeLocales()
        {
            if (_active) return;

            _locale = _locale == 0 ? 1 : 0;

            StartCoroutine(SetLocale());
        }

        IEnumerator SetLocale()
        {
            _active = true;
            yield return LocalizationSettings.InitializationOperation;

            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_locale];
            PlayerPrefs.SetInt("LocaleKey", _locale);

            _active = false;
        }
    }
}
