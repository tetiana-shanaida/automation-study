﻿using BoDi;
using OpenQA.Selenium;

namespace BDD3.PageObject.Widgets
{
    public class AutoComplete:BaseClass
    {
        private IWebDriver webDriver;
        public AutoComplete(IObjectContainer conteiner) : base(conteiner)
        {
            webDriver = conteiner.Resolve<IWebDriver>();
        }

        private string multipleColorNamesInputField = "//input[@id='autoCompleteMultipleInput']";
        private string suggestedOptions = "//div[contains(@class, 'auto-complete__menu')]//descendant::div[contains(@id, 'react-select-2-option')]";
        private string existingColor = "//div[contains(@class, 'auto-complete__multi-value__label')]";
        private string ToRemoveColor (string color) => $"//div[text()='{color}']//following-sibling::div[contains(@class, 'auto-complete__multi-value__remove')]";

        public AutoComplete GoToWidgetsCategory()
        {
            ScrollToElement(CategoryByName("Widgets"));
            Click(CategoryByName("Widgets"));
            Click("//div[text()='Widgets']//following-sibling::div");
            return this;
        }

        public AutoComplete GoToAutoCompleteSection()
        {
            Click(SectionByName("Auto Complete"));
            return this;
        }

        public void EnterLetter(string text)
        {
            FillInputField(multipleColorNamesInputField, text);
        }

        public void EnterColors(string color)
        {
            FillInputField(multipleColorNamesInputField, color);
            Click(suggestedOptions);
        }

        public void DeleteColor(string color)
        {
            Click(ToRemoveColor(color));
        }

        public int GetAmountOfSuggestedOptions()
        {
            IList<IWebElement> elements = FindElements(suggestedOptions);
            return elements.Count;
        }

        public bool CheckSuggestedOptions(string text)
        {
            IList<IWebElement> elements = FindElements(suggestedOptions);
            bool containsText = true;
            foreach (IWebElement element in elements)
            {
                var suggestedText = element.Text;
                containsText = suggestedText.Contains(text, StringComparison.CurrentCultureIgnoreCase);
                if (containsText == false)
                {
                    break;
                }
            }
            return containsText;
        }

        public bool IsColorExistInField(string color) //I thought that the task meant to check that there are those colors that you wrote and no others, but then that method will always accept three values and will not be "universal"
        {
            IList<IWebElement> elements = FindElements(existingColor);
            foreach (var element in elements)
            {
                var existingColor = element.Text;
                if (existingColor == color)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
