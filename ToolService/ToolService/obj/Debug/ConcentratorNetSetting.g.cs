﻿#pragma checksum "..\..\ConcentratorNetSetting.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "02A7DEC8C558F990086B2233329F6319BEEF2D51"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace ToolService {
    
    
    /// <summary>
    /// ConcentratorNetSetting
    /// </summary>
    public partial class ConcentratorNetSetting : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\ConcentratorNetSetting.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox concentratorText;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\ConcentratorNetSetting.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox concentratorIPText;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\ConcentratorNetSetting.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox concentratorPortText;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\ConcentratorNetSetting.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox localHostSubnetMsakText;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\ConcentratorNetSetting.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox remoteHostIPText;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\ConcentratorNetSetting.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox remoteHostPortText;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\ConcentratorNetSetting.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox gatewayIPText;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\ConcentratorNetSetting.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button excuteBtn;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\ConcentratorNetSetting.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button cancelBtn;
        
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
            System.Uri resourceLocater = new System.Uri("/ToolService;component/concentratornetsetting.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ConcentratorNetSetting.xaml"
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
            this.concentratorText = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.concentratorIPText = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.concentratorPortText = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.localHostSubnetMsakText = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.remoteHostIPText = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.remoteHostPortText = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.gatewayIPText = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.excuteBtn = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\ConcentratorNetSetting.xaml"
            this.excuteBtn.Click += new System.Windows.RoutedEventHandler(this.excuteBtn_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.cancelBtn = ((System.Windows.Controls.Button)(target));
            
            #line 21 "..\..\ConcentratorNetSetting.xaml"
            this.cancelBtn.Click += new System.Windows.RoutedEventHandler(this.cancelBtn_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

