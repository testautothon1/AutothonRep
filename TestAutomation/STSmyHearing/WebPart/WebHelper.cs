/*
 * Created by Ranorex
 * User: Z002XFCE
 * Date: 8/3/2017
 * Time: 10:58 AM
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
using Ranorex.Core.Repository;
using Ranorex.Core.Testing;
using System.Linq;

namespace STSmyHearing.WebPart
{
	/// <summary>
	/// Description of Class1.
	/// </summary>
	public class WebHelper
	{
		public static STSmyHearingRepository repos = STSmyHearingRepository.Instance;
		
		public WebHelper()
		{
		}
		
		public void Login(string userName, string password)
		{
			Report.Screenshot();

			repos.STAGINGSigniaTeleCarePortal.NavigationBarNavbar.DropDown.Click();
			repos.STAGINGSigniaTeleCarePortal.NavigationBarNavbar.FlagFlagEnUS.Click();
			repos.STAGINGSigniaTeleCarePortal.ContainerLogin.Username.PressKeys(userName);
			repos.STAGINGSigniaTeleCarePortal.ContainerLogin.Password.PressKeys(password);
			repos.STAGINGSigniaTeleCarePortal.ContainerLogin.Login.Click();
			if(repos.DoYouWantGoogleChromeToSaveYourP.NeverInfo.Exists(new Duration(2000)))
			{
				repos.DoYouWantGoogleChromeToSaveYourP.Never.Click();
			}
			Report.Screenshot();
		}
		
		public void SelectHCP(string hcp)
		{
			repos.STAGINGSigniaTeleCarePortal.CustomerOverviewPage.CheckboxInfo.WaitForExists(Duration.FromMilliseconds(20000));
			repos.STAGINGSigniaTeleCarePortal.CustomerOverviewPage.Checkbox.Click();
			//if(!repos.STAGINGSigniaTeleCarePortal.HCPInfo.Exists(Duration.FromMilliseconds(2000)))
			repos.STAGINGSigniaTeleCarePortal.CustomerOverviewPage.HCP.Click();
			Report.Screenshot();

		}
		
		public void ModuleLessonSelection(string moduleName, int lessonNumber)
		{
			RepoItemInfo rInfo= null;
			string selectedComboText=string.Empty;
			//var comboText;
			
			if(moduleName.Equals("Module1",StringComparison.CurrentCultureIgnoreCase))
				rInfo=repos.STAGINGSigniaTeleCarePortal.ManageLessons.Module1LessonsInfo;
			else if(moduleName.Equals("Module2",StringComparison.CurrentCultureIgnoreCase))
				rInfo=repos.STAGINGSigniaTeleCarePortal.ManageLessons.Module2LessonsInfo;
			else if(moduleName.Equals("Module3",StringComparison.CurrentCultureIgnoreCase))
				rInfo=repos.STAGINGSigniaTeleCarePortal.ManageLessons.Module3LessonsInfo;
			else if(moduleName.Equals("Module4",StringComparison.CurrentCultureIgnoreCase))
				rInfo=repos.STAGINGSigniaTeleCarePortal.ManageLessons.Module4LessonsInfo;
			
			IList<DivTag> moduleLessonsList= rInfo.CreateAdapters<Ranorex.DivTag>();
//			var comboText1=moduleLessonsList[lessonNumber-1].FindDescendants<OptionTag>().FirstOrDefault(x=>x.Selected);
//			
//			selectedComboText=comboText.InnerText;
			
			//comboText.
			var comboText=moduleLessonsList[lessonNumber-1].FindDescendants<OptionTag>().Select(x=>new
			                                                                                    {
			                                                                                    	selected=x.Selected,
			                                                                                    	innerText=x.InnerText
			                                                                                    }
			                                                                                   ).ToList();
			selectedComboText=comboText.FirstOrDefault(x=>x.selected).innerText;
			
			
			////			var test = dropDwn.FindDescendants<SpanTag>().Select(y=> new
			////			                                                     {
			////			                                                     	innertext = y.InnerText,
			////			                                                     	elem = y
			////			                                                     }
			////			                                                    );
			
			
//			var optionTags=moduleLessonsList[lessonNumber-1].FindDescendants<OptionTag>();
//			
//			foreach(OptionTag ot in optionTags)
//			{
//				if(ot.Selected)
//				{
//					selectedComboText = ot.InnerText;
//				}
//			}
			Ranorex.Report.Info(selectedComboText);
		}
		
		public void SelectModuleLesson(string moduleName, int lessonNumber, string lessonName)
		{
			RepoItemInfo rInfo= null;
			string selectedComboText=string.Empty;
			
			if(moduleName.Equals("Module1",StringComparison.CurrentCultureIgnoreCase))
				rInfo=repos.STAGINGSigniaTeleCarePortal.ManageLessons.Module1LessonsInfo;
			else if(moduleName.Equals("Module2",StringComparison.CurrentCultureIgnoreCase))
				rInfo=repos.STAGINGSigniaTeleCarePortal.ManageLessons.Module2LessonsInfo;
			else if(moduleName.Equals("Module3",StringComparison.CurrentCultureIgnoreCase))
				rInfo=repos.STAGINGSigniaTeleCarePortal.ManageLessons.Module3LessonsInfo;
			else if(moduleName.Equals("Module4",StringComparison.CurrentCultureIgnoreCase))
				rInfo=repos.STAGINGSigniaTeleCarePortal.ManageLessons.Module4LessonsInfo;
			
			IList<DivTag> moduleLessonsList= rInfo.CreateAdapters<Ranorex.DivTag>();
			
			moduleLessonsList.ElementAt(lessonNumber-1).FindDescendants<OptionTag>().
				FirstOrDefault(x=>x.InnerText.Trim().
				               Equals(lessonName,StringComparison.CurrentCultureIgnoreCase)).Select();
			
//			var optionTags=moduleLessonsList[lessonNumber-1].FindDescendants<OptionTag>();
//
//			foreach(OptionTag ot in optionTags)
//			{
//				if(ot.InnerText.Equals(lessonName))
//				{
//					ot.Select();
//				}
//			}
			//Ranorex.Report.Info(selectedComboText);
		}
		
		public void ClickOnSettingsOption(string option)
		{
			repos.STAGINGSigniaTeleCarePortal.ContainerProfileManage.PatientNavigation.Settings.Click();
			var dropDwn = repos.STAGINGSigniaTeleCarePortal.ContainerProfileManage.PatientNavigation.DropdownMenuUp;
			var settingOption = dropDwn.FindDescendants<SpanTag>().FirstOrDefault(x=>x.InnerText.Trim().Equals(option,StringComparison.InvariantCultureIgnoreCase));
			settingOption.Click();
			Report.Screenshot();
		}
		
//		public void ClickOnConnectToMyHearing()
//		{
//			repos.STAGINGSigniaTeleCarePortal.ContainerProfileManage.PatientNavigation.Settings.Click();
//			Report.Screenshot();
//			var connectToMyHearing = repos.STAGINGSigniaTeleCarePortal.ContainerProfileManage.PatientNavigation.ConnectToMyHearing;
//			repos.STAGINGSigniaTeleCarePortal.ContainerProfileManage.PatientNavigation.ConnectToMyHearing2.Click();
//		}
//
		public void ReadConnectionCode()
		{
			Delay.Seconds(2);
			string connectionCode=repos.STAGINGSigniaTeleCarePortal.CustomerSetupPage.ConnectionCode.InnerText;
			Report.Success(connectionCode);
			AppVariables.entryLeftCode=connectionCode.Split('-')[0].Trim(' ');
			AppVariables.entryRightCode=connectionCode.Split('-')[1].Trim(' ');
			Report.Screenshot();

		}
		
		public void Logout()
		{
			repos.STAGINGSigniaTeleCarePortal.NavigationBarNavbar.DropdownUsername.Click();
			repos.STAGINGSigniaTeleCarePortal.NavigationBarNavbar.Logout.Click();
			Report.Screenshot();
		}
		
		public void CloseBrowser()
		{
			IList<Ranorex.WebDocument> AllDoms = Host.Local.FindDescendants<Ranorex.WebDocument>();
			if (AllDoms.Count >=1)
			{
				foreach (WebDocument myDom in AllDoms)
				{
					if(myDom.Visible)
						myDom.Close();
				}
			}
		}
		
//		public void CreatePatient()
//		{
//			repos.STAGINGSigniaTeleCarePortal.CustomerOverviewPage.AddPatient.Click();
//			repos.STAGINGSigniaTeleCarePortal
//		}
		
		
		public void Select(string pOptions)
		{

			string[] stringarry = {"1","2","3","4"};
			var dropDwn = repos.STAGINGSigniaTeleCarePortal.ContainerProfileManage.PatientNavigation.DropdownMenuUp;
			var selected = dropDwn.FindDescendants<SpanTag>().FirstOrDefault((x=>x.InnerText.Trim().Equals(pOptions,StringComparison.InvariantCultureIgnoreCase)));//.Click();
			var test = dropDwn.FindDescendants<SpanTag>().Select(y=> new
			                                                     {
			                                                     	innertext = y.InnerText,
			                                                     	elem = y
			                                                     }
			                                                    );
			
			var intarry = stringarry.Select(int.Parse);

			//repos.STAGINGSigniaTeleCarePortal.DropdownMenuUp
		}
		
		
		
		
	}
}
