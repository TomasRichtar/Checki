using TMPro;
using UnityEngine;

public class FormTextInput : MonoBehaviour, IFormInput
{
    [SerializeField] private TextMeshProUGUI _label;
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private TextMeshProUGUI _errorMessage;
    
    private void Awake()
    {
        if(_label == null)
            Debug.LogError("Dropdown form component is missing label field");
        
        if(_inputField == null)
            Debug.LogError("Dropdown form component is missing dropdown field");

        if (_errorMessage == null)
            Debug.LogError("Dropdown form component is missing errorMessage field");
    }

    
    public void InitInput(FormComponentConfig data)
    {
        _label.text = gameObject.name = data.Name;
        _errorMessage.text = "";
    }

    public string GetInput()
    {
        // Delete error message when trying to get value
        _errorMessage.text = "";
        return _inputField.text;
    }

    public void ShowError(string errorMsg)
    {
        _errorMessage.text = errorMsg;
    }
}
