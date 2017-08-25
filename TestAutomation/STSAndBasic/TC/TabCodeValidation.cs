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

namespace STSAndBasic.TC
{
#pragma warning disable 0436 //(CS0436) The type 'type' in 'assembly' conflicts with the imported type 'type2' in 'assembly'. Using the type defined in 'assembly'.
    /// <summary>
    ///The TabCodeValidation recording.
    /// </summary>
    [TestModule("158c4b67-f0ca-4812-be69-e9ddbee84364", ModuleType.Recording, 1)]
    public partial class TabCodeValidation : ITestModule
    {
        /// <summary>
        /// Holds an instance of the STSAndBasic.STSAndBasicRepository repository.
        /// </summary>
        public static STSAndBasic.STSAndBasicRepository repo = STSAndBasic.STSAndBasicRepository.Instance;

        static TabCodeValidation instance = new TabCodeValidation();

        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public TabCodeValidation()
        {
        }

        /// <summary>
        /// Gets a static instance of this recording.
        /// </summary>
        public static TabCodeValidation Instance
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

            // Step## Pre-Condition: Fresh Installation of the app
            Report.Log(ReportLevel.Info, "Section", "Step## Pre-Condition: Fresh Installation of the app", new RecordItemIndex(0));
            
            StartApp();
            Delay.Milliseconds(0);
            
            // Step## Open the app & Accept and Continue the startup text
            Report.Log(ReportLevel.Info, "Section", "Step## Open the app & Accept and Continue the startup text", new RecordItemIndex(2));
            
            AcceptAndContinueWelcomeScreen();
            Delay.Milliseconds(0);
            
            // Validation## Country selection screen is presented
            Report.Log(ReportLevel.Info, "Section", "Validation## Country selection screen is presented", new RecordItemIndex(4));
            
            VerifyActionBarTitle("Country");
            Delay.Milliseconds(0);
            
            // Step## Select the country location and click on done
            Report.Log(ReportLevel.Info, "Section", "Step## Select the country location and click on done", new RecordItemIndex(6));
            
            SelectCountry("Rest of World");
            Delay.Milliseconds(0);
            
            TapOnRightButton("Click on 'Done'");
            Delay.Milliseconds(0);
            
            // Validation## Access code screen is displayed
            Report.Log(ReportLevel.Info, "Section", "Validation## Access code screen is displayed", new RecordItemIndex(9));
            
            VerifyActionBarTitle("Access Code");
            Delay.Milliseconds(0);
            
            // Step## Leave the code field blank and press Done
            Report.Log(ReportLevel.Info, "Section", "Step## Leave the code field blank and press Done", new RecordItemIndex(11));
            
            DoneAuthenticationScreen();
            Delay.Milliseconds(0);
            
            // Validation## Verification screen - Code doesn't match message is displayed with two options -Need an access code and Try Again
            Report.Log(ReportLevel.Info, "Section", "Validation## Verification screen - Code doesn't match message is displayed with two options -Need an access code and Try Again", new RecordItemIndex(13));
            
            ValidateContentsContains("Access code not valid.;Need an access code?", ValueConverter.ArgumentFromString<bool>("isFound", "True"));
            Delay.Milliseconds(0);
            
            ValidateTryAgainExists();
            Delay.Milliseconds(0);
            
            // Step## Press Try Again
            Report.Log(ReportLevel.Info, "Section", "Step## Press Try Again", new RecordItemIndex(16));
            
            TapTryAgain();
            Delay.Milliseconds(0);
            
            // Validation## Access code screen is displayed
            Report.Log(ReportLevel.Info, "Section", "Validation## Access code screen is displayed", new RecordItemIndex(18));
            
            VerifyActionBarTitle("Access Code");
            Delay.Milliseconds(0);
            
            // Step## Enter the code partially and press Done
            Report.Log(ReportLevel.Info, "Section", "Step## Enter the code partially and press Done", new RecordItemIndex(20));
            
            EnterPartialCode();
            Delay.Milliseconds(0);
            
            DoneAuthenticationScreen();
            Delay.Milliseconds(0);
            
            // Validation## Verification screen - Code doesn't match message is displayed with two options -Need an access code and Try Again
            Report.Log(ReportLevel.Info, "Section", "Validation## Verification screen - Code doesn't match message is displayed with two options -Need an access code and Try Again", new RecordItemIndex(23));
            
            ValidateContentsContains("Access code not valid.;Need an access code?", ValueConverter.ArgumentFromString<bool>("isFound", "True"));
            Delay.Milliseconds(0);
            
            ValidateTryAgainExists();
            Delay.Milliseconds(0);
            
            // Step## Press Try Again
            Report.Log(ReportLevel.Info, "Section", "Step## Press Try Again", new RecordItemIndex(26));
            
            TapTryAgain();
            Delay.Milliseconds(0);
            
            // Validation## Access code screen is displayed
            Report.Log(ReportLevel.Info, "Section", "Validation## Access code screen is displayed", new RecordItemIndex(28));
            
            VerifyActionBarTitle("Access Code");
            Delay.Milliseconds(0);
            
            // Step## Enter invalid authentication and press Done
            Report.Log(ReportLevel.Info, "Section", "Step## Enter invalid authentication and press Done", new RecordItemIndex(30));
            
            EnterPartialCode();
            Delay.Milliseconds(0);
            
            DoneAuthenticationScreen();
            Delay.Milliseconds(0);
            
            // Validation## Verification screen - Code doesn't match message is displayed with two options -Need an access code and Try Again
            Report.Log(ReportLevel.Info, "Section", "Validation## Verification screen - Code doesn't match message is displayed with two options -Need an access code and Try Again", new RecordItemIndex(33));
            
            ValidateContentsContains("Access code not valid.;Need an access code?", ValueConverter.ArgumentFromString<bool>("isFound", "True"));
            Delay.Milliseconds(0);
            
            ValidateTryAgainExists();
            Delay.Milliseconds(0);
            
            // Step## Press Try Again
            Report.Log(ReportLevel.Info, "Section", "Step## Press Try Again", new RecordItemIndex(36));
            
            TapTryAgain();
            Delay.Milliseconds(0);
            
            BackAction();
            Delay.Milliseconds(0);
            
            // Step## Press Need an access code
            Report.Log(ReportLevel.Info, "Section", "Step## Press Need an access code", new RecordItemIndex(39));
            
            TapNeedAnAccessCode();
            Delay.Milliseconds(0);
            
            // Validation## Pre-populated email
            Report.Log(ReportLevel.Info, "Section", "Validation## Pre-populated email", new RecordItemIndex(41));
            
            VerifyActionBarTitle("Contact us");
            Delay.Milliseconds(0);
            
            ValidateContentsContains("To;Subject", ValueConverter.ArgumentFromString<bool>("isFound", "True"));
            Delay.Milliseconds(0);
            
            ValidatePrePopulatedEmail();
            Delay.Milliseconds(0);
            
        }

#region Image Feature Data
#endregion
    }
#pragma warning restore 0436
}