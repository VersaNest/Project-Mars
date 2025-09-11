using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Reqnroll;
using Reqnroll.BoDi;
using Reqnroll_ProjectMars.Pages;
using Reqnroll_ProjectMars.Utilities;

namespace Reqnroll_ProjectMars.Hooks
{
    [Binding]
    public class ProjectMarsHooks
    {
        private readonly IObjectContainer _container;


        public ProjectMarsHooks(IObjectContainer container) => _container = container;

        [BeforeScenario]
        public void BeforeScenario()
        {
            var options = new ChromeOptions();
            options.AddArgument("start-maximized");

            var driver = new ChromeDriver(options);
            _container.RegisterInstanceAs<IWebDriver>(driver);


            var settings = ConfigReader.LoadSettings();
            driver.Navigate().GoToUrl(settings.BaseUrl);
        }



        [AfterScenario]
        public void AfterScenario()
        {
            _container.Resolve<IWebDriver>().Quit();
        }

        [AfterFeature]
        public static void AfterFeature()
        {
            try
            {
                //Clean up of all languages and skills from the profile after testing

                var settings = ConfigReader.LoadSettings();
                
                var options = new ChromeOptions();
                options.AddArgument("start-maximized");

                using var driver = new ChromeDriver(options);
                driver.Navigate().GoToUrl(settings.BaseUrl);

                var profilePage = new ProfilePage(driver);
                var loginPage = new LoginPage(driver);


                loginPage.signFromMain();
                loginPage.userLogin(settings.Username, settings.Password);



                profilePage.WaitUntilLanguagesTabVisible();
                profilePage.DeleteAllLanguages();

                profilePage.clickSkillsTab();
                profilePage.WaitUntilSkillsTabContentVisible();
                profilePage.DeleteAllSkills();



            }
            catch (Exception ex)
            {
                Console.WriteLine($"Cleanup failed after feature: {ex.Message}");
            }
        }
    }
}
