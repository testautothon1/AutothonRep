/*
 * Created by Ranorex
 * User: Z002XFCE
 * Date: 6/28/2017
 * Time: 1:28 PM
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
using OpenQA.Selenium.Interactions;

namespace STSmyHearing
{
	/// <summary>
	/// Description of AndroidSuiteSetUp.
	/// </summary>
	[TestModule("F7BE559F-6339-485A-B996-D763E4A82164", ModuleType.UserCode, 1)]
	public class AndroidSuiteSetUp : ITestModule
	{
		/// <summary>
		/// Constructs a new instance.
		/// </summary>
		public AndroidSuiteSetUp()
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
			Mouse.DefaultMoveTime = 300;
			Keyboard.DefaultKeyPressTime = 100;
			Delay.SpeedFactor = 1.0;
			
			string applicationName=TestSuite.Current.Parameters["AppName"];
			string appActivity="md5887522972be76f398b6ce0dd53353466.MainActivity";
			
			try
			{
				KeywordImplementation.LaunchWeb("chrome","http:www.google.com");
				KeywordImplementation.TypeText(By.Name("q"),"executeautomation");
//				KeywordImplementation.LaunchAndroidApp(TestSuite.Current.Parameters["Device"],applicationName,appActivity);
////				Report.Info("Platform Version "+((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).Capabilities.GetCapability("platformVersion"));////..getCapabilities().getCapability("platformVersion")
////				Report.Info("Device Name "+((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).Capabilities.GetCapability("deviceName"));
//				if(KeywordImplementation.waitForObjectExist(By.Name("Start"),TimeSpan.FromSeconds(60)))
//					KeywordImplementation.Click(By.Name("Start"));
//				
//				for(int count=0;count<14;count++)
//				{
//					KeywordImplementation.Click(By.XPath("//*[contains(@content-desc,'imageConnectHCP')]"));
//				}
//				
//				KeywordImplementation.Click(By.Name("Integration"));
//				//imageConnectHCP
//				
//				KeywordImplementation.TypeText(By.XPath("//*[contains(@content-desc,'entryFieldLeft')]"),AppVariables.entryLeftCode);
//				KeywordImplementation.TypeText(By.XPath("//*[contains(@content-desc,'entryFieldRight')]"),AppVariables.entryRightCode);
//				
//				((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).PressKeyCode(AndroidKeyCode.Back);
//				Logger.logSnapshot();
//				KeywordImplementation.Tap("Connect Now");
//				if(KeywordImplementation.waitForObjectExist(By.Name("Consent Statement"),TimeSpan.FromSeconds(15)))
//				{
//					Logger.logSnapshot();
//					KeywordImplementation.Tap("Accept");
//				}
//				if(KeywordImplementation.waitForObjectExist(By.Name("Pair your hearing aids"),TimeSpan.FromSeconds(10)))
//				{
//					Logger.logSnapshot();
//					KeywordImplementation.Tap("Start");
//				}
//				if(KeywordImplementation.waitForObjectExist(By.Name("Confirmation Sound"),TimeSpan.FromSeconds(10)))
//				{
//					Logger.logSnapshot();
//					KeywordImplementation.Tap("Yes");
//				}
//				if(KeywordImplementation.waitForObjectExist(By.Name("Pairing Finished"),TimeSpan.FromSeconds(10)))
//				{
//					Logger.logSnapshot();
//					KeywordImplementation.Tap("Close");
//				}
//				if(KeywordImplementation.waitForObjectExist(By.Name("Ready to go"),TimeSpan.FromSeconds(10)))
//				{
//					Logger.logSnapshot();
//					KeywordImplementation.Tap("Start using the app");
//				}
//				
				
				
				
				
				
				
				/*IWebElement connectHCP= KeywordImplementation.UIObject(By.XPath("//*[contains(@content-desc,'imageConnectHCP')]"));
				
				IWebElement entryFieldLeft= KeywordImplementation.UIObject(By.XPath("//*[contains(@content-desc,'entryFieldLeft')]"));
				((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).Tap(1,entryFieldLeft,0);
				//	Delay.Seconds(3);
				for(int count=0;count<10;count++)
				{
					((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).Tap(1,connectHCP,0);
					((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).Tap(1,connectHCP,0);
					Delay.Milliseconds(500);
				}
				((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).Tap(1,connectHCP,0);
				((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).Tap(1,connectHCP,0);
				Delay.Seconds(1);
				((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).Tap(1,connectHCP,0);
				((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).Tap(1,connectHCP,0);
				Delay.Seconds(1);
				((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).Tap(1,connectHCP,0);
				((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).Tap(1,connectHCP,0);
				Delay.Seconds(1);
				((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).Tap(1,connectHCP,0);
				((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).Tap(1,connectHCP,0);
				((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).Tap(1,connectHCP,0);
				((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).Tap(1,connectHCP,0);
				((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).Tap(1,connectHCP,0);
				((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).Tap(1,connectHCP,0);
				
				//			KeywordImplementation.DoubleTap(By.Name("Advanced"));
//				KeywordImplementation.DoubleTap(By.Name("Further Information"));
//				KeywordImplementation.DoubleTap(By.Name("Further Information"));
//				KeywordImplementation.DoubleTap(By.Name("Further Information"));
//				KeywordImplementation.DoubleTap(By.Name("Further Information"));
//				KeywordImplementation.DoubleTap(By.Name("Further Information"));
//				KeywordImplementation.DoubleTap(By.Name("Further Information"));
//				KeywordImplementation.DoubleTap(By.Name("Further Information"));
//				KeywordImplementation.DoubleTap(By.Name("Further Information"));
//				KeywordImplementation.DoubleTap(By.Name("Further Information"));
//				KeywordImplementation.DoubleTap(By.Name("Further Information"));
//				KeywordImplementation.DoubleTap(By.Name("Further Information"));
//				KeywordImplementation.DoubleTap(By.Name("Further Information"));
//				KeywordImplementation.DoubleTap(By.Name("Further Information"));
//				KeywordImplementation.Click(By.ClassName("android.widget.ImageView"));
//				KeywordImplementation.Click(By.ClassName("android.widget.ImageView"));
//				KeywordImplementation.Click(By.ClassName("android.widget.ImageView"));
//				KeywordImplementation.Click(By.ClassName("android.widget.ImageView"));
//				KeywordImplementation.Click(By.ClassName("android.widget.ImageView"));
//				KeywordImplementation.Click(By.ClassName("android.widget.ImageView"));
//				KeywordImplementation.Click(By.ClassName("android.widget.ImageView"));
//				KeywordImplementation.Click(By.ClassName("android.widget.ImageView"));
//				KeywordImplementation.Click(By.ClassName("android.widget.ImageView"));
				//			KeywordImplementation.Click(By.ClassName("android.widget.ImageView"));
				
				
				
//				if(KeywordImplementation.waitForObjectExist(By.Name("OK"),TimeSpan.FromSeconds(5)))
//				{
//					KeywordImplementation.Click(By.Name("OK"));
//				}
//				var size=Accessor.getDriver().Manage().Window.Size;
//
//				//Swipe from Bottom to Top and Top to bottom
//				//Find swipe start and end point from screen's width and height.
//				int starty=0,endy=0,startx=0;
//				bool isTablet= size.Width>=600;
//				starty = (int) (size.Height * 0.50);
//				endy = (int) (size.Height*0.20);
//				startx = size.Width / 2;
//				int count=0;
//				while (count != 3 && (!KeywordImplementation.waitForObjectExist(By.Name("Accept & Continue"), TimeSpan.FromSeconds(1))))
//				{
//					((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).Swipe(startx, starty, startx, endy, 500);
//					count++;
//				}
//				Report.Success("App Info screen is available");
//				Logger.logSnapshot();
//				KeywordImplementation.Click(By.Name("Accept & Continue"));
//
//				if(KeywordImplementation.waitForObjectExist(By.Name("Setup"),TimeSpan.FromSeconds(3)))
//				{
//					KeywordImplementation.Click(By.Name("Manual Setup"));
//				}
//
//				if(KeywordImplementation.waitForObjectExist(By.Name("Allow"),TimeSpan.FromSeconds(3)))
//				{
//					KeywordImplementation.Click(By.Name("Allow"));
//				}
//
//				KeywordImplementation.Click(By.Name("Signia Products"));
//
//				//A confirmation sound played from the hearing aids indicates a successful pairing.Did you hear the confirmation sound?
//				if(KeywordImplementation.waitForObjectExist(By.Id("textViewSoundTest"),TimeSpan.FromSeconds(3)))
//				{
//					KeywordImplementation.Click(By.Name("Yes"));
//					Logger.logSnapshot();
//				}
//				KeywordImplementation.Click(By.Name("Yes"));
//				KeywordImplementation.Click(By.Name("6"));
//				if(KeywordImplementation.waitForObjectExist(By.Name("Do you use a tinnitus program?"),TimeSpan.FromSeconds(3)))
//				{
//					KeywordImplementation.Click(By.Name("Yes"));
//				}
//				KeywordImplementation.Click(By.Name("6"));
//
//				////Swipe from Bottom to Top and Top to bottom
//				while (count != 3 && (!KeywordImplementation.waitForObjectExist(By.Name("Continue"), TimeSpan.FromSeconds(1))))
//				{
//					((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).Swipe(startx, starty, startx, endy, 500);
//					count++;
//				}
//				KeywordImplementation.Click(By.Name("Continue"));
//
//
//				if(KeywordImplementation.waitForObjectExist(By.Name("Setup is Complete."),TimeSpan.FromSeconds(3)))
//				{
//					Ranorex.Report.Success("Setup is completed");
//					Logger.logSnapshot();
//					KeywordImplementation.Click(By.Name("Continue"));
//				}
//
//				Logger.logSnapshot();*/

				
			}
			finally
			{
				if(Accessor.getDriver()!=null)
					Accessor.getDriver().Quit();
			}
			
		}
	}
}
