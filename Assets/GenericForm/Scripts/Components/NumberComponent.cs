using System.Text.RegularExpressions;
using TMPro;

namespace GenericForm
{
    public class NumberComponent : InputFormComponent
    {
        public NumberComponent(bool mandatory, bool validation, IFormInput inputField) : base(mandatory,validation, inputField) { }
        
        protected override Result<string> Regex(string input)
        {
            return Result<string>.Success(input);
        }
    }
}