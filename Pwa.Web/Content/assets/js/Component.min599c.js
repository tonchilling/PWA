/*!
 * Remark (http://getbootstrapadmin.com/remark)
 * Copyright 2017 amazingsurge
 * Licensed under the Themeforest Standard Licenses
 */
! function (global, factory) {
    if ("function" == typeof define && define.amd) define("/Component", ["exports", "jquery"], factory);
    else if ("undefined" != typeof exports) factory(exports, require("jquery"));
    else {
        var mod = {
            exports: {}
        };
        factory(mod.exports, global.jQuery), global.Component = mod.exports
    }
}(this, function (exports, _jquery) {
    "use strict";
    Object.defineProperty(exports, "__esModule", {
        value: !0
    });
    var _jquery2 = babelHelpers.interopRequireDefault(_jquery);
    void 0 === Object.assign && (Object.assign = _jquery2.default.extend);
    var _class = function () {
        function _class() {
            var options = arguments.length > 0 && void 0 !== arguments[0] ? arguments[0] : {};
            babelHelpers.classCallCheck(this, _class), this.$el = options.$el ? options.$el : (0, _jquery2.default)(document), this.el = this.$el[0], delete options.$el, this.options = options, this.isProcessed = !1
        }
        return babelHelpers.createClass(_class, [{
            key: "initialize",
            value: function () { }
        }, {
            key: "process",
            value: function () { }
        }, {
            key: "run",
            value: function () {
                this.isProcessed || (this.initialize(), this.process()), this.isProcessed = !0
            }
        }, {
            key: "triggerResize",
            value: function () {
                if (document.createEvent) {
                    var ev = document.createEvent("Event");
                    ev.initEvent("resize", !0, !0), window.dispatchEvent(ev)
                } else {
                    element = document.documentElement;
                    var event = document.createEventObject();
                    element.fireEvent("onresize", event)
                }
            }
        }]), _class
    }();
    exports.default = _class
});