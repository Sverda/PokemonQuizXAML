﻿

#pragma checksum "C:\Users\Damian\Documents\Visual Studio 2015\Projects\PokemonQuizXAML\PokemonQuizXAML\PokemonQuizXAML.Windows\AddPokemonPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "CCDED773E984CA33CBAF1E64DE0E1942"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PokemonQuizXAML
{
    partial class AddPokemonPage : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 66 "..\..\AddPokemonPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.addPokemon_Click;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 63 "..\..\AddPokemonPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.pickDisplayPokemonImage_Click;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 57 "..\..\AddPokemonPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.pickHiddenPokemonImage_Click;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}

