using System.Web;
using System.Web.Optimization;

namespace DashBoard.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                    "~/Scripts/jquery-{version}.js",
                    "~/Scripts/jquery.easing.1.3.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive-ajax.js",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));
            bundles.Add(new ScriptBundle("~/bundles/jsolution").Include(
                "~/assets/plugins/perfect-scrollbar/perfect-scrollbar.min.js",
                "~/Scripts/scripts.js",
                "~/assets/plugins/pace/pace.min.js",
                "~/Scripts/select2.js"
                ));
            bundles.Add(new StyleBundle("~/bundels/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/font-awesome.css",
                      "~/Content/style.css",
                      "~/Content/responsive.css",
                      "~/Content/datepicker.css",
                      "~/Content/animate.min.css",
                      "~/Content/select2.css",
                      "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/bundles/asset").Include(
               "~/assets/images/favicon.png",
               "~/assets/images/favicon.png",
               "~/assets/images/apple-touch-icon-57-precomposed.png",
               "~/assets/images/apple-touch-icon-114-precomposed.png",
               "~/assets/images/apple-touch-icon-72-precomposed.png",
               "~/assets/images/apple-touch-icon-144-precomposed.png",
               "~/assets/plugins/pace/pace-theme-flash.css",
               "~/assets/plugins/perfect-scrollbar/perfect-scrollbar.css"

                ));
        }
    }
}

