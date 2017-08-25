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

namespace STSAndTouchControl
{
	/// <summary>
	/// Description of AndroidSetUp.
	/// </summary>
	[TestModule("7FD70A91-C36A-47ED-B695-CB2937C57262", ModuleType.UserCode, 1)]
	public class AndroidSetUp : ITestModule
	{ 
		bool isPopup;
		/// <summary>
		/// Constructs a new instance.
		/// </summary>
		public AndroidSetUp()
		{
			// Do not delete - a parameterless constructor is required!
			isPopup=true;
			
		}
		public AndroidSetUp(bool isPopup)
		{
			this.isPopup = isPopup;
		}
//		string _isRatetheApp = "false";
//		[TestVariable("635e797b-0685-4779-afc7-0c158b0b4142")]
//		public string isRatetheApp
//		{
//			get { return _isRatetheApp; }
//			set { _isRatetheApp = value; }
//		}

		
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
			string appActivity="md558fba4ce7ce0bf1978fcd2662fa3cc65.SplashActivity";
			
			//appActivity="md56f35e8d86ebdc1429f36073ae400eed3.SplashActivity";
			KeywordImplementation.LaunchAndroidApp(TestSuite.Current.Parameters["Device"],applicationName,appActivity);
		//	KeywordImplementation.LaunchAndroidApp(TestSuite.Current.Parameters["Device"],applicationName,appActivity);
			
			if(KeywordImplementation.waitForObjectExist(By.Name("OK"),TimeSpan.FromSeconds(5)))
			{
				KeywordImplementation.Click(By.Name("OK"));
			}
			if(isPopup)
			{
				if(KeywordImplementation.waitForObjectExist(By.Name("Not now"),TimeSpan.FromSeconds(1)))
					KeywordImplementation.Click(By.Name("Not now"));
				else if ( KeywordImplementation.waitForObjectExist(By.Name("Später vielleicht"),TimeSpan.FromSeconds(1)))
					KeywordImplementation.Click(By.Name("Später vielleicht"));
			}
			
			Report.Info(TestSuite.Current.Parameters["Device"]);
		}
	}
}
