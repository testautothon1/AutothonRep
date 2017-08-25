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
    /// Description of iOSSetup.
    /// </summary>
    [TestModule("F85B821B-A621-4F86-B2E1-A5AE71D07318", ModuleType.UserCode, 1)]
    public class iOSSetup : ITestModule
    {
    	bool isPopup;
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public iOSSetup()
        {
            // Do not delete - a parameterless constructor is required!
            isPopup=true;
        }
        
        public iOSSetup(bool isPopup)
		{
			this.isPopup = isPopup;
		}

        /// <summary>
        /// Performs the playback of actions in this module.
        /// </summary>
        /// <remarks>You should not call this method directly, instead pass the module
        /// instance to the <see cref="TestModuleRunner.Run(ITestModule)"/> method
        /// that will in turn invoke this method.</remarks>
        void ITestModule.Run()
        {
           
			string applicationName=TestSuite.Current.Parameters["AppName"];
			string appPath=TestSuite.Current.Parameters["AppPath"];

			KeywordImplementation.LaunchiOSApp(new Uri("http://"+TestSuite.Current.Parameters["Uri"]+":4723/wd/hub"),TestSuite.Current.Parameters["Device"],applicationName,appPath);
		     
			if(KeywordImplementation.waitForObjectExist(By.Id("OK"),TimeSpan.FromSeconds(5)))
			{
				KeywordImplementation.Click(By.Id("OK"));
			}
			
			if(isPopup)
			{
				if(KeywordImplementation.waitForObjectExist(By.Name("Not now"),TimeSpan.FromSeconds(1)))
					KeywordImplementation.Click(By.Name("Not now"));
				else if ( KeywordImplementation.waitForObjectExist(By.Name("Später vielleicht"),TimeSpan.FromSeconds(1)))
					KeywordImplementation.Click(By.Name("Später vielleicht"));
			}
        }
    }
}
