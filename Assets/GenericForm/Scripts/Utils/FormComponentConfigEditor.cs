using UnityEngine;

[System.Serializable]
public struct FormComponentConfigEditor
{
    [Tooltip("Name of form component")]public string Name;
    [Tooltip("Type of form component")]public FormComponentType Type;
    [Tooltip("Config for Dropdown Type Component")]public DropdownConfig DropdownConfig;
    [Tooltip("Should be input validated")]public bool Validation;
    [Tooltip("Is input mandatory")]public bool Mandatory;
}

