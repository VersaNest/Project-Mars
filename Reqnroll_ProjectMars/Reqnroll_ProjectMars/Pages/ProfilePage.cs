using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Reqnroll_ProjectMars.Utilities;

namespace Reqnroll_ProjectMars.Pages
{
    public class ProfilePage
    {
        private readonly IWebDriver newDriver;
        private readonly BasePage basePage;

        IWebElement addNewButton;

        public ProfilePage(IWebDriver driver)
        {
            newDriver = driver;
            basePage = new BasePage(driver);
        }

        //Methods related to Languages tab

        public void ClickLanguageTab()
        {
            IWebElement languagesTabInput = basePage.WaitUntilClickable(By.XPath("//a[contains(text(), 'Languages')]"));
            languagesTabInput.Click();
        }

        public IWebElement? FindAddButton()
        {
            try
            {
                return newDriver.FindElement(By.XPath("//div[contains(text(),'Add New')]"));
            }
            catch (NoSuchElementException)
            {
                return null; 
            }
        }
        public void AddNewLanguages(string langName, string langLevel)
        {

            
            addNewButton = FindAddButton();
            addNewButton.Click();

            IWebElement languageName = basePage.WaitUntilVisible(By.XPath("//input[@placeholder='Add Language']"));
            languageName.Clear();
            languageName.SendKeys(langName);

            IWebElement levelDropdown = basePage.WaitUntilVisible(By.XPath("//select[@name='level']"));
            SelectElement selectLevel = new SelectElement(levelDropdown);
            if (!string.IsNullOrWhiteSpace(langLevel))
            {
                selectLevel.SelectByText(langLevel);
            }

            IWebElement addButton = basePage.WaitUntilClickable(By.XPath("//input[@value='Add']"));
            addButton.Click();
        }


