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
using System.Drawing;
using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Repository;
using Ranorex.Core.Testing;

namespace STSAndTouchControl
{
#pragma warning disable 0436 //(CS0436) The type 'type' in 'assembly' conflicts with the imported type 'type2' in 'assembly'. Using the type defined in 'assembly'.
    /// <summary>
    /// The class representing the STSAndTouchControlRepository element repository.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCode("Ranorex", "6.2")]
    [RepositoryFolder("5cc381e5-290e-438d-8676-262e4a1c6728")]
    public partial class STSAndTouchControlRepository : RepoGenBaseFolder
    {
        static STSAndTouchControlRepository instance = new STSAndTouchControlRepository();

        /// <summary>
        /// Gets the singleton class instance representing the STSAndTouchControlRepository element repository.
        /// </summary>
        [RepositoryFolder("5cc381e5-290e-438d-8676-262e4a1c6728")]
        public static STSAndTouchControlRepository Instance
        {
            get { return instance; }
        }

        /// <summary>
        /// Repository class constructor.
        /// </summary>
        public STSAndTouchControlRepository() 
            : base("STSAndTouchControlRepository", "/", null, 0, false, "5cc381e5-290e-438d-8676-262e4a1c6728", ".\\RepositoryImages\\STSAndTouchControlRepository5cc381e5.rximgres")
        {
        }

#region Variables

#endregion

        /// <summary>
        /// The Self item info.
        /// </summary>
        [RepositoryItemInfo("5cc381e5-290e-438d-8676-262e4a1c6728")]
        public virtual RepoItemInfo SelfInfo
        {
            get
            {
                return _selfInfo;
            }
        }
    }

    /// <summary>
    /// Inner folder classes.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCode("Ranorex", "6.2")]
    public partial class STSAndTouchControlRepositoryFolders
    {
    }
#pragma warning restore 0436
}