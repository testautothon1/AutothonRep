/*
 * Created by Ranorex
 * User: Z003RWZS
 * Date: 4/17/2017
 * Time: 3:57 PM
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

namespace STSiOSMFApp
{
	/// <summary>
	/// Description of AndroidSuiteSetUp.
	/// </summary>
	[TestModule("585DC168-B463-4AB9-A34F-15A5B787DC65", ModuleType.UserCode, 1)]
	public class IOSSuiteSetup : CommonHelper,ITestModule
	{
		public static string applicationName=string.Empty;
		public static string appActivity= string.Empty;
		
		/// <summary>
		/// Constructs a new instance.
		/// </summary>
		public IOSSuiteSetup()
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
			BrandBasedHINames();
			try
			{
				Report.Info("Brand   "+TestSuite.Current.Parameters["Brand"]);
				Report.Info("Device   "+TestSuite.Current.Parameters["Device"]);
				Report.Info("App   "+TestSuite.Current.Parameters["AppName"]);
				string applicationName=TestSuite.Current.Parameters["AppName"];
				string appPath=TestSuite.Current.Parameters["AppPath"];

				KeywordImplementation.LaunchiOSApp(new Uri("http://"+TestSuite.Current.Parameters["Uri"]+":4723/wd/hub"),TestSuite.Current.Parameters["Device"],applicationName,appPath);

				if(KeywordImplementation.waitForObjectExist(By.Id("OK"),TimeSpan.FromSeconds(5)))
				{
					KeywordImplementation.Click(By.Id("OK"));
				}
				var size=Accessor.getDriver().Manage().Window.Size;

				/*****************************/
				//Swipe from Bottom to Top
				//Find swipe start and end point from screen's width and height.
				int starty = (int) (size.Height * 0.70);
				int endy = (int) (size.Height*0.20);
				int startx = size.Width / 2;

				int count=0;
				while(count!=3 && (!KeywordImplementation.waitForObjectExist(By.Id("Accept & Continue"),TimeSpan.FromSeconds(1))))
				{
					((IOSDriver<IOSElement>)Accessor.getDriver()).Swipe(startx, starty, startx, endy, 1000);
					count++;
				}
				Report.Success("Welcome Page screen is available");
				KeywordImplementation.Click(By.Id("AgreeCheckBox"));
				Logger.logSnapshot();
				KeywordImplementation.Click(By.Id("Accept & Continue"));
				Delay.Seconds(15);
				if(KeywordImplementation.waitForObjectExist(By.Id("Country"),TimeSpan.FromSeconds(3)))
				{
					Logger.logSuccess("Country selection screen is shown");
					Logger.logSnapshot();

					while(count<=25 && (!KeywordImplementation.waitForObjectExist(By.Id("Rest of World"),TimeSpan.FromSeconds(1))))
					{
						((IOSDriver<IOSElement>)Accessor.getDriver()).Swipe(startx, starty, startx, endy, 500);
						count++;
					}
					KeywordImplementation.Click(By.Id("Rest of World"));
					KeywordImplementation.Click(By.Id("Done"));
				}

				KeywordImplementation.TypeText(By.Id("Passcode1"),"8981");
				KeywordImplementation.TypeText(By.Id("Passcode2"),"9313");
				KeywordImplementation.TypeText(By.Id("Passcode3"),"9666");

				Report.Success("Access code screeen is available");
				Logger.logSnapshot();
				KeywordImplementation.Click(By.Id("Done"));
				
				Delay.Seconds(10);
				Ranorex.Report.Success(Accessor.getDriver().FindElement(By.Id("Code entered successfully.")).Text);
				Logger.logSnapshot();
				KeywordImplementation.Click(By.Id("Continue"));
				if(TestSuite.Current.Parameters["Brand"].Equals("Connexx",StringComparison.CurrentCultureIgnoreCase))
				{
					KeywordImplementation.Click(By.Name("Done"));
				}

				Ranorex.Report.Success("Customer Ear Molds screen is available");
				Logger.logSnapshot();
				KeywordImplementation.Click(By.Id("Continue"));
				//KeywordImplementation.Click(By.Id("Continue"));

				if(KeywordImplementation.waitForObjectExist(By.XPath("//UIAAlert[@name='Sensitive Client Data']"),TimeSpan.FromSeconds(3)))
				{
					Ranorex.Report.Success("Security alert has popped up");
					Logger.logSnapshot();
					KeywordImplementation.Click(By.Id("OK"));
				}

				Logger.logSnapshot();
				if(TestSuite.Current.Parameters["isTablet"].Equals("True",StringComparison.CurrentCultureIgnoreCase))
					KeywordImplementation.Click(By.Id("SettingsIcon"));
				else
					KeywordImplementation.Click(By.Id("NavBarRight"));

				SwitchSettingOmniture(false);

//				}
			}
			finally
			{
				if(Accessor.getDriver()!=null)
					Accessor.getDriver().Quit();
			}
			
		}

		public void SwitchSettingOmniture(bool on)
		{
			var switchOmniture=KeywordImplementation.UIObject(By.Id("switchSettingUsage"),TimeSpan.FromSeconds(1));
			
			if((on.Equals(true) && (switchOmniture.GetAttribute("value").Equals("0") || switchOmniture.GetAttribute("value").Equals("false"))) ||  (on.Equals(false) && (switchOmniture.GetAttribute("value").Equals("1") || switchOmniture.GetAttribute("value").Equals("true"))))
			{
				switchOmniture.Click();
				Report.Success("Switch Omniture is " + switchOmniture.GetAttribute("value"));
			}
			Logger.logSnapshot();
		}
		
	}
}
