using TMPro;

namespace GenericForm
{
    public class ZipcodeComponent : InputFormComponent
    {
        public ZipcodeComponent(bool mandatory, bool validation,IFormInput inputField) : base(mandatory,validation, inputField) { }
        
        protected override Result<string> Regex(string input)
        {
            // TODO Regex based on Country selection > Selected country must be mandatory
            // https://stackoverflow.com/questions/578406/what-is-the-ultimate-postal-code-and-zip-regex
            
            return Result<string>.Success(input);
        }
    }
}