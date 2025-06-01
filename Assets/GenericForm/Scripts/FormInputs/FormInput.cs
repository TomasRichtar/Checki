public interface IFormInput
{
    void InitInput(FormComponentConfig data);
    string GetInput();
    void ShowError(string errorMsg);
}
