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

namespace STSiOSMFApp
{
	/// <summary>
	/// Description of AndroidSetUp.
	/// </summary>
	[TestModule("4F88485A-8FCD-46A9-A88A-F593FBA4F9BD", ModuleType.UserCode, 1)]
	public class IOSSetup : CommonHelper,ITestModule
	{
		
		/// <summary>
		/// Constructs a new instance.
		/// </summary>
		public IOSSetup()
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
			//Set HI names based on brand of APP
			BrandBasedHINames();
			string applicationName=TestSuite.Current.Parameters["AppName"];
			//	string appActivity="md56f35e8d86ebdc1429f36073ae400eed3.SplashActivity";
			string appPath=TestSuite.Current.Parameters["AppPath"];
			KeywordImplementation.LaunchiOSApp(new Uri("http://"+TestSuite.Current.Parameters["Uri"]+":4723/wd/hub"),TestSuite.Current.Parameters["Device"],applicationName,appPath);
			//KeywordImplementation.LaunchiOSApp(new Uri("http://"+TestSuite.Current.Parameters["Uri"]+":4723/wd/hub"),TestSuite.Current.Parameters["Device"],applicationName,appPath);
			
			if(KeywordImplementation.waitForObjectExist(By.Id("OK"),TimeSpan.FromSeconds(5)))
			{
				KeywordImplementation.Click(By.Id("OK"));
			}
			
		}
	}
}
