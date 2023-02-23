using NUnit.Framework;
using NunitJsonData.DataConfig;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace NunitJsonData.TestCases;
public class Demo
{

    IWebDriver driver;

    [SetUp]
    public void SetUp()
    {
        new DriverManager().SetUpDriver(new ChromeConfig());

        driver = new ChromeDriver();
        driver.Url = "https://www.saucedemo.com/";
        driver.Manage().Window.Maximize();
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
    }

    [TestCaseSource(typeof(CredentialDataSource<Credential>))]
    public void TC_01(Credential credential)
    {
        driver.FindElement(By.CssSelector("#user-name")).SendKeys(credential.Username);

        driver.FindElement(By.CssSelector("#password")).SendKeys(credential.Password);

        driver.FindElement(By.CssSelector("#login-button")).Click();

        Assert.That(driver.FindElement(By.CssSelector("span.title")).Displayed, Is.True); 
    }

    [TearDown]
    public void TearDown()
    {
        if (driver is not null)
        {
            driver.Manage().Cookies.DeleteAllCookies();
            driver.Quit();
        }
    }
}