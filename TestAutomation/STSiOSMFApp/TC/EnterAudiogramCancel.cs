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
    ///The EnterAudiogramCancel recording.
    /// </summary>
    [TestModule("b1d5b02a-0a88-43ac-89e1-c9a7939b5cab", ModuleType.Recording, 1)]
    public partial class EnterAudiogramCancel : ITestModule
    {
        /// <summary>
        /// Holds an instance of the STSiOSMFApp.STSiOSMFAppRepository repository.
        /// </summary>
        public static STSiOSMFApp.STSiOSMFAppRepository repo = STSiOSMFApp.STSiOSMFAppRepository.Instance;

        static EnterAudiogramCancel instance = new EnterAudiogramCancel();

        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public EnterAudiogramCancel()
        {
        }

        /// <summary>
        /// Gets a static instance of this recording.
        /// </summary>
        public static EnterAudiogramCancel Instance
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

            // Step## Existing customer configured with audiogram
            Report.Log(ReportLevel.Info, "Section", "Step## Existing customer configured with audiogram", new RecordItemIndex(0));
            
            CreatePatient("Cancel", "Audiogram");
            Delay.Milliseconds(0);
            
            SelectCustomerContinue();
            Delay.Milliseconds(0);
            
            ValidateContents("Enter Audiogram;Select Hearing Aid;Select Sound Profile;Fit Coupling;Assemble Hearing Aid;Send Data to Hearing Aid;Check Sound Comfort");
            Delay.Milliseconds(0);
            
            CloseApp();
            Delay.Milliseconds(0);
            
            StartApp();
            Delay.Milliseconds(0);
            
            // Step## Open the app and goto List
            Report.Log(ReportLevel.Info, "Section", "Step## Open the app and goto List", new RecordItemIndex(6));
            
            // Step## Select the existing customer (with audiogram)
            Report.Log(ReportLevel.Info, "Section", "Step## Select the existing customer (with audiogram)", new RecordItemIndex(7));
            
            SearchCustomer("Cancel");
            Delay.Milliseconds(0);
            
            SelectCustomer("Cancel", "Audiogram");
            Delay.Milliseconds(0);
            
            // Step## Select Enter audiogram
            Report.Log(ReportLevel.Info, "Section", "Step## Select Enter audiogram", new RecordItemIndex(10));
            
            TapOn("Enter Audiogram");
            Delay.Milliseconds(0);
            
            // Step## Enter audiogram points (left and right ear ) for frequencies 500Hz, 1kHz, 2kHz, 4kHz
            Report.Log(ReportLevel.Info, "Section", "Step## Enter audiogram points (left and right ear ) for frequencies 500Hz, 1kHz, 2kHz, 4kHz", new RecordItemIndex(12));
            
            DrawAudiogramPoints("Right", "500,50;1000,50;2000,50;4000,50");
            Delay.Milliseconds(0);
            
            DrawAudiogramPoints("Left", "500,50;1000,50;2000,50;4000,50");
            Delay.Milliseconds(0);
            
            // Step## Press the Cancel
            Report.Log(ReportLevel.Info, "Section", "Step## Press the Cancel", new RecordItemIndex(15));
            
            TapOnLeftButton("Tap on 'Cancel'");
            Delay.Milliseconds(0);
            
            // Validation## Warning message "Unsaved audiogram data will be lost. Are you sure of your selection?" is displayed with two Options: OK and Cancel
            Report.Log(ReportLevel.Info, "Section", "Validation## Warning message \"Unsaved audiogram data will be lost. Are you sure of your selection?\" is displayed with two Options: OK and Cancel", new RecordItemIndex(17));
            
            ValidateALertMessage("Unsaved audiogram data will be lost. Are you sure of your selection?");
            Delay.Milliseconds(0);
            
            ValidateContentNames("OK;Cancel");
            Delay.Milliseconds(0);
            
            // Step## Tap Cancel
            Report.Log(ReportLevel.Info, "Section", "Step## Tap Cancel", new RecordItemIndex(20));
            
            ClickOnAlertMessage("Cancel");
            Delay.Milliseconds(0);
            
            // Validation## Remains in the audiogram input screen
            Report.Log(ReportLevel.Info, "Section", "Validation## Remains in the audiogram input screen", new RecordItemIndex(22));
            
            ValidateAudiogramPoints("Right", "500,50;1000,50;2000,50;4000,50");
            Delay.Milliseconds(0);
            
            // Step## Press the Cancel
            Report.Log(ReportLevel.Info, "Section", "Step## Press the Cancel", new RecordItemIndex(24));
            
            TapOnLeftButton("Tap on 'Cancel'");
            Delay.Milliseconds(0);
            
            // Validation## Warning message "Unsaved audiogram data will be lost. Are you sure of your selection?" is displayed with two Options: OK and Cancel
            Report.Log(ReportLevel.Info, "Section", "Validation## Warning message \"Unsaved audiogram data will be lost. Are you sure of your selection?\" is displayed with two Options: OK and Cancel", new RecordItemIndex(26));
            
            ValidateALertMessage("Unsaved audiogram data will be lost. Are you sure of your selection?");
            Delay.Milliseconds(0);
            
            // Step## Tap on OK
            Report.Log(ReportLevel.Info, "Section", "Step## Tap on OK", new RecordItemIndex(28));
            
            ClickOnAlertMessage("OK");
            Delay.Milliseconds(0);
            
            // Validation## Goes back to overview screen, without saving audiogram data
            Report.Log(ReportLevel.Info, "Section", "Validation## Goes back to overview screen, without saving audiogram data", new RecordItemIndex(30));
            
            ValidateContents("Enter Audiogram;Select Hearing Aid;Select Sound Profile;Fit Coupling;Assemble Hearing Aid;Send Data to Hearing Aid;Check Sound Comfort");
            Delay.Milliseconds(0);
            
            TapOn("Enter Audiogram");
            Delay.Milliseconds(0);
            
            ValidateAudiogramPoints("Right", "500,;1000,;2000,;4000,");
            Delay.Milliseconds(0);
            
            ValidateAudiogramPoints("Left", "500,;1000,;2000,;4000,");
            Delay.Milliseconds(0);
            
        }

#region Image Feature Data
#endregion
    }
#pragma warning restore 0436
}
