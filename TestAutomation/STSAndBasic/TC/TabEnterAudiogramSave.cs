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
    ///The TabEnterAudiogramSave recording.
    /// </summary>
    [TestModule("24209285-0cf6-4913-a55d-4189ca33c3dc", ModuleType.Recording, 1)]
    public partial class TabEnterAudiogramSave : ITestModule
    {
        /// <summary>
        /// Holds an instance of the STSAndBasic.STSAndBasicRepository repository.
        /// </summary>
        public static STSAndBasic.STSAndBasicRepository repo = STSAndBasic.STSAndBasicRepository.Instance;

        static TabEnterAudiogramSave instance = new TabEnterAudiogramSave();

        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public TabEnterAudiogramSave()
        {
        }

        /// <summary>
        /// Gets a static instance of this recording.
        /// </summary>
        public static TabEnterAudiogramSave Instance
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

            // Step## Pre-Condition -Initial setup and Access code setup is done
            Report.Log(ReportLevel.Info, "Section", "Step## Pre-Condition -Initial setup and Access code setup is done", new RecordItemIndex(0));
            
            // Step## Open the app, go to New section
            Report.Log(ReportLevel.Info, "Section", "Step## Open the app, go to New section", new RecordItemIndex(1));
            
            TapOn("Client List");
            Delay.Milliseconds(0);
            
            // Validation## Customer creation screen is displayed
            Report.Log(ReportLevel.Info, "Section", "Validation## Customer creation screen is displayed", new RecordItemIndex(3));
            
            VerifyActionBarTitle("Client List");
            Delay.Milliseconds(0);
            
            ValidateContents("Last name;First name;Date of birth;Email address;Mobile phone");
            Delay.Milliseconds(0);
            
            // Step## Enter last name, first name and Audiogram available ON - press Continue button
            Report.Log(ReportLevel.Info, "Section", "Step## Enter last name, first name and Audiogram available ON - press Continue button", new RecordItemIndex(6));
            
            CreatePatient("EnterAudiogram", "Save");
            Delay.Milliseconds(0);
            
            SelectCustomerContinue();
            Delay.Milliseconds(0);
            
            // Step## Click on Enter Audiogram
            Report.Log(ReportLevel.Info, "Section", "Step## Click on Enter Audiogram", new RecordItemIndex(9));
            
            TapOn("Enter Audiogram");
            Delay.Milliseconds(0);
            
            // Validation## Audiogram entry screen opens
            Report.Log(ReportLevel.Info, "Section", "Validation## Audiogram entry screen opens", new RecordItemIndex(11));
            
            TabVerifyActionBarDetailTitle("Audiogram");
            Delay.Milliseconds(0);
            
            // Step## Tap Done without any audiogram entries
            Report.Log(ReportLevel.Info, "Section", "Step## Tap Done without any audiogram entries", new RecordItemIndex(13));
            
            TabTapOnRightButton("Tap On 'Done'");
            Delay.Milliseconds(0);
            
            // Validation## Returns to the overview screen with Enter Audiogram highlighted
            Report.Log(ReportLevel.Info, "Section", "Validation## Returns to the overview screen with Enter Audiogram highlighted", new RecordItemIndex(15));
            
            VerifyActionBarTitle("EnterAudiogram, Save");
            Delay.Milliseconds(0);
            
            VerifyTextHighlighted("Enter Audiogram");
            Delay.Milliseconds(0);
            
            // Step## Click on Enter Audiogram
            Report.Log(ReportLevel.Info, "Section", "Step## Click on Enter Audiogram", new RecordItemIndex(18));
            
            TapOn("Enter Audiogram");
            Delay.Milliseconds(0);
            
            // Validation## By default the frequencies for x-axis 500 Hz 1kHz 2kHz 4kHz should be visible without scrolling and dB levels of y-axis is dynamic from 0 dB to 120 dB
            Report.Log(ReportLevel.Info, "Section", "Validation## By default the frequencies for x-axis 500 Hz 1kHz 2kHz 4kHz should be visible without scrolling and dB levels of y-axis is dynamic from 0 dB to 120 dB", new RecordItemIndex(20));
            
            ValidateContentsContains("500 Hz;1 kHz;2 kHz;4 kHz;dB;120", ValueConverter.ArgumentFromString<bool>("isFound", "True"));
            Delay.Milliseconds(0);
            
            // Step## Enter the left audiogram - Tap on the audiogram input box for 500Hz, 1kHz and enter value - say 50dB using the picker
            Report.Log(ReportLevel.Info, "Section", "Step## Enter the left audiogram - Tap on the audiogram input box for 500Hz, 1kHz and enter value - say 50dB using the picker", new RecordItemIndex(22));
            
            DrawAudiogramPointsWithPicker("Left", "500,50;1000,50");
            Delay.Milliseconds(0);
            
            // Validation## Point is plotted at 50dB for 500Hz, 1kHz
            Report.Log(ReportLevel.Info, "Section", "Validation## Point is plotted at 50dB for 500Hz, 1kHz", new RecordItemIndex(24));
            
            ValidateAudiogramPoints("Left", "500,50;1000,50");
            Delay.Milliseconds(0);
            
            // Step## Tap on the dB line to enter an audiogram point for 2kHz, 4kHz - say 50dB
            Report.Log(ReportLevel.Info, "Section", "Step## Tap on the dB line to enter an audiogram point for 2kHz, 4kHz - say 50dB", new RecordItemIndex(26));
            
            DrawAudiogramPoints("Left", "2000,50;4000,50");
            Delay.Milliseconds(0);
            
            // Validation## Numeric value should be dynamically updated in the dB input box .
            Report.Log(ReportLevel.Info, "Section", "Validation## Numeric value should be dynamically updated in the dB input box .", new RecordItemIndex(28));
            
            ValidateAudiogramPoints("Left", "2000,50;4000,50");
            Delay.Milliseconds(0);
            
            // Step## Tap on the audiogram input box for 8kHz and try to set all possible value 0 - 120 dB from the picker
            Report.Log(ReportLevel.Info, "Section", "Step## Tap on the audiogram input box for 8kHz and try to set all possible value 0 - 120 dB from the picker", new RecordItemIndex(30));
            
            // Validation## The values selected from the picker is adapted to the value field.
            Report.Log(ReportLevel.Info, "Section", "Validation## The values selected from the picker is adapted to the value field.", new RecordItemIndex(31));
            
            DrawAudiogramPointsWithPicker("Left", "8000,0");
            Delay.Milliseconds(0);
            
            ValidateAudiogramPoints("Left", "8000,0");
            Delay.Milliseconds(0);
            
            DrawAudiogramPointsWithPicker("Left", "8000,10");
            Delay.Milliseconds(0);
            
            ValidateAudiogramPoints("Left", "8000,10");
            Delay.Milliseconds(0);
            
            DrawAudiogramPointsWithPicker("Left", "8000,20");
            Delay.Milliseconds(0);
            
            ValidateAudiogramPoints("Left", "8000,20");
            Delay.Milliseconds(0);
            
            DrawAudiogramPointsWithPicker("Left", "8000,30");
            Delay.Milliseconds(0);
            
            ValidateAudiogramPoints("Left", "8000,30");
            Delay.Milliseconds(0);
            
            DrawAudiogramPointsWithPicker("Left", "8000,40");
            Delay.Milliseconds(0);
            
            ValidateAudiogramPoints("Left", "8000,40");
            Delay.Milliseconds(0);
            
            DrawAudiogramPointsWithPicker("Left", "8000,50");
            Delay.Milliseconds(0);
            
            ValidateAudiogramPoints("Left", "8000,50");
            Delay.Milliseconds(0);
            
            DrawAudiogramPointsWithPicker("Left", "8000,60");
            Delay.Milliseconds(0);
            
            ValidateAudiogramPoints("Left", "8000,60");
            Delay.Milliseconds(0);
            
            DrawAudiogramPointsWithPicker("Left", "8000,70");
            Delay.Milliseconds(0);
            
            ValidateAudiogramPoints("Left", "8000,70");
            Delay.Milliseconds(0);
            
            DrawAudiogramPointsWithPicker("Left", "8000,80");
            Delay.Milliseconds(0);
            
            ValidateAudiogramPoints("Left", "8000,80");
            Delay.Milliseconds(0);
            
            DrawAudiogramPointsWithPicker("Left", "8000,90");
            Delay.Milliseconds(0);
            
            ValidateAudiogramPoints("Left", "8000,90");
            Delay.Milliseconds(0);
            
            DrawAudiogramPointsWithPicker("Left", "8000,100");
            Delay.Milliseconds(0);
            
            ValidateAudiogramPoints("Left", "8000,100");
            Delay.Milliseconds(0);
            
            DrawAudiogramPointsWithPicker("Left", "8000,120");
            Delay.Milliseconds(0);
            
            ValidateAudiogramPoints("Left", "8000,120");
            Delay.Milliseconds(0);
            
            // Step## Tap Done
            Report.Log(ReportLevel.Info, "Section", "Step## Tap Done", new RecordItemIndex(56));
            
            TabTapOnRightButton("Tap On 'Done'");
            Delay.Milliseconds(0);
            
            ClickOnAlertMessage("Yes");
            Delay.Milliseconds(0);
            
            // Step## Enter the right audiogram - Tap on the audiogram input box for 500Hz, 1kHz and enter value - say 50dB using the picker
            Report.Log(ReportLevel.Info, "Section", "Step## Enter the right audiogram - Tap on the audiogram input box for 500Hz, 1kHz and enter value - say 50dB using the picker", new RecordItemIndex(59));
            
            DrawAudiogramPointsWithPicker("Right", "500,50;1000,50");
            Delay.Milliseconds(0);
            
            // Validation## Point is plotted at 50dB for 500Hz, 1kHz
            Report.Log(ReportLevel.Info, "Section", "Validation## Point is plotted at 50dB for 500Hz, 1kHz", new RecordItemIndex(61));
            
            ValidateAudiogramPoints("Right", "500,50;1000,50");
            Delay.Milliseconds(0);
            
            // Step## Tap on the dB line to enter an audiogram point for 2kHz, 4kHz - say 50dB
            Report.Log(ReportLevel.Info, "Section", "Step## Tap on the dB line to enter an audiogram point for 2kHz, 4kHz - say 50dB", new RecordItemIndex(63));
            
            DrawAudiogramPoints("Right", "2000,50;4000,50");
            Delay.Milliseconds(0);
            
            // Validation## Numeric value should be dynamically updated in the dB input box .
            Report.Log(ReportLevel.Info, "Section", "Validation## Numeric value should be dynamically updated in the dB input box .", new RecordItemIndex(65));
            
            ValidateAudiogramPoints("Right", "500,50;1000,50");
            Delay.Milliseconds(0);
            
            // Step## Tap on the audiogram input box for 8kHz and try to set all possible value 0 - 120 dB from the picker
            Report.Log(ReportLevel.Info, "Section", "Step## Tap on the audiogram input box for 8kHz and try to set all possible value 0 - 120 dB from the picker", new RecordItemIndex(67));
            
            // Validation## The values selected from the picker is adapted to the value field.
            Report.Log(ReportLevel.Info, "Section", "Validation## The values selected from the picker is adapted to the value field.", new RecordItemIndex(68));
            
            DrawAudiogramPointsWithPicker("Right", "8000,0");
            Delay.Milliseconds(0);
            
            ValidateAudiogramPoints("Right", "8000,0");
            Delay.Milliseconds(0);
            
            DrawAudiogramPointsWithPicker("Right", "8000,10");
            Delay.Milliseconds(0);
            
            ValidateAudiogramPoints("Right", "8000,10");
            Delay.Milliseconds(0);
            
            DrawAudiogramPointsWithPicker("Right", "8000,20");
            Delay.Milliseconds(0);
            
            ValidateAudiogramPoints("Right", "8000,20");
            Delay.Milliseconds(0);
            
            DrawAudiogramPointsWithPicker("Right", "8000,30");
            Delay.Milliseconds(0);
            
            ValidateAudiogramPoints("Right", "8000,30");
            Delay.Milliseconds(0);
            
            DrawAudiogramPointsWithPicker("Right", "8000,40");
            Delay.Milliseconds(0);
            
            ValidateAudiogramPoints("Right", "8000,40");
            Delay.Milliseconds(0);
            
            DrawAudiogramPointsWithPicker("Right", "8000,50");
            Delay.Milliseconds(0);
            
            ValidateAudiogramPoints("Right", "8000,50");
            Delay.Milliseconds(0);
            
            DrawAudiogramPointsWithPicker("Right", "8000,60");
            Delay.Milliseconds(0);
            
            ValidateAudiogramPoints("Right", "8000,60");
            Delay.Milliseconds(0);
            
            DrawAudiogramPointsWithPicker("Right", "8000,70");
            Delay.Milliseconds(0);
            
            ValidateAudiogramPoints("Right", "8000,70");
            Delay.Milliseconds(0);
            
            DrawAudiogramPointsWithPicker("Right", "8000,80");
            Delay.Milliseconds(0);
            
            ValidateAudiogramPoints("Right", "8000,80");
            Delay.Milliseconds(0);
            
            DrawAudiogramPointsWithPicker("Right", "8000,90");
            Delay.Milliseconds(0);
            
            ValidateAudiogramPoints("Right", "8000,90");
            Delay.Milliseconds(0);
            
            DrawAudiogramPointsWithPicker("Right", "8000,100");
            Delay.Milliseconds(0);
            
            ValidateAudiogramPoints("Right", "8000,100");
            Delay.Milliseconds(0);
            
            DrawAudiogramPointsWithPicker("Right", "8000,120");
            Delay.Milliseconds(0);
            
            ValidateAudiogramPoints("Right", "8000,120");
            Delay.Milliseconds(0);
            
            // Step## Tap Done
            Report.Log(ReportLevel.Info, "Section", "Step## Tap Done", new RecordItemIndex(93));
            
            TabTapOnRightButton("Tap On 'Done'");
            Delay.Milliseconds(0);
            
            // Validation## Audiogram is saved and shown with same values on the overview screen.
            Report.Log(ReportLevel.Info, "Section", "Validation## Audiogram is saved and shown with same values on the overview screen.", new RecordItemIndex(95));
            
            VerifyActionBarTitle("EnterAudiogram, Save");
            Delay.Milliseconds(0);
            
            ValidateAudiogramPointsOnCustomerOverviewScreen("Right", "500,50;1000,50;2000,50;4000,50;8000,120", ValueConverter.ArgumentFromString<bool>("isExist", "True"));
            Delay.Milliseconds(0);
            
            ValidateAudiogramPointsOnCustomerOverviewScreen("Left", "500,50;1000,50;2000,50;4000,50;8000,120", ValueConverter.ArgumentFromString<bool>("isExist", "True"));
            Delay.Milliseconds(0);
            
        }

#region Image Feature Data
#endregion
    }
#pragma warning restore 0436
}
