namespace GenericForm
{
    public interface IFormComponent
    {
        public Result<string> GetInput();

        public void DisplayError(string error);
    }
}
