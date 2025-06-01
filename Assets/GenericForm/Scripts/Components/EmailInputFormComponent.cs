using TMPro;

namespace GenericForm
{
    public class EmailInputFormComponent : InputFormComponent
    {
        public EmailInputFormComponent(bool mandatory,bool validation, IFormInput inputField) : base(mandatory, validation,inputField) { }
        
        protected override Result<string> Regex(string input)
        {
            /*
            var isEmail = new Regex(
                @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
                RegexOptions.IgnoreCase);
            */
            
            return input.Contains("@")
                ? Result<string>.Success(input)
                : Result<string>.Failure("Email musí obsahovat @");
        }
    }
}