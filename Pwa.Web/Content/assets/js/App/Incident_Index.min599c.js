/*!
 * Remark (http://getbootstrapadmin.com/remark)
 * Copyright 2017 amazingsurge
 * Licensed under the Themeforest Standard Licenses
 */
! function(global, factory) {
    if ("function" == typeof define && define.amd) define("/App/Incident_Index", ["exports", "BaseApp"], factory);
    else if ("undefined" != typeof exports) factory(exports, require("BaseApp"));
    else {
        var mod = {
            exports: {}
        };
        factory(mod.exports, global.BaseApp), global.AppPage = mod.exports
    }
}(this, function(exports, _BaseApp2) {
    "use strict";

    function getInstance() {
        return instance || (instance = new AppPage), instance
    }
    Object.defineProperty(exports, "__esModule", {
        value: !0
    }), exports.getInstance = exports.run = exports.AppPage = void 0;
    var AppPage = function(_BaseApp) {
            function AppPage() {
                return babelHelpers.classCallCheck(this, AppPage), babelHelpers.possibleConstructorReturn(this, (AppPage.__proto__ || Object.getPrototypeOf(AppPage)).apply(this, arguments))
            }
            return babelHelpers.inherits(AppPage, _BaseApp), babelHelpers.createClass(AppPage, [{
                key: "initialize",
                value: function() {
                    babelHelpers.get(AppPage.prototype.__proto__ || Object.getPrototypeOf(AppPage.prototype), "initialize", this).call(this), this.$actionBtn = $(".site-action"), this.$actionToggleBtn = this.$actionBtn.find(".site-action-toggle"),
                      , this.$content = $("#contactsContent"), this.states = {
                        checked: !1
                    }
                }
            }, {
                key: "process",
                value: function() {
                    babelHelpers.get(AppPage.prototype.__proto__ || Object.getPrototypeOf(AppPage.prototype), "process", this).call(this), this.setupActionBtn(), this.bindListChecked(), this.handlSlidePanelContent()
                }
            }, {
                key: "listChecked",
                value: function(checked) {
                    var api = this.$actionBtn.data("actionBtn");
                    checked ? api.show() : api.hide(), this.states.checked = checked
                }
            }, {
                key: "setupActionBtn",
                value: function() {
                    var _this2 = this;
                    this.$actionToggleBtn.on("click", function(e) {
                        _this2.states.checked || (_this2.$addMainForm.modal("show"), e.stopPropagation())
                    })
                }
            }, {
                key: "bindListChecked",
                value: function() {
                    var _this3 = this;
                    this.$content.on("asSelectable::change", function(e, api, checked) {
                        _this3.listChecked(checked)
                    })
                }
            }, {
                key: "handlSlidePanelContent",
                value: function() {
                    var _this4 = this;
                    $(document).on("click", "[data-toggle=edit]", function() {
                        var $button = $(this),
                            $form = $button.parents(".slidePanel").find(".user-info");
                        $button.toggleClass("active"), $form.toggleClass("active")
                    }), $(document).on("change", ".user-info .form-group", function(e) {
                        var $input = $(_this4).find("input");
                        $(_this4).siblings("span").html($input.val())
                    })
                }
            }]), AppPage
        }(babelHelpers.interopRequireDefault(_BaseApp2).default),
        instance = null;
    exports.default = AppPage, exports.AppPage = AppPage, exports.run = function() {
        getInstance().run()
    }, exports.getInstance = getInstance
});



/*!
 * Remark (http://getbootstrapadmin.com/remark)
 * Copyright 2017 amazingsurge
 * Licensed under the Themeforest Standard Licenses
 */
! function(global, factory) {
    if ("function" == typeof define && define.amd) define("/apps/Incident_Index", ["jquery"], factory);
    else if ("undefined" != typeof exports) factory(require("jquery"));
    else {
        var mod = {
            exports: {}
        };
        factory(global.jQuery), global.appsContacts = mod.exports
    }
}(this, function(_jquery) {
    "use strict";
    (0, babelHelpers.interopRequireDefault(_jquery).default)(document).ready(function() {
        AppPage.run()
    })
});