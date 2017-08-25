/*
 * Created by Ranorex
 * User: Z003RWZS
 * Date: 4/17/2017
 * Time: 5:26 PM
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

namespace STSAndBasic
{
	/// <summary>
	/// Description of AndroidSetUp.
	/// </summary>
	[TestModule("27473C54-4E8F-4635-8F97-4A828ABA5B18", ModuleType.UserCode, 1)]
	public class AndroidSetUp : CommonHelper,ITestModule
	{
		/// <summary>
		/// Constructs a new instance.
		/// </summary>
		public AndroidSetUp()
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
			
			string applicationName=TestSuite.Current.Parameters["AppName"];
			string appActivity="md56f35e8d86ebdc1429f36073ae400eed3.SplashActivity";
			
			KeywordImplementation.LaunchAndroidApp(TestSuite.Current.Parameters["Device"],applicationName,appActivity);
//			KeywordImplementation.LaunchAndroidApp(TestSuite.Current.Parameters["Device"],applicationName,appActivity);
			
			if(KeywordImplementation.waitForObjectExist(By.Name("OK"),TimeSpan.FromSeconds(5)))
			{
				KeywordImplementation.Click(By.Name("OK"));
			}
			
			//Handling "Connect to cloud" window
//			if(KeywordImplementation.waitForObjectExist(By.Name("Would you like to connect to cloud ?"),TimeSpan.FromSeconds(2)))
//			{
//				KeywordImplementation.Click(By.Name("NO"));
//				Delay.Seconds(2);
//			}
			Report.Info(TestSuite.Current.Parameters["Device"]);
		}
	}
}
