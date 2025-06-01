using System;

namespace GenericForm
{
    public class DropdownFormComponent : IFormComponent
    {
        private readonly IFormInput _formInput;

        public DropdownFormComponent(bool mandatory, bool validation, IFormInput inputField)
        {
            _formInput = inputField;
        }

        public void DisplayError(string error)
        {
            _formInput.ShowError(error);
        }
        
        public Result<string> GetInput()
        {
            var dropdownValue = _formInput.GetInput();
            return string.IsNullOrWhiteSpace(dropdownValue)
                ? Result<string>.Failure("Musí být vybraná nějaká hodnota")
                : Result<string>.Success(dropdownValue);
        }
        
        /*
        private readonly List<string> _values;
        public DropdownFormComponent(TMP_Dropdown dropdown, DropdownConfig optionDataConfig)
        {
            _dropdown = dropdown;
            _dropdown.ClearOptions();
            
            var values =  optionDataConfig.Values.text.Split('\n');
            _values = values.Select(x => x).ToList();
            
            var names = optionDataConfig.Names.text.Split('\n');
            foreach (var item in names.Select(x => x).ToList())
            {
                _dropdown.options.Add(new TMP_Dropdown.OptionData { text = item });
            }

            _dropdown.value = 0;
        }
        
        public Result<string> GetInput()
        {
            return Result<string>.Success(_values[_dropdown.value]);
        }
        */
    }
}