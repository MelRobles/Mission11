using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Mission7_Books.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission7_Books.Infrastructure
{
    [HtmlTargetElement("div", Attributes = "page-blah")]
    public class PagnationTagHelper : TagHelper
    {
        //Dynamically create the page links for us

        private IUrlHelperFactory uhf;

        public PagnationTagHelper(IUrlHelperFactory temp)
        {
            uhf = temp;
        }
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext vc { get; set; }
        //Different than the View Context
        public PageInfo PageBlah { get; set; }
        public string PageAction { get; set; }
        public string PageClass { get; set; }
        public bool PageClassesEnabled { get; set; }
        public string PageClassNormal { get; set; }
        public string PageClassSelected { get; set; }
       
        public override void Process (TagHelperContext thc, TagHelperOutput tho)
        {
            IUrlHelper uh = uhf.GetUrlHelper(vc);

            //For overall
            TagBuilder final = new TagBuilder("div");

            for (int i = 1; i <= PageBlah.TotalPages; i++) 
            {
                //For individual links
                TagBuilder tb = new TagBuilder("a");
                tb.Attributes["href"] = uh.Action(PageAction, new { pageNum = i });
               
                if (PageClassesEnabled)
                {
                    tb.AddCssClass(PageClass);
                    tb.AddCssClass(i == PageBlah.CurrentPage ? PageClassSelected : PageClassNormal);
                }
                tb.AddCssClass(PageClass);
                tb.InnerHtml.Append(i.ToString());

                final.InnerHtml.AppendHtml(tb);
            }

            tho.Content.AppendHtml(final.InnerHtml);
        }
    }
}
