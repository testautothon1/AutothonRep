﻿///////////////////////////////////////////////////////////////////////////////
//
// This file was automatically generated by RANOREX.
// Your custom recording code should go in this file.
// The designer will only add methods to this file, so your custom code won't be overwritten.
// http://www.ranorex.com
// 
///////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using WinForms = System.Windows.Forms;
using AppLayer;
using AppLayer.AppiumService;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Repository;
using Ranorex.Core.Testing;

namespace STSiOSMFApp.TC
{
	public partial class CodeValidation:CommonHelper
	{
		/// <summary>
		/// This method gets called right after the recording has been started.
		/// It can be used to execute recording specific initialization code.
		/// </summary>
		private void Init()
		{
			// Your recording specific initialization code goes here.
		}

		public void ValidatePrePopulatedEmail()
		{
			if(KeywordImplementation.UIObject(By.Id("toField")).GetAttribute("value").Contains("@sivantos.com") )//&& KeywordImplementation.UIObject(By.Id("toField")).Text.Contains("."))
				Logger.logSuccess(string.Format("Pre-populated mail id is'{0}'",KeywordImplementation.UIObject(By.Id("toField")).GetAttribute("value")));
			else
				Logger.logFailure(string.Format("Pre-populated e-mail id '{0}' is not correct",KeywordImplementation.UIObject(By.Id("toField")).GetAttribute("value")));
			
			if(KeywordImplementation.UIObject(By.Id("subjectField")).GetAttribute("value").Length!=0)
				Logger.logSuccess(string.Format("Pre-populated mail subject is '{0}'",KeywordImplementation.UIObject(By.Id("subjectField")).Text));
			else
				Logger.logFailure("Pre-populated subject is empty");
			
			if(KeywordImplementation.UIObject(By.Id("Message body")).GetAttribute("value").Length!=0)
				Logger.logSuccess(string.Format("Pre-populated mail contents is'{0}'",KeywordImplementation.UIObject(By.Id("Message body")).GetAttribute("value")));
			else
				Logger.logFailure("Pre-populated mail content is empty");
			Logger.logSnapshot();
		}
	}
}