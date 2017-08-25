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
    ///The Imprint recording.
    /// </summary>
    [TestModule("b1c6c266-5482-4b37-ae85-b7ae451dbe98", ModuleType.Recording, 1)]
    public partial class Imprint : ITestModule
    {
        /// <summary>
        /// Holds an instance of the STSAndBasic.STSAndBasicRepository repository.
        /// </summary>
        public static STSAndBasic.STSAndBasicRepository repo = STSAndBasic.STSAndBasicRepository.Instance;

        static Imprint instance = new Imprint();

        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public Imprint()
        {
        }

        /// <summary>
        /// Gets a static instance of this recording.
        /// </summary>
        public static Imprint Instance
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

            // Step## Open the app, tap on the info icon
            Report.Log(ReportLevel.Info, "Section", "Step## Open the app, tap on the info icon", new RecordItemIndex(0));
            
            ClickInfoButton();
            Delay.Milliseconds(0);
            
            VerifyActionBarTitle("Info");
            Delay.Milliseconds(0);
            
            // Step## Look for items under Imprint
            Report.Log(ReportLevel.Info, "Section", "Step## Look for items under Imprint", new RecordItemIndex(3));
            
            // Validation## There are three items under the Imprint menu
            Report.Log(ReportLevel.Info, "Section", "Validation## There are three items under the Imprint menu", new RecordItemIndex(4));
            
            ValidateContents("Legal Notice;Privacy Policy;Terms & Conditions");
            Delay.Milliseconds(0);
            
            // Step## Press Legal Notice.
            Report.Log(ReportLevel.Info, "Section", "Step## Press Legal Notice.", new RecordItemIndex(6));
            
            SelectPageFromInfo("Legal Notice");
            Delay.Milliseconds(0);
            
            // Validation## Legal Notice is opened including the following details -Name and Address of manufacturer,• product name,• version number,• year of manufacture,• CE sign if available,• country of origin.Details are brand specific - Signia, Connexx
            Report.Log(ReportLevel.Info, "Section", "Validation## Legal Notice is opened including the following details -Name and Address of manufacturer,• product name,• version number,• year of manufacture,• CE sign if available,• country of origin.Details are brand specific - Signia, Connexx", new RecordItemIndex(8));
            
            ValidateContentsContains("Legal Notice;Copyright;App;version;Sivantos Pte. Ltd. 2017. All Rights Reserved;Corporate Information;Managing Directors;Official Address;Legal Manufacturer;Germany", ValueConverter.ArgumentFromString<bool>("isFound", "True"));
            Delay.Milliseconds(0);
            
            ValidateCESign();
            Delay.Milliseconds(0);
            
            // Step## Press Back
            Report.Log(ReportLevel.Info, "Section", "Step## Press Back", new RecordItemIndex(11));
            
            BackAction();
            Delay.Milliseconds(0);
            
            // Validation## Info screen is displayed.
            Report.Log(ReportLevel.Info, "Section", "Validation## Info screen is displayed.", new RecordItemIndex(13));
            
            VerifyActionBarTitle("Info");
            Delay.Milliseconds(0);
            
            // Step## Press Privacy Policy
            Report.Log(ReportLevel.Info, "Section", "Step## Press Privacy Policy", new RecordItemIndex(15));
            
            SelectPageFromInfo("Privacy Policy");
            Delay.Milliseconds(0);
            
            // Validation## Detailed information present
            Report.Log(ReportLevel.Info, "Section", "Validation## Detailed information present", new RecordItemIndex(17));
            
            ValidateContents("Privacy Policy;Sivantos Commitment to Data Privacy Protection;Personal Data;Purpose of Use;Purpose Limitation;Communications or Utilization Data;Non-Personal Data Collected Automatically;\"Cookies\" - Information Stored Automatically on Your Computer;Children;Security;Links to Other Web Sites;Questions and Comments");
            Delay.Milliseconds(0);
            
            // Step## Press Back
            Report.Log(ReportLevel.Info, "Section", "Step## Press Back", new RecordItemIndex(19));
            
            BackAction();
            Delay.Milliseconds(0);
            
            // Validation## Info screen is displayed.
            Report.Log(ReportLevel.Info, "Section", "Validation## Info screen is displayed.", new RecordItemIndex(21));
            
            VerifyActionBarTitle("Info");
            Delay.Milliseconds(0);
            
            // Step## Press Terms & Conditions
            Report.Log(ReportLevel.Info, "Section", "Step## Press Terms & Conditions", new RecordItemIndex(23));
            
            SelectPageFromInfo("Terms & Conditions");
            Delay.Milliseconds(0);
            
            // Validation## Detailed information present
            Report.Log(ReportLevel.Info, "Section", "Validation## Detailed information present", new RecordItemIndex(25));
            
            ValidateContents("Terms of use;1. Scope;2. Services;3. Registration;4. Rights of Use to Information, Software and Documentation;5. Intellectual Property;6. Duties of the user;7. Hyperlinks;8. Liability for Defects of Title or Quality;9. Other Liability, Viruses;10. Compliance with Export Control Regulations;11. Data Privacy Protection;12. Supplementary Agreements, Place of Jurisdiction, Applicable Law");
            Delay.Milliseconds(0);
            
            // Step## Goto settings -> select language
            Report.Log(ReportLevel.Info, "Section", "Step## Goto settings -> select language", new RecordItemIndex(27));
            
            BackAction();
            Delay.Milliseconds(0);
            
            BackAction();
            Delay.Milliseconds(0);
            
            TapOnRightButton("Select settings screen");
            Delay.Milliseconds(0);
            
            SelectLanguageFromSettings();
            Delay.Milliseconds(0);
            
            // Step## Change the language to Chinese and press Done
            Report.Log(ReportLevel.Info, "Section", "Step## Change the language to Chinese and press Done", new RecordItemIndex(32));
            
            SelectLanguage("Chinese");
            Delay.Milliseconds(0);
            
            TapOnRightButton("Click on 'Done'");
            Delay.Milliseconds(0);
            
            // Step## Select Info icon -> press Legal notice
            Report.Log(ReportLevel.Info, "Section", "Step## Select Info icon -> press Legal notice", new RecordItemIndex(35));
            
            BackAction();
            Delay.Milliseconds(0);
            
            ClickInfoButton();
            Delay.Milliseconds(0);
            
            SelectPageFromInfo("Legal Notice");
            Delay.Milliseconds(0);
            
            // Validation## Should be displayed in english only. App language changes should not be reflected in the imprint section
            Report.Log(ReportLevel.Info, "Section", "Validation## Should be displayed in english only. App language changes should not be reflected in the imprint section", new RecordItemIndex(39));
            
            ValidateContentsContains("Copyright;App;version;Sivantos Pte. Ltd. 2017. All Rights Reserved;Corporate Information;Managing Directors;Official Address;Legal Manufacturer;Germany", ValueConverter.ArgumentFromString<bool>("isFound", "True"));
            Delay.Milliseconds(0);
            
            ValidateCESign();
            Delay.Milliseconds(0);
            
            // Step## Reset the language to English
            Report.Log(ReportLevel.Info, "Section", "Step## Reset the language to English", new RecordItemIndex(42));
            
            BackAction();
            Delay.Milliseconds(0);
            
            BackAction();
            Delay.Milliseconds(0);
            
            TapOnRightButton("Select settings screen");
            Delay.Milliseconds(0);
            
            SelectLanguageFromSettings();
            Delay.Milliseconds(0);
            
            SelectLanguage("English");
            Delay.Milliseconds(0);
            
            TapOnRightButton("Click on 'Done'");
            Delay.Milliseconds(0);
            
            BackAction();
            Delay.Milliseconds(0);
            
        }

#region Image Feature Data
#endregion
    }
#pragma warning restore 0436
}
