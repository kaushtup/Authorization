#pragma checksum "/Users/macoxs/Desktop/CrossoverSpa_Core/CrossoverSpa.Core/Views/Error/UnAuthorized.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e0448819f6342742814026dc682ffd8bb4f954a2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Error_UnAuthorized), @"mvc.1.0.view", @"/Views/Error/UnAuthorized.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Error/UnAuthorized.cshtml", typeof(AspNetCore.Views_Error_UnAuthorized))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "/Users/macoxs/Desktop/CrossoverSpa_Core/CrossoverSpa.Core/Views/_ViewImports.cshtml"
using CrossoverSpa.Core;

#line default
#line hidden
#line 2 "/Users/macoxs/Desktop/CrossoverSpa_Core/CrossoverSpa.Core/Views/_ViewImports.cshtml"
using CrossoverSpa.Core.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e0448819f6342742814026dc682ffd8bb4f954a2", @"/Views/Error/UnAuthorized.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4860ccd979c4deccd674a812b4534ca12951fafb", @"/Views/_ViewImports.cshtml")]
    public class Views_Error_UnAuthorized : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "/Users/macoxs/Desktop/CrossoverSpa_Core/CrossoverSpa.Core/Views/Error/UnAuthorized.cshtml"
  
    Layout = null;

#line default
#line hidden
            BeginContext(27, 166, true);
            WriteLiteral("<h1>Oops! Page not found</h1>\r\n<h1 class=\"text-danger\">401 Error.</h1>\r\n<h3 class=\"text-danger\">we are sorry, but you are not authorized to access this page.</h3>\r\n\r\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591