#pragma checksum "C:\Users\Siew Mun\Desktop\FYP-APP\HRMS\HRMS\Views\Home\WhatIfAnalysis.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b2a5f0215e1958da668a5c7ea805c96bc9f593c1"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_WhatIfAnalysis), @"mvc.1.0.view", @"/Views/Home/WhatIfAnalysis.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/WhatIfAnalysis.cshtml", typeof(AspNetCore.Views_Home_WhatIfAnalysis))]
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
#line 1 "C:\Users\Siew Mun\Desktop\FYP-APP\HRMS\HRMS\Views\_ViewImports.cshtml"
using HRMS;

#line default
#line hidden
#line 2 "C:\Users\Siew Mun\Desktop\FYP-APP\HRMS\HRMS\Views\_ViewImports.cshtml"
using HRMS.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b2a5f0215e1958da668a5c7ea805c96bc9f593c1", @"/Views/Home/WhatIfAnalysis.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ce1d7316414513ea68c01560b78a295bf5398467", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_WhatIfAnalysis : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<WhatIfAnalysisViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("breadcrumb-link"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Details", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Home", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "C:\Users\Siew Mun\Desktop\FYP-APP\HRMS\HRMS\Views\Home\WhatIfAnalysis.cshtml"
  
    var DateString = Model.Date.ToString("dd-MM-yyyy");
    ViewData["Title"] = Model.Date.ToString("dd MMMM yyyy") + " Analysis";
    var xlabel = Newtonsoft.Json.JsonConvert.SerializeObject(Model.DatesLabel);


#line default
#line hidden
            BeginContext(255, 184, true);
            WriteLiteral("\r\n<!-- pageheader -->\r\n<div class=\"row\">\r\n    <div class=\"col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12\">\r\n        <div class=\"page-header\">\r\n            <h2 class=\"pageheader-title\">");
            EndContext();
            BeginContext(440, 17, false);
#line 13 "C:\Users\Siew Mun\Desktop\FYP-APP\HRMS\HRMS\Views\Home\WhatIfAnalysis.cshtml"
                                    Write(ViewData["Title"]);

