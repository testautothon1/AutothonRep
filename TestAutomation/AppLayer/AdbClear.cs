/*
 * Created by Ranorex
 * User: ManishK
 * Date: 5/27/2016
 * Time: 11:12 AM
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
using System.Diagnostics;


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


namespace AndroidRepLayer
{
	/// <summary>
	/// Description of AdbClear.
	/// </summary>
	[TestModule("474F462F-44A7-4D62-85CC-853766F56387", ModuleType.UserCode, 1)]
	public class AdbClear : ITestModule
	{
		/// <summary>
		/// Constructs a new instance.
		/// </summary>
		public AdbClear()
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
			
			string applicationName=string.Empty;
			
//			if(TestSuite.Current.Parameters["Brand"].Equals("Connexx",StringComparison.CurrentCultureIgnoreCase))
//				applicationName="com.connexx.fit2go";
//			else if(TestSuite.Current.Parameters["Brand"].Equals("Signia",StringComparison.CurrentCultureIgnoreCase))
//				applicationName="com.signia.mobilefitting";

			String ANDROID_HOME = System.Environment.GetEnvironmentVariable("ANDROID_HOME");
			String adbPath = System.IO.Path.Combine(ANDROID_HOME, "platform-tools", "adb.exe");
			
//			adbPath = "\"" + adbPath + "\"";
//			//first clear all logs
//			Process.Start(adbPath, "-s "+TestSuite.Current.Parameters["Device"].Split('-')[1]+" shell pm clear "+applicationName);
			Process.Start(adbPath, "-s "+TestSuite.Current.Parameters["Device"]+" shell pm clear "+TestSuite.Current.Parameters["AppName"]);
			
			//((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).ResetApp();
		}
	}
}
