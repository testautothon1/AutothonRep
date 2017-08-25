﻿///////////////////////////////////////////////////////////////////////////////
//
// This file was automatically generated by RANOREX.
// DO NOT MODIFY THIS FILE! It is regenerated by the designer.
// All your modifications will be lost!
// http://www.ranorex.com
//
///////////////////////////////////////////////////////////////////////////////

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
using Ranorex.Core.Repository;

namespace STSiOSMFApp.TC
{
#pragma warning disable 0436 //(CS0436) The type 'type' in 'assembly' conflicts with the imported type 'type2' in 'assembly'. Using the type defined in 'assembly'.
    /// <summary>
    ///The TabFirstTimeLaunch recording.
    /// </summary>
    [TestModule("5e1314a6-b795-470f-8f01-b6524764679a", ModuleType.Recording, 1)]
    public partial class TabFirstTimeLaunch : ITestModule
    {
        /// <summary>
        /// Holds an instance of the STSiOSMFApp.STSiOSMFAppRepository repository.
        /// </summary>
        public static STSiOSMFApp.STSiOSMFAppRepository repo = STSiOSMFApp.STSiOSMFAppRepository.Instance;

        static TabFirstTimeLaunch instance = new TabFirstTimeLaunch();

        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public TabFirstTimeLaunch()
        {
        }

        /// <summary>
        /// Gets a static instance of this recording.
        /// </summary>
        public static TabFirstTimeLaunch Instance
        {
            get { return instance; }
        }

#region Variables

#endregion

        /// <summary>
        /// Starts the replay of the static recording <see cref="Instance"/>.
        /// </summary>
        [System.CodeDom.Compiler.GeneratedCode("Ranorex", "6.2")]
        public static void Start()
        {
            TestModuleRunner.Run(Instance);
        }

        /// <summary>
        /// Performs the playback of actions in this recording.
        /// </summary>
        /// <remarks>You should not call this method directly, instead pass the module
        /// instance to the <see cref="TestModuleRunner.Run(ITestModule)"/> method
        /// that will in turn invoke this method.</remarks>
        [System.CodeDom.Compiler.GeneratedCode("Ranorex", "6.2")]
        void ITestModule.Run()
        {
            Mouse.DefaultMoveTime = 300;
            Keyboard.DefaultKeyPressTime = 100;
            Delay.SpeedFactor = 1.00;

            Init();

            // ## Validation Splash screen is displayed after which Welcome screen is opened with the option to access more info and Accept and Continue.
            Report.Log(ReportLevel.Info, "Section", "## Validation Splash screen is displayed after which Welcome screen is opened with the option to access more info and Accept and Continue.", new RecordItemIndex(0));
            
            ScrollUp();
            Delay.Milliseconds(0);
            
            ValidateContentNames("More information;Accept & Continue");
            Delay.Milliseconds(0);
            
            // ##Step Press More Information
            Report.Log(ReportLevel.Info, "Section", "##Step Press More Information", new RecordItemIndex(3));
            
            TapOn("More information");
            Delay.Milliseconds(0);
            
            // ##Validation Important info regarding the app is displayed.
            Report.Log(ReportLevel.Info, "Section", "##Validation Important info regarding the app is displayed.", new RecordItemIndex(5));
            
            TabVerifyNavBarTitle("Important Info");
            Delay.Milliseconds(0);
            
            // ##Step Click/Swipe to navigate back
            Report.Log(ReportLevel.Info, "Section", "##Step Click/Swipe to navigate back", new RecordItemIndex(7));
            
            TapOnLeftButton("Tap on back icon");
            Delay.Milliseconds(0);
            
            // ##Validation Welcome screen is displayed
            Report.Log(ReportLevel.Info, "Section", "##Validation Welcome screen is displayed", new RecordItemIndex(9));
            
            ValidateContentNames("More information;Accept & Continue");
            Delay.Milliseconds(0);
            
            // ##Step Tap on Accept and Continue
            Report.Log(ReportLevel.Info, "Section", "##Step Tap on Accept and Continue", new RecordItemIndex(11));
            
            AcceptAndContinueWelcomeScreen();
            Delay.Milliseconds(0);
            
            // ##Validation Country selection screen is displayed.
            Report.Log(ReportLevel.Info, "Section", "##Validation Country selection screen is displayed.", new RecordItemIndex(13));
            
            TabVerifyNavBarTitle("Country");
            Delay.Milliseconds(0);
            
            // Step## Select the country and click on done button
            Report.Log(ReportLevel.Info, "Section", "Step## Select the country and click on done button", new RecordItemIndex(15));
            
            TapOnRightButton("Click on 'Done'");
            Delay.Milliseconds(0);
            
            // Validation## Access code screen is displayed. Initial set-up is done.
            Report.Log(ReportLevel.Info, "Section", "Validation## Access code screen is displayed. Initial set-up is done.", new RecordItemIndex(17));
            
            TabVerifyNavBarTitle("Access Code");
            Delay.Milliseconds(0);
            
        }

#region Image Feature Data
#endregion
    }
#pragma warning restore 0436
}
