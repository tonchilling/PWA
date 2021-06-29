using System.Web;
using System.Web.Optimization;

namespace Pwa.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/Pages").Include(
             "~/Content/assets/vendor/babel-external-helpers/babel-external-helpers599c.js"
    ,"~/Content/assets/vendor/jquery/jquery.min599c.js"
    ,"~/Content/assets/vendor/popper-js/umd/popper.min599c.js"
    ,"~/Content/assets/vendor/bootstrap/bootstrap.min599c.js"
    ,"~/Content/assets/vendor/animsition/animsition.min599c.js"
    ,"~/Content/assets/vendor/mousewheel/jquery.mousewheel599c.js"
    ,"~/Content/assets/vendor/asscrollbar/jquery-asScrollbar.min599c.js"
    ,"~/Content/assets/vendor/asscrollable/jquery-asScrollable.min599c.js"
    ,"~/Content/assets/vendor/ashoverscroll/jquery-asHoverScroll.min599c.js"
    ,"~/Content/assets/vendor/intro-js/intro.min599c.js"
    ,"~/Content/assets/vendor/screenfull/screenfull599c.js"
    ,"~/Content/assets/vendor/slidepanel/jquery-slidePanel.min599c.js"
    ,"~/Content/assets/vendor/chart-js/Chart.min599c.js"
    ,"~/Content/assets/vendor/slidepanel/jquery-slidePanel.min599c.js"
    ,"~/Content/assets/vendor/aspaginator/jquery-asPaginator.min599c.js"
    ,"~/Content/assets/vendor/jquery-placeholder/jquery.placeholder599c.js"
    ,"~/Content/assets/vendor/bootbox/bootbox.min599c.js"
    ,"~/Content/assets/js/Component.min599c.js"
    ,"~/Content/assets/js/Plugin.min599c.js"
    ,"~/Content/assets/js/Base.min599c.js"
    ,"~/Content/assets/js/Config.min599c.js"
    ,"~/Content/assets/js/Section/Menubar.min599c.js"
    ,"~/Content/assets/js/Section/GridMenu.min599c.js"
    ,"~/Content/assets/js/Section/Sidebar.min599c.js"
    ,"~/Content/assets/js/Section/PageAside.min599c.js"
    ,"~/Content/assets/js/Plugin/menu.min599c.js"
    ,"~/Content/assets/js/config/colors.min599c.js"
    ,"~/Content/assets/js/config/tour.min599c.js"
    ,"~/Content/assets/js/SitE.min599c.js"
     ,"~/Content/assets/js/Plugin/asscrollable.min599c.js"
      ,"~/Content/assets/js/Plugin/slidepanel.min599c.js"
        ,"~/Content/assets/js/Plugin/sticky-header.min599c.js"
         ,"~/Content/assets/js/Plugin/action-btn.min599c.js"
          ,"~/Content/assets/js/Plugin/asselectable.min599c.js"
           ,"~/Content/assets/js/Plugin/editlist.min599c.js"
            ,"~/Content/assets/js/Plugin/aspaginator.min599c.js"
             ,"~/Content/assets/js/Plugin/animate-list.min599c.js"
              ,"~/Content/assets/js/Plugin/jquery-placeholder.min599c.js"
               ,"~/Content/assets/js/Plugin/material.min599c.js"
                ,"~/Content/assets/js/Plugin/selectable.min599c.js"
                 ,"~/Content/assets/js/Plugin/bootbox.min599c.js"
                  ,"~/Content/assets/js/BaseApp.min599c.js"
                   ,"~/Content/assets/js/charts/chartjs.min599c.js"));

            bundles.Add(new ScriptBundle("~/bundles/assets").Include(
                     "~/Content/assets/vendor/babel-external-helpers/babel-external-helpers599c.js"
                ,"~/Content/assets/vendor/breakpoints/breakpoints.min599c.js"
    ,"~/Content/assets/vendor/jquery/jquery.min599c.js"
    ,"~/Content/assets/vendor/popper-js/umd/popper.min599c.js"
    ,"~/Content/assets/vendor/bootstrap/bootstrap.min599c.js"
    ,"~/Content/assets/vendor/animsition/animsition.min599c.js"
    ,"~/Content/assets/vendor/mousewheel/jquery.mousewheel599c.js"
         , "~/Content/assets/vendor/asscrollbar/jquery-asScrollbar.min599c.js"
     , "~/Content/assets/vendor/asscrollable/jquery-asScrollable.min599c.js" 
    , "~/Content/assets/vendor/ashoverscroll/jquery-asHoverScroll.min599c.js"
    ,"~/Content/assets/vendor/intro-js/intro.min599c.js"
    ,"~/Content/assets/vendor/screenfull/screenfull599c.js"
    ,"~/Content/assets/vendor/slidepanel/jquery-slidePanel.min599c.js"
    ,"~/Content/assets/vendor/jquery-placeholder/jquery.placeholder599c.js"
    ,"~/Content/assets/js/Component.min599c.js"
    ,"~/Content/assets/js/Plugin.min599c.js"
    ,"~/Content/assets/js/Base.min599c.js"
    ,"~/Content/assets/js/Config.min599c.js"
    ,"~/Content/assets/js/Section/Menubar.min599c.js"
    ,"~/Content/assets/js/Section/GridMenu.min599c.js"
    ,"~/Content/assets/js/Section/Sidebar.min599c.js"
    ,"~/Content/assets/js/Section/PageAside.min599c.js"
    ,"~/Content/assets/js/Plugin/menu.min599c.js"
    ,"~/Content/assets/js/config/colors.min599c.js"
    ,"~/Content/assets/js/config/tour.min599c.js"
    ,"~/Content/assets/js/SitE.min599c.js"
    ,"~/Content/assets/js/Plugin/asscrollable.min599c.js"
    ,"~/Content/assets/js/Plugin/slidepanel.min599c.js"
    ,"~/Content/assets/js/Plugin/material.min599c.js"));
            /*     bundles.Add(new ScriptBundle("~/bundles/assets")
              .IncludeDirectory("~/Content/assets/vendor", "*.js", true)
              .IncludeDirectory("~/Content/assets/js", "*.js", true));

               
                  */






            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css"
                      ,"~/Content/site.css"
                     , "~/Content/assets/css/bootstrap.miN599C.css"
    , "~/Content/assets/css/bootstrap-extend.min599c.css"
    , "~/Content/assets/css/sitE.min599c.css"
    , "~/Content/assets/vendor/bootstrap-select/bootstrap-select.min599c.css"
    , "~/Content/assets/css/skintools.min599c.css"
    , "~/Content/assets/vendor/animsition/animsition.min599c.css"
      , "~/Content/assets/examples/css/dashboard/team.min599c.css"
    , "~/Content/assets/vendor/asscrollable/asScrollable.min599c.css"
    , "~/Content/assets/vendor/switchery/switchery.min599c.css"
    , "~/Content/assets/vendor/intro-js/introjs.min599c.css"
    , "~/Content/assets/vendor/slidepanel/slidePanel.min599c.css"
    , "~/Content/assets/vendor/flag-icon-css/flag-icon.min599c.css"
    , "~/Content/assets/css/charts/chartjs.min599c.css"
    , "~/Content/assets/css/pages/incident_index.min599c.css"
    , "~/Content/assets/fonts/web-icons/web-icons.min599c.css"
    , "~/Content/assets/fonts/brand-icons/brand-icons.min599c.css"));


            bundles.Add(new StyleBundle("~/Content/assets")
                  .IncludeDirectory("~/Content/assets/css", "*.css", true)
                   .IncludeDirectory("~/Content/assets/vendor", "*.css", true)
                    .IncludeDirectory("~/Content/assets/fonts", "*.css", true));

            bundles.Add(new StyleBundle("~/Content/Pages").Include(
           "~/Content/assets/css/BooTstrap.min599C.css"
    ,"~/Content/assets/css/bootstrap-extend.min599c.css"
        , "~/fonts/font-awesome.min599c.css"
    , "~/Content/assets/css/sitE.min599c.css" 
    , "~/Content/assets/vendor/bootstrap-select/bootstrap-select.min599c.css"
    , "~/Content/assets/css/skintools.min599c.css"
    ,"~/Content/assets/vendor/animsition/animsition.min599c.css"
      , "~/Content/assets/examples/css/dashboard/team.min599c.css"
    , "~/Content/assets/vendor/asscrollable/asScrollable.min599c.css"
    ,"~/Content/assets/vendor/switchery/switchery.min599c.css"
    ,"~/Content/assets/vendor/intro-js/introjs.min599c.css"
    ,"~/Content/assets/vendor/slidepanel/slidePanel.min599c.css"
    ,"~/Content/assets/vendor/flag-icon-css/flag-icon.min599c.css"
    ,"~/Content/assets/css/charts/chartjs.min599c.css"
    ,"~/Content/assets/css/pages/incident_index.min599c.css"
    ,"~/Content/assets/fonts/web-icons/web-icons.min599c.css"
    ,"~/Content/assets/fonts/brand-icons/brand-icons.min599c.css"));

            /*
             * 
             *  <!-- Stylesheets -->
   ,"~/Content/assets/css/bootstrap.min599c.css"
   ,"~/Content/assets/css/bootstrap-extend.min599c.css"
   ,"~/Content/assets/css/site.min599c.css2"
   ,"~/Content/assets/css/skintools.min599c.css"
   ,"~/Content/assets/vendor/animsition/animsition.min599c.css"
   ,"~/Content/assets/vendor/asscrollable/asScrollable.min599c.css"
   ,"~/Content/assets/vendor/switchery/switchery.min599c.css"
   ,"~/Content/assets/vendor/intro-js/introjs.min599c.css"
   ,"~/Content/assets/vendor/slidepanel/slidePanel.min599c.css"
   ,"~/Content/assets/vendor/flag-icon-css/flag-icon.min599c.css"
       ,"~/Content/assets/fonts/web-icons/web-icons.min599c.css"
   ,"~/Content/assets/fonts/brand-icons/brand-icons.min599c.css"
   ,"~/Content/assets/css/charts/chartjs.min599c.css"
   ,"~/Content/assets/css/pages/incident_index.min599c.css"

    <!-- Fonts -->




    <!-- Map -->
   ,"https://unpkg.com/leaflet@1.6.0/dist/leaflet.css" />
    <link rel='stylesheet' href='https://cdnjs.cloudflare.com/ajax/libs/leaflet.draw/0.4.9/leaflet.draw.css'>
        <script src="~/Content/assets/js/Plugin/skintools.min599c.js"</script>

   ,"https://unpkg.com/leaflet-search@2.3.7/dist/leaflet-search.src.css" />
             <!-- Stylesheets-->
   ,"~/Content/assets/css/bootstrap.min599c.css"
   ,"~/Content/assets/css/bootstrap-extend.min599c.css"
   ,"~/Content/assets/css/site.min599c.css"
     
    <!-- Skin tools (demo site only) -->
   ,"~/Content/assets/css/skintools.min599c.css"
    <script src="~/Content/assets/js/Plugin/skintools.min599c.js"</script>

    <!-- Plugins -->
   ,"~/Content/assets/vendor/animsition/animsition.min599c.css"
   ,"~/Content/assets/vendor/asscrollable/asScrollable.min599c.css"
   ,"~/Content/assets/vendor/switchery/switchery.min599c.css"
   ,"~/Content/assets/vendor/intro-js/introjs.min599c.css"
   ,"~/Content/assets/vendor/slidepanel/slidePanel.min599c.css"
   ,"~/Content/assets/vendor/flag-icon-css/flag-icon.min599c.css"

    <!-- Page -->
   ,"~/Content/assets/css/pages/login-v3.min599c.css"

    <!-- Fonts -->
   ,"~/Content/assets/fonts/web-icons/web-icons.min599c.css"
   ,"~/Content/assets/fonts/brand-icons/brand-icons.min599c.css"
    <link rel='stylesheet' href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,300italic"
 
             **/


        }
    }
}
