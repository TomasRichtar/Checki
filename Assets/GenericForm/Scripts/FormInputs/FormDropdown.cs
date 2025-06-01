using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class FormDropdown : MonoBehaviour, IFormInput
{
    [SerializeField] private TextMeshProUGUI _label;
    [SerializeField] private TMP_Dropdown _dropdown;
    [SerializeField] private TextMeshProUGUI _errorMessage;

    private List<string> _values;

    private void Awake()
    {
        if(_label == null)
            Debug.LogError("Dropdown form component is missing label field");
        
        if(_dropdown == null)
            Debug.LogError("Dropdown form component is missing dropdown field");

        if (_errorMessage == null)
            Debug.LogError("Dropdown form component is missing errorMessage field");
    }

    public void InitInput(FormComponentConfig data)
    {
        _label.text = gameObject.name = data.Name;
        _errorMessage.text = "";
        
        _dropdown.ClearOptions();
            
        var values =  data.DropdownConfig.Values.text.Split('\n');
        _values = values.Select(x => x).ToList();
            
        var names = data.DropdownConfig.Names.text.Split('\n');
        foreach (var item in names.Select(x => x).ToList())
        {
            _dropdown.options.Add(new TMP_Dropdown.OptionData { text = item });
        }

        _dropdown.value = 0;
    }
    
    public string GetInput()
    {
        // Delete error message when trying to get value
        _errorMessage.text = "";
        return _values[_dropdown.value];
    }
    
    public void ShowError(string errorMsg)
    {
        _errorMessage.text = errorMsg;
    }
}
