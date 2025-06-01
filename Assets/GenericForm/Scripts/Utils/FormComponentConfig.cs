[System.Serializable]
public struct FormComponentConfig
{
    public string Name;
    public FormComponentType Type;
    public DropdownConfig DropdownConfig;
    public bool Validation;
    public bool Mandatory;
    public IFormInput Input;
}

