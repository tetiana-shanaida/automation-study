﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Xml.Linq;

namespace BDD3.PageObject
{
    public class BaseClass
    {
        private IWebDriver webdriver;
        private WebDriverWait wait;

        public BaseClass(IWebDriver driver)
        {
            this.webdriver = driver;
            wait = new WebDriverWait(webdriver, TimeSpan.FromSeconds(10));
        }

        protected string CategoryByName (string name) => $"//h5[text()='{name}']//ancestor::div[contains(@class, 'card')]";
        protected string SectionByName(string name) => $"//span[text()='{name}']//ancestor::li";

        public IWebElement FindElement(string locator)
        {
            var element = wait.Until(e => e.FindElement(By.XPath(locator)));
            return element;
        }

        public void Click(string locator)
        {
            wait.Until(e => e.FindElement(By.XPath(locator)));
            FindElement(locator).Click();
        }

        public void FillInputField(string locator, string text)
        {
            var element = wait.Until(e => e.FindElement(By.XPath(locator)));
            element.Clear();
            element.SendKeys(text);
        }

        //public string GetElementAttribute(string locator, string attribute)
        //{
        //    wait.Until(e => e.FindElement(By.XPath(locator)));
        //    return FindElement(locator).GetAttribute(attribute);
        //}

        public string GetElementText(string locator)
        {
            wait.Until(e => e.FindElement(By.XPath(locator)));
            return FindElement(locator).Text;
        }
    }
}
