/*
 * Created by Ranorex
 * User: Z003RWZS
 * Date: 4/17/2017
 * Time: 3:57 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
//extern alias WebDriver;
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
using AppLayer.AppiumService;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Appium.Service;
using System.Linq;


namespace STSAndBasic
{
	/// <summary>
	/// Description of AndroidSuiteSetUp.
	/// </summary>
	[TestModule("749088A9-6881-45B3-8359-6C11D3DAE5C0", ModuleType.UserCode, 1)]
	public class AndroidSuiteSetUp : CommonHelper, ITestModule
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
			//initialising HI values globally based on the AppBrand.
			BrandBasedHINames();
			Mouse.DefaultMoveTime = 300;
			Keyboard.DefaultKeyPressTime = 100;
			Delay.SpeedFactor = 1.0;

			string applicationName = TestSuite.Current.Parameters["AppName"];
			string appActivity = "md56f35e8d86ebdc1429f36073ae400eed3.SplashActivity";
			try
			{
				KeywordImplementation.LaunchAndroidApp(TestSuite.Current.Parameters["Device"],applicationName,appActivity);
				
//				KeywordImplementation.LaunchAndroidApp(TestSuite.Current.Parameters["Device"], applicationName, appActivity);
				Report.Info("Platform Version " + ((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).Capabilities.GetCapability("platformVersion"));////..getCapabilities().getCapability("platformVersion")
				Report.Info("Device Name " + ((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).Capabilities.GetCapability("deviceName"));

				if (KeywordImplementation.waitForObjectExist(By.Name("OK"), TimeSpan.FromSeconds(5)))
				{
					KeywordImplementation.Click(By.Name("OK"));
				}
				var size = Accessor.getDriver().Manage().Window.Size;

				//Swipe from Bottom to Top and Top to bottom
				//Find swipe start and end point from screen's width and height.
				int starty = 0, endy = 0, startx = 0;
				bool isTablet = size.Width >= 600;
				starty = (int)(size.Height * 0.50);
				endy = (int)(size.Height * 0.20);
				startx = size.Width / 2;
				int count = 0;
				while (count != 3 && (!KeywordImplementation.waitForObjectExist(By.Id("Btn_AppInfo_Accept"), TimeSpan.FromSeconds(1))))
				{
					((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).Swipe(startx, starty, startx, endy, 500);
					count++;
				}
				Report.Success("Welcome Page screen is available");
				Logger.logSnapshot();
				KeywordImplementation.Click(By.Id("CheckBox_AcceptTAndC"));
				KeywordImplementation.Click(By.Id("Btn_AppInfo_Accept"));
				Delay.Seconds(15);
				// For SSO
//				((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).Swipe(startx, starty, startx, endy, 0);
//				((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).Swipe(startx, starty, startx, endy, 0);
//				//                KeywordImplementation.SwipeVerticle(3,0.50,0.20,By.XPath("//*[contains(@content-desc,'Yes, Allow')]"));
//				KeywordImplementation.Click(By.XPath("//*[contains(@content-desc,'Yes, Allow')]"));
//
				if (KeywordImplementation.waitForObjectExist(By.Name("Country"), TimeSpan.FromSeconds(3)))
				{
					KeywordImplementation.Click(By.Name("Done"));
				}

				KeywordImplementation.TypeText(By.Id("Txt_Legitimation_Code1"), "8981");
				KeywordImplementation.TypeText(By.Id("Txt_Legitimation_Code2"), "9313");
				KeywordImplementation.TypeText(By.Id("Txt_Legitimation_Code3"), "9666");
				Report.Success("Access code screeen is available");
				Logger.logSnapshot();
				KeywordImplementation.Click(By.Name("Done"));
				Delay.Seconds(3);

				Ranorex.Report.Success(Accessor.getDriver().FindElement(By.Id("Txt_Legitimation_Msg")).Text);
				Logger.logSnapshot();
				while (count != 3 && (!KeywordImplementation.waitForObjectExist(By.Id("Continue"), TimeSpan.FromSeconds(1))))
				{
					((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).Swipe(startx, starty, startx, endy, 500);
					count++;
				}
				KeywordImplementation.Click(By.Name("Continue"));
				if (applicationName.Equals("com.connexx.fit2go", StringComparison.CurrentCultureIgnoreCase))
				{
					KeywordImplementation.Click(By.Name("Done"));
				}

				Ranorex.Report.Success("Customer Ear Molds screen is available");
				Logger.logSnapshot();
				count = 0;
				while (count != 3 && (!KeywordImplementation.waitForObjectExist(By.Id("Btn_CustomMolds_Continue"), TimeSpan.FromSeconds(1))))
				{
					((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).Swipe(startx, starty, startx, endy, 500);
					count++;
				}
				KeywordImplementation.Click(By.Id("Btn_CustomMolds_Continue"));
				//Selecting the HIs from the list available for recommendation
				//SwitchToChooseHI();

				if (KeywordImplementation.waitForObjectExist(By.Id("Txt_CommonAlert_Title"), TimeSpan.FromSeconds(3)))
				{
					Ranorex.Report.Success("Security alert has popped up");
					Logger.logSnapshot();
					KeywordImplementation.Click(By.Name("OK"));
				}

				Logger.logSnapshot();

				if (TestSuite.Current.Parameters["isTablet"].Equals("True", StringComparison.CurrentCultureIgnoreCase))
					KeywordImplementation.Click(By.Id("Tab_Cutomer_Settings"));
				else
				{
					TearDown.CloseApplication();
					TestModuleRunner.Run(new STSAndBasic.AndroidSetUp());

					KeywordImplementation.Click(By.Id("Btn_ActionBar_Right"), "Click on Settings");
				}
				Delay.Seconds(2);
				SwitchSettingOmniture(false);
			}
			finally
			{
				if (Accessor.getDriver() != null)
					Accessor.getDriver().Quit();
			}
		}

		public void SwitchSettingOmniture(bool on)
		{
			var switchOmniture = KeywordImplementation.UIObject(By.Id("Switch_Settings_Omniture"), TimeSpan.FromSeconds(1));
			if ((on.Equals(true) && switchOmniture.Text.Equals("Off")) || (on.Equals(false) && switchOmniture.Text.Equals("On")))
			{
				switchOmniture.Click();
				Report.Success("Switch Omniture is " + switchOmniture.Text);
			}
			Logger.logSnapshot();
		}

	}
}
