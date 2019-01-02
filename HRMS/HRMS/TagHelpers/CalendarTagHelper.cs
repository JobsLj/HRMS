using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using HRMS.Models;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace HRMS.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("calendar", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class CalendarTagHelper : TagHelper
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public List<CalendarEvent> Events { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "section";
            output.Attributes.Add("class", "calendar");
            output.Content.SetHtmlContent(GetHtml());
            output.TagMode = TagMode.StartTagAndEndTag;
        }

        private string GetHtml()
        {
            var monthStart = new DateTime(Year, Month, 1);
            var events = Events?.GroupBy(e => e.Date);
            var currentDate = DateTime.Today;

            var html = new XDocument(
                new XElement("div",
                    new XAttribute("class", "container-fluid"),
                    new XElement("header",
                        new XElement("h4",
                            new XAttribute("class", "display-4 mb-2 text-center"),
                            monthStart.ToString("MMMM yyyy")
                        ),
                        new XElement("div",
                            new XAttribute("class", "row d-none d-lg-flex p-1 bg-dark text-white"),
                            Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>().Select(d =>
                                new XElement("h5",
                                    new XAttribute("class", "col-lg p-1 text-center"),
                                    d.ToString()
                                )
                            )
                        )
                    ),
                    new XElement("div",
                        new XAttribute("class", "row border border-right-0 border-bottom-0"),
                        GetDatesHtml()
                    )
                )
            );

            return html.ToString();

            IEnumerable<XElement> GetDatesHtml()
            {
                var startDate = monthStart.AddDays(-(int)monthStart.DayOfWeek);
                var dates = Enumerable.Range(0, 42).Select(i => startDate.AddDays(i));              

                foreach (var d in dates)
                {
                    if (d.DayOfWeek == DayOfWeek.Sunday && d != startDate)
                    {
                        yield return new XElement("div",
                            new XAttribute("class", "w-100"),
                            String.Empty
                        );
                    }

                    var mutedClasses = "d-none d-lg-inline-block bg-light text-muted";

                    if (d.Date == currentDate)
                    {
                        yield return new XElement("div",
                            new XAttribute("class", $"day col-lg p-2 border border-left-0 current-date border-top-0 text-truncate {(d.Month != monthStart.Month ? mutedClasses : null)}"),
                            new XElement("h5",
                                new XAttribute("class", "row align-items-center"),
                                new XElement("span",
                                    new XAttribute("class", "date col-1"),
                                    d.Day
                                ),
                                new XElement("small",
                                    new XAttribute("class", "col d-lg-none text-center text-muted"),
                                    d.DayOfWeek.ToString()
                                ),
                                new XElement("span",
                                    new XAttribute("class", "col-1"),
                                    String.Empty
                                )
                            ),
                            GetEventHtml(d)
                        );
                    }
                    else
                    {
                        yield return new XElement("div",
                            new XAttribute("class", $"day col-lg p-2 border border-left-0 border-top-0 text-truncate {(d.Month != monthStart.Month ? mutedClasses : null)}"),
                            new XElement("h5",
                                new XAttribute("class", "row align-items-center"),
                                new XElement("span",
                                    new XAttribute("class", "date col-1"),
                                    d.Day
                                ),
                                new XElement("small",
                                    new XAttribute("class", "col d-lg-none text-center text-muted"),
                                    d.DayOfWeek.ToString()
                                ),
                                new XElement("span",
                                    new XAttribute("class", "col-1"),
                                    String.Empty
                                )
                            ),
                            GetEventHtml(d)
                        );
                    }
                }
            }

            IEnumerable<XElement> GetEventHtml(DateTime d)
            {
                if (d < currentDate)
                {
                    return events?.SingleOrDefault(e => e.Key == d)?.Select(e =>
                    new XElement("a",
                        new XAttribute("class", $"event d-block p-1 pl-2 pr-2 mb-1 rounded text-truncate small over-{e.Type} text-white cal-event over-event"),
                        new XAttribute("data-toggle", $"modal"),
                        new XAttribute("data-target", $"#detailsModal"),
                        new XAttribute("title", e.Adr),
                        new XElement("span",
                            new XAttribute("class", $"occupancy"),
                            $"Occupancy: {e.Occupancy}"
                            ),
                        new XElement("span",
                            new XAttribute("class", $"percentage"),
                            "+3%"
                            ),
                        e.Adr
                    )
                ) ?? new[] {
                new XElement("p",
                    new XAttribute("class", "d-lg-none"),
                    "No events"
                )
                };
                }
                else
                {
                    return events?.SingleOrDefault(e => e.Key == d)?.Select(e =>
                    new XElement("a",
                        new XAttribute("class", $"event d-block p-1 pl-2 pr-2 mb-1 rounded text-truncate small bg-{e.Type} text-white cal-event"),
                        new XAttribute("href", $"/Home/Details/" + d.Day + "-" + d.Month + "-" + d.Year),
                        new XAttribute("title", e.Adr),
                        new XElement("span",
                            new XAttribute("class", $"occupancy"),
                            $"Occupancy: {e.Occupancy}"
                            ),
                        new XElement("span",
                            new XAttribute("class", $"percentage"),
                            "+3%"
                            ),
                        e.Adr
                    )
                ) ?? new[] {
                new XElement("p",
                    new XAttribute("class", "d-lg-none"),
                    "No events"
                )
                };
                }         
            }
        }
    }
}
