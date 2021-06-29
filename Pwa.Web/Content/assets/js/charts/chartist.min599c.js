/*!
 * Remark (http://getbootstrapadmin.com/remark)
 * Copyright 2017 amazingsurge
 * Licensed under the Themeforest Standard Licenses
 */
! function(global, factory) {
    if ("function" == typeof define && define.amd) define("/charts/chartist", ["jquery", "Site"], factory);
    else if ("undefined" != typeof exports) factory(require("jquery"), require("Site"));
    else {
        var mod = {
            exports: {}
        };
        factory(global.jQuery, global.Site), global.chartsChartist = mod.exports
    }
}(this, function(_jquery, _Site) {
    "use strict";
    var _jquery2 = babelHelpers.interopRequireDefault(_jquery),
        Site = babelHelpers.interopRequireWildcard(_Site);
    (0, _jquery2.default)(document).ready(function($) {
            Site.run()
        }),
        function() {
            var cssAnimationData = {
                    labels: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"],
                    series: [
                        [1, 2, 2.7, 0, 3, 5, 3, 4, 8, 10, 12, 7],
                        [0, 1.2, 2, 7, 2.5, 9, 5, 8, 9, 11, 14, 4],
                        [10, 9, 8, 6.5, 6.8, 6, 5.4, 5.3, 4.5, 4.4, 3, 2.8]
                    ]
                },
                cssAnimationResponsiveOptions = [
                    [{
                        axisX: {
                            labelInterpolationFnc: function(value, index) {
                                return index % 2 == 0 && value
                            }
                        }
                    }]
                ];
            new Chartist.Line("#exampleLineAnimation", cssAnimationData, null, cssAnimationResponsiveOptions)
        }(), new Chartist.Line("#exampleSimpleLine", {
            labels: ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday"],
            series: [
                [12, 9, 7, 8, 5],
                [2, 1, 3.5, 7, 3],
                [1, 3, 4, 5, 6]
            ]
        }, {
            fullWidth: !0,
            chartPadding: {
                right: 40
            }
        }),
        function() {
            var ctScatterTimes = function(n) {
                    return Array.apply(null, new Array(n))
                },
                ctScatterData = ctScatterTimes(52).map(Math.random).reduce(function(data, rnd, index) {
                    return data.labels.push(index + 1), data.series.forEach(function(series) {
                        series.push(100 * Math.random())
                    }), data
                }, {
                    labels: [],
                    series: ctScatterTimes(4).map(function() {
                        return []
                    })
                }),
                ctScatterOptions = {
                    showLine: !1,
                    axisX: {
                        labelInterpolationFnc: function(value, index) {
                            return index % 13 == 0 ? "W" + value : null
                        }
                    }
                },
                ctScatterResponsiveOptions = [
                    ["screen and (min-width: 640px)", {
                        axisX: {
                            labelInterpolationFnc: function(value, index) {
                                return index % 4 == 0 ? "W" + value : null
                            }
                        }
                    }]
                ];
            new Chartist.Line("#exampleLineScatter", ctScatterData, ctScatterOptions, ctScatterResponsiveOptions)
        }(),
        function() {
            new Chartist.Line("#exampleTooltipsLine", {
                labels: ["1", "2", "3", "4", "5", "6"],
                series: [{
                    name: "Fibonacci sequence",
                    data: [1, 2, 3, 5, 8, 13]
                }, {
                    name: "Golden section",
                    data: [1, 1.618, 2.618, 4.236, 6.854, 11.09]
                }]
            }, {
                plugins: [Chartist.plugins.tooltip()]
            });
            (0, _jquery2.default)("#exampleTooltipsLine")
        }(), new Chartist.Line("#exampleAreaLine", {
            labels: [1, 2, 3, 4, 5, 6, 7, 8],
            series: [
                [5, 9, 7, 8, 5, 3, 5, 4]
            ]
        }, {
            low: 0,
            showArea: !0
        }), new Chartist.Line("#exampleOnlyArea", {
            labels: [1, 2, 3, 4, 5, 6, 7, 8],
            series: [
                [1, 2, 3, 1, -2, 0, 1, 0],
                [-2, -1, -2, -1, -2.5, -1, -2, -1],
                [0, 0, 0, 1, 2, 2.5, 2, 1],
                [2.5, 2, 1, .5, 1, .5, -1, -2.5]
            ]
        }, {
            high: 3,
            low: -3,
            showArea: !0,
            showLine: !1,
            showPoint: !1,
            fullWidth: !0,
            axisX: {
                showLabel: !1,
                showGrid: !1
            }
        }),
        function() {
            var animationsChart = new Chartist.Line("#exampleLineAnimations", {
                    labels: ["1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12"],
                    series: [
                        [12, 9, 7, 8, 5, 4, 6, 2, 3, 3, 4, 6],
                        [4, 5, 3, 7, 3, 5, 5, 3, 4, 4, 5, 5],
                        [5, 3, 4, 5, 6, 3, 3, 4, 5, 6, 3, 4],
                        [3, 4, 5, 6, 7, 6, 4, 5, 6, 7, 6, 3]
                    ]
                }, {
                    low: 0
                }),
                seq = 0;
            animationsChart.on("created", function() {
                seq = 0
            }), animationsChart.on("draw", function(data) {
                if (seq++, "line" === data.type) data.element.animate({
                    opacity: {
                        begin: 80 * seq + 1e3,
                        dur: 500,
                        from: 0,
                        to: 1
                    }
                });
                else if ("label" === data.type && "x" === data.axis) data.element.animate({
                    y: {
                        begin: 80 * seq,
                        dur: 500,
                        from: data.y + 100,
                        to: data.y,
                        easing: "easeOutQuart"
                    }
                });
                else if ("label" === data.type && "y" === data.axis) data.element.animate({
                    x: {
                        begin: 80 * seq,
                        dur: 500,
                        from: data.x - 100,
                        to: data.x,
                        easing: "easeOutQuart"
                    }
                });
                else if ("point" === data.type) data.element.animate({
                    x1: {
                        begin: 80 * seq,
                        dur: 500,
                        from: data.x - 10,
                        to: data.x,
                        easing: "easeOutQuart"
                    },
                    x2: {
                        begin: 80 * seq,
                        dur: 500,
                        from: data.x - 10,
                        to: data.x,
                        easing: "easeOutQuart"
                    },
                    opacity: {
                        begin: 80 * seq,
                        dur: 500,
                        from: 0,
                        to: 1,
                        easing: "easeOutQuart"
                    }
                });
                else if ("grid" === data.type) {
                    var pos1Animation = {
                            begin: 80 * seq,
                            dur: 500,
                            from: data[data.axis.units.pos + "1"] - 30,
                            to: data[data.axis.units.pos + "1"],
                            easing: "easeOutQuart"
                        },
                        pos2Animation = {
                            begin: 80 * seq,
                            dur: 500,
                            from: data[data.axis.units.pos + "2"] - 100,
                            to: data[data.axis.units.pos + "2"],
                            easing: "easeOutQuart"
                        },
                        ctAnimations = {};
                    ctAnimations[data.axis.units.pos + "1"] = pos1Animation, ctAnimations[data.axis.units.pos + "2"] = pos2Animation, ctAnimations.opacity = {
                        begin: 80 * seq,
                        dur: 500,
                        from: 0,
                        to: 1,
                        easing: "easeOutQuart"
                    }, data.element.animate(ctAnimations)
                }
            }), animationsChart.on("created", function() {
                window.__exampleAnimateTimeout && (clearTimeout(window.__exampleAnimateTimeout), window.__exampleAnimateTimeout = null), window.__exampleAnimateTimeout = setTimeout(animationsChart.update.bind(animationsChart), 12e3)
            })
        }(), new Chartist.Line("#examplePathAnimation", {
            labels: ["Mon", "Tue", "Wed", "Thu", "Fri", "Sat"],
            series: [
                [1, 5, 2, 5, 4, 3],
                [2, 3, 4, 8, 1, 2],
                [5, 4, 3, 2, 1, .5]
            ]
        }, {
            low: 0,
            showArea: !0,
            showPoint: !1,
            fullWidth: !0
        }).on("draw", function(data) {
            "line" !== data.type && "area" !== data.type || data.element.animate({
                d: {
                    begin: 2e3 * data.index,
                    dur: 2e3,
                    from: data.path.clone().scale(1, 0).translate(0, data.chartRect.height()).stringify(),
                    to: data.path.clone().stringify(),
                    easing: Chartist.Svg.Easing.easeOutQuint
                }
            })
        }), new Chartist.Line("#exampleSmoothingLine", {
            labels: [1, 2, 3, 4, 5],
            series: [
                [1, 5, 10, 0, 1],
                [10, 15, 0, 1, 2]
            ]
        }, {
            lineSmooth: Chartist.Interpolation.simple({
                divisor: 2
            }),
            fullWidth: !0,
            chartPadding: {
                right: 20
            },
            low: 0
        }),
        function() {
            var biPolarData = {
                    labels: ["W1", "W2", "W3", "W4", "W5", "W6", "W7", "W8", "W9", "W10"],
                    series: [
                        [1, 2, 4, 8, 6, -2, -1, -4, -6, -2]
                    ]
                },
                biPolarOptions = {
                    high: 10,
                    low: -10,
                    axisX: {
                        labelInterpolationFnc: function(value, index) {
                            return index % 2 == 0 ? value : null
                        }
                    }
                };
            new Chartist.Bar("#exampleBiPolarBar", biPolarData, biPolarOptions)
        }(),
        function() {
            var overlappingData = {
                    labels: ["Jan", "Feb", "Mar", "Apr", "Mai", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"],
                    series: [
                        [5, 4, 3, 7, 5, 10, 3, 4, 8, 10, 6, 8],
                        [3, 2, 9, 5, 4, 6, 4, 6, 7, 8, 7, 4]
                    ]
                },
                overlappingOptions = {
                    seriesBarDistance: 10
                },
                overlappingResponsiveOptions = [
                    ["screen and (max-width: 640px)", {
                        seriesBarDistance: 5,
                        axisX: {
                            labelInterpolationFnc: function(value) {
                                return value[0]
                            }
                        }
                    }]
                ];
            new Chartist.Bar("#exampleOverlappingBar", overlappingData, overlappingOptions, overlappingResponsiveOptions)
        }(), new Chartist.Bar("#examplePeakCirclesBar", {
            labels: ["W1", "W2", "W3", "W4", "W5", "W6", "W7", "W8", "W9", "W10"],
            series: [
                [1, 2, 4, 8, 6, -2, -1, -4, -6, -2]
            ]
        }, {
            high: 10,
            low: -10,
            axisX: {
                labelInterpolationFnc: function(value, index) {
                    return index % 2 == 0 ? value : null
                }
            }
        }).on("draw", function(data) {
            "bar" === data.type && data.group.append(new Chartist.Svg("circle", {
                cx: data.x2,
                cy: data.y2,
                r: 2 * Math.abs(Chartist.getMultiValue(data.value)) + 5
            }, "ct-slice-pie"))
        }), new Chartist.Bar("#exampleMultiLabelsBar", {
            labels: ["First quarter of the year", "Second quarter of the year", "Third quarter of the year", "Fourth quarter of the year"],
            series: [
                [6e4, 4e4, 8e4, 7e4],
                [4e4, 3e4, 7e4, 65e3],
                [8e3, 3e3, 1e4, 6e3]
            ]
        }, {
            seriesBarDistance: 10,
            axisX: {
                offset: 60
            },
            axisY: {
                offset: 80,
                labelInterpolationFnc: function(value) {
                    return value + " CHF"
                },
                scaleMinSpace: 15
            }
        }), new Chartist.Bar("#exampleStackedBar", {
            labels: ["Q1", "Q2", "Q3", "Q4"],
            series: [
                [8e5, 12e5, 14e5, 13e5],
                [2e5, 4e5, 5e5, 3e5],
                [1e5, 2e5, 4e5, 6e5]
            ]
        }, {
            stackBars: !0,
            axisY: {
                labelInterpolationFnc: function(value) {
                    return value / 1e3 + "k"
                }
            }
        }).on("draw", function(data) {
            "bar" === data.type && data.element.attr({
                style: "stroke-width: 30px"
            })
        }), new Chartist.Bar("#exampleHorizontalBar", {
            labels: ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"],
            series: [
                [5, 4, 3, 7, 5, 10, 3],
                [3, 2, 9, 5, 4, 6, 4]
            ]
        }, {
            seriesBarDistance: 10,
            reverseData: !0,
            horizontalBars: !0,
            axisY: {
                offset: 70
            }
        }), new Chartist.Bar("#exampleResponsiveBar", {
            labels: ["Quarter 1", "Quarter 2", "Quarter 3", "Quarter 4"],
            series: [
                [5, 4, 3, 7],
                [3, 2, 9, 5],
                [1, 5, 8, 4],
                [2, 3, 4, 6],
                [4, 1, 2, 1]
            ]
        }, {
            stackBars: !0,
            axisX: {
                labelInterpolationFnc: function(value) {
                    return value.split(/\s+/).map(function(word) {
                        return word[0]
                    }).join("")
                }
            },
            axisY: {
                offset: 20
            }
        }, [
            ["screen and (min-width: 480px)", {
                reverseData: !0,
                horizontalBars: !0,
                axisX: {
                    labelInterpolationFnc: Chartist.noop
                },
                axisY: {
                    offset: 60
                }
            }],
            ["screen and (min-width: 992px)", {
                stackBars: !1,
                seriesBarDistance: 10
            }],
            ["screen and (min-width: 1200px)", {
                reverseData: !1,
                horizontalBars: !1,
                seriesBarDistance: 15
            }]
        ]),
        function() {
            var simplePiedata = {
                    series: [5, 3, 4]
                },
                simplePieSum = function(a, b) {
                    return a + b
                };
            new Chartist.Pie("#exampleSimplePie", simplePiedata, {
                labelInterpolationFnc: function(value) {
                    return Math.round(value / simplePiedata.series.reduce(simplePieSum) * 100) + "%"
                }
            })
        }(),
        function() {
            var labelsPieData = {
                    labels: ["Bananas", "Apples", "Grapes"],
                    series: [20, 15, 40]
                },
                labelsPieOptions = {
                    labelInterpolationFnc: function(value) {
                        return value[0]
                    }
                },
                labelsPieResponsiveOptions = [
                    ["screen and (min-width: 640px)", {
                        chartPadding: 30,
                        labelOffset: 100,
                        labelDirection: "explode",
                        labelInterpolationFnc: function(value) {
                            return value
                        }
                    }],
                    ["screen and (min-width: 1024px)", {
                        labelOffset: 80,
                        chartPadding: 20
                    }]
                ];
            new Chartist.Pie("#exampleLabelsPie", labelsPieData, labelsPieOptions, labelsPieResponsiveOptions)
        }(), new Chartist.Pie("#exampleGaugePie", {
            series: [20, 10, 30, 40]
        }, {
            donut: !0,
            donutWidth: 60,
            startAngle: 270,
            total: 200,
            showLabel: !1
        })
});