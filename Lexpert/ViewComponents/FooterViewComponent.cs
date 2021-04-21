using Lexpert.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lexpert.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        private AppDbContext context;

        public FooterViewComponent(AppDbContext context)
        {
            this.context = context;

        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var info = context.SiteSetting.FirstOrDefault();
            var navpr = context.NavlinkSetup.FirstOrDefault();
            GlobSetVM glob = new GlobSetVM();
            if (info != null)
            {
                glob.SettingsId = info.SettingsId;
                glob.IsBlogEnabled = info.IsBlogEnabled;
                glob.UseSmallFooter = info.UseSmallFooter;
            }
            if (navpr != null)
            {
                glob.BasicProductId = navpr.BasicProductId;
                glob.AdvanceProductId = navpr.AdvanceProductId;
                glob.UltimateProductId = navpr.UltimateProductId;
            }

            return View(glob);
        }
    }
}