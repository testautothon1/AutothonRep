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

namespace STSAndBasic.TC
{
	public partial class RecommendationOfHIsWithoutAudiogram:CommonHelper
	{
		/// <summary>
		/// This method gets called right after the recording has been started.
		/// It can be used to execute recording specific initialization code.
		/// </summary>
		private void Init()
		{
			// Your recording specific initialization code goes here.
		}
		
		private void SkipInstructions()
		{
			KeywordImplementation.Click(By.Id("ImgBtn_PureToneInst_Next"));
			KeywordImplementation.Click(By.Id("Img_PureToneInst_Skip"));
		}
		
		private void DrawPTEAudiogramPoints(string side, string points)
		{
			Logger.logInfo(string.Format("Draw audiogram points \"{0}\"",points));
			if(KeywordImplementation.UIObject(By.Id("Txt_ActionBar_Title"),TimeSpan.FromSeconds(5)).Text.Equals("Pure Tone Estimation"))
			{
				Logger.logInfo("PTE page is selected");
				if(side.Equals("right",StringComparison.CurrentCultureIgnoreCase))
				{
					if(KeywordImplementation.UIObject(By.Id("Btn_ActionBar_Right")).GetAttribute("name").Contains("Right") && KeywordImplementation.UIObject(By.Id("Txt_PureToneTest_FreqWithoutBg")).Text.Contains("500 Hz"))
					{
						Logger.logInfo("PTE right side is selected");
					}
				}
				
				if(side.Equals("left",StringComparison.CurrentCultureIgnoreCase))
				{
					if(KeywordImplementation.UIObject(By.Id("Btn_ActionBar_Right")).GetAttribute("name").Contains("Right") && KeywordImplementation.UIObject(By.Id("Txt_PureToneTest_FreqWithoutBg")).Text.Contains("500 Hz"))
					{
						KeywordImplementation.Click(By.Id("Img_PureToneInst_Skip"));
						Logger.logInfo("Selecting the PTE left side");
					}
					if(KeywordImplementation.UIObject(By.Id("Btn_ActionBar_Right")).GetAttribute("name").Contains("Left") && KeywordImplementation.UIObject(By.Id("Txt_PureToneTest_FreqWithoutBg")).Text.Contains("500 Hz"))
					{
						Logger.logInfo("PTE left side is selected");
					}
				}
				
				//IWebElement elementImgBtnPteIncrease = Accessor.getDriver().FindElement(By.Id("ImgBtn_PureToneTest_Increase"));
				string[] aPoints=points.Split(';');
				foreach(string point in aPoints)
				{
					int audValue=0;
					audValue=(int.Parse(point)-30)/5;
					IWebElement elementImgBtnPteIncrease = Accessor.getDriver().FindElement(By.Id("ImgBtn_PureToneTest_Increase"));
					for(int i=0;i<audValue;i++)
					{
						elementImgBtnPteIncrease.Click();
						//KeywordImplementation.Click(By.Id("ImgBtn_PureToneTest_Increase"));
					}
					
					if(audValue>=13)
						KeywordImplementation.Click(By.Id("ImgBtn_PureToneTest_HeardUnHeard"));
					
					KeywordImplementation.Click(By.Id("ImgBtn_PureToneInst_Next"));
				}
			}
			
			
			
			
		}

		private void PTEDone()
		{
			if(KeywordImplementation.waitForObjectExist(By.Id("Img_PureToneInst_Skip"),TimeSpan.FromSeconds(2)))
			{
				KeywordImplementation.Click(By.Id("Img_PureToneInst_Skip"));
			}
			KeywordImplementation.Click(By.Id("ImgBtn_PureToneInst_Next"));
		}
		
		private void TapOnObject(string objName)
		{
		//	((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).BackgroundApp(2);
		
		
			var size=Accessor.getDriver().Manage().Window.Size;
			
			//Swipe from Bottom to Top and Top to bottom
			//Find swipe start and end point from screen's width and height.
			int starty = (int) (size.Height * 0.50);
			int endy = (int) (size.Height*0.80);
			int startx = size.Width / 2;
			((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).Swipe(startx, endy, startx, starty, 500);
			((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).Swipe(startx, starty, startx, endy, 500);
			
			int count=0;
			while(count!=3 && (!KeywordImplementation.waitForObjectExist(By.Name(objName),TimeSpan.FromSeconds(1))))
			{
				((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).Swipe(startx, endy, startx, starty, 500);
				count++;
			}
			
			if(KeywordImplementation.waitForObjectExist(By.Name(objName),TimeSpan.FromSeconds(1)))
				((AndroidDriver<OpenQA.Selenium.Appium.Android.AndroidElement>)Accessor.getDriver()).FindElementByAndroidUIAutomator("new UiSelector().text(\"" + objName + "\")");
				//KeywordImplementation.Click(By.Name(objName));
		}

		private void TapOnCoupling(string objName)
		{
			if(KeywordImplementation.UIObject(By.Id("Txt_ActionBar_Title"),TimeSpan.FromSeconds(5)).Text.Equals("Select Coupling"))
				KeywordImplementation.Click(By.Name(objName));
		}

		private void TapOnMonauralHearingSystem()
		{
			KeywordImplementation.Click(By.Id("Layout_Monaural_HearingSystem"));
		}

		
	}
}