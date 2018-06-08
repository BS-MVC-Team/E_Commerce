using System.Web;
using System.Web.Optimization;

namespace Commerce
{
    public class BundleConfig
    {
        // 如需統合的詳細資訊，請瀏覽 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用開發版本的 Modernizr 進行開發並學習。然後，當您
            // 準備好可進行生產時，請使用 https://modernizr.com 的建置工具，只挑選您需要的測試。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/vendor/css").Include(
                "~/vendor/bootstrap/css/bootstrap.min.css",
                "~/vendor/animate/animate.css",
                "~/vendor/css-hamburgers/hamburgers.min.css",
                "~/vendor/animsition/css/animsition.min.css",
                "~/vendor/select2/select2.min.css",
                "~/vendor/daterangepicker/daterangepicker.css",
                "~/vendor/slick/slick.css",
                "~/vendor/MagnificPopup/magnific-popup.css",
                "~/vendor/perfect-scrollbar/perfect-scrollbar.css"
                ));
            bundles.Add(new StyleBundle("~/fonts/css").Include(
                "~/fonts/font-awesome-4.7.0/css/font-awesome.min.css",
                "~/fonts/iconic/css/material-design-iconic-font.min.css",
                "~/fonts/linearicons-v1.0.0/icon-font.min.css"
                ));
            bundles.Add(new StyleBundle("~/images/icons").Include(
                "~/images/icons/favicon.png"));
            bundles.Add(new StyleBundle("~/css/css").Include(
                "~/css/util.css",
                "~/css/main.css"
                ));
            bundles.Add(new StyleBundle("~/vendor/js1").Include(
                "~/vendor/daterangepicker/moment.min.js",
                "~/vendor/daterangepicker/daterangepicker.js",
                "~/vendor/jquery/jquery-3.2.1.min.js",
                "~/vendor/animsition/js/animsition.min.js",
                "~/vendor/bootstrap/js/popper.js",
                "~/vendor/slick/slick.min.js",
                "~/vendor/parallax100/parallax100.js",
                "~/vendor/MagnificPopup/jquery.magnific-popup.min.js",
                "~/vendor/isotope/isotope.pkgd.min.js",
                "~/vendor/sweetalert/sweetalert.min.js",
                "~/vendor/perfect-scrollbar/perfect-scrollbar.min.js"
                ,
                "~/vendor/bootstrap/js/bootstrap.min.js",
                "~/vendor/select2/select2.min.js"));
            bundles.Add(new StyleBundle("~/js/js").Include(
                "~/js/slick-custom.js",
                "~/js/main.js"));

        }
    }
}
