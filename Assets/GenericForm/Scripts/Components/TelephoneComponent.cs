using System.Text.RegularExpressions;
using TMPro;

namespace GenericForm
{
    public class TelephoneComponent : InputFormComponent
    {
        public TelephoneComponent(bool mandatory, bool validation, IFormInput inputField) : base(mandatory, validation,inputField) { }
        
        protected override Result<string> Regex(string input)
        {
            var isNumber = new Regex(
                @"(([+][(]?[0-9]{1,3}[)]?)|([(]?[0-9]{4}[)]?))\s*[)]?[-\s\.]?[(]?[0-9]{1,3}[)]?([-\s\.]?[0-9]{3})([-\s\.]?[0-9]{3,4})",
                RegexOptions.IgnoreCase);

            var errorMsg = input.Contains("+") 
                ? "Není platný formát telefoního čísla" 
                : "Musí obsahovat předčíslí";
                
            return isNumber.IsMatch(input)
                ?  Result<string>.Success(input)
                :  Result<string>.Failure(errorMsg);
            
        }
    }
}
