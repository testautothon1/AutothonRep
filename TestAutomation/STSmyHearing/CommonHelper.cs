/*
 * Created by Ranorex
 * User: Z002XFCE
 * Date: 6/28/2017
 * Time: 1:17 PM
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
using System.Diagnostics;
using Castle.Components.DictionaryAdapter.Xml;
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

namespace STSmyHearing
{
	/// <summary>
	/// Description of CommonHelper.
	/// </summary>
	public class CommonHelper
	{
		public CommonHelper()
		{
		}
		
		public void TapOnHome()
		{
			KeywordImplementation.Click(By.Id("home"));
		}
		
		public void TapOn(string objectName)
		{
			KeywordImplementation.Tap(objectName);
		}

		public void BackAction()
		{
			//Delay.Seconds(2);
			KeywordImplementation.PressBack();
		}
		
		public void ValidateActionBarTitle(string title)
		{
			if(KeywordImplementation.UIObject(By.Id("action_bar_title"),TimeSpan.FromSeconds(5)).Text.Equals(title))
				Logger.logSuccess(title+" has found");
			else
				Logger.logFailure(title+" has not found");
		}
		
		public void ValidateContents(string contents)
		{
			string[] contentsArray= contents.Split(';');
			
			foreach(string str in contentsArray)
			{
				bool check=false;
				//KeywordImplementation.SwipeVerticle(3,0.60,0.20,By.Name(str));
				KeywordImplementation.SwipeVerticle(3,0.60,0.20,By.XPath("//*[contains(@text,'"+str+"')]"));
				if(KeywordImplementation.waitForObjectExist(By.XPath("//*[contains(@text,'"+str+"')]"),TimeSpan.FromSeconds(1)))
				{
					check=true;
					Logger.logSuccess(string.Format("Information '{0}' text has found",str));
				}
				
				if(!check)
				{
					Logger.logFailure(string.Format("Information '{0}' text has not found",str));
				}
			}
			Logger.logSnapshot();
			
		}
		
		public void ValidateContentsWithDesc(string contentDisc,Boolean takeScreenshot)
		{
			string[] contentsArray= contentDisc.Split(';');
			
			foreach(string str in contentsArray)
			{
				bool check=false;
				KeywordImplementation.SwipeVerticle(3,0.60,0.20,By.XPath("//*[contains(@content-desc,'"+str+"')]"));
				if(KeywordImplementation.waitForObjectExist(By.XPath("//*[contains(@content-desc,'"+str+"')]"),TimeSpan.FromSeconds(1)))
				{
					check=true;
					Logger.logSuccess(string.Format("Information '{0}' text has found",str));
				}
				
				if(!check)
				{
					Logger.logFailure(string.Format("Information '{0}' text has not found",str));
				}
			}
			if(takeScreenshot)
				Logger.logSnapshot();
		}
		
		public void LogSnapshot()
		{
			Logger.logSnapshot();
		}
		
		public void SwipePageVerticle(int count,double startY, double endY,Boolean takeScreenshot)
		{
			int cnt=0;
			var size=Accessor.getDriver().Manage().Window.Size;
			int starty = (int) (size.Height *startY);
			int endy = (int) (size.Height*endY);
			int startx = size.Width / 2;
			while(cnt!=count)
			{
				if(takeScreenshot)
					Logger.logSnapshot();
				((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).Swipe(startx, starty, startx, endy, 1000);
				
				cnt++;
			}
			
			if(takeScreenshot)
				Logger.logSnapshot();
		}
		
		public void ValidateAppVersion()
		{
			string versionInfo=KeywordImplementation.UIObject(By.Id("Txt_Info_Msg6"),TimeSpan.FromSeconds(2)).Text;
			if((versionInfo.Split('.').Length-1)==3)
				Logger.logSuccess(string.Format("Application version {0} displayed is correct",versionInfo));
			else
				Logger.logFailure(string.Format("Application version {0} displayed is incorrect",versionInfo));
		}
		
		public void CloseApp()
		{
			TearDown.CloseApplication();
		}
		
		public void StartApp()
		{
			TestModuleRunner.Run(new AndroidSetup());
		}
		
		
		
	}
}
