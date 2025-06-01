using TastyCore._Examples.UiManager.Scripts;
using Richi;

public class ExampleScanPage : Page
{
   public void Settings()
   {
      PageController.PushPage<ExampleSettingsPage>(new ExampleOptionPageData
      {
         ExampleDebugOption = "Data passing does work"
      });
   }

   protected override void InitData(IPageData data) { }
}
