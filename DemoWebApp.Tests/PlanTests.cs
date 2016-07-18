using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
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
            _driver.Navigate().GoToUrl("https://www.rogers.com/consumer/wireless/share-everything");
        }

        [TearDown]
        public void AfterEachTest()
        {
            _driver.Quit();
        }

        [Test]
        public void ScrapSite()
        {
            string region = "BC";

            // Select Region
            _driver.FindElement(By.ClassName("dropdown-toggle")).Click();
            _driver.FindElement(By.Id(region)).Click();

            // Solution for "StaleElementReference"
            // maybe there's a better solution?
            _driver.Navigate().Refresh();

            // Scrape Plans (BC for now...)
            Plans scrapePlans = new Plans();


            var scrapePackageNames = _driver.FindElements(By.ClassName("package-data"));
            var scrapePrices = _driver.FindElements(By.ClassName("price-dollars"));

            var scrapePackagesAndPrices = scrapePackageNames.Zip(scrapePrices, (package, price) =>
                new { PackageName = package, Price = price });

            foreach (var plan in scrapePackagesAndPrices)
            {
                scrapePlans.AddPlan(region, plan.PackageName.Text, plan.Price.Text);
            }

            // Add Mock AB Plans
            //Plans programsAB = new Plans();
            //programsAB.AddPlan("AB", "2GB", "300");
            //programsAB.AddPlan("AB", "4GB", "300");
            //programsAB.AddPlan("AB", "6GB", "300");


            // Combine the Plans 
            Dictionary<string, List<Plan>> combinedPlans = new Dictionary<string, List<Plan>>()
            {
                {region, scrapePlans.PlanList},
                //{"AB", programsAB.PlanList }
            };


            // Copy Today's Data to Yesterday's Data

            string sourceFile = @"C:\Users\Public\TestFolder\Today.json";
            string destFile = @"C:\Users\Public\TestFolder\Yesterday.json";

            if (File.Exists(sourceFile))
            {
                File.Copy(sourceFile, destFile, true);
            }
            
            // Write New data to Today
            using (StreamWriter file = File.CreateText(@"C:\Users\Public\TestFolder\Today.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, combinedPlans);
            }
        }
    }

}
