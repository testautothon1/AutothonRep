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

namespace STSAndTouchControl
{
	/// <summary>
	/// Description of AndroidSuiteSetUp.
	/// </summary>
	[TestModule("B47A2C84-B31D-404C-96AF-4D44E1AECA1A", ModuleType.UserCode, 1)]
	public class AndroidSuiteSetUp : CommonHelper,ITestModule
	{
		
		string _isOmnitureOn = "False";
		[TestVariable("3736a7f9-cc9f-46ed-be7c-ad2287d802a1")]
		public string isOmnitureOn
		{
			get { return _isOmnitureOn; }
			set { _isOmnitureOn = value; }
		}
		
		
		//bool isOmnitureOn;
		/// <summary>
		/// Constructs a new instance.
		/// </summary>
		public AndroidSuiteSetUp()
		{
			//isOmnitureOn = true;
		}
		/// <remarks>You should not call this method directly, instead pass the module
		/// instance to the <see cref="TestModuleRunner.Run(ITestModule)"/> method
		/// that will in turn invoke this method.</remarks>
		void ITestModule.Run()
		{
			Mouse.DefaultMoveTime = 300;
			Keyboard.DefaultKeyPressTime = 100;
			Delay.SpeedFactor = 1.0;
			
			string applicationName=TestSuite.Current.Parameters["AppName"];
			string appActivity="md558fba4ce7ce0bf1978fcd2662fa3cc65.SplashActivity";
			
			try
			{
				KeywordImplementation.LaunchAndroidApp(TestSuite.Current.Parameters["Device"],applicationName,appActivity);
				//KeywordImplementation.LaunchAndroidApp(TestSuite.Current.Parameters["Device"],applicationName,appActivity);
				Report.Info("Platform Version "+((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).Capabilities.GetCapability("platformVersion"));////..getCapabilities().getCapability("platformVersion")
				Report.Info("Device Name "+((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).Capabilities.GetCapability("deviceName"));
				
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
					((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).Swipe(startx, starty, startx, endy, 500);
					count++;
				}
				Report.Success("App Info screen is available");
				Logger.logSnapshot();
				KeywordImplementation.Click(By.Name("Accept & Continue"));
				
				if(KeywordImplementation.waitForObjectExist(By.Name("Welcome to the touchControl app."),TimeSpan.FromSeconds(3)))
				{
					KeywordImplementation.Click(By.Name("Manual Setup"));
				}
				
				if(KeywordImplementation.waitForObjectExist(By.Name("Allow"),TimeSpan.FromSeconds(3)))
				{
					KeywordImplementation.Click(By.Name("Allow"));
				}
				
				KeywordImplementation.Click(By.Name("Signia Products"));
				
				//A confirmation sound played from the hearing aids indicates a successful pairing.Did you hear the confirmation sound?
				//	if(KeywordImplementation.waitForObjectExist(By.XPath("//*contains[@value,'']"),TimeSpan.FromSeconds(3)))
				if(KeywordImplementation.waitForObjectExist(By.Id("textViewSoundTest"),TimeSpan.FromSeconds(3)))
				{
					KeywordImplementation.Click(By.Name("Yes"));
					Logger.logSnapshot();
				}
				KeywordImplementation.Click(By.Name("Yes"));
				KeywordImplementation.Click(By.Name("6"));
				if(KeywordImplementation.waitForObjectExist(By.Name("Do you use a tinnitus program?"),TimeSpan.FromSeconds(3)))
				{
					KeywordImplementation.Click(By.Name("Yes"));
				}
				KeywordImplementation.Click(By.Name("6"));
				
				////Swipe from Bottom to Top and Top to bottom
				while (count != 3 && (!KeywordImplementation.waitForObjectExist(By.Name("Continue"), TimeSpan.FromSeconds(1))))
				{
					((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).Swipe(startx, starty, startx, endy, 500);
					count++;
				}
				KeywordImplementation.Click(By.Name("Continue"));
				
				
				if(KeywordImplementation.waitForObjectExist(By.Name("Setup is Complete."),TimeSpan.FromSeconds(3)))
				{
					Ranorex.Report.Success("Setup is completed");
					Logger.logSnapshot();
					KeywordImplementation.Click(By.Name("Continue"));
				}
				//Disable Omniture Settings
				if(!Convert.ToBoolean(isOmnitureOn))
				{
					TapOnSettings();
					SwitchUsageStatistics(Convert.ToBoolean(isOmnitureOn));
				}
				
				
				//Logger.logSnapshot();
			}
			finally
			{
				if(Accessor.getDriver()!=null)
					Accessor.getDriver().Quit();
			}
			
		}
		
		
		
	}
}
