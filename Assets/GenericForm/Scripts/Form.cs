
using System.Collections.Generic;

namespace GenericForm
{
   public class Form
   {
      private bool _correct;
      private Dictionary<string, string> _formData;

      private Dictionary<FormComponentConfig, IFormComponent> _componentsData;
      
      public Form(List<FormComponentConfig> formConfig)
      {
         _correct = false;
         _formData = new Dictionary<string, string>();
         
         _componentsData = new Dictionary<FormComponentConfig, IFormComponent>();
         
         foreach (var component in formConfig)
         {
            _componentsData.Add(component, CreateForm(component));
         }
         
         IFormComponent CreateForm(FormComponentConfig formObject)
         {
            return formObject.Type switch
            {
               FormComponentType.Text => new TextInputFormComponent(formObject.Mandatory,formObject.Validation, formObject.Input),
               FormComponentType.Email => new EmailInputFormComponent(formObject.Mandatory,formObject.Validation,  formObject.Input),
               FormComponentType.Number => new NumberComponent(formObject.Mandatory, formObject.Validation, formObject.Input),
               FormComponentType.Telephone => new TelephoneComponent(formObject.Mandatory,formObject.Validation,  formObject.Input),
               FormComponentType.Zipcode => new ZipcodeComponent(formObject.Mandatory,formObject.Validation,  formObject.Input),
               FormComponentType.Dropdown =>  new DropdownFormComponent(formObject.Mandatory,formObject.Validation,  formObject.Input),
               _ => null
            };
         }
      }
      
      public Result<Dictionary<string, string>> ValidateForm()
      {
         _correct = true;
         _formData.Clear();
         
         foreach (var formComponent in _componentsData)
         {
            var result = formComponent.Value.GetInput();
            if (result.IsSuccess)
            {
               _formData.Add(formComponent.Key.Name, result.Value);
            }
            else
            {
               _correct = false;
               formComponent.Value.DisplayError(result.ErrorMessage);
            }
         }

         return FormValues();
      }

      public Result<Dictionary<string, string>> FormValues()
      {
         return _correct
            ? Result<Dictionary<string, string>>.Success(_formData)
            : Result<Dictionary<string, string>>.Failure("Validation was not successful");
      }
   }
}