/*
 * Created by Ranorex
 * User: IC014157
 * Date: 10/5/2016
 * Time: 3:39 PM
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
using AppLayer.AppiumService;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Appium.Service;
using System.Linq;


namespace STSiOSMFApp
{
	/// <summary>
	/// Description of CommonHelper.
	/// </summary>
	public class CommonHelper
	{
		public CommonHelper()
		{
		}
		
		public void ClickInfoButton()
		{
			KeywordImplementation.Click(By.Id("Back"));
			Logger.logInfo("Clicked on Info",true);
		}
		
		public static void BrandBasedHINames()
		{
			if(TestSuite.Current.Parameters["AppName"].Equals("com.signia.mobilefitting",StringComparison.CurrentCultureIgnoreCase))
			{
				TestSuite.Current.Parameters["H1"] = "Run P";
				TestSuite.Current.Parameters["H2"] = "Run SP";
				TestSuite.Current.Parameters["H3"] = "Run Click ITC";
				TestSuite.Current.Parameters["H4"] = "Run Click CIC";
			}
			else if(TestSuite.Current.Parameters["AppName"].Equals("com.connexx.fit2go",StringComparison.CurrentCultureIgnoreCase))
			{
				TestSuite.Current.Parameters["H1"] = "Targa P 5A";
				TestSuite.Current.Parameters["H2"] = "Targa HP 5A";
				TestSuite.Current.Parameters["H3"] = "inoX ITC 5A";
				TestSuite.Current.Parameters["H4"] = "inoX CIC 5A";
			}
			else if(TestSuite.Current.Parameters["AppName"].Equals("com.aandm.fit2go",StringComparison.CurrentCultureIgnoreCase))
			{
				TestSuite.Current.Parameters["H1"] = "XTM P A4";
				TestSuite.Current.Parameters["H2"] = "XTM XP A4";
				TestSuite.Current.Parameters["H3"] = "XTM IF ITC A4";
				TestSuite.Current.Parameters["H4"] = "XTM IF CIC A4";
			}
			else if(TestSuite.Current.Parameters["AppName"].Equals("com.audioservice.fit2go",StringComparison.CurrentCultureIgnoreCase))
			{
				TestSuite.Current.Parameters["H1"] = "Volta P M";
				TestSuite.Current.Parameters["H2"] = "Volta HP M";
				TestSuite.Current.Parameters["H3"] = "Volta quiX P M";
				TestSuite.Current.Parameters["H4"] = "Volta quiX M";
			}
			else
			{
				Logger.logFailure("AppName not found");
			}
		}

		public string HISelection(string HI)
		{
			if(HI.Equals("HA1"))
			{
				HI = TestSuite.Current.Parameters["H1"];
			}
			else if(HI.Equals("HA2"))
			{
				HI = TestSuite.Current.Parameters["H2"];
			}
			else if(HI.Equals("HA3"))
			{
				HI = TestSuite.Current.Parameters["H3"];
			}
			else if(HI.Equals("HA4"))
			{
				HI = TestSuite.Current.Parameters["H4"];
			}
			return HI;
		}

		public void SelectPageFromInfo(string pageName)
		{
			Logger.logInfo(string.Format("Select page '{0}' from info",pageName));
			if(pageName.Equals("Supported Hearing Aids",StringComparison.CurrentCultureIgnoreCase))
				KeywordImplementation.Click(By.Id("Supported Hearing Aids"));
			else if(pageName.Equals("Medical Info",StringComparison.CurrentCultureIgnoreCase))
				KeywordImplementation.Click(By.Id("Medical Info"));
			else if(pageName.Equals("App Info",StringComparison.CurrentCultureIgnoreCase))
				KeywordImplementation.Click(By.Id("App Info"));
			else if(pageName.Equals("FAQs",StringComparison.CurrentCultureIgnoreCase))
				KeywordImplementation.Click(By.Id("FAQs"));
			else if(pageName.Equals("Legal Notice",StringComparison.CurrentCultureIgnoreCase))
				KeywordImplementation.Click(By.Id("Legal Notice"));
			else if(pageName.Equals("Privacy Policy",StringComparison.CurrentCultureIgnoreCase))
				KeywordImplementation.Click(By.Id("Privacy Policy"));
			else if(pageName.Equals("Terms & Conditions",StringComparison.CurrentCultureIgnoreCase))
				//KeywordImplementation.Click(By.Id("Terms And Condition"));
				KeywordImplementation.Click(By.Id("Terms & Conditions"));
			Logger.logSnapshot();
		}
		
		public void ValidateInfoPage(string pageName)
		{
			if(KeywordImplementation.waitForObjectExist(By.Id(pageName),TimeSpan.FromSeconds(2)))
				Logger.logSuccess(string.Format("'{0}' page has opened",pageName));
			else
				Logger.validateFailure(string.Format("'{0}' page has not opened",pageName));
			Logger.logSnapshot();
		}
		
		public void ValidateContents(string contents)
		{
			string[] contentsArray= contents.Split(';');
			
			var size=Accessor.getDriver().Manage().Window.Size;
			
			/*****************************/
			//Swipe from Bottom to Top
			//Find swipe start and end point from screen's width and height.
			int starty = (int) (size.Height * 0.50);
			int endy = (int) (size.Height*0.20);
			int startx = size.Width / 2;
			
			foreach(string str in contentsArray)
			{
				bool check=false;
				
				int count=0;
				while(count!=10 && (!KeywordImplementation.waitForObjectExist(By.XPath("//*[contains(@value,'"+str+"')]"),TimeSpan.FromSeconds(1))))
				{
					((IOSDriver<IOSElement>)Accessor.getDriver()).Swipe(startx, starty, startx, endy, 1000);
					count++;
				}
				
				if(KeywordImplementation.waitForObjectExist(By.XPath("//*[contains(@value,'"+str+"')]"),TimeSpan.FromSeconds(1)))
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
		
		public void ValidateContentLabels(string contents)
		{
			string[] contentsArray= contents.Split(';');
			
			foreach(string str in contentsArray)
			{
				bool check=false;
				if(KeywordImplementation.waitForObjectExist(By.XPath("//*[contains(@label,'"+str+"')]"),TimeSpan.FromSeconds(1)))
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
		
		public void ValidateContentNames(string contents)
		{
			string[] contentsArray= contents.Split(';');
			
			foreach(string str in contentsArray)
			{
				bool check=false;
				
				if(KeywordImplementation.waitForObjectExist(By.XPath("//*[contains(@name,'"+str+"')]"),TimeSpan.FromSeconds(1)))
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
		
		public void CreatePatient(string lastName, string firstName)
		{
			CreatePatient(lastName,firstName,string.Empty);
		}
		
		public void CreatePatient(string lastName, string firstName, string Mmmmmmmmm_dd_yyyy)
		{
			if(!TestSuite.Current.Parameters["isTablet"].Equals("True",StringComparison.CurrentCultureIgnoreCase))
				KeywordImplementation.Click(By.Id("new"), "Click for New");
			KeywordImplementation.TypeText(By.Id("txtLastName"),lastName);
			KeywordImplementation.TypeText(By.Id("txtFirstName"),firstName);
			KeywordImplementation.Click(By.Id("Done"), "Click on Done");
			Logger.logInfo("Entered Last name and First name",true);
			
			if(Mmmmmmmmm_dd_yyyy != String.Empty)
			{
				SwipeForDOB(Mmmmmmmmm_dd_yyyy.Split('_')[0],Mmmmmmmmm_dd_yyyy.Split('_')[1],Mmmmmmmmm_dd_yyyy.Split('_')[2]);
			}
		}
		
		public void SwipeForDOB(string Mmmmmmmmm,string dd, string yyyy)
		{
			string custDOBFormat =KeywordImplementation.UIObject(By.Id("txtDOB")).Text;// Accessor.getDriver().FindElement(By.XPath("//UIATextField[3]")).Text;
			string[] dobFormatArray=null;
			if(custDOBFormat.Contains('/'))
				dobFormatArray=custDOBFormat.Split('/');
			else if(custDOBFormat.Contains('-'))
				dobFormatArray=custDOBFormat.Split('-');
			KeywordImplementation.Click(By.Id("txtDOB"));
			
			if(dobFormatArray[0].Contains("d"))
			{
				KeywordImplementation.UIObject(By.XPath("//UIAPickerWheel[1]")).SendKeys(dd);
				//Accessor.getDriver().FindElementByXPath("//UIAPickerWheel[1]").SendKeys(dd);
			}
			
			else if(dobFormatArray[0].Contains("M"))
			{
				KeywordImplementation.UIObject(By.XPath("//UIAPickerWheel[1]")).SendKeys(Mmmmmmmmm);
			}
			
			else if(dobFormatArray[0].Contains("y"))
			{
				KeywordImplementation.UIObject(By.XPath("//UIAPickerWheel[1]")).SendKeys(yyyy);
			}
			
			
			if(dobFormatArray[1].Contains("d"))
			{
				KeywordImplementation.UIObject(By.XPath("//UIAPickerWheel[2]")).SendKeys(dd);
			}
			
			else if(dobFormatArray[1].Contains("M"))
			{
				KeywordImplementation.UIObject(By.XPath("//UIAPickerWheel[2]")).SendKeys(Mmmmmmmmm);
			}
			
			else if(dobFormatArray[1].Contains("y"))
			{
				KeywordImplementation.UIObject(By.XPath("//UIAPickerWheel[2]")).SendKeys(yyyy);
			}
			
			if(dobFormatArray[2].Contains("d"))
			{
				KeywordImplementation.UIObject(By.XPath("//UIAPickerWheel[3]")).SendKeys(dd);
			}
			
			else if(dobFormatArray[2].Contains("M"))
			{
				KeywordImplementation.UIObject(By.XPath("//UIAPickerWheel[3]")).SendKeys(Mmmmmmmmm);
			}
			
			else if(dobFormatArray[2].Contains("y"))
			{
				KeywordImplementation.UIObject(By.XPath("//UIAPickerWheel[3]")).SendKeys(yyyy);
			}
			
			KeywordImplementation.Click(By.Id("Done"), "Date selected", true);
			
		}
		
		public void SelectCustomerContinue()
		{
			KeywordImplementation.SwipeVerticle(3,0.80,0.50,By.Id("Continue"));
			KeywordImplementation.Click(By.Id("Continue"), "Continue");
		}
		
		public void TapOnRightButton(string message)
		{
			KeywordImplementation.Click(By.Id("NavBarRight"),message);
		}
		
		public void TapOnLeftButton(string message)
		{
			KeywordImplementation.Click(By.Id("NavBarLeft"),message);
		}
		
		public void TabTapOnDetailNavBarRight(string message)
		{
			KeywordImplementation.Click(By.Id("DetailNavBarRight"),message);
		}
		
		public void TabTapOnDetailNavBarLeft(string message)
		{
			KeywordImplementation.Click(By.Id("DetailNavBarLeft"),message);
		}
		
		public void TapOnInfoButton(string message)
		{
			if(!TestSuite.Current.Parameters["isTablet"].Equals("True",StringComparison.CurrentCultureIgnoreCase))
			{
				KeywordImplementation.Click(By.Id("NavBarLeft"),message);
			}
			else
				KeywordImplementation.Click(By.Id("Info"),message);
		}
		
		public void SearchCustomer(string firstName)
		{
			//Delay.Seconds(3);
			KeywordImplementation.TypeText(By.Id("searchBar"),firstName);
//			((IOSDriver<IOSElement>)Accessor.getDriver()).Keyboard.SendKeys(Keys.Return);//.HideKeyboard.Swipe(startx, starty, startx, endy, 1000);
//			Delay.Seconds(1);
//			KeywordImplementation.Click(By.Id("searchBar"),firstName);
		}
		
		public void SelectCustomer(string lastName,string firstName)
		{
			KeywordImplementation.Click(By.XPath("//UIATableView[@name='Search results']//UIAStaticText[@name='"+lastName+", "+firstName+"']"));
		}
		
		public void ValidateLastName(string lastName)
		{
			if(KeywordImplementation.UIObject(By.Id("txtLastName")).Text.Contains(lastName))
				Logger.logSuccess(string.Format("LastName '{0}' is found",lastName));
			else
				Logger.logFailure(string.Format("LastName '{0}' is not found",lastName));
		}
		
		public void ValidateFirstName(string firstName)
		{
			if(KeywordImplementation.UIObject(By.Id("txtFirstName")).Text.Contains(firstName))
				Logger.logSuccess(string.Format("FirstName '{0}' is found",firstName));
			else
				Logger.logFailure(string.Format("FirstName '{0}' is not found",firstName));
		}
		
		public void ValidateCustomerEditableFields()
		{
			KeywordImplementation.Click(By.Id("txtLastName"));
			
			if(KeywordImplementation.waitForObjectExist(By.ClassName("UIAKeyboard"),TimeSpan.FromSeconds(3)) || KeywordImplementation.waitForObjectExist(By.XPath("//XCUIElementTypeWindow[2]"),TimeSpan.FromSeconds(3)))
			{
				Logger.logSuccess("'Last Name' field is editable");
				KeywordImplementation.Click(By.XPath("//UIAWindow[2]//UIAButton[contains(@name,'Done')]"));
			}
			else
				Logger.logFailure("'Last Name' field is not editable");
			
			KeywordImplementation.Click(By.Id("txtFirstName"));
			if(KeywordImplementation.waitForObjectExist(By.ClassName("UIAKeyboard"),TimeSpan.FromSeconds(3)) || KeywordImplementation.waitForObjectExist(By.XPath("//XCUIElementTypeWindow[2]"),TimeSpan.FromSeconds(3)))
			{
				Logger.logSuccess("'First Name' field is editable");
				KeywordImplementation.Click(By.XPath("//UIAWindow[2]//UIAButton[contains(@name,'Done')]"));
			}
			else
				Logger.logFailure("'First Name' field is not editable");
			
			KeywordImplementation.Click(By.Id("txtDOB"));
			if(KeywordImplementation.waitForObjectExist(By.ClassName("UIAPicker"),TimeSpan.FromSeconds(3)) || KeywordImplementation.waitForObjectExist(By.ClassName("XCUIElementTypeDatePicker"),TimeSpan.FromSeconds(3)))
			{
				Logger.logSuccess("'DOB' field is editable");
//				if(KeywordImplementation.UIObject(By.Id("txtDOB")).Text.Contains("yyyy"))
				if(KeywordImplementation.waitForObjectExist(By.Id("txtDOB"),TimeSpan.FromSeconds(1)))
					KeywordImplementation.Click(By.Id("Clear"));
				else
				{
					if(TestSuite.Current.Parameters["Version"].Equals("10",StringComparison.CurrentCultureIgnoreCase))
						KeywordImplementation.Click(By.XPath("//XCUIElementTypeOther/XCUIElementTypeButton[contains(@name,'Done')]"));
					else
						KeywordImplementation.Click(By.XPath("//UIAScrollView//UIAButton[contains(@name,'Done')]"));
				}
			}
			else
				Logger.logFailure("'DOB' field is not editable");
			
			if(KeywordImplementation.UIObject(By.Id("switchAudiogramEnable"),TimeSpan.FromSeconds(5)).Enabled)
				Logger.logSuccess("'Audiogram available' field is editable");
			else
				Logger.logFailure("'Audiogram available' field is not editable");
			
			KeywordImplementation.Click(By.Id("Notes"));
			if(KeywordImplementation.waitForObjectExist(By.Id("Txtnotes"),TimeSpan.FromSeconds(3)))
			{
				Logger.logSuccess("'Notes' field is expanded");
				KeywordImplementation.Click(By.XPath("//UIAWindow[2]//UIAButton[contains(@name,'Done')]"));
			}
			else
				Logger.logFailure("'Notes' field is not expanded");
		}

		public void VerifySwitchAudiogramAvailable(bool on)
		{
			Delay.Seconds(2);
			var switchAudiogram=KeywordImplementation.UIObject(By.Id("switchAudiogramEnable"),TimeSpan.FromSeconds(1));
			Logger.logInfo("Switch Audiogram value is "+switchAudiogram.GetAttribute("value"));
			string message=null;
			if((on.Equals(true) && (switchAudiogram.GetAttribute("value").Equals("1") || switchAudiogram.GetAttribute("value").Equals("true"))) ||  (on.Equals(false) && (switchAudiogram.GetAttribute("value").Equals("0") || switchAudiogram.GetAttribute("value").Equals("false"))))
			{
				message= on? "Switch Audiogram is on":"Switch Audiogram is off";
				Logger.logSuccess(message,true);
			}
			else
			{
				message=on? "Switch Audiogram is off":"Switch Audiogram is on";
				Logger.logFailure(message,true);
			}
			
			//	Logger.ConditionalValidation((on.Equals(true) && switchAudiogram.GetAttribute("value").Equals("1")) ||  (on.Equals(false) && switchAudiogram.GetAttribute("value").Equals("0")),on,"Switch Audiogram is on","Switch Audiogram is off");
		}
		
		public void ValidateContentsContains(string contents,bool isFound )
		{
			string[] contentsArray= contents.Split(';');
			
			var size=Accessor.getDriver().Manage().Window.Size;
			
			/*****************************/
			//Swipe from Bottom to Top
			//Find swipe start and end point from screen's width and height.
			int starty = (int) (size.Height * 0.20);
			int endy = (int) (size.Height*0.50);
			int startx = size.Width / 2;
			
			foreach(string str in contentsArray)
			{
				bool check=false;
				var textViewArray=((AndroidDriver<OpenQA.Selenium.Appium.iOS.IOSElement>)Accessor.getDriver()).FindElementsByXPath("//UIAStaticText[@visible='true']");
				int swipeCount=0;
				while(swipeCount!=3)
				{
					foreach(var textView in textViewArray)
					{
						if(textView.GetAttribute("value").Contains(str))
						{
							check=true;
							break;
						}
					}
					if(!check)
						((IOSDriver<IOSElement>)Accessor.getDriver()).Swipe(startx, starty, startx, endy, 1000);
					else
						break;
					textViewArray=((AndroidDriver<OpenQA.Selenium.Appium.iOS.IOSElement>)Accessor.getDriver()).FindElementsByXPath("//UIAStaticText[@visible='true']");
					swipeCount++;
				}
				
				string message=null;
				if(check==isFound)
				{
					message=isFound?string.Format("Information '{0}' text has found",str):string.Format("Information '{0}' text has not found",str);
					Logger.logSuccess(message);
				}
				else
				{
					message=isFound?string.Format("Information '{0}' text has not found",str):string.Format("Information '{0}' text has found",str);
					Logger.logFailure(message);
				}
				
			}
			Logger.logSnapshot();
		}
		
		public void ValidateWorkflowEnabled(string workflow, bool isEnabled)
		{
			Logger.ConditionalValidation(KeywordImplementation.UIObject(By.Id(workflow)).Enabled,isEnabled,string.Format("{0} is enabled",workflow),string.Format("{0} is disabled",workflow));
		}

		public void VerifyActionBarTitle(string title)
		{
			//if(KeywordImplementation.waitForObjectExist(By.Id(title),TimeSpan.FromSeconds(5)))
			if(KeywordImplementation.waitForObjectExist(By.XPath("//UIANavigationBar//UIAStaticText[contains(@name,'"+title+"')]"),TimeSpan.FromSeconds(5)))
				Logger.logSuccess(title+" has found");
			else
				Logger.logFailure(title+" has not found");
			Logger.logSnapshot();
		}

		public void TapNew()
		{
			Logger.logInfo("Select New patient");
			KeywordImplementation.Click(By.Id("new"));
		}

		public void ValidateCustomerScreen()
		{
			if(KeywordImplementation.waitForObjectExist(By.Id("new"),TimeSpan.FromSeconds(2)) && KeywordImplementation.waitForObjectExist(By.Id("list"),TimeSpan.FromSeconds(2)))
				Logger.logSuccess("Customer screen has opened with new and list segmented control");
			else
				Logger.logFailure("Customer screen has not opened");
		}
		
		public void ValidateDOB(string d_m_yyyy)
		{
			string custDOBFormat = Accessor.getDriver().FindElement(By.Id("txtDOB")).Text;
			string[] custDobFormatArray=null;
			if(custDOBFormat.Contains('/'))
				custDobFormatArray=custDOBFormat.Split('/');
			else if(custDOBFormat.Contains('-'))
				custDobFormatArray=custDOBFormat.Split('-');
			
			string[] dobFormatArray=d_m_yyyy.Split('_');
			bool check=false;
			foreach(string dob in dobFormatArray)
			{
				check=false;
				foreach(string custDob in custDobFormatArray)
				{
					if(custDob.Contains(dob))
						//if(dob.Contains(custDob))
					{
						check=true;
						break;
					}
					
				}
				if(!check)
				{
					Logger.logFailure("DOB has not entered correctly",true);
					break;
				}
				
			}
			if(check)
			{
				Logger.logSuccess("DOB has entered correctly",true);
			}
		}

		public void ValidateCustomerContinueEnabled(bool enabled)
		{
			var size=Accessor.getDriver().Manage().Window.Size;
			
			//Swipe from Bottom to Top and Top to bottom
			//Find swipe start and end point from screen's width and height.
			int starty = (int) (size.Height * 0.50);
			int endy = (int) (size.Height*0.20);
			int startx = size.Width / 2;
			
			int count=0;
			while(count!=3 && (!KeywordImplementation.waitForObjectExist(By.Id("Continue"),TimeSpan.FromSeconds(1))))
			{
				((IOSDriver<IOSElement>)Accessor.getDriver()).Swipe(startx, endy, startx, starty, 500);
				count++;
			}
			string message=null;
			if(KeywordImplementation.UIObject(By.Id("Continue")).Enabled==enabled)
			{
				message=enabled?"Continue button is enabled":"Continue button is disabled";
				Logger.logSuccess(message);
			}
			else
			{
				message=enabled?"Continue button is disabled":"Continue button is enabled";
				Logger.logFailure(message);
			}
			Logger.logSnapshot();
		}
		
		public void StartApp()
		{
			//iOSSetup.RunApplication();
			TestModuleRunner.Run(new IOSSetup());
		}

		public void CloseApp()
		{
			TearDown.CloseApplication();
		}
		
		public void ClickOnAlertMessage(string option)
		{
			if(KeywordImplementation.waitForObjectExist(By.XPath("//UIAAlert//UIAButton[@name='"+option+"']"),TimeSpan.FromSeconds(4)))
			{
				KeywordImplementation.Click(By.XPath("//UIAAlert//UIAButton[@name='"+option+"']"));
			}
			Ranorex.Delay.Seconds(2);
		}

		public void ValidateALertMessage(string message)
		{
			if(KeywordImplementation.waitForObjectExist(By.XPath("//*[contains(@value,'"+message+"')]"),TimeSpan.FromSeconds(2)))
				Logger.logSuccess(string.Format("Popup displayed the message '{0}'",message));
			else
				Logger.logFailure(string.Format("Popup doesn't displayed the message '{0}",message));
			Logger.logSnapshot();
		}
		
		public void	ValidateALertMessage( string message, bool isAlert)
		{
			if(isAlert)
			{
				if(KeywordImplementation.waitForObjectExist(By.XPath("//*[contains(@value,'"+message+"')]"),TimeSpan.FromSeconds(2)))
				{
					Logger.logSuccess(string.Format("Popup displayed the message '{0}'",message));
					ClickOnAlertMessage("OK");
				}
				else
					Logger.logFailure(string.Format("Popup doesn't displayed the message '{0}",message));
			}
			else if(KeywordImplementation.waitForObjectExist(By.XPath("//*[contains(@value,'"+message+"')]"),TimeSpan.FromSeconds(2)))
			{
				Logger.logFailure(string.Format("Popup displayed the message '{0}'",message));
				ClickOnAlertMessage("OK");
			}
			Logger.logSnapshot();
		}
		
		public void TapOn(string objectName)
		{
			Logger.logInfo(string.Format("Tap on '{0}'",objectName));
			KeywordImplementation.Click(By.Id(objectName));
		}
		
		public void TapOnWithValue(string objectName)
		{
			Logger.logInfo(string.Format("Tap on '{0}'",objectName));
			var size=Accessor.getDriver().Manage().Window.Size;
			int starty = (int) (size.Height * 0.50);
			int endy = (int) (size.Height*0.20);
			int startx = size.Width / 2;
			int count=0;
			while(count!=3 && (!KeywordImplementation.waitForObjectExist(By.XPath("//*[contains(@value,'"+objectName+"')]"),TimeSpan.FromSeconds(1))))
			{
				((IOSDriver<IOSElement>)Accessor.getDriver()).Swipe(startx, endy, startx, starty, 500);
				count++;
			}
			
			KeywordImplementation.Click(By.XPath("//*[contains(@value,'"+objectName+"')]"));
		}
		
		public void TapOnOK()
		{
			if(KeywordImplementation.waitForObjectExist(By.Id("OK"),TimeSpan.FromSeconds(2)))
				KeywordImplementation.Click(By.Id("OK"));
		}
		
		public void ValidateAudiogramPoints(string side, string points)
		{
			if(side.Equals("right",StringComparison.CurrentCultureIgnoreCase))
			{
				KeywordImplementation.Click(By.Id("Right"));
			}
			if(side.Equals("left",StringComparison.CurrentCultureIgnoreCase))
			{
				KeywordImplementation.Click(By.Id("Left"));
			}
			
			string[] aPoints=points.Split(';');
			//	Dictionary<int, int> auPoints=aPoints.ToDictionary(sKey=>int.Parse(sKey.Split(',')[0]),sValue=>int.Parse(sValue.Split(',')[1]));
			Dictionary<string, string> auPoints=aPoints.ToDictionary(sKey=>sKey.Split(',')[0],sValue=>sValue.Split(',')[1]);

			IWebElement elementImgbtnAudiNext = Accessor.getDriver().FindElement(By.Id("ImgBtn_Audi_Next"));
			IWebElement elementImgbtnAudiPrev = Accessor.getDriver().FindElement(By.Id("ImgBtn_Audi_Prev"));
			
			elementImgbtnAudiPrev.Click();
			elementImgbtnAudiPrev.Click();

			bool check=false;
			int count=0;
			
			foreach(KeyValuePair<string, string> pair in auPoints)
			{
				check=false;
				
				if((pair.Key.ToString().Contains("4000")|| pair.Key.ToString().Contains("8000")) && count==0)
				{
					elementImgbtnAudiNext.Click();
					elementImgbtnAudiNext.Click();
					count++;
					
				}
				else if((pair.Key.ToString().Contains("250")|| pair.Key.ToString().Contains("500")||pair.Key.ToString().Contains("1000")|| pair.Key.ToString().Contains("2000")) && count==1)
				{
					elementImgbtnAudiPrev.Click();
					elementImgbtnAudiPrev.Click();
					count--;
				}
				string slValue=null;
				slValue= pair.Value.ToString();
				if(pair.Key.ToString().Contains("250") )
				{
					if((slValue.Equals(string.Empty) && KeywordImplementation.UIObject(By.Id(side+"TextField250Hz"),TimeSpan.FromSeconds(3)).GetAttribute("value")==null) || (KeywordImplementation.UIObject(By.Id(side+"TextField250Hz"),TimeSpan.FromSeconds(3)).GetAttribute("value").Contains(slValue)) )
						check=true;
				}
				else if(pair.Key.ToString().Contains("500") )
				{
					if((slValue.Equals(string.Empty) && KeywordImplementation.UIObject(By.Id(side+"TextField500Hz"),TimeSpan.FromSeconds(3)).GetAttribute("value")==null) || (KeywordImplementation.UIObject(By.Id(side+"TextField500Hz"),TimeSpan.FromSeconds(3)).GetAttribute("value").Contains(slValue)) )
						check=true;
				}
				else if(pair.Key.ToString().Contains("1000") )
				{
					if((slValue.Equals(string.Empty) && KeywordImplementation.UIObject(By.Id(side+"TextField1000Hz"),TimeSpan.FromSeconds(3)).GetAttribute("value")==null) || (KeywordImplementation.UIObject(By.Id(side+"TextField1000Hz"),TimeSpan.FromSeconds(3)).GetAttribute("value").Contains(slValue)) )
						check=true;
				}
				else if(pair.Key.ToString().Contains("2000") )
				{
					if((slValue.Equals(string.Empty) && KeywordImplementation.UIObject(By.Id(side+"TextField2000Hz"),TimeSpan.FromSeconds(3)).GetAttribute("value")==null) || (KeywordImplementation.UIObject(By.Id(side+"TextField2000Hz"),TimeSpan.FromSeconds(3)).GetAttribute("value").Contains(slValue)) )
						check=true;
				}
				else if(pair.Key.ToString().Contains("4000") )
				{
					if((slValue.Equals(string.Empty) && KeywordImplementation.UIObject(By.Id(side+"TextField4000Hz"),TimeSpan.FromSeconds(3)).GetAttribute("value")==null) || (KeywordImplementation.UIObject(By.Id(side+"TextField4000Hz"),TimeSpan.FromSeconds(3)).GetAttribute("value").Contains(slValue)) )
						check=true;
				}
				else if(pair.Key.ToString().Contains("8000") )
				{
					if((slValue.Equals(string.Empty) && KeywordImplementation.UIObject(By.Id(side+"TextField8000Hz"),TimeSpan.FromSeconds(3)).GetAttribute("value")==null) || (KeywordImplementation.UIObject(By.Id(side+"TextField8000Hz"),TimeSpan.FromSeconds(3)).GetAttribute("value").Contains(slValue)) )
						check=true;
				}

				if(!check)
				{
					Logger.logFailure("Audiogram point validation failed",true);
					break;
				}
			}
			if(check)
				Logger.logSuccess("Audiogram point validation passed",true);
		}

		public void DrawAudiogramPoints(string side, string points)
		{
			Logger.logInfo(string.Format("Draw audiogram points \"{0}\" for {1} side",points,side));
			
			KeywordImplementation.Click(By.Id(side));
			
			string[] aPoints=points.Split(';');
			Dictionary<int, int> auPoints=aPoints.ToDictionary(sKey=>int.Parse(sKey.Split(',')[0]),sValue=>int.Parse(sValue.Split(',')[1]));
			
			
			foreach(KeyValuePair<int, int> pair in auPoints)
			{
				Ranorex.Report.Info(pair.Key+"   "+pair.Value);
			}
			
			int lTemp=0;
			int rTemp=0;
			
			foreach(KeyValuePair<int, int> pair in auPoints)
			{
				string audiTextBox=string.Empty;
				if((pair.Key.ToString().Contains("250") || pair.Key.ToString().Contains("500") || pair.Key.ToString().Contains("1000")) && lTemp==0)
				{
					lTemp++;
					rTemp=0;
					if(!TestSuite.Current.Parameters["isTablet"].Equals("True",StringComparison.CurrentCultureIgnoreCase))
					{
						KeywordImplementation.Click(By.Id("ImgBtn_Audi_Prev"));
						KeywordImplementation.Click(By.Id("ImgBtn_Audi_Prev"));
					}
					
				}
				if((pair.Key.ToString().Contains("2000") || pair.Key.ToString().Contains("4000") || pair.Key.ToString().Contains("8000")) && rTemp==0)
				{
					rTemp++;
					lTemp=0;
					if(!TestSuite.Current.Parameters["isTablet"].Equals("True",StringComparison.CurrentCultureIgnoreCase))
					{
						KeywordImplementation.Click(By.Id("ImgBtn_Audi_Next"));
						KeywordImplementation.Click(By.Id("ImgBtn_Audi_Next"));
					}
				}
				
				audiTextBox=side+"TextField"+pair.Key.ToString()+"Hz";
				KeywordImplementation.Click(By.Id(audiTextBox));
				KeywordImplementation.UIObject(By.XPath("//UIAPickerWheel[1]")).SendKeys(pair.Value+" dB");
				KeywordImplementation.Click(By.XPath("//UIAButton[contains(@name,'Done')]"));
				
			}
		}
		
		public void CloseSession()
		{
			Logger.logInfo("Close session");
			KeywordImplementation.Click(By.Id("Close Session"));
			
			//Handling Rate the App pop-up
			if(KeywordImplementation.waitForObjectExist(By.Id("How do you like the app"),TimeSpan.FromSeconds(2)))
			{
				Logger.logInfo("Tap on 'Please remind me later'",true);
				KeywordImplementation.Click(By.Id("Please remind me later"));
			}
		}

		public void SoundComfortOk()
		{
			Logger.logInfo("Tap on ok");
			var size=Accessor.getDriver().Manage().Window.Size;
			
			//Swipe from Bottom to Top and Top to bottom
			//Find swipe start and end point from screen's width and height.
			int starty = (int) (size.Height * 0.70);
			int endy = (int) (size.Height*0.20);
			int startx = size.Width / 2;
			((IOSDriver<IOSElement>)Accessor.getDriver()).Swipe(startx, starty, startx, endy, 500);
			KeywordImplementation.Click(By.Id("OK"));
		}

		public void SoundSharper()
		{
			Bitmap imageBeforeSoundChange = new Bitmap(Logger.CaptureSnapshot(By.Id("Layout_SoundComfort_Sharp")));
			KeywordImplementation.Click(By.Id("soundPlusButton"));
			KeywordImplementation.Click(By.Id("soundPlusButton"));
			Bitmap imageAfterSoundChange = new Bitmap(Logger.CaptureSnapshot(By.Id("Layout_SoundComfort_Sharp")));
			Logger.logInfo("Validating Sound has changed");
			Logger.ValidateImagesChanged(imageBeforeSoundChange,imageAfterSoundChange,true);
			//bool a = Ranorex.Imaging.Compare( imageBeforeSoundChange, imageAfterSoundChange);
		}

		public void VolumeIncrease()
		{
			Bitmap imageBeforeVolumeChange = new Bitmap(Logger.CaptureSnapshot(By.Id("Layout_SoundComfort_Volume")));
			KeywordImplementation.Click(By.Id("volumePlusButton"));
			KeywordImplementation.Click(By.Id("volumePlusButton"));
			Bitmap imageAfterVolumeChange = new Bitmap(Logger.CaptureSnapshot(By.Id("Layout_SoundComfort_Volume")));
			Logger.logInfo("Validating Volume has changed");
			Logger.ValidateImagesChanged(imageBeforeVolumeChange,imageAfterVolumeChange,true);
		}

		public void ConnectHI()
		{
			var size=Accessor.getDriver().Manage().Window.Size;
			
			/*****************************/
			//Swipe from Bottom to Top
			//Find swipe start and end point from screen's width and height.
			int starty = (int) (size.Height * 0.50);
			int endy = (int) (size.Height*0.20);
			int startx = size.Width / 2;
			((IOSDriver<IOSElement>)Accessor.getDriver()).Swipe(startx, starty, startx, endy, 500);
			
			Logger.logInfo("Tap on Connect HI");
			KeywordImplementation.Click(By.Id("Connect Hearing Aid"));
			//ValidateALertMessage("Please switch OFF hearing aid for right ear side.");
			if(!TestSuite.Current.Parameters["isTablet"].Equals("True",StringComparison.CurrentCultureIgnoreCase))
			{
				ClickOnAlertMessage("OK");
				Logger.logInfo("Tap on Yes for the - hear the confirmation tone from the hearing aid");
				KeywordImplementation.Click(By.Id("Yes"));
				//	ValidateALertMessage("Pairing and initial programming complete.");
				KeywordImplementation.Click(By.Id("OK"));
			}
		}

		public void SelectPage(string pageName)
		{
			KeywordImplementation.Click(By.Id(pageName));
			Logger.logInfo(string.Format("Selected page '{0}'",pageName),true);
		}

		public void ValidateMonauralClusterSelected(string cluster, string side)
		{
			var monauralClusterSelected=KeywordImplementation.UIObject(By.Id("Txt_Monaural_ComfortProfile"));
			
			if(monauralClusterSelected.GetAttribute("value").Contains(cluster))
				Logger.logSuccess("Monaural cluster "+cluster+" is selected");
			else
				Logger.logFailure("Monaural cluster "+cluster+" is not selected");
			
			if(monauralClusterSelected.GetAttribute("value").Contains(side))
				Logger.logSuccess("Monaural cluster recommendation has given for "+side+" ear");
			else
				Logger.logFailure("Monaural coupling recommendation has not given for "+side+" ear");
		}

		public void ValidateMonauralCouplingSelected(string coupling, string side)
		{
			var monauralCouplingSelected=KeywordImplementation.UIObject(By.Id("Txt_Monaural_MoldSelected"));

			if(monauralCouplingSelected.GetAttribute("value").Contains(coupling))
				Logger.logSuccess("Monaural coupling "+coupling+" is selected");
			else
				Logger.logFailure("Monaural coupling "+coupling+" is not selected");

			if(monauralCouplingSelected.GetAttribute("value").Contains(side))
				Logger.logSuccess("Monaural coupling recommendation has given for "+side+" ear");
			else
				Logger.logFailure("Monaural coupling recommendation has not given for "+side+" ear");
		}

		public void ValidateMonauralHISelected(string HI, string side)
		{
			HI = HISelection(HI);
			var monauralHISelected=KeywordImplementation.UIObject(By.Id("Txt_Monaural_HISelected"));

			if(monauralHISelected.GetAttribute("value").Contains(HI))
				Logger.logSuccess("Monaural HI "+HI+" is selected");
			else
				Logger.logFailure("Monaural HI "+HI+" is not selected");
			
			if(monauralHISelected.GetAttribute("value").Contains(side))
				Logger.logSuccess("Monaural HI recommendation has given for "+side+" ear");
			else
				Logger.logFailure("Monaural HI recommendation has not given for "+side+" ear");
		}
		
		public void ValidateBinauralClusterSelected(string cluster, string side)
		{
			IWebElement clusterElement=null;
			if(side.Equals("Right",StringComparison.CurrentCultureIgnoreCase))
				clusterElement=KeywordImplementation.UIObject(By.Id("Txt_Binaural_ComfortProfileRight"));
			else if(side.Equals("Left",StringComparison.CurrentCultureIgnoreCase))
				clusterElement=KeywordImplementation.UIObject(By.Id("Txt_Binaural_ComfortProfileLeft"));
			
			if(clusterElement.GetAttribute("value").Contains(cluster))
				Logger.logSuccess(string.Format("Profile '{0}' is selcted for '{1}' side",cluster,side));
			else
				Logger.logFailure(string.Format("Profile '{0}' is not selcted for '{1}' side",cluster,side));
		}

		public void ValidateBinauralCouplingSelected(string coupling, string side)
		{
			IWebElement couplingElement=null;
			if(side.Equals("Right",StringComparison.CurrentCultureIgnoreCase))
				couplingElement=KeywordImplementation.UIObject(By.Id("Txt_Binaural_MoldSelectedRight"));
			else if(side.Equals("Left",StringComparison.CurrentCultureIgnoreCase))
				couplingElement=KeywordImplementation.UIObject(By.Id("Txt_Binaural_MoldSelectedLeft"));
			
			if(couplingElement.GetAttribute("value").Contains(coupling))
				Logger.logSuccess(string.Format("Coupling '{0}' is selcted for '{1}' side",coupling,side));
			else
				Logger.logFailure(string.Format("Coupling '{0}' is not selcted for '{1}' side",coupling,side));
		}

		public void ValidateBinauralHISelected(string HI, string side)
		{
			HI = HISelection(HI);
			IWebElement hiElement=null;
			if(side.Equals("Right",StringComparison.CurrentCultureIgnoreCase))
				hiElement=KeywordImplementation.UIObject(By.Id("Txt_Binaural_HISelectedRight"));
			else if(side.Equals("Left",StringComparison.CurrentCultureIgnoreCase))
				hiElement=KeywordImplementation.UIObject(By.Id("Txt_Binaural_HISelectedLeft"));
			
			if(hiElement.GetAttribute("value").Contains(HI))
				Logger.logSuccess(string.Format("HI '{0}' is selcted for '{1}' side",hiElement.Text,side));
			else
				Logger.logFailure(string.Format("HI '{0}' is not selcted for '{1}' side",hiElement.Text,side));
		}

		public void AcceptAndContinueWelcomeScreen()
		{
			var size=Accessor.getDriver().Manage().Window.Size;
			
			//Swipe from Bottom to Top and Top to bottom
			//Find swipe start and end point from screen's width and height.
			int starty = (int) (size.Height * 0.50);
			int endy = (int) (size.Height*0.20);
			int startx = size.Width / 2;
			
			int count=0;
			while(count!=3 && (!KeywordImplementation.waitForObjectExist(By.Id("Accept & Continue"),TimeSpan.FromSeconds(1))))
			{
				((IOSDriver<IOSElement>)Accessor.getDriver()).Swipe(startx, starty, startx, endy, 500);
				count++;
			}
			KeywordImplementation.Click(By.Id("AgreeCheckBox"));
			Logger.logInfo("Welcome Page screen is available",true);
			KeywordImplementation.Click(By.Id("Accept & Continue"));
		}
		
		public void EnterCorrectCode()
		{
			Logger.logInfo("Enter correct code");
			KeywordImplementation.TypeText(By.Id("Passcode1"),"8981");
			KeywordImplementation.TypeText(By.Id("Passcode2"),"9313");
			KeywordImplementation.TypeText(By.Id("Passcode3"),"9666");
		}

		public void EnterPartialCode()
		{
			Logger.logInfo("Enter partial code");
			KeywordImplementation.TypeText(By.Id("Passcode1"),"8113");
		}

		public void SelectCountry(string country)
		{
			var size=Accessor.getDriver().Manage().Window.Size;
			
			int starty = (int) (size.Height * 0.70);
			int endy = (int) (size.Height*0.20);
			int startx = size.Width / 2;
			int count=0;
			
			while(count!=20 && (!KeywordImplementation.waitForObjectExist(By.Id(country),TimeSpan.FromSeconds(1))))
			{
				((IOSDriver<IOSElement>)Accessor.getDriver()).Swipe(startx, starty, startx, endy, 500);
				count++;
			}
			KeywordImplementation.Click(By.Id(country));
		}
		
		public void SwitchSettingUseMold(bool YesNo)
		{
			var switchUseMold=KeywordImplementation.UIObject(By.Id("switchSettingMolds"),TimeSpan.FromSeconds(1));
			
			if((YesNo.Equals(true) && (switchUseMold.GetAttribute("value").Equals("0") || switchUseMold.GetAttribute("value").Equals("false"))) ||  (YesNo.Equals(false) && (switchUseMold.GetAttribute("value").Equals("1") || switchUseMold.GetAttribute("value").Equals("true"))))
			{
				switchUseMold.Click();
				Report.Success("Switch Omniture is " + switchUseMold.GetAttribute("value"));
			}
			Logger.logSnapshot();
		}

		public void TapOnWithSwipe(string objectName)
		{
			objectName = HISelection(objectName);
			var size=Accessor.getDriver().Manage().Window.Size;
			
			int starty = (int) (size.Height * 0.50);
			int endy = (int) (size.Height*0.20);
			int startx = size.Width / 2;
			
			int count=0;
			
			while(count!=3 && (!KeywordImplementation.waitForObjectExist(By.Id(objectName),TimeSpan.FromSeconds(1))))
			{
				((IOSDriver<IOSElement>)Accessor.getDriver()).Swipe(startx, starty, startx, endy, 500);
				count++;
			}
			KeywordImplementation.Click(By.Id(objectName));
		}
		
		public void ValidateRecommendedHI(string HI,string side)
		{
			KeywordImplementation.SwipeVerticle(3,0.5,0.2,By.Id("Recommended"));
			
			string recommendedHI=KeywordImplementation.UIObject(By.Id("Recommended")).GetAttribute("value");
			
			if(recommendedHI.Contains(side))
				Logger.logSuccess("Recommended HI has given for "+side+" ear");
			else
				Logger.logFailure("Recommended HI has not given for "+side+" ear");
			
			if(recommendedHI.Contains(HI))
				Logger.logSuccess(string.Format("Recommended HI is '{0}'",HI));
			else
				Logger.logFailure(string.Format("Recommended HI is '{0}' instead of '{1}'",recommendedHI,HI));
		}
		
		public void ValidateRecommendedCoupling(string coupling,string side)
		{
			KeywordImplementation.SwipeVerticle(3,0.5,0.2,By.Id("Recommended"));
			
			string recommendedCoupling=KeywordImplementation.UIObject(By.Id("Recommended")).GetAttribute("value");
			
			if(recommendedCoupling.Contains(side))
				Logger.logSuccess("Recommended coupling has given for "+side+" ear");
			else
				Logger.logFailure("Recommended coupling has not given for "+side+" ear");
			
			if(recommendedCoupling.Contains(coupling))
				Logger.logSuccess(string.Format("Recommended coupling is '{0}'",coupling));
			else
				Logger.logFailure(string.Format("Recommended coupling is '{0}' instead of '{1}'",recommendedCoupling,coupling));
		}
		
		public void ValidateSuitableCluster(string cluster,string side)
		{
			KeywordImplementation.SwipeVerticle(3,0.5,0.2,By.Id("Suitable"));
			
			string suitableCluster=KeywordImplementation.UIObject(By.Id("Suitable")).GetAttribute("value");
			
			if(suitableCluster.Contains(side))
				Logger.logSuccess("Suitable cluster has given for "+side+" ear");
			else
				Logger.logFailure("Suitable cluster has not given for "+side+" ear");
			
			if(suitableCluster.Contains(cluster))
				Logger.logSuccess(string.Format("Suitable cluster is '{0}'",cluster));
			else
				Logger.logFailure(string.Format("Suitable cluster is '{0}' instead of '{1}'",suitableCluster,cluster));
		}

//		public void	ValidateALertMessage( string message)
//		{
//			if(KeywordImplementation.waitForObjectExist(By.Id("OK"),TimeSpan.FromSeconds(2)))
//			{
//				if(KeywordImplementation.waitForObjectExist.UIObject(By.Id(message),TimeSpan.FromSeconds(1)).Text.Contains(message))
//					Logger.logSuccess(string.Format("Popup displayed the message '{0}'",message));
//				else
//					Logger.logFailure(string.Format("Popup displayed the message '{0}'",KeywordImplementation.UIObject(By.Id("Txt_CommonAlert_Msg"),TimeSpan.FromSeconds(1)).Text));
//				Logger.logSnapshot();
//			}
//
//		}
		
		public void DeleteClient()
		{
			KeywordImplementation.Click(By.Id("Delete Client"));
			if(KeywordImplementation.waitForObjectExist(By.ClassName("UIAAlert"),TimeSpan.FromSeconds(3)))
				KeywordImplementation.Click(By.Id("OK"));
		}
		
		public void TabVerifyNavBarTitle(string title)
		{
			if(KeywordImplementation.UIObject(By.Id("NavBarTitle"),TimeSpan.FromSeconds(5)).Text.Equals(title))
				Logger.logSuccess(title+" has found");
			else
				Logger.logFailure(title+" has not found");
			Logger.logSnapshot();
		}
		
		public void TabVerifyNavBarFititngTitle(string title)
		{
			if(KeywordImplementation.UIObject(By.Id("NavBarFittingTitle"),TimeSpan.FromSeconds(5)).Text.Equals(title))
				Logger.logSuccess(title+" has found");
			else
				Logger.logFailure(title+" has not found");
			Logger.logSnapshot();
		}
		
		public void TabVerifyNavBarDetailTitle(string title)
		{
			if(KeywordImplementation.UIObject(By.Id("NavBarDetailTitle"),TimeSpan.FromSeconds(5)).Text.Equals(title))
				Logger.logSuccess(title+" has found");
			else
				Logger.logFailure(title+" has not found");
			Logger.logSnapshot();
		}
		
		public void SendDataToHearingAid(string side)
		{
			Logger.logInfo(string.Format("Tap on 'Send Data to Hearing Aid' for {0} side",side));
			if(side.Equals("Right",StringComparison.CurrentCultureIgnoreCase))
				KeywordImplementation.Click(By.Id("RightEar_Send Data to Hearing Aid"));
			else if(side.Equals("Left",StringComparison.CurrentCultureIgnoreCase))
				KeywordImplementation.Click(By.Id("LeftEar_Send Data to Hearing Aid"));
		}

		public void TapOnBinauralHI(string message, string side)
		{
			Logger.logInfo(message);
			if(side.Equals("Right",StringComparison.CurrentCultureIgnoreCase))
				KeywordImplementation.Click(By.Id("Txt_Binaural_HISelectedRight"));
			else if(side.Equals("Left",StringComparison.CurrentCultureIgnoreCase))
				KeywordImplementation.Click(By.Id("Txt_Binaural_HISelectedLeft"));
		}

		public void EditAudiogram()
		{
			Logger.logInfo("Select the Audiogram Tab");
			KeywordImplementation.Click(By.Id("Layout_Monaural_Audiogram"));
			if(!TestSuite.Current.Parameters["isTablet"].Equals("True",StringComparison.CurrentCultureIgnoreCase))
				TapOnRightButton("Tap on 'Edit' ");
			else
				TabTapOnDetailNavBarRight("Tap on 'Edit' ");
		}

		public void ConfirmationToneYes()
		{
			ClickOnAlertMessage("OK");
			Logger.logInfo("Tap on Yes for the - hear the confirmation tone from the hearing aid");
			KeywordImplementation.Click(By.Id("Yes"));
			KeywordImplementation.Click(By.Id("OK"));
			//		ValidateALertMessage("Pairing and initial programming complete.");
			ClickOnAlertMessage("OK");
		}

		public void SelectCouplingIfAvailable(bool isCouplingAvailable, string coupling)
		{
			if(isCouplingAvailable)
			{
				TapOn(coupling);
			}
		}

		public void TapOnMonauralHI(string message)
		{
			Logger.logInfo(message);
			KeywordImplementation.Click(By.Id("Txt_Monaural_HISelected"));
		}
		
		public void ValidateCESign()
		{
			KeywordImplementation.SwipeVerticle(3,0.50,0.20,By.Name("CELogo.png"));
			if(KeywordImplementation.waitForObjectExist(By.Name("CELogo.png"),TimeSpan.FromSeconds(2)))
				Logger.logSuccess("CE sign exists");
			else
				Logger.logFailure("CE sign does not exist");
			Logger.logSnapshot();
		}
		
		public void SelectLanguage(string language)
		{
			//Logger.logInfo("Select the language "+language);
			if(language.Equals("English",StringComparison.InvariantCultureIgnoreCase))
				KeywordImplementation.Click(By.Name("English"));
			else if(language.Equals("French",StringComparison.InvariantCultureIgnoreCase))
				KeywordImplementation.Click(By.Name("Français"));
			else if(language.Equals("Spanish",StringComparison.InvariantCultureIgnoreCase))
				KeywordImplementation.Click(By.Name("Español"));
			else if(language.Equals("Hindi",StringComparison.InvariantCultureIgnoreCase))
				KeywordImplementation.Click(By.Name("Hindi"));
			else if(language.Equals("Chinese",StringComparison.InvariantCultureIgnoreCase))
				KeywordImplementation.Click(By.Name("中国 (Simplified Chinese)"));
//			else if(language.Equals("Chinese",StringComparison.InvariantCultureIgnoreCase))
//				KeywordImplementation.Click(By.Name("繁體中文 (Traditional Chinese)"));
			Logger.logInfo("Select the language "+language,true);
			//Logger.logSnapshot();
		}
		
		public void ContinueWithLeftEar()
		{
			Delay.Seconds(2);
			Logger.logInfo("Continue With Left Ear");
			KeywordImplementation.SwipeVerticle(3,0.5,0.2,By.Name("Continue With Left Ear"));
			KeywordImplementation.Click(By.Name("Continue With Left Ear"));
		}
		
	}
}
