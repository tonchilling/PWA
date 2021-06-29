/*!
 * Remark (http://getbootstrapadmin.com/remark)
 * Copyright 2017 amazingsurge
 * Licensed under the Themeforest Standard Licenses
 */
! function (global, factory) {
    if ("function" == typeof define && define.amd) define("/Plugin/action-btn", ["exports", "jquery", "Plugin"], factory);
    else if ("undefined" != typeof exports) factory(exports, require("jquery"), require("Plugin"));
    else {
        var mod = {
            exports: {}
        };
        factory(mod.exports, global.jQuery, global.Plugin), global.PluginActionBtn = mod.exports
    }
}(this, function (exports, _jquery, _Plugin2) {
    "use strict";
    Object.defineProperty(exports, "__esModule", {
        value: !0
    });
    var _jquery2 = babelHelpers.interopRequireDefault(_jquery),
        _Plugin3 = babelHelpers.interopRequireDefault(_Plugin2),
        defaults = {
            trigger: "click",
            toggleSelector: ".site-action-toggle",
            listSelector: ".site-action-buttons",
            activeClass: "active",
            onShow: function () { },
            onHide: function () { }
        },
        actionBtn = function () {
            function actionBtn(element, options) {
                babelHelpers.classCallCheck(this, actionBtn), this.element = element, this.$element = (0, _jquery2.default)(element), this.options = _jquery2.default.extend({}, defaults, options, this.$element.data()), this.init()
            }
            return babelHelpers.createClass(actionBtn, [{
                key: "init",
                value: function () {
                    this.showed = !1, this.$toggle = this.$element.find(this.options.toggleSelector), this.$list = this.$element.find(this.options.listSelector);
                    var self = this;
                    "hover" === this.options.trigger ? (this.$element.on("mouseenter", this.options.toggleSelector, function () {
                        self.showed || self.show()
                    }), this.$element.on("mouseleave", this.options.toggleSelector, function () {
                        self.showed && self.hide()
                    })) : this.$element.on("click", this.options.toggleSelector, function () {
                        self.showed ? self.hide() : self.show()
                    })
                }
            }, {
                key: "show",
                value: function () {
                    this.showed || (this.$element.addClass(this.options.activeClass), this.showed = !0, this.options.onShow.call(this))
                }
            }, {
                key: "hide",
                value: function () {
                    this.showed && (this.$element.removeClass(this.options.activeClass), this.showed = !1, this.options.onHide.call(this))
                }
            }], [{
                key: "_jQueryInterface",
                value: function (options) {
                    for (var _len = arguments.length, args = Array(_len > 1 ? _len - 1 : 0), _key = 1; _key < _len; _key++) args[_key - 1] = arguments[_key];
                    if ("string" != typeof options) return this.each(function () {
                        _jquery2.default.data(this, "actionBtn") || _jquery2.default.data(this, "actionBtn", new actionBtn(this, options))
                    });
                    var method = options;
                    if (/^\_/.test(method)) return !1;
                    if (!/^(get)$/.test(method)) return this.each(function () {
                        var api = _jquery2.default.data(this, "actionBtn");
                        api && "function" == typeof api[method] && api[method].apply(api, args)
                    });
                    var api = this.first().data("actionBtn");
                    return api && "function" == typeof api[method] ? api[method].apply(api, args) : void 0
                }
            }]), actionBtn
        }();
    _jquery2.default.fn.actionBtn = actionBtn._jQueryInterface, _jquery2.default.fn.actionBtn.constructor = actionBtn, _jquery2.default.fn.actionBtn.noConflict = function () {
        return _jquery2.default.fn.actionBtn = window.JQUERY_NO_CONFLICT, actionBtn._jQueryInterface
    };
    var ActionBtn = function (_Plugin) {
        function ActionBtn() {
            return babelHelpers.classCallCheck(this, ActionBtn), babelHelpers.possibleConstructorReturn(this, (ActionBtn.__proto__ || Object.getPrototypeOf(ActionBtn)).apply(this, arguments))
        }
        return babelHelpers.inherits(ActionBtn, _Plugin), babelHelpers.createClass(ActionBtn, [{
            key: "getName",
            value: function () {
                return "actionBtn"
            }
        }], [{
            key: "getDefaults",
            value: function () {
                return defaults
            }
        }]), ActionBtn
    }(_Plugin3.default);
    _Plugin3.default.register("actionBtn", ActionBtn), exports.default = ActionBtn
});