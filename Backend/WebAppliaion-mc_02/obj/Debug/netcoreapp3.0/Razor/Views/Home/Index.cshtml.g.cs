#pragma checksum "/home/greysonj/mc_02/Backend/WebAppliaion-mc_02/Views/Home/Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6c88d6842902b798259c75b0cd4782975930761c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6c88d6842902b798259c75b0cd4782975930761c", @"/Views/Home/Index.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<WebApplication_mc_02.Models.DTO.Login>
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
#nullable restore
#line 2 "/home/greysonj/mc_02/Backend/WebAppliaion-mc_02/Views/Home/Index.cshtml"
  
    Layout = null;

#line default
#line hidden
#nullable disable
            WriteLiteral("<html>\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6c88d6842902b798259c75b0cd4782975930761c3196", async() => {
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
                WriteLiteral(@"   .form-style-5 label {
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
                -webkit-box-sizing: border-b");
                WriteLiteral(@"ox;
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
                height: 3");
                WriteLiteral(@"5px;
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

            .form-style-5 button[type=""button""],
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
   ");
                WriteLiteral(@"             border-width: 1px 1px 3px;
                margin-bottom: 10px;
            }

                .form-style-5 button[type=""button""]:hover,
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
            var formData = ""{Username:"" + username + "", Password:"" + password + ""}""
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
");
                WriteLiteral("    </script>\n    <title>Login</title>\n");
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
            WriteLiteral("\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6c88d6842902b798259c75b0cd4782975930761c9560", async() => {
                WriteLiteral(@"
    <nav class=""navbar navbar-expand-sm bg-dark navbar-dark justify-content-between"">
        <!-- Brand -->
        <a class=""navbar-brand"" href=""#"">Logo</a>

        <!-- Links -->
        <ul class=""navbar-nav"">
            <li class=""nav-item"">
                <a class=""nav-link"" href=""#"">Chats</a>
            </li>
            <li class=""nav-item"">
                <a class=""nav-link"" href=""#"">Profile</a>
            </li>
            <li class=""nav-item"">
                <a class=""nav-link"" href=""#"">Settings</a>
            </li>
        </ul>
    </nav>
    <div class=""container-fluid"">
        <div class=""row"">
            <div class=""col-sm-3"" style=""background-color:rgba(255,255,255,.1);"">
                <div class=""list-group"">
                    <a href=""#"" class=""list-group-item list-group-item-action"">First chat<span class=""badge"">12</span></a>
                    <a href=""#"" class=""list-group-item list-group-item-action"">Second chat<span class=""badge"">69</span></a>
                    <a href");
                WriteLiteral(@"=""#"" class=""list-group-item list-group-item-action"">Third chat<span class=""badge"">999</span></a>
                </div>
            </div>
            <div class=""col-sm-9"" style=""background-color:rgba(255,255,255,.1);"">
                <ul class=""list-group"">
                    <li class=""list-group-item"">New <span class=""badge"">12</span></li>
                    <li class=""list-group-item"">Deleted <span class=""badge"">5</span></li>
                    <li class=""list-group-item"">Warnings <span class=""badge"">3</span></li>
                </ul>
            </div>
        </div>
    </div>
");
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
            WriteLiteral("\n</html>\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<WebApplication_mc_02.Models.DTO.Login> Html { get; private set; }
    }
}
#pragma warning restore 1591