        public bool IsAddButtonVisible(int timeoutInSeconds = 10)
        {
            try
            {
                return new WebDriverWait(newDriver, TimeSpan.FromSeconds(timeoutInSeconds)).Until(drv =>
                {
                    var button = FindAddButton();
                    if (button != null && button.Displayed)
                    {                        
                        return true;
                    }
                    return false;
                });
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        public bool IsAddButtonNotVisible(int timeoutInSeconds = 10)
        {
            try
            {
                return new WebDriverWait(newDriver, TimeSpan.FromSeconds(timeoutInSeconds)).Until(drv =>
                {
                    try
                    {
                        var button = FindAddButton();
                        return button == null || !button.Displayed;
                    }
                    catch (NoSuchElementException)
                    {
                        return true; 
                    }
                    catch (StaleElementReferenceException)
                    {
                        return true; 
                    }
                });
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }
        public string UpdateLanguage(string langName, string newLang, string newLevel)
        {
            var rowsCollection = basePage.WaitUntilVisibleAll(By.XPath("//table/tbody/tr"));
            var rows = rowsCollection.ToList();


            for (int i = 0; i < rows.Count; i++)
            {
                IWebElement languageName = rows[i].FindElement(By.XPath("./td[1]"));

                if (languageName.Text.Equals(langName, StringComparison.OrdinalIgnoreCase))
                {
                    IWebElement updateIcon = rows[i].FindElement(By.XPath("./td[3]//i[contains(@class,'outline write')]"));
                    updateIcon.Click();

                    IWebElement langTextBox = basePage.WaitUntilVisible(By.XPath("//input[@placeholder='Add Language']"));
                    langTextBox.Clear();
                    langTextBox.SendKeys(newLang);

                    IWebElement levelDropdown = basePage.WaitUntilVisible(By.XPath("//select[@name='level']"));
                    SelectElement selectLevel = new SelectElement(levelDropdown);
                    selectLevel.SelectByText(newLevel);

                    IWebElement updateButton = basePage.WaitUntilClickable(By.XPath("//input[@value='Update']"));
                    updateButton.Click();

                    var updatedRows = basePage.WaitUntilVisibleAll(By.XPath("//table/tbody/tr")).ToList();
                    IWebElement updatedLanguage = rows[i].FindElement(By.XPath("./td[1]"));
                    return updatedLanguage.Text;
                }
            }
            return "Failed";
        }

        public string DeleteLanguage(string langName)
        {

            var rowsCollection = basePage.WaitUntilVisibleAll(By.XPath("//table/tbody/tr"));
            var rows = rowsCollection.ToList();

            for (int i = 0; i < rows.Count; i++)
            {
                IWebElement languageName = rows[i].FindElement(By.XPath("./td[1]"));

                if (languageName.Text.Equals(langName, StringComparison.OrdinalIgnoreCase))
                {
                    IWebElement deleteIcon = rows[i].FindElement(By.XPath("./td[3]//i[contains(@class,'remove')]"));
                    deleteIcon.Click();
                    return "Success"; 
                }
            }
            return "Failed";
        }


        public string CheckDuplicateLanguage(string name, string level)
        {
            return CheckDuplicateInfo(name, level, By.XPath("//table/tbody/tr"));
        }


        //Methods related to skills tab


        public void ClickSkillsTab()
        {
            IWebElement skillsTabInput = basePage.WaitUntilClickable(By.XPath("//a[@data-tab='second']"));
            skillsTabInput.Click();
        }

        public void AddNewSkill(string skillName, string skillLevel)
        {
            
            WaitUntilSkillsTabContentVisible();

            IWebElement addNewButton = basePage.WaitUntilClickable(By.XPath("//div[@data-tab='second']//div[contains(text(), 'Add New')]"));
            addNewButton.Click();


            IWebElement newSkill = basePage.WaitUntilVisible(By.XPath("//input[@placeholder='Add Skill']"));
            newSkill.Clear();
            newSkill.SendKeys(skillName);

            IWebElement levelDropdown = basePage.WaitUntilVisible(By.XPath("//select[@name='level']"));
            SelectElement selectLevel = new SelectElement(levelDropdown);

            if (!string.IsNullOrWhiteSpace(skillLevel))
            {
                selectLevel.SelectByText(skillLevel);
            }

            IWebElement addButton = basePage.WaitUntilClickable(By.XPath("//input[@value='Add']"));
            addButton.Click();
        }

        public string UpdateSkill(string oldSkillName, string newSkillName, string newSkillLevel)
        {

            var rowsCollection = basePage.WaitUntilVisibleAll(By.XPath("//div[@data-tab='second']//table/tbody/tr"));
            var rows = rowsCollection.ToList();

            for (int i = 0; i < rows.Count; i++)
            {
                IWebElement skillName = rows[i].FindElement(By.XPath("./td[1]"));

                if (skillName.Text.Equals(oldSkillName, StringComparison.OrdinalIgnoreCase))
                {
                    IWebElement updateIcon = rows[i].FindElement(By.XPath("./td[3]//i[contains(@class,'outline write')]"));
                    updateIcon.Click();

                    IWebElement skillTextBox = basePage.WaitUntilVisible(By.XPath("//div[@data-tab='second']//input[@placeholder='Add Skill']"));
                    skillTextBox.Clear();
                    skillTextBox.SendKeys(newSkillName);

                    IWebElement levelDropdown = basePage.WaitUntilVisible(By.XPath("//div[@data-tab='second']//select[@name='level']"));
                    SelectElement selectLevel = new SelectElement(levelDropdown);
                    selectLevel.SelectByText(newSkillLevel);

                    IWebElement updateButton = basePage.WaitUntilClickable(By.XPath("//div[@data-tab='second']//input[@value='Update']"));
                    updateButton.Click();

                    var updatedRows = basePage.WaitUntilVisibleAll(By.XPath("//div[@data-tab='second']//table/tbody/tr")).ToList();
                    IWebElement updatedSkill = rows[i].FindElement(By.XPath("./td[1]"));
                    return updatedSkill.Text;
                }
            }
            return "Failed";

        }

        public string DeleteSkill(string oldSkillName)
        {

            var rowsCollection = basePage.WaitUntilVisibleAll(By.XPath("//div[@data-tab='second']//table/tbody/tr"));
            var rows = rowsCollection.ToList();

            for (int i = 0; i < rows.Count; i++)
            {
                IWebElement skillName = rows[i].FindElement(By.XPath("./td[1]"));

                if (skillName.Text.Equals(oldSkillName, StringComparison.OrdinalIgnoreCase))
                {
                    IWebElement deleteIcon = rows[i].FindElement(By.XPath("./td[3]//i[contains(@class,'remove')]"));
                    deleteIcon.Click();
                    return "Success";
                }
            }
            return "Failed"; 
        }


        public string CheckDuplicateSkill(string name, string level)
        {
            return CheckDuplicateInfo(name, level, By.XPath("//div[@data-tab='second']//table/tbody/tr"));
        }



        //Methods common for both languages and skills tab


        public string CheckDuplicateInfo(string name, string level, By rowLocator)
        {
            var rowsCollection = basePage.WaitUntilVisibleAll(rowLocator);

            var rows = rowsCollection.ToList();

            for (int i = 0; i < rows.Count; i++)
            {
                IWebElement dataName = rows[i].FindElement(By.XPath("./td[1]"));

                if (dataName.Text.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    IWebElement dataLevel = rows[i].FindElement(By.XPath("./td[2]"));
                    if (dataLevel.Text.Equals(level, StringComparison.OrdinalIgnoreCase))
                    {
                        return "Both duplicate";
                    }
                    else
                        return "Name duplicate";

                }
            }
            return "Original";

        }
        public string RetreiveMessage()
        {
            IWebElement displayedMessage = basePage.WaitUntilVisible(By.XPath("//div[@class='ns-box-inner']"));
            return displayedMessage.Text;
        }
        public string VerifyDetailsDisplayed(int columnIndex)
        {
            IWebElement lastRow = basePage.WaitUntilVisible(By.XPath($"//table/tbody[last()]/tr[last()]/td[{columnIndex}]"));
            return lastRow.Text;

        }


        //Methods for cleanup of languages and skills after testing

        public void DeleteAllLanguages()
        {
            ActivateLanguagesTab();
            DeleteAllRows(By.XPath("//div[@data-tab='first' and contains(@class,'active')]//table/tbody/tr"));

        }

        public void ActivateLanguagesTab()
        {
            var languagesTab = newDriver.FindElement(By.XPath("//a[@data-tab='first']"));
            languagesTab.Click();

            basePage.WaitUntilVisible(By.XPath("//div[@data-tab='first' and contains(@class,'active')]"));
        }

        public void WaitUntilLanguagesTabVisible()
        {
            try
            {
                IWebElement languageTab = basePage.WaitUntilVisible(By.XPath("//a[contains(text(), 'Languages')]"));
                languageTab.Click();


            }
            catch (WebDriverTimeoutException)
            {
                throw new Exception("Languages tab did not appear within timeout.");
            }
        }



        public void DeleteAllSkills()
        {
            ActivateSkillsTab();
            DeleteAllRows(By.XPath("//div[@data-tab='second' and contains(@class,'active')]//table/tbody/tr"));

        }
        public void ActivateSkillsTab()
        {
            var skillsTab = newDriver.FindElement(By.XPath("//a[@data-tab='second']"));
            skillsTab.Click();

            basePage.WaitUntilVisible(By.XPath("//div[@data-tab='second' and contains(@class,'active')]"));
     
        }
        public void WaitUntilSkillsTabContentVisible()
        {
            try
            {
                basePage.WaitUntilVisible(By.XPath("//div[@data-tab='second' and contains(@class,'active')]"));

            }
            catch (WebDriverTimeoutException)
            {
                throw new Exception("Skills tab content did not appear within timeout.");
            }
        }


        public void DeleteAllRows(By tableLocator)
        {
            IReadOnlyCollection<IWebElement> rows;

            try
            {
                rows = newDriver.FindElements(tableLocator);
            }
            catch (NoSuchElementException)
            {
                return;
            }

            while (rows.Count > 0)
            {
                try
                {
                    var deleteIcon = rows.First().FindElement(By.XPath(".//i[contains(@class,'remove icon')]"));
                    deleteIcon.Click();

                    try
                    {
                        var message = basePage.WaitUntilVisible(By.CssSelector(".ns-box-inner"), 5);
                        var closeMessage = newDriver.FindElement(By.CssSelector(".ns-close"));
                        closeMessage.Click();
                    }
                    catch (WebDriverTimeoutException)
                    {
                        
                    }

                    rows = newDriver.FindElements(tableLocator);
                }
                catch (StaleElementReferenceException)
                {
                    
                    rows = newDriver.FindElements(tableLocator);
                }
            }
        }


    }
}
