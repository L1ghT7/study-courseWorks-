﻿#pragma checksum "..\..\..\Windows\PartnerWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "EBAF874F0ABCD3C7333C1535E1D26354"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using ForeignTradeContractsWorkstation.App.windows;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace ForeignTradeContractsWorkstation.App.windows {
    
    
    /// <summary>
    /// PartnerWindow
    /// </summary>
    public partial class PartnerWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\..\Windows\PartnerWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dataGrid;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\Windows\PartnerWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox listBox;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\Windows\PartnerWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button addRecord;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\Windows\PartnerWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button updateRecord;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\Windows\PartnerWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button deleteRecord;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\Windows\PartnerWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label searchLabel;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\Windows\PartnerWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox searchTextBox;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ForeignTradeContractsWorkstation.App;component/windows/partnerwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Windows\PartnerWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.dataGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 16 "..\..\..\Windows\PartnerWindow.xaml"
            this.dataGrid.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.DataGrid_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.listBox = ((System.Windows.Controls.ListBox)(target));
            return;
            case 3:
            this.addRecord = ((System.Windows.Controls.Button)(target));
            
            #line 39 "..\..\..\Windows\PartnerWindow.xaml"
            this.addRecord.Click += new System.Windows.RoutedEventHandler(this.addRecord_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.updateRecord = ((System.Windows.Controls.Button)(target));
            
            #line 40 "..\..\..\Windows\PartnerWindow.xaml"
            this.updateRecord.Click += new System.Windows.RoutedEventHandler(this.updateRecord_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.deleteRecord = ((System.Windows.Controls.Button)(target));
            
            #line 41 "..\..\..\Windows\PartnerWindow.xaml"
            this.deleteRecord.Click += new System.Windows.RoutedEventHandler(this.deleteRecord_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.searchLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.searchTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 43 "..\..\..\Windows\PartnerWindow.xaml"
            this.searchTextBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.searchTextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

