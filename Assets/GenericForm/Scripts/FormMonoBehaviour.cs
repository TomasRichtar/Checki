using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace GenericForm
{
    public class FormMonoBehaviour : MonoBehaviour
    {
        [Header("Graphics")]
        [Tooltip("Prefab of Layout parent object")]
        [SerializeField] public LayoutGroup _layoutPrefab;
        
        [Tooltip("Prefab of input components")]
        [SerializeField] private FormTextInput _inputComponentPrefab;
        
        [Tooltip("Prefab of dropdown components")]
        [SerializeField] private FormDropdown _dropdownComponentPrefab;
        
        [Tooltip("Prefab of confirmation button")]
        [SerializeField] private Button _confirmButtonPrefab;

        [Header("Form Fields")] 
        [SerializeField] private List<FormComponentConfigEditor> _components;
        
        private Transform _content;
        private Transform _componentsParent;

        private Form _form;
        
        void Start()
        {
            _content = GetComponentInChildren<ScrollRect>().content;
            
            // Create Layout
            _componentsParent = Instantiate(_layoutPrefab, _content).transform;
            
            // Generate form

            var formConfig = new List<FormComponentConfig>();
            foreach (var component in _components)
            {
                formConfig.Add(GetConfig(component));
            }

            _form = new Form(formConfig);
            
            Instantiate(_confirmButtonPrefab,_content).onClick.AddListener(ButtonClick);
            StartCoroutine(ScrollRectInit());
            
            // This part is little bit ugly,
            // but its because i wanted to completely separate "unity" from Form
            FormComponentConfig GetConfig(FormComponentConfigEditor componentObject) 
            {
                var inputInstance = GetInput(componentObject.Type);
                
                var config = new FormComponentConfig
                {
                    Name = componentObject.Name,
                    Type = componentObject.Type,
                    DropdownConfig = componentObject.DropdownConfig,
                    Validation = componentObject.Validation,
                    Mandatory = componentObject.Validation,
                    Input = inputInstance
                };
                
                inputInstance.InitInput(config);
                return config;
            }
            
            IFormInput GetInput(FormComponentType inputType)
            {
                // For know there are only 2 choices
                // Change to switch when more

                return inputType == FormComponentType.Dropdown
                    ? (IFormInput)Instantiate(_dropdownComponentPrefab, _componentsParent)
                    : (IFormInput)Instantiate(_inputComponentPrefab, _componentsParent);
            }

            IEnumerator ScrollRectInit()
            {
                yield return new WaitForEndOfFrame();
                GetComponentInChildren<ScrollRect>().ScrollToTop();
            }
        }

        private void OnValidate()
        {
            var groups = _components.GroupBy(x => x.Name);
            foreach (var g in groups)
            {
                if (g.Count() == 1) continue;
                Debug.LogError($"Items in form cannot share name. There is {g.Count()}x {g.Key}");
            }
        }

        private void ButtonClick()
        {
            var result = _form.ValidateForm();
            if (result.IsSuccess)
            {
                foreach (var formValue in result.Value)
                {
                    Debug.Log($"{formValue.Key} - {formValue.Value}");
                }
            }
            else
            {
                Debug.Log(result.ErrorMessage);
            }
        }

       
    }
}

