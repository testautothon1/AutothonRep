/*
 * Created by Ranorex
 * User: Z002XFCE
 * Date: 7/12/2017
 * Time: 3:09 PM
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

using AppLayer;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;
using AppLayer.AppiumService;
using OpenQA.Selenium.Interactions;

namespace STSmyHearing
{
	/// <summary>
	/// Description of AndroidSetup.
	/// </summary>
	[TestModule("DDFB6A0E-69F8-494C-9469-9D369198C6F6", ModuleType.UserCode, 1)]
	public class AndroidSetup : ITestModule
	{
		/// <summary>
		/// Constructs a new instance.
		/// </summary>
		public AndroidSetup()
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
			
			//appActivity="md56f35e8d86ebdc1429f36073ae400eed3.SplashActivity";
			
			KeywordImplementation.LaunchAndroidApp(TestSuite.Current.Parameters["Device"],applicationName,appActivity);
			
			if(KeywordImplementation.waitForObjectExist(By.Name("Skip"),TimeSpan.FromSeconds(15)))
			{
				KeywordImplementation.Click(By.Name("Skip"));
			}
			
			Report.Info(TestSuite.Current.Parameters["Device"]);
		}
	}
}
