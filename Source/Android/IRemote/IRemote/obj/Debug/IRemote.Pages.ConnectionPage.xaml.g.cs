// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.42000
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace IRemote {
    
    
    [global::Xamarin.Forms.Xaml.XamlFilePathAttribute("/Users/kirillgolowko/Repos/cw_1/IRemote/IRemote/Pages/ConnectionPage.xaml")]
    public partial class ConnectionPage : global::Xamarin.Forms.ContentPage {
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private global::Xamarin.Forms.ListView Devices;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private global::Xamarin.Forms.Label status;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private void InitializeComponent() {
            global::Xamarin.Forms.Xaml.Extensions.LoadFromXaml(this, typeof(ConnectionPage));
            Devices = global::Xamarin.Forms.NameScopeExtensions.FindByName <global::Xamarin.Forms.ListView>(this, "Devices");
            status = global::Xamarin.Forms.NameScopeExtensions.FindByName <global::Xamarin.Forms.Label>(this, "status");
        }
    }
}
