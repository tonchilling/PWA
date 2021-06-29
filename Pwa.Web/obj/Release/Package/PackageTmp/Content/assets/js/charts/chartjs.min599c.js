/*!
 * Remark (http://getbootstrapadmin.com/remark)
 * Copyright 2017 amazingsurge
 * Licensed under the Themeforest Standard Licenses
 */
! function(global, factory) {
    if ("function" == typeof define && define.amd) define("/charts/chartjs", ["jquery", "Site"], factory);
    else if ("undefined" != typeof exports) factory(require("jquery"), require("Site"));
    else {
        var mod = {
            exports: {}
        };
        factory(global.jQuery, global.Site), global.chartsChartjs = mod.exports
    }
}(this, function(_jquery, _Site) {
    "use strict";
    var _jquery2 = babelHelpers.interopRequireDefault(_jquery),
        Site = babelHelpers.interopRequireWildcard(_Site);
    (0, _jquery2.default)(document).ready(function($) {
            Site.run()
        }), Chart.defaults.global.responsive = !0,
        
        function() {
            var pieData = {
                labels: ["มอบหมาย", "ดำเนินการ", "ปิดงาน"],
                datasets: [{
                    data: [10, 5, 16],
                    backgroundColor: ["#ffc107", "#007bff", "#28a745"],
                    hoverBackgroundColor:  ["#ffc107", "#007bff", "#28a745"]
                }]
            };
      /*      new Chart(document.getElementById("exampleChartjsPie").getContext("2d"), {
                type: "pie",
                data: pieData,
                options: {
                    responsive: !0
                }
            })*/
        }()
       
});