#line default
#line hidden
            EndContext();
            BeginContext(457, 194, true);
            WriteLiteral("</h2>\r\n            <div class=\"page-breadcrumb\">\r\n                <nav aria-label=\"breadcrumb\">\r\n                    <ol class=\"breadcrumb\">\r\n                        <li class=\"breadcrumb-item\">");
            EndContext();
            BeginContext(651, 59, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "cebe4a88bac44a438aa58d154a75cd4b", async() => {
                BeginContext(697, 9, true);
                WriteLiteral("Dashboard");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(710, 109, true);
            WriteLiteral("</li>\r\n                        <li class=\"breadcrumb-item\" aria-current=\"page\">\r\n                            ");
            EndContext();
            BeginContext(819, 114, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "229d1f5f3b984e6fbe388aa020eb8694", async() => {
                BeginContext(922, 7, true);
                WriteLiteral("Details");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-selected", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 19 "C:\Users\Siew Mun\Desktop\FYP-APP\HRMS\HRMS\Views\Home\WhatIfAnalysis.cshtml"
                                                                                  WriteLiteral(DateString);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["selected"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-selected", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["selected"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(933, 4246, true);
            WriteLiteral(@"
                        </li>
                        <li class=""breadcrumb-item active"" aria-current=""page"">Analysis</li>
                    </ol>
                </nav>
            </div>
        </div>
    </div>
</div>


<!-- Average Daily Rate What-If -->
<div class=""row"">
    <div class=""col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12"">
        <div class=""card"">
            <h5 class=""card-header"">Average Daily Rate</h5>
            <div class=""card-body"">
                <canvas id=""averageDailyRate"" width=""400"" height=""150""></canvas>
            </div>
            <div class=""card-body border-top"">
                <div class=""row"">
                    <div class=""offset-xl-1 col-xl-3 col-lg-3 col-md-12 col-sm-12 col-12 p-3"">
                        <h4> Today's Earning: $2,800.30</h4>
                        <p>
                            Suspendisse potenti. Done csit amet rutrum.
                        </p>
                    </div>
                    <div class=""off");
            WriteLiteral(@"set-xl-1 col-xl-3 col-lg-3 col-md-6 col-sm-12 col-12 p-3"">
                        <h2 class=""font-weight-normal mb-3""><span>$48,325</span>                                                    </h2>
                        <div class=""mb-0 mt-3 legend-item"">
                            <span class=""fa-xs text-primary mr-1 legend-title ""><i class=""fa fa-fw fa-square-full""></i></span>
                            <span class=""legend-text"">Predicted ADR</span>
                        </div>
                    </div>
                    <div class=""offset-xl-1 col-xl-3 col-lg-3 col-md-6 col-sm-12 col-12 p-3"">
                        <h2 class=""font-weight-normal mb-3"">
                            <span>$59,567</span>
                        </h2>
                        <div class=""text-muted mb-0 mt-3 legend-item""> <span class=""fa-xs text-secondary mr-1 legend-title""><i class=""fa fa-fw fa-square-full""></i></span><span class=""legend-text"">Adjusted ADR</span></div>
                    </div>
           ");
            WriteLiteral(@"     </div>
            </div>
        </div>
    </div>

    <!-- RevPAR What-If -->
    <div class=""col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12"">
        <div class=""card"">
            <h5 class=""card-header"">Revenue Per Average Room</h5>
            <div class=""card-body"">
                <canvas id=""revenuePerAvg"" width=""400"" height=""150""></canvas>
            </div>
            <div class=""card-body border-top"">
                <div class=""row"">
                    <div class=""offset-xl-1 col-xl-3 col-lg-3 col-md-12 col-sm-12 col-12 p-3"">
                        <h4> Today's Earning: $2,800.30</h4>
                        <p>
                            Suspendisse potenti. Done csit amet rutrum.
                        </p>
                    </div>
                    <div class=""offset-xl-1 col-xl-3 col-lg-3 col-md-6 col-sm-12 col-12 p-3"">
                        <h2 class=""font-weight-normal mb-3""><span>$48,325</span>                                                    </h2>
");
            WriteLiteral(@"                        <div class=""mb-0 mt-3 legend-item"">
                            <span class=""fa-xs text-primary mr-1 legend-title ""><i class=""fa fa-fw fa-square-full""></i></span>
                            <span class=""legend-text"">Predicted RevPAR</span>
                        </div>
                    </div>
                    <div class=""offset-xl-1 col-xl-3 col-lg-3 col-md-6 col-sm-12 col-12 p-3"">
                        <h2 class=""font-weight-normal mb-3"">
                            <span>$59,567</span>
                        </h2>
                        <div class=""text-muted mb-0 mt-3 legend-item""> <span class=""fa-xs text-secondary mr-1 legend-title""><i class=""fa fa-fw fa-square-full""></i></span><span class=""legend-text"">Adjusted RevPAR</span></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>


<!-- Line Chart -->
<script type=""text/javascript"">
    $(function () {
        var ctx = document.getElementById('av");
            WriteLiteral("erageDailyRate\').getContext(\'2d\');\r\n        var myChart = new Chart(ctx, {\r\n            type: \'line\',\r\n\r\n            data: {\r\n                labels: ");
            EndContext();
            BeginContext(5180, 16, false);
#line 107 "C:\Users\Siew Mun\Desktop\FYP-APP\HRMS\HRMS\Views\Home\WhatIfAnalysis.cshtml"
                   Write(Html.Raw(xlabel));

#line default
#line hidden
            EndContext();
            BeginContext(5196, 2100, true);
            WriteLiteral(@",
                datasets: [{
                    label: 'Predicted ADR',
                    data: [12, 19, 3, 17, 6, 3, 47],
                    backgroundColor: ""rgba(89, 105, 255,0.5)"",
                    borderColor: ""rgba(89, 105, 255,0.7)"",
                    borderWidth: 2

                }, {
                    label: 'Adjusted ADR',
                    data: [2, 29, 5, 5, 2, 3, 10],
                    backgroundColor: ""rgba(255, 64, 123,0.5)"",
                    borderColor: ""rgba(255, 64, 123,0.7)"",
                    borderWidth: 2
                }]
            },
            options: {
                legend: {
                    display: true,
                    position: 'bottom',
                    labels: {
                        fontColor: '#71748d',
                        fontSize: 14,
                    }
                },
                scales: {
                    yAxes: [{
                        ticks: {
                            // Inclu");
            WriteLiteral(@"de a dollar sign in the ticks
                            callback: function (value, index, values) {
                                return '$' + value;
                            }
                        }
                    }]
                },

                scales: {
                    xAxes: [{
                        ticks: {
                            fontSize: 14,
                            fontFamily: 'Circular Std Book',
                            fontColor: '#71748d',
                        }
                    }],
                    yAxes: [{
                        ticks: {
                            fontSize: 14,
                            fontFamily: 'Circular Std Book',
                            fontColor: '#71748d',
                        }
                    }]
                }
            }
        });


        var ctx = document.getElementById('revenuePerAvg').getContext('2d');
        var myChart = new Chart(ctx, {
            type: 'lin");
            WriteLiteral("e\',\r\n\r\n            data: {\r\n                labels: ");
            EndContext();
            BeginContext(7297, 16, false);
#line 168 "C:\Users\Siew Mun\Desktop\FYP-APP\HRMS\HRMS\Views\Home\WhatIfAnalysis.cshtml"
                   Write(Html.Raw(xlabel));

#line default
#line hidden
            EndContext();
            BeginContext(7313, 1928, true);
            WriteLiteral(@",
                datasets: [{
                    label: 'Predicted ADR',
                    data: [12, 19, 3, 17, 6, 3, 47],
                    backgroundColor: ""rgba(89, 105, 255,0.5)"",
                    borderColor: ""rgba(89, 105, 255,0.7)"",
                    borderWidth: 2

                }, {
                    label: 'Adjusted ADR',
                    data: [2, 29, 5, 5, 2, 3, 10],
                    backgroundColor: ""rgba(255, 64, 123,0.5)"",
                    borderColor: ""rgba(255, 64, 123,0.7)"",
                    borderWidth: 2
                }]
            },

            options: {
                legend: {
                    display: true,
                    position: 'bottom',
                    labels: {
                        fontColor: '#71748d',
                        fontSize: 14,
                    }
                },
                scales: {
                    yAxes: [{
                        ticks: {
                            // Inc");
            WriteLiteral(@"lude a dollar sign in the ticks
                            callback: function (value, index, values) {
                                return '$' + value;
                            }
                        }
                    }]
                },

                scales: {
                    xAxes: [{
                        ticks: {
                            fontSize: 14,
                            fontFamily: 'Circular Std Book',
                            fontColor: '#71748d',
                        }
                    }],
                    yAxes: [{
                        ticks: {
                            fontSize: 14,
                            fontFamily: 'Circular Std Book',
                            fontColor: '#71748d',
                        }
                    }]
                }
            }
        });
    });
</script>

");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<WhatIfAnalysisViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
