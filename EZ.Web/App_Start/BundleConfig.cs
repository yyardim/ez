using System.Web;
using System.Web.Optimization;

namespace EZ.Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Force optimization to be on or off, regarding of web.config setting
            //BundleTable.EnableOptimizations = false;
            bundles.UseCdn = false;

            // .debug.js, -vsdoc.js, and .intellisense.js files are in BundleTable.Bundles.IgnoreList by default.
            // Clear out the list and add back the ones we want to ignore.
            bundles.IgnoreList.Clear();
            bundles.IgnoreList.Ignore("*-vsdoc.js");
            bundles.IgnoreList.Ignore("*intellisense.js");

            // Modernizr goes separate since it loads first
            bundles.Add(new ScriptBundle("~/bundles/modernizr")
                .Include("~/Scripts/lib/modernizr-{version}.js"));

            // jQuery
            bundles.Add(new ScriptBundle("~/bundles/jquery",
                "//ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js")
                .Include("~/Scripts/lib/jquery-{version}.js"));

            // 3rd Party Javascript files
            bundles.Add(new ScriptBundle("~/bundles/jsextlibs")
                //.IncludeDirectory("~/Scripts/lib", "*.js", searchSubdirectories: false));
                .Include(
                    "~/Scripts/lib/json2.js", // IE7 needs this

                    // jQuery plugins
                    "~/Scripts/lib/jquery-ui-{version}.js",
                    "~/Scripts/lib/jquery.unobtrusive*",
                    "~/Scripts/lib/jquery.validate*",
                    "~/Scripts/lib/jquery.mockjson.js",
                    "~/Scripts/lib/TrafficCop.js",
                    "~/Scripts/lib/infuser.js", // depends on TrafficCop

                    // Knockout and its plugins
                    "~/Scripts/lib/knockout-{version}.js",
                    "~/Scripts/lib/knockout.activity.js",
                    "~/Scripts/lib/knockout.asyncCommand.js",
                    "~/Scripts/lib/knockout.dirtyFlag.js",
                    "~/Scripts/lib/knockout.validation.js",
                    "~/Scripts/lib/koExternalTemplateEngine.js",
                    
                    //Breeze plugins
                    "~/Scripts/lib/q.js",
                    "~/Scripts/lib/breeze.debug.js",
                    "~/Scripts/lib/breeze.intellisense.js",

                    // Other 3rd party libraries
                    "~/Scripts/lib/underscore.js",
                    "~/Scripts/lib/moment.js",
                    "~/Scripts/lib/sammy-{version}.js",
                    "~/Scripts/lib/amplify.*",
                    "~/Scripts/lib/toastr.js",
                    "~/Scripts/lib/bootstrap.min.js"
                    ));

            bundles.Add(new ScriptBundle("~/bundles/jsmocks")
                .IncludeDirectory("~/Scripts/app/mock", "*.js", searchSubdirectories: false));

            // All application JS files (except mocks)
            bundles.Add(new ScriptBundle("~/bundles/jsapplibs")
                .IncludeDirectory("~/Scripts/app/", "*.js", searchSubdirectories: false));          //DEV
                //.IncludeDirectory("~/Scripts/app/", "*.min.js", searchSubdirectories: false));    //PRODUCTION

            // 3rd Party CSS files
            bundles.Add(new StyleBundle("~/Content/Template/css").Include(
                "~/Content/Template/css/bootstrap-theme.min.css",
                "~/Content/Template/css/bootstrap.min.css",
                "~/Content/Site.css"));
            
            // Custom LESS files
            bundles.Add(new Bundle("~/Content", new LessTransform(), new CssMinify())
                .Include("~/Content/Site.less"));
        }
    }
}