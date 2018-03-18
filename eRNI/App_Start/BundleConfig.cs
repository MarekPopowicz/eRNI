using System.Web;
using System.Web.Optimization;

namespace eRNI
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/eRNIservice.js",
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/decimalValidation.js"));

            bundles.Add(new ScriptBundle("~/bundles/combobox").Include(
                        "~/Scripts/combobox.js"
                        ));

            // Użyj wersji deweloperskiej biblioteki Modernizr do nauki i opracowywania rozwiązań. Następnie, kiedy wszystko będzie
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap-datepicker.js",
                      "~/Scripts/locales/bootstrap-datepicker.pl.min.js",
                       "~/Scripts/datePickerReady.js",
                      "~/Scripts/tooltip.js",
                      "~/Scripts/jquery.dataTables.min.js",
                      "~/Scripts/dataTables.bootstrap.min.js",
                      "~/Scripts/MVCDataTableJqueryBootStrap-ui.js",
                      "~/Scripts/bootstrap-combobox.js",
                      "~/Scripts/bootstrap-modals.js",
                      "~/Scripts/respond.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                     "~/Content/dataTables.bootstrap.css",
                      "~/Content/bootstrap-datepicker3.css",
                      "~/Content/bootstrap-combobox.css",
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
