using System.Text.RegularExpressions;
using TMPro;

namespace GenericForm
{
    public class TextInputFormComponent : InputFormComponent
    {
        public TextInputFormComponent(bool mandatory, bool validation, IFormInput inputField) : base(mandatory, validation,inputField) { }
        
        protected override Result<string> Regex(string input)
        {
            // TODO Diakritika
            
            //var isText = new Regex(@"^[a-zA-Z ]*$", RegexOptions.IgnoreCase);
            var isText = new Regex(@"^\p{L}*$", RegexOptions.IgnoreCase);
            return isText.IsMatch(input) 
                ? Result<string>.Success(input) 
                : Result<string>.Failure("Může obsahovat pouze znaky");
        }
    }
}
