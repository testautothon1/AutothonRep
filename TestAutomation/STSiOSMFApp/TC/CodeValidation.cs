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
    ///The CodeValidation recording.
    /// </summary>
    [TestModule("5bf9a465-c2ec-4b64-91a3-f49b650e6bb8", ModuleType.Recording, 1)]
    public partial class CodeValidation : ITestModule
    {
        /// <summary>
        /// Holds an instance of the STSiOSMFApp.STSiOSMFAppRepository repository.
        /// </summary>
        public static STSiOSMFApp.STSiOSMFAppRepository repo = STSiOSMFApp.STSiOSMFAppRepository.Instance;

        static CodeValidation instance = new CodeValidation();

        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public CodeValidation()
        {
        }

        /// <summary>
        /// Gets a static instance of this recording.
        /// </summary>
        public static CodeValidation Instance
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

            // Step## Open the app & Accept and Continue the startup text
            Report.Log(ReportLevel.Info, "Section", "Step## Open the app & Accept and Continue the startup text", new RecordItemIndex(0));
            
            AcceptAndContinueWelcomeScreen();
            Delay.Milliseconds(0);
            
            // Validation## Country selection screen is presented
            Report.Log(ReportLevel.Info, "Section", "Validation## Country selection screen is presented", new RecordItemIndex(2));
            
            VerifyActionBarTitle("Country");
            Delay.Milliseconds(0);
            
            // Step## Select the country location & click on done
            Report.Log(ReportLevel.Info, "Section", "Step## Select the country location & click on done", new RecordItemIndex(4));
            
            SelectCountry("Rest of World");
            Delay.Milliseconds(0);
            
            TapOnRightButton("Click on 'Done'");
            Delay.Milliseconds(0);
            
            // Validation## Access code screen is displayed
            Report.Log(ReportLevel.Info, "Section", "Validation## Access code screen is displayed", new RecordItemIndex(7));
            
            VerifyActionBarTitle("Access Code");
            Delay.Milliseconds(0);
            
            // Step## Leave the code field blank and press Done
            Report.Log(ReportLevel.Info, "Section", "Step## Leave the code field blank and press Done", new RecordItemIndex(9));
            
            TapOnRightButton("Tap on 'Done'");
            Delay.Milliseconds(0);
            
            // Validation## Verification screen - Code doesn't match message is displayed with two options -Need an access code and Try Again
            Report.Log(ReportLevel.Info, "Section", "Validation## Verification screen - Code doesn't match message is displayed with two options -Need an access code and Try Again", new RecordItemIndex(11));
            
            ValidateContentLabels("Access code not valid.;Need an access code?;Try again");
            Delay.Milliseconds(0);
            
            // Step## Press Try Again
            Report.Log(ReportLevel.Info, "Section", "Step## Press Try Again", new RecordItemIndex(13));
            
            TapOn("Try again");
            Delay.Milliseconds(0);
            
            // Validation## Access code screen is displayed
            Report.Log(ReportLevel.Info, "Section", "Validation## Access code screen is displayed", new RecordItemIndex(15));
            
            VerifyActionBarTitle("Access Code");
            Delay.Milliseconds(0);
            
            // Step## Enter the code partially and press Done
            Report.Log(ReportLevel.Info, "Section", "Step## Enter the code partially and press Done", new RecordItemIndex(17));
            
            EnterPartialCode();
            Delay.Milliseconds(0);
            
            TapOnRightButton("Tap on 'Done'");
            Delay.Milliseconds(0);
            
            // Validation## Verification screen - Code doesn't match message is displayed with two options -Need an access code and Try Again
            Report.Log(ReportLevel.Info, "Section", "Validation## Verification screen - Code doesn't match message is displayed with two options -Need an access code and Try Again", new RecordItemIndex(20));
            
            ValidateContentLabels("Access code not valid.;Need an access code?;Try again");
            Delay.Milliseconds(0);
            
            // Step## Press Try Again
            Report.Log(ReportLevel.Info, "Section", "Step## Press Try Again", new RecordItemIndex(22));
            
            TapOn("Try again");
            Delay.Milliseconds(0);
            
            // Validation## Access code screen is displayed
            Report.Log(ReportLevel.Info, "Section", "Validation## Access code screen is displayed", new RecordItemIndex(24));
            
            VerifyActionBarTitle("Access Code");
            Delay.Milliseconds(0);
            
            // Step## Enter invalid Authentication and press Done
            Report.Log(ReportLevel.Info, "Section", "Step## Enter invalid Authentication and press Done", new RecordItemIndex(26));
            
            EnterPartialCode();
            Delay.Milliseconds(0);
            
            TapOnRightButton("Tap on 'Done'");
            Delay.Milliseconds(0);
            
            // Step## Press Need an access code
            Report.Log(ReportLevel.Info, "Section", "Step## Press Need an access code", new RecordItemIndex(29));
            
            TapOnRightButton("Tap on 'Done'");
            Delay.Milliseconds(0);
            
            TapOn("Need an access code?");
            Delay.Milliseconds(0);
            
            // Validation## Opens the pre-populated email
            Report.Log(ReportLevel.Info, "Section", "Validation## Opens the pre-populated email", new RecordItemIndex(32));
            
            ValidatePrePopulatedEmail();
            Delay.Milliseconds(0);
            
        }

#region Image Feature Data
#endregion
    }
#pragma warning restore 0436
}
