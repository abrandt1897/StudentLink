#pragma checksum "C:\Users\greys\Documents\Github\mc_02\Backend\WebAppliaion-mc_02\Views\Students\CreateStudent.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f9ce04ede2bbd153bf441622d51ede809c632107"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Students_CreateStudent), @"mvc.1.0.view", @"/Views/Students/CreateStudent.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f9ce04ede2bbd153bf441622d51ede809c632107", @"/Views/Students/CreateStudent.cshtml")]
    public class Views_Students_CreateStudent : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<WebApplication_mc_02.Models.Students>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("background-color:#111111"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\greys\Documents\Github\mc_02\Backend\WebAppliaion-mc_02\Views\Students\CreateStudent.cshtml"
  
    Layout = null;

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<!DOCTYPE html>\r\n\r\n<html>\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f9ce04ede2bbd153bf441622d51ede809c6321073417", async() => {
                WriteLiteral(@"
    <meta name=""viewport"" content=""width=device-width"" />
    <link rel=""stylesheet"" href=""https://maxcdn.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css"">
    <script src=""https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js""></script>
    <script src=""https://maxcdn.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js""></script>

    <style>
        <style type=""text/css"" >

        .tab {
            display: inline-block;
            margin-left: 40px;
        }

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
        }

   ");
                WriteLiteral(@"     .form-style-5 label {
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
            -webkit-box-sizing: border-box;
            -moz-box-sizing: border-box;
            background-co");
                WriteLiteral(@"lor: #e8eeef;
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
            height: 35px;
        }

        .form-style-5 .number {
            background: #1abc9c;
            color: #fff;
            height: 30px");
                WriteLiteral(@";
            width: 30px;
            display: inline-block;
            font-size: 0.8em;
            margin-right: 4px;
            line-height: 30px;
            text-align: center;
            text-shadow: 0 1px 0 rgba(255,255,255,0.2);
            border-radius: 15px 15px 15px 0px;
        }
            .form-style-5 button[type=""submit""],
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
                border-width: 1px 1px 3px;
                margin-bottom: 10px;
            }
                .form-style-5 button[type=""submit""]:hover,
         ");
                WriteLiteral(@"       .form-style-5 input[type=""submit""]:hover,
                .form-style-5 input[type=""button""]:hover {
                    background: #109177;
                }
    </style>
    <script>
        function getToken() {
            var childWindow = window.open(""https://canvas.iastate.edu/profile/settings"");
            childWindow.focus();
        }

        function createAccount() {
            var username = document.getElementById(""username"").value;
            var password = document.getElementById(""password"").value;
            var token = document.getElementById(""token"").value;
            var formData = ""{Username:"" + username + "", Password:"" + password + ""}"";
            console.log(formData);
            console.log(token);
            $.ajax({
                type: ""PUT"",
                url: ""/api/Students/""+token,
                data: formData,
                success: function (response) {
                    console.log(response)
                },
                ");
                WriteLiteral(@"dataType: ""json"",
                contentType: ""application/json""
            });
        }

        function allowCreation() {
            document.getElementById(""creationbutton"").removeAttribute(""disabled"");
        }
    </script>
    <title>Create Account</title>
");
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
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f9ce04ede2bbd153bf441622d51ede809c63210710049", async() => {
                WriteLiteral("\r\n    <div class=\"container justify-content-center\">\r\n        <div class=\"form-style-5 bg-dark\">\r\n");
#nullable restore
#line 167 "C:\Users\greys\Documents\Github\mc_02\Backend\WebAppliaion-mc_02\Views\Students\CreateStudent.cshtml"
             using (Html.BeginForm("PostStudent", "Students", FormMethod.Post))
            {

#line default
#line hidden
#nullable disable
                WriteLiteral(@"                <nav class=""navbar navbar-dark bg-dark"">
                    <button class=""navbar-toggler"" type=""button"" data-toggle=""collapse"" data-target=""#navbarToggleExternalContent"" aria-controls=""navbarToggleExternalContent"" aria-expanded=""false"" aria-label=""Toggle navigation"">
                        <!--<span class=""navbar-toggler-icon""></span>-->
                        <legend><span class=""number "">1</span>Get Token</legend>
                    </button>
                </nav>
");
                WriteLiteral(@"                <div class=""collapse"" id=""navbarToggleExternalContent"">
                    <div class=""bg-dark text-white p-4"">
                        <input type=""button"" value=""Get Token"" onclick=""getToken()"" />
                        <legend><span class=""number"">i</span>Login to your Canvas profile</legend>
                        <legend><span class=""number"">ii</span>Click <code>New Access Token</code></legend>
                        <legend><span class=""number"">iii</span><code>Generate Token</code></legend>
                        <legend><span class=""number"">iv</span>Copy Token</legend>
                    </div>
                </div>
                <fieldset>
                    <nav class=""navbar navbar-dark bg-dark"">
                        <button class=""navbar-toggler"" type=""button"" onclick=""allowCreation()"" data-toggle=""collapse"" data-target=""#navbarToggleExternalContent1"" aria-controls=""navbarToggleExternalContent1"" aria-expanded=""false"" aria-label=""Toggle navigation"">
         ");
                WriteLiteral(@"                   <legend><span class=""number"">2</span>Create Account</legend>
                        </button>
                    </nav>

                    <div class=""collapse"" id=""navbarToggleExternalContent1"">
                        <div class=""bg-dark p-4"">
                            <input class=""text-white"" id=""username"" type=""text"" name=""Username"" placeholder=""Create Username *"">
                            <input class=""text-white"" id=""password"" type=""password"" name=""Password"" placeholder=""Create Password *"">
                            <input class=""text-white"" id=""token"" type=""text"" name=""canvasOAuthToken"" placeholder=""Paste Token *"" />
                        </div>
                    </div>
                </fieldset>
                <button disabled id=""creationbutton"" type=""submit"">Create Account</button>
");
#nullable restore
#line 201 "C:\Users\greys\Documents\Github\mc_02\Backend\WebAppliaion-mc_02\Views\Students\CreateStudent.cshtml"
            }

#line default
#line hidden
#nullable disable
                WriteLiteral("        </div>\r\n    </div>\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</html>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<WebApplication_mc_02.Models.Students> Html { get; private set; }
    }
}
#pragma warning restore 1591
