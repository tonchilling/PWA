/*!
 * Remark (http://getbootstrapadmin.com/remark)
 * Copyright 2017 amazingsurge
 * Licensed under the Themeforest Standard Licenses
 */
! function (global, factory) {
    if ("function" == typeof define && define.amd) define("/Plugin/switchery", ["exports", "Plugin", "Config"], factory);
    else if ("undefined" != typeof exports) factory(exports, require("Plugin"), require("Config"));
    else {
        var mod = {
            exports: {}
        };
        factory(mod.exports, global.Plugin, global.Config), global.PluginSwitchery = mod.exports
    }
}(this, function (exports, _Plugin2, _Config) {
    "use strict";
    Object.defineProperty(exports, "__esModule", {
        value: !0
    });
    var _Plugin3 = babelHelpers.interopRequireDefault(_Plugin2),
        Config = babelHelpers.interopRequireWildcard(_Config),
        SwitcheryPlugin = function (_Plugin) {
            function SwitcheryPlugin() {
                return babelHelpers.classCallCheck(this, SwitcheryPlugin), babelHelpers.possibleConstructorReturn(this, (SwitcheryPlugin.__proto__ || Object.getPrototypeOf(SwitcheryPlugin)).apply(this, arguments))
            }
            return babelHelpers.inherits(SwitcheryPlugin, _Plugin), babelHelpers.createClass(SwitcheryPlugin, [{
                key: "getName",
                value: function () {
                    return "switchery"
                }
            }, {
                key: "render",
                value: function () {
                    "undefined" != typeof Switchery && new Switchery(this.$el[0], this.options)
                }
            }], [{
                key: "getDefaults",
                value: function () {
                    return {
                        color: Config.colors("primary", 600)
                    }
                }
            }]), SwitcheryPlugin
        }(_Plugin3.default);
    _Plugin3.default.register("switchery", SwitcheryPlugin), exports.default = SwitcheryPlugin
});