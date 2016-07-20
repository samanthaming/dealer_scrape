﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace DemoWebApp.Tests
{
    [TestFixture]
    public class PlanTests
    {
        private IWebDriver _driver;

        [SetUp]
        public void BeforeEachTest()
        {
            _driver = new FirefoxDriver();
            _driver.Navigate().GoToUrl("https://www.rogers.com/consumer/wireless/smartphone-plans");
            _driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void AfterEachTest()
        {
            _driver.Quit();
        }

        [Test]
        public void ScrapeRogerSiteAllPlans()
        {
            string region = "BC";

            // Select Region
            _driver.FindElement(By.ClassName("dropdown-toggle")).Click();
            _driver.FindElement(By.Id(region)).Click();

            // Solution for "StaleElementReference"
            // maybe there's a better solution?
            _driver.Navigate().Refresh();

            // Create Tab and Calling Names
            // (improve: scrape tab/calling name instead of hardcode)
            string[] tabs = new string[] {"tab1", "tab2", "tab3", "tab4", "tab5"};
            string[] callings = new string[] {"canada", "local"};

            // Scrape Data
            Plans scrapePlans = new Plans();

            foreach (var tab in tabs)
            {
                foreach (var calling in callings)
                {
                    var elements = _driver.FindElements(By.CssSelector($"[data-slider='{tab}'] .share-{calling} .tile"));
                    foreach (var element in elements)
                    {
                        scrapePlans.AddPlan(
                            element.GetAttribute("data-name"), 
                            element.GetAttribute("data-data"), 
                            element.GetAttribute("data-price"), 
                            element.GetAttribute("data-term"),
                            tab,
                            calling,
                            $"{tab}{calling}{element.GetAttribute("data-data")}" // id, must be unique
                        );
                    }
                }
            }

            // Combine All Province Plans 
            Dictionary<string, List<Plan>> combinedPlans = new Dictionary<string, List<Plan>>()
            {
                {region, scrapePlans.PlanList}
                //{"AB", programsAB.PlanList }
            };

            // Setting up the Path
            string basePath = @"C:\Users\Public\Happy";
            string combinedDirectory = Path.Combine(basePath, region);
            string filePath = string.Concat(DateTime.Now.ToFileTime().ToString(), ".json");
            string combinedPath = Path.Combine(basePath, region, filePath); 

            // Create Directory if it doesn't exists
            Directory.CreateDirectory(combinedDirectory);

            // Create file  if it doesn't exists
            if (!System.IO.File.Exists(combinedPath))
            {
                using (StreamWriter file = File.CreateText(combinedPath))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, combinedPlans);
                }
            }
            else
            {
                Console.WriteLine("File \"{0}\" already exists.", combinedPath);
                return;
            }
        }
    }
}
