/*
 * Created by Ranorex
 * User: IC014157
 * Date: 6/9/2017
 * Time: 10:52 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Threading;
using WinForms = System.Windows.Forms;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

using AppLayer;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using AppLayer.AppiumService;

namespace STSiOSTouchControl
{
	/// <summary>
	/// Description of iOSSuiteSetup.
	/// </summary>
	[TestModule("5CD759A5-D93E-45FB-A136-BA8E839AD67F", ModuleType.UserCode, 1)]
	public class iOSSuiteSetup : CommonHelper, ITestModule
	{
		string _isOmnitureOn = "False";
		[TestVariable("d23d7b46-d814-48f6-9b0e-6864343084c7")]
		public string isOmnitureOn
		{
			get { return _isOmnitureOn; }
			set { _isOmnitureOn = value; }
		}
		
		/// <summary>
		/// Constructs a new instance.
		/// </summary>
		public iOSSuiteSetup()
		{
			// Do not delete - a parameterless constructor is required!
		}

		/// <summary>
		/// Performs the playback of actions in this module.
		/// </summary>
		/// <remarks>You should not call this method directly, instead pass the module
		/// instance to the <see cref="TestModuleRunner.Run(ITestModule)"/> method
		/// that will in turn invoke this method.</remarks>
		void ITestModule.Run()
		{
			try
			{
				Report.Info("Brand   "+TestSuite.Current.Parameters["Brand"]);
				Report.Info("Device   "+TestSuite.Current.Parameters["Device"]);
				Report.Info("App   "+TestSuite.Current.Parameters["AppName"]);
				string applicationName=TestSuite.Current.Parameters["AppName"];
				string appPath=TestSuite.Current.Parameters["AppPath"];

				KeywordImplementation.LaunchiOSApp(new Uri("http://"+TestSuite.Current.Parameters["Uri"]+":4723/wd/hub"),TestSuite.Current.Parameters["Device"],applicationName,appPath);

				
				if(KeywordImplementation.waitForObjectExist(By.Name("OK"),TimeSpan.FromSeconds(5)))
				{
					KeywordImplementation.Click(By.Name("OK"));
				}
				var size=Accessor.getDriver().Manage().Window.Size;
				
				//Swipe from Bottom to Top and Top to bottom
				//Find swipe start and end point from screen's width and height.
				int starty=0,endy=0,startx=0;
				bool isTablet= size.Width>=600;
				starty = (int) (size.Height * 0.50);
				endy = (int) (size.Height*0.20);
				startx = size.Width / 2;
				int count=0;
				while (count != 3 && (!KeywordImplementation.waitForObjectExist(By.Name("Accept & Continue"), TimeSpan.FromSeconds(1))))
				{
					((IOSDriver<IOSElement>)Accessor.getDriver()).Swipe(startx, starty, startx, endy, 500);
					count++;
				}
				Report.Success("App Info screen is available");
				Logger.logSnapshot();
				KeywordImplementation.Click(By.Name("Accept & Continue"));
				
				if(KeywordImplementation.waitForObjectExist(By.Name("Welcome to the touchControl app."),TimeSpan.FromSeconds(3)))
				{
					KeywordImplementation.Click(By.Name("Manual Setup"));
				}
				
				if(KeywordImplementation.waitForObjectExist(By.Name("Allow"),TimeSpan.FromSeconds(1)))
				{
					KeywordImplementation.Click(By.Name("Allow"));
				}
				
				KeywordImplementation.Click(By.Name("Signia products"));
				
				//A confirmation sound played from the hearing aids indicates a successful pairing.Did you hear the confirmation sound?
				if(KeywordImplementation.waitForObjectExist(By.XPath("//*[contains(@value,'Did you hear the confirmation sound?')]"),TimeSpan.FromSeconds(1)))
				{
					KeywordImplementation.Click(By.Name("Yes"));
					Logger.logSnapshot();
				}
				
				if(KeywordImplementation.waitForObjectExist(By.XPath("//*[contains(@value,'Do you use different hearing programs on your hearing aids for different conditions?')]"),TimeSpan.FromSeconds(1)))
				{
					KeywordImplementation.Click(By.Name("Yes"));
					Logger.logSnapshot();
				}
				KeywordImplementation.Click(By.Name("6"));
				if(KeywordImplementation.waitForObjectExist(By.Name("Do you use a tinnitus program?"),TimeSpan.FromSeconds(3)))
				{
					KeywordImplementation.Click(By.Name("Yes"));
				}
				KeywordImplementation.Click(By.Name("6"));
				
				////Swipe from Bottom to Top and Top to bottom
				while (count != 3 && (!KeywordImplementation.waitForObjectExist(By.Name("Continue"), TimeSpan.FromSeconds(1))))
				{
					((IOSDriver<IOSElement>)Accessor.getDriver()).Swipe(startx, starty, startx, endy, 500);
					count++;
				}
				KeywordImplementation.Click(By.Name("Continue"));
				
				
				if(KeywordImplementation.waitForObjectExist(By.Name("Setup is Complete."),TimeSpan.FromSeconds(3)))
				{
					Ranorex.Report.Success("Setup is completed");
					Logger.logSnapshot();
					KeywordImplementation.Click(By.Name("Continue"));
				}
				
				
				if(!Convert.ToBoolean(isOmnitureOn))
				{
					Logger.logInfo("Click on Settings icon");
					KeywordImplementation.Click(By.Id("Main LowerBar more"));
					KeywordImplementation.Click(By.Id("switchUsageStatistics"));
				}
				
				Logger.logSnapshot();
			}
			finally
			{
				if(Accessor.getDriver()!=null)
					Accessor.getDriver().Quit();
			}
		}
	}
}
