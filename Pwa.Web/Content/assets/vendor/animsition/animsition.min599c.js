/*!
 * animsition v4.0.2
 * A simple and easy jQuery plugin for CSS animated page transitions.
 * http://blivesta.github.io/animsition
 * License : MIT
 * Author : blivesta (http://blivesta.com/)
 */
! function (t) {
    "use strict";
    "function" == typeof define && define.amd ? define(["jquery"], t) : "object" == typeof exports ? module.exports = t(require("jquery")) : t(jQuery)
}(function (t) {
    "use strict";
    var n = "animsition",
        i = !1;
    t(window).on("load", function () {
        i = !0
    });
    var a = {
        init: function (o) {
            o = t.extend({
                inClass: "fade-in",
                outClass: "fade-out",
                inDuration: 1500,
                outDuration: 800,
                linkElement: ".animsition-link",
                loading: !0,
                loadingParentElement: "body",
                loadingClass: "animsition-loading",
                loadingInner: "",
                timeout: !1,
                timeoutCountdown: 5e3,
                onLoadEvent: !0,
                browser: ["animation-duration", "-webkit-animation-duration"],
                overlay: !1,
                overlayClass: "animsition-overlay-slide",
                overlayParentElement: "body",
                transition: function (t) {
                    window.location.href = t
                }
            }, o), a.settings = {
                timer: !1,
                data: {
                    inClass: "animsition-in-class",
                    inDuration: "animsition-in-duration",
                    outClass: "animsition-out-class",
                    outDuration: "animsition-out-duration",
                    overlay: "animsition-overlay"
                },
                events: {
                    inStart: "animsition.inStart",
                    inEnd: "animsition.inEnd",
                    outStart: "animsition.outStart",
                    outEnd: "animsition.outEnd"
                }
            };
            var e = a.supportCheck.call(this, o);
            if (!e && o.browser.length > 0 && (!e || !this.length)) return "console" in window || (window.console = {}, window.console.log = function (t) {
                return t
            }), this.length || console.log("Animsition: Element does not exist on page."), e || console.log("Animsition: Does not support this browser."), a.destroy.call(this);
            var s = a.optionCheck.call(this, o);
            return s && t("." + o.overlayClass).length <= 0 && a.addOverlay.call(this, o), o.loading && t("." + o.loadingClass).length <= 0 && a.addLoading.call(this, o), this.each(function () {
                var e = this,
                    s = t(this),
                    r = t(window),
                    l = t(document),
                    d = s.data(n);
                d || (o = t.extend({}, o), s.data(n, {
                    options: o
                }), o.timeout && a.addTimer.call(e), o.onLoadEvent && (i ? (a.settings.timer && clearTimeout(a.settings.timer), a["in"].call(e)) : r.on("load", function (t) {
                    a.settings.timer && clearTimeout(a.settings.timer), a["in"].call(e)
                })), r.on("pageshow." + n, function (t) {
                    t.originalEvent.persisted && a["in"].call(e)
                }), r.on("unload." + n, function () { }), l.on("click." + n, o.linkElement, function (n) {
                    n.preventDefault();
                    var i = t(this),
                        o = i.attr("href");
                    2 === n.which || n.metaKey || n.shiftKey || navigator.platform.toUpperCase().indexOf("WIN") !== -1 && n.ctrlKey ? window.open(o, "_blank") : a.out.call(e, i, o)
                }))
            })
        },
        addOverlay: function (n) {
            t(n.overlayParentElement).prepend('<div class="' + n.overlayClass + '"></div>')
        },
        addLoading: function (n) {
            t(n.loadingParentElement).append('<div class="' + n.loadingClass + '">' + n.loadingInner + "</div>")
        },
        removeLoading: function () {
            var i = t(this),
                a = i.data(n).options,
                o = t(a.loadingParentElement).children("." + a.loadingClass);
            o.fadeOut().remove()
        },
        addTimer: function () {
            var i = this,
                o = t(this),
                e = o.data(n).options;
            a.settings.timer = setTimeout(function () {
                a["in"].call(i), t(window).off("load." + n)
            }, e.timeoutCountdown)
        },
        supportCheck: function (n) {
            var i = t(this),
                a = n.browser,
                o = a.length,
                e = !1;
            0 === o && (e = !0);
            for (var s = 0; s < o; s++)
                if ("string" == typeof i.css(a[s])) {
                    e = !0;
                    break
                }
            return e
        },
        optionCheck: function (n) {
            var i, o = t(this);
            return i = !(!n.overlay && !o.data(a.settings.data.overlay))
        },
        animationCheck: function (i, a, o) {
            var e = t(this),
                s = e.data(n).options,
                r = typeof i,
                l = !a && "number" === r,
                d = a && "string" === r && i.length > 0;
            return l || d ? i = i : a && o ? i = s.inClass : !a && o ? i = s.inDuration : a && !o ? i = s.outClass : a || o || (i = s.outDuration), i
        },
        "in": function () {
            var i = this,
                o = t(this),
                e = o.data(n).options,
                s = o.data(a.settings.data.inDuration),
                r = o.data(a.settings.data.inClass),
                l = a.animationCheck.call(i, s, !1, !0),
                d = a.animationCheck.call(i, r, !0, !0),
                u = a.optionCheck.call(i, e),
                c = o.data(n).outClass;
            e.loading && a.removeLoading.call(i), c && o.removeClass(c), u ? a.inOverlay.call(i, d, l) : a.inDefault.call(i, d, l)
        },
        inDefault: function (n, i) {
            var o = t(this);
            o.css({
                "animation-duration": i + "ms"
            }).addClass(n).trigger(a.settings.events.inStart).animateCallback(function () {
                o.removeClass(n).css({
                    opacity: 1
                }).trigger(a.settings.events.inEnd)
            })
        },
        inOverlay: function (i, o) {
            var e = t(this),
                s = e.data(n).options;
            e.css({
                opacity: 1
            }).trigger(a.settings.events.inStart), t(s.overlayParentElement).children("." + s.overlayClass).css({
                "animation-duration": o + "ms"
            }).addClass(i).animateCallback(function () {
                e.trigger(a.settings.events.inEnd)
            })
        },
        out: function (i, o) {
            var e = this,
                s = t(this),
                r = s.data(n).options,
                l = i.data(a.settings.data.outClass),
                d = s.data(a.settings.data.outClass),
                u = i.data(a.settings.data.outDuration),
                c = s.data(a.settings.data.outDuration),
                m = l ? l : d,
                g = u ? u : c,
                f = a.animationCheck.call(e, m, !0, !1),
                v = a.animationCheck.call(e, g, !1, !1),
                h = a.optionCheck.call(e, r);
            s.data(n).outClass = f, h ? a.outOverlay.call(e, f, v, o) : a.outDefault.call(e, f, v, o)
        },
        outDefault: function (i, o, e) {
            var s = t(this),
                r = s.data(n).options;
            s.css({
                "animation-duration": o + 1 + "ms"
            }).addClass(i).trigger(a.settings.events.outStart).animateCallback(function () {
                s.trigger(a.settings.events.outEnd), r.transition(e)
            })
        },
        outOverlay: function (i, o, e) {
            var s = this,
                r = t(this),
                l = r.data(n).options,
                d = r.data(a.settings.data.inClass),
                u = a.animationCheck.call(s, d, !0, !0);
            t(l.overlayParentElement).children("." + l.overlayClass).css({
                "animation-duration": o + 1 + "ms"
            }).removeClass(u).addClass(i).trigger(a.settings.events.outStart).animateCallback(function () {
                r.trigger(a.settings.events.outEnd), l.transition(e)
            })
        },
        destroy: function () {
            return this.each(function () {
                var i = t(this);
                t(window).off("." + n), i.css({
                    opacity: 1
                }).removeData(n)
            })
        }
    };
    t.fn.animateCallback = function (n) {
        var i = "animationend webkitAnimationEnd";
        return this.each(function () {
            var a = t(this);
            a.on(i, function () {
                return a.off(i), n.call(this)
            })
        })
    }, t.fn.animsition = function (i) {
        return a[i] ? a[i].apply(this, Array.prototype.slice.call(arguments, 1)) : "object" != typeof i && i ? void t.error("Method " + i + " does not exist on jQuery." + n) : a.init.apply(this, arguments)
    }
});