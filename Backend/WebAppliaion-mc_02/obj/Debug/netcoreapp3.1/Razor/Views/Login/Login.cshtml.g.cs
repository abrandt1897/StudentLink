#pragma checksum "/home/greysonj/mc_02/Backend/WebAppliaion-mc_02/Views/Login/Login.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "800f560ebe3fd5fd4329aae6bf92a94607de439a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Login_Login), @"mvc.1.0.razor-page", @"/Views/Login/Login.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"800f560ebe3fd5fd4329aae6bf92a94607de439a", @"/Views/Login/Login.cshtml")]
    public class Views_Login_Login : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("    <html>\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "800f560ebe3fd5fd4329aae6bf92a94607de439a2645", async() => {
                WriteLiteral(@"
        <!-- Information about the page -->
        <!--This is the comment tag-->
        <title>Login</title>

        <link rel=""stylesheet"" href=""https://maxcdn.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css"">
        <script src=""https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js""></script>
        <script src=""https://maxcdn.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js""></script>

        <style>
            <style type=""text/css"" >
            .form-style-5 {
                max-width: 500px;
                padding: 10px 20px;
                background: #f4f7f8;
                margin: 10px auto;
                padding: 20px;
                background: #f4f7f8;
                border-radius: 8px;
                font-family: Georgia, ""Times New Roman"", Times, serif;
            }

            .form-style-5 fieldset {
                border: none;
            }

            .form-style-5 legend {
                font-size: 1.4em;
                margin-bottom: 10px;
  ");
                WriteLiteral(@"          }

            .form-style-5 label {
                display: block;
                margin-bottom: 8px;
            }

            .form-style-5 input[type=""text""],
            .form-style-5 input[type=""password""],
            .form-style-5 input[type=""date""],
            .form-style-5 input[type=""datetime""],
            .form-style-5 input[type=""email""],
            .form-style-5 input[type=""number""],
            .form-style-5 input[type=""search""],
            .form-style-5 input[type=""time""],
            .form-style-5 input[type=""url""],
            .form-style-5 textarea,
            .form-style-5 select {
                font-family: Georgia, ""Times New Roman"", Times, serif;
                background: rgba(255,255,255,.1);
                border: none;
                border-radius: 4px;
                font-size: 15px;
                margin: 0;
                outline: 0;
                padding: 10px;
                width: 100%;
                box-sizing: border-box;
                -webki");
                WriteLiteral(@"t-box-sizing: border-box;
                -moz-box-sizing: border-box;
                background-color: #e8eeef;
                color: #8a97a0;
                -webkit-box-shadow: 0 1px 0 rgba(0,0,0,0.03) inset;
                box-shadow: 0 1px 0 rgba(0,0,0,0.03) inset;
                margin-bottom: 30px;
            }

                .form-style-5 input[type=""text""]:focus,
                .form-style-5 input[type=""date""]:focus,
                .form-style-5 input[type=""datetime""]:focus,
                .form-style-5 input[type=""email""]:focus,
                .form-style-5 input[type=""number""]:focus,
                .form-style-5 input[type=""search""]:focus,
                .form-style-5 input[type=""time""]:focus,
                .form-style-5 input[type=""url""]:focus,
                .form-style-5 textarea:focus,
                .form-style-5 select:focus {
                    background: #d2d9dd;
                }

            .form-style-5 select {
                -webkit-appearance: menulist-button;
   ");
                WriteLiteral(@"             height: 35px;
            }

            .form-style-5 .number {
                background: #1abc9c;
                color: #fff;
                height: 30px;
                width: 30px;
                display: inline-block;
                font-size: 0.8em;
                margin-right: 4px;
                line-height: 30px;
                text-align: center;
                text-shadow: 0 1px 0 rgba(255,255,255,0.2);
                border-radius: 15px 15px 15px 0px;
            }

            .form-style-5 input[type=""submit""],
            .form-style-5 input[type=""button""] {
                position: relative;
                display: block;
                padding: 19px 39px 18px 39px;
                color: #FFF;
                margin: 0 auto;
                background: #1abc9c;
                font-size: 18px;
                text-align: center;
                font-style: normal;
                width: 100%;
                border: 1px solid #16a085;
                border-width: ");
                WriteLiteral(@"1px 1px 3px;
                margin-bottom: 10px;
            }

                .form-style-5 input[type=""submit""]:hover,
                .form-style-5 input[type=""button""]:hover {
                    background: #109177;
                }
        </style>
        <script>
            function sendLogin() {
                var loginForm = document.getElementById(""loginform"");
                console.log(loginForm);
                var username = loginForm[1].value
                var password = loginForm[2].value
                var formData = ""{Username:""+username+"", Password:""+password+""}""
                console.log(formData);
                $.ajax({
                    type: ""POST"",
                    url: ""/api/Login"",
                    data: formData,
                    success: function () {
                           console.log(""success"")
                    },
                    dataType: ""json"",
                    contentType: ""application/json""
                });
            }
        </s");
                WriteLiteral("cript>\n    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "800f560ebe3fd5fd4329aae6bf92a94607de439a8976", async() => {
                WriteLiteral(@"
        <!--Contents of the webpage-->
        <div class=""container card justify-content-center"">
            <div class=""form-style-5"">
                <form id=""loginform"">
                    <fieldset>
                        <legend><span class=""number"">1</span> Student Login</legend>
                        <input type=""text"" name=""Username"" placeholder=""Your Username *"">
                        <input type=""password"" name=""Password"" placeholder=""Your Password *"">
                    </fieldset>
                    <input type=""button"" value=""Login"" onclick=""sendLogin(this)""/>
                </form>
            </div>
        </div>
    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n    </html>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<WebApplication_mc_02.Views.LoginModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<WebApplication_mc_02.Views.LoginModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<WebApplication_mc_02.Views.LoginModel>)PageContext?.ViewData;
        public WebApplication_mc_02.Views.LoginModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
