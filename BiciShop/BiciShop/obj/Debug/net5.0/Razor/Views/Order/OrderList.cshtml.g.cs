#pragma checksum "C:\Users\Tat1a\source\repos\BiciShop\BiciShop\Views\Order\OrderList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "00ea5199d0440a1b5889fc7091db6ef22043b7ce"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Order_OrderList), @"mvc.1.0.view", @"/Views/Order/OrderList.cshtml")]
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
#nullable restore
#line 1 "C:\Users\Tat1a\source\repos\BiciShop\BiciShop\Views\_ViewImports.cshtml"
using BiciShop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Tat1a\source\repos\BiciShop\BiciShop\Views\_ViewImports.cshtml"
using BiciShop.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"00ea5199d0440a1b5889fc7091db6ef22043b7ce", @"/Views/Order/OrderList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"642cb351d30adeffea9a48deeac6b5c80d08a7ec", @"/Views/_ViewImports.cshtml")]
    public class Views_Order_OrderList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-dark"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("width: 50%;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<div class=\"d-flex justify-content-around flex-wrap mt-5\">\r\n");
#nullable restore
#line 3 "C:\Users\Tat1a\source\repos\BiciShop\BiciShop\Views\Order\OrderList.cshtml"
     foreach (var order in Model.Orders)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"        <div class=""card mb-3"" style=""width: 30rem;"">

            <div class=""card-body"">
                <div id=""carouselExampleCaptions"" style=""min-height: 10rem;"" class=""carousel slide"" data-ride=""carousel"">

                    <div class=""carousel-inner"">
");
#nullable restore
#line 11 "C:\Users\Tat1a\source\repos\BiciShop\BiciShop\Views\Order\OrderList.cshtml"
                         for (int i = 0; i < order.OrderUnits.Count; i++)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <div");
            BeginWriteAttribute("class", " class=\"", 514, "\"", 560, 2);
            WriteAttributeValue("", 522, "carousel-item", 522, 13, true);
#nullable restore
#line 13 "C:\Users\Tat1a\source\repos\BiciShop\BiciShop\Views\Order\OrderList.cshtml"
WriteAttributeValue("", 535, i == 0 ? " active": "", 535, 25, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" style=\"min-height: 10rem;\">\r\n                                <img");
            BeginWriteAttribute("src", " src=\"", 627, "\"", 691, 2);
            WriteAttributeValue("", 633, "data:image/png;base64,", 633, 22, true);
#nullable restore
#line 14 "C:\Users\Tat1a\source\repos\BiciShop\BiciShop\Views\Order\OrderList.cshtml"
WriteAttributeValue("", 655, order.OrderUnits[i].Bicicleta.Photo, 655, 36, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"d-block w-100 h-100\" alt=\"...\">\r\n                            </div>\r\n");
#nullable restore
#line 16 "C:\Users\Tat1a\source\repos\BiciShop\BiciShop\Views\Order\OrderList.cshtml"
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </div>\r\n                </div>\r\n\r\n\r\n                <h3 class=\"card-title mt-3\">");
#nullable restore
#line 22 "C:\Users\Tat1a\source\repos\BiciShop\BiciShop\Views\Order\OrderList.cshtml"
                                       Write(order.GetTotalValue());

#line default
#line hidden
#nullable disable
            WriteLiteral(" ???</h3>\r\n                <p class=\"card-text text-success\">");
#nullable restore
#line 23 "C:\Users\Tat1a\source\repos\BiciShop\BiciShop\Views\Order\OrderList.cshtml"
                                             Write(order.HandlePhase.PhaseName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n\r\n                <div class=\"d-flex justify-content-center\">\r\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "00ea5199d0440a1b5889fc7091db6ef22043b7ce6764", async() => {
                WriteLiteral("Details");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "href", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 1105, "~/Order/OrderInfo/", 1105, 18, true);
#nullable restore
#line 26 "C:\Users\Tat1a\source\repos\BiciShop\BiciShop\Views\Order\OrderList.cshtml"
AddHtmlAttributeValue("", 1123, order.OrderId, 1123, 14, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                </div>\r\n\r\n                <ul class=\"list-group list-group-flush mt-3\">\r\n                    <li class=\"list-group-item\">");
#nullable restore
#line 30 "C:\Users\Tat1a\source\repos\BiciShop\BiciShop\Views\Order\OrderList.cshtml"
                                           Write(order.Date);

#line default
#line hidden
#nullable disable
            WriteLiteral("</li>\r\n\r\n                </ul>\r\n            </div>\r\n        </div>\r\n");
#nullable restore
#line 35 "C:\Users\Tat1a\source\repos\BiciShop\BiciShop\Views\Order\OrderList.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n\r\n<input type=\"hidden\"");
            BeginWriteAttribute("value", " value=\"", 1446, "\"", 1489, 1);
#nullable restore
#line 38 "C:\Users\Tat1a\source\repos\BiciShop\BiciShop\Views\Order\OrderList.cshtml"
WriteAttributeValue("", 1454, Model.OrderQueryOptions.PageNumber, 1454, 35, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" id=\"pageNumberInp\" />\r\n<div style=\"display: flex; justify-content: center; flex-wrap: nowrap; overflow-x: auto;\">\r\n");
#nullable restore
#line 40 "C:\Users\Tat1a\source\repos\BiciShop\BiciShop\Views\Order\OrderList.cshtml"
     for (int i = 1; i <= Model.PageCount; i++)
    {
        if (i == Model.OrderQueryOptions.PageNumber)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <button class=\"btn btn-success m-1 asyncLoadBtn pageBtn\">");
#nullable restore
#line 44 "C:\Users\Tat1a\source\repos\BiciShop\BiciShop\Views\Order\OrderList.cshtml"
                                                                Write(i);

#line default
#line hidden
#nullable disable
            WriteLiteral("</button>\r\n");
#nullable restore
#line 45 "C:\Users\Tat1a\source\repos\BiciShop\BiciShop\Views\Order\OrderList.cshtml"
        }
        else
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <button class=\"btn btn-dark m-1 asyncLoadBtn pageBtn\">");
#nullable restore
#line 48 "C:\Users\Tat1a\source\repos\BiciShop\BiciShop\Views\Order\OrderList.cshtml"
                                                             Write(i);

#line default
#line hidden
#nullable disable
            WriteLiteral("</button>\r\n");
#nullable restore
#line 49 "C:\Users\Tat1a\source\repos\BiciShop\BiciShop\Views\Order\OrderList.cshtml"
        }
    }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
</div>
<script type=""text/javascript"">
    $('.asyncLoadBtn').click(function () {
        let sortOrder = $('#sortOrderSelect').val()
        let pageNumber = 1;
        console.log('order async btn')

        if ($(this).hasClass('pageBtn')) {
            pageNumber = $(this).text();
        }

        $('#orderList').load(`/Order/OrderList/?firstLoad=false&orderQueryOptions.orderSort=${sortOrder}&orderQueryOptions.pageNumber=${pageNumber}`)
    })
</script>");
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
