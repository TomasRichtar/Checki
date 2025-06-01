namespace GenericForm
{
    public abstract class InputFormComponent : IFormComponent
    {
        private bool _mandatory;
        private bool _validation;
        private IFormInput _formInput;

        protected InputFormComponent(bool mandatory, bool validation, IFormInput inputField)
        {
            _mandatory = mandatory;
            _validation = validation;
            _formInput = inputField;
        }
        
        public Result<string> GetInput()
        {
            if (string.IsNullOrWhiteSpace(_formInput.GetInput()) && _mandatory)
                return Result<string>.Failure("Toto políčko nemůže být prázdné");

            return _validation
                ? Regex(_formInput.GetInput())
                : Result<string>.Success(_formInput.GetInput());
        }

        public void DisplayError(string error)
        {
            _formInput.ShowError(error);
        }

        protected abstract Result<string> Regex(string input);
    }
}
