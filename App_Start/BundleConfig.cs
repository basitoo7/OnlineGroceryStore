using System.Web;
using System.Web.Optimization;

namespace GroceryStore
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                //"~/Scripts/jquery-{version}.js"));
                        //"~/js/jquery-1.11.1.min.js",
                        "~/Scripts/Category.js",
                        "~/Scripts/CheckOut.js",
                        "~/Scripts/CustomerOrder.js",
                        "~/js/move-top.js",
                        "~/js/easing.js",
                        "~/js/jquery.datetimepicker.full.min.js",
                        "~/js/jquery.flexslider.js",
                        "~/js/bootstrap.min.js",
                        "~/js/minicart.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      //"~/css/style.css",
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/css/style.css",
                      "~/css/jquery.datetimepicker.css",
                      "~/css/font-awesome.css",
                      "~/css/flexslider.css"
                      ));
        }
    }
}
