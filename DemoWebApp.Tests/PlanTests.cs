using System;
using System.IO;
using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

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

            // [TO DO] Need to account for when test fails:
            //  - run that specific test again
            //  - or let user know test fails and they manually run it again
            //  - what happens to the file when test fails?
        }
        [Test]
        public void ScrapeRogerPlansForAB()
        {
            scrapeRegion("AB");
        }
        [Test]
        public void ScrapeRogerPlansForBC()
        {
            scrapeRegion("BC");
        }
        [Test]
        public void ScrapeRogerPlansForMB()
        {
            scrapeRegion("MB");
        }
        [Test]
        public void ScrapeRogerPlansForNB()
        {
            scrapeRegion("NB");
        }
        [Test]
        public void ScrapeRogerPlansForNL()
        {
            scrapeRegion("NL");
        }
        [Test]
        public void ScrapeRogerPlansForNS()
        {
            scrapeRegion("NS");
        }
        [Test]
        public void ScrapeRogerPlansForNT()
        {
            scrapeRegion("NT");
        }
        [Test]
        public void ScrapeRogerPlansForNU()
        {
            scrapeRegion("NU");
        }
        [Test]
        public void ScrapeRogerPlansForON()
        {
            scrapeRegion("ON");
        }
        [Test]
        public void ScrapeRogerPlansForPE()
        {
            scrapeRegion("PE");
        }
        [Test]
        public void ScrapeRogerPlansForQC()
        {
            scrapeRegion("QC");
        }
        [Test]
        public void ScrapeRogerPlansForSK()
        {
            scrapeRegion("SK");
        }
        [Test]
        public void ScrapeRogerPlansForYT()
        {
            scrapeRegion("YT");
        }

        public void scrapeRegion(string _region)
        {
            string region = _region;

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
                            $"{tab}{calling}{element.GetAttribute("data-data")}", // id, must be unique
                            region
                        );
                    }
                }
            }

            // Setting up the Path
            string basePath = @"C:\Users\Public\Rogers";
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
                    serializer.Serialize(file, scrapePlans.PlanList);
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
