using UnityEngine;

[CreateAssetMenu(menuName = "GenericForm/Create dropdown config",fileName = "DropdownConfig")]
public class DropdownConfig : ScriptableObject
{
    [SerializeField] private TextAsset _dropdownTexts;
    [SerializeField] private TextAsset _dropdownValues;

    public TextAsset Names => _dropdownTexts;
    public TextAsset Values => _dropdownValues;
}
