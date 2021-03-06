/*!
 * Datepicker for Bootstrap v1.4.0 (https://github.com/bouroo/bootstrap-datepicker-thai)
 *
 * Copyright 2012 Stefan Petre
 * Improvements by Andrew Rowls
 * Convert to Buddhist Era by Kawin Viriyaprasupsook
 * Licensed under the Apache License v2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 */
! function (t, e) {
    function a() {
        return new Date(Date.UTC.apply(Date, arguments))
    }

    function i() {
        var t = new Date;
        return a(t.getFullYear(), t.getMonth(), t.getDate())
    }

    function s(t, e) {
        return t.getUTCFullYear() === e.getUTCFullYear() && t.getUTCMonth() === e.getUTCMonth() && t.getUTCDate() === e.getUTCDate()
    }

    function n(t) {
        return function () {
            return this[t].apply(this, arguments)
        }
    }

    function r(e, a) {
        function i(t, e) {
            return e.toLowerCase()
        }
        var s, n = t(e).data(),
            r = {},
            o = new RegExp("^" + a.toLowerCase() + "([A-Z])");
        a = new RegExp("^" + a.toLowerCase());
        for (var h in n) a.test(h) && (s = h.replace(o, i), r[s] = n[h]);
        return r
    }

    function o(e) {
        var a = {};
        if (m[e] || (e = e.split("-")[0], m[e])) {
            var i = m[e];
            return t.each(v, function (t, e) {
                e in i && (a[e] = i[e])
            }), a
        }
    }
    var h = new Date,
        l = 543,
        d = h.getUTCFullYear() + l / 2,
        c = function () {
            var e = {
                get: function (t) {
                    return this.slice(t)[0]
                },
                contains: function (t) {
                    for (var e = t && t.valueOf(), a = 0, i = this.length; i > a; a++)
                        if (this[a].valueOf() === e) return a;
                    return -1
                },
                remove: function (t) {
                    this.splice(t, 1)
                },
                replace: function (e) {
                    e && (t.isArray(e) || (e = [e]), this.clear(), this.push.apply(this, e))
                },
                clear: function () {
                    this.length = 0
                },
                copy: function () {
                    var t = new c;
                    return t.replace(this), t
                }
            };
            return function () {
                var a = [];
                return a.push.apply(a, arguments), t.extend(a, e), a
            }
        }(),
        u = function (e, a) {
            this._process_options(a), this.dates = new c, this.viewDate = this.o.defaultViewDate, this.focusDate = null, this.element = t(e), this.isInline = !1, this.isInput = this.element.is("input"), this.component = this.element.hasClass("date") ? this.element.find(".add-on, .input-group-addon, .btn") : !1, this.hasInput = this.component && this.element.find("input").length, this.component && 0 === this.component.length && (this.component = !1), this.picker = t(y.template), this._buildEvents(), this._attachEvents(), this.isInline ? this.picker.addClass("datepicker-inline").appendTo(this.element) : this.picker.addClass("datepicker-dropdown dropdown-menu"), this.o.rtl && this.picker.addClass("datepicker-rtl"), this.viewMode = this.o.startView, this.o.calendarWeeks && this.picker.find("tfoot .today, tfoot .clear").attr("colspan", function (t, e) {
                return parseInt(e) + 1
            }), this._allow_update = !1, this.setStartDate(this._o.startDate), this.setEndDate(this._o.endDate), this.setDaysOfWeekDisabled(this.o.daysOfWeekDisabled), this.setDatesDisabled(this.o.datesDisabled), this.fillDow(), this.fillMonths(), this._allow_update = !0, this.update(), this.showMode(), this.isInline && this.show()
        };
    u.prototype = {
        constructor: u,
        _process_options: function (s) {
            this._o = t.extend({}, this._o, s);
            var n = this.o = t.extend({}, this._o),
                r = n.language;
            switch (m[r] || (r = r.split("-")[0], m[r] || (r = D.language)), n.language = r, n.startView) {
                case 2:
                case "decade":
                    n.startView = 2;
                    break;
                case 1:
                case "year":
                    n.startView = 1;
                    break;
                default:
                    n.startView = 0
            }
            switch (n.minViewMode) {
                case 1:
                case "months":
                    n.minViewMode = 1;
                    break;
                case 2:
                case "years":
                    n.minViewMode = 2;
                    break;
                default:
                    n.minViewMode = 0
            }
            n.startView = Math.max(n.startView, n.minViewMode), n.multidate !== !0 && (n.multidate = Number(n.multidate) || !1, n.multidate !== !1 && (n.multidate = Math.max(0, n.multidate))), n.multidateSeparator = String(n.multidateSeparator), n.weekStart %= 7, n.weekEnd = (n.weekStart + 6) % 7;
            var o = y.parseFormat(n.format);
            if (n.startDate !== -1 / 0 && (n.startDate = n.startDate ? n.startDate instanceof Date ? this._local_to_utc(this._zero_time(n.startDate)) : y.parseDate(n.startDate, o, n.language) : -1 / 0), 1 / 0 !== n.endDate && (n.endDate = n.endDate ? n.endDate instanceof Date ? this._local_to_utc(this._zero_time(n.endDate)) : y.parseDate(n.endDate, o, n.language) : 1 / 0), n.daysOfWeekDisabled = n.daysOfWeekDisabled || [], t.isArray(n.daysOfWeekDisabled) || (n.daysOfWeekDisabled = n.daysOfWeekDisabled.split(/[,\s]*/)), n.daysOfWeekDisabled = t.map(n.daysOfWeekDisabled, function (t) {
                return parseInt(t, 10)
            }), n.datesDisabled = n.datesDisabled || [], !t.isArray(n.datesDisabled)) {
                var h = [];
                h.push(y.parseDate(n.datesDisabled, o, n.language)), n.datesDisabled = h
            }
            n.datesDisabled = t.map(n.datesDisabled, function (t) {
                return y.parseDate(t, o, n.language)
            });
            var l = String(n.orientation).toLowerCase().split(/\s+/g),
                d = n.orientation.toLowerCase();
            if (l = t.grep(l, function (t) {
                return /^auto|left|right|top|bottom$/.test(t)
            }), n.orientation = {
                x: "auto",
                y: "auto"
            }, d && "auto" !== d)
                if (1 === l.length) switch (l[0]) {
                    case "top":
                    case "bottom":
                        n.orientation.y = l[0];
                        break;
                    case "left":
                    case "right":
                        n.orientation.x = l[0]
                } else d = t.grep(l, function (t) {
                    return /^left|right$/.test(t)
                }), n.orientation.x = d[0] || "auto", d = t.grep(l, function (t) {
                    return /^top|bottom$/.test(t)
                }), n.orientation.y = d[0] || "auto";
            else;
            if (n.defaultViewDate) {
                var c = n.defaultViewDate.year || (new Date).getFullYear(),
                    u = n.defaultViewDate.month || 0,
                    p = n.defaultViewDate.day || 1;
                n.defaultViewDate = a(c, u, p)
            } else n.defaultViewDate = i();
            n.showOnFocus = n.showOnFocus !== e ? n.showOnFocus : !0
        },
        _events: [],
        _secondaryEvents: [],
        _applyEvents: function (t) {
            for (var a, i, s, n = 0; n < t.length; n++) a = t[n][0], 2 === t[n].length ? (i = e, s = t[n][1]) : 3 === t[n].length && (i = t[n][1], s = t[n][2]), a.on(s, i)
        },
        _unapplyEvents: function (t) {
            for (var a, i, s, n = 0; n < t.length; n++) a = t[n][0], 2 === t[n].length ? (s = e, i = t[n][1]) : 3 === t[n].length && (s = t[n][1], i = t[n][2]), a.off(i, s)
        },
        _buildEvents: function () {
            var e = {
                keyup: t.proxy(function (e) {
                    -1 === t.inArray(e.keyCode, [27, 37, 39, 38, 40, 32, 13, 9]) && this.update()
                }, this),
                keydown: t.proxy(this.keydown, this)
            };
            this.o.showOnFocus === !0 && (e.focus = t.proxy(this.show, this)), this.isInput ? this._events = [
                [this.element, e]
            ] : this.component && this.hasInput ? this._events = [
                [this.element.find("input"), e],
                [this.component, {
                    click: t.proxy(this.show, this)
                }]
            ] : this.element.is("div") ? this.isInline = !0 : this._events = [
                [this.element, {
                    click: t.proxy(this.show, this)
                }]
            ], this._events.push([this.element, "*", {
                blur: t.proxy(function (t) {
                    this._focused_from = t.target
                }, this)
            }], [this.element, {
                blur: t.proxy(function (t) {
                    this._focused_from = t.target
                }, this)
            }]), this._secondaryEvents = [
                [this.picker, {
                    click: t.proxy(this.click, this)
                }],
                [t(window), {
                    resize: t.proxy(this.place, this)
                }],
                [t(document), {
                    "mousedown touchstart": t.proxy(function (t) {
                        this.element.is(t.target) || this.element.find(t.target).length || this.picker.is(t.target) || this.picker.find(t.target).length || this.hide()
                    }, this)
                }]
            ]
        },
        _attachEvents: function () {
            this._detachEvents(), this._applyEvents(this._events)
        },
        _detachEvents: function () {
            this._unapplyEvents(this._events)
        },
        _attachSecondaryEvents: function () {
            this._detachSecondaryEvents(), this._applyEvents(this._secondaryEvents)
        },
        _detachSecondaryEvents: function () {
            this._unapplyEvents(this._secondaryEvents)
        },
        _trigger: function (e, a) {
            var i = a || this.dates.get(-1),
                s = this._utc_to_local(i);
            this.element.trigger({
                type: e,
                date: s,
                dates: t.map(this.dates, this._utc_to_local),
                format: t.proxy(function (t, e) {
                    0 === arguments.length ? (t = this.dates.length - 1, e = this.o.format) : "string" == typeof t && (e = t, t = this.dates.length - 1), e = e || this.o.format;
                    var a = this.dates.get(t);
                    return y.formatDate(a, e, this.o.language)
                }, this)
            })
        },
        show: function () {
            return this.element.attr("readonly") && this.o.enableOnReadonly === !1 ? void 0 : (this.isInline || this.picker.appendTo(this.o.container), this.place(), this.picker.show(), this._attachSecondaryEvents(), this._trigger("show"), (window.navigator.msMaxTouchPoints || "ontouchstart" in document) && this.o.disableTouchKeyboard && t(this.element).blur(), this)
        },
        hide: function () {
            return this.isInline ? this : this.picker.is(":visible") ? (this.focusDate = null, this.picker.hide().detach(), this._detachSecondaryEvents(), this.viewMode = this.o.startView, this.showMode(), this.o.forceParse && (this.isInput && this.element.val() || this.hasInput && this.element.find("input").val()) && this.setValue(), this._trigger("hide"), this) : this
        },
        remove: function () {
            return this.hide(), this._detachEvents(), this._detachSecondaryEvents(), this.picker.remove(), delete this.element.data().datepicker, this.isInput || delete this.element.data().date, this
        },
        _utc_to_local: function (t) {
            return t && new Date(t.getTime() + 6e4 * t.getTimezoneOffset())
        },
        _local_to_utc: function (t) {
            return t && new Date(t.getTime() - 6e4 * t.getTimezoneOffset())
        },
        _zero_time: function (t) {
            return t && new Date(t.getFullYear(), t.getMonth(), t.getDate())
        },
        _zero_utc_time: function (t) {
            return t && new Date(Date.UTC(t.getUTCFullYear(), t.getUTCMonth(), t.getUTCDate()))
        },
        getDates: function () {
            return t.map(this.dates, this._utc_to_local)
        },
        getUTCDates: function () {
            return t.map(this.dates, function (t) {
                return new Date(t)
            })
        },
        getDate: function () {
            return this._utc_to_local(this.getUTCDate())
        },
        getUTCDate: function () {
            var t = this.dates.get(-1);
            return "undefined" != typeof t ? new Date(t) : null
        },
        clearDates: function () {
            var t;
            this.isInput ? t = this.element : this.component && (t = this.element.find("input")), t && t.val("").change(), this.update(), this._trigger("changeDate"), this.o.autoclose && this.hide()
        },
        setDates: function () {
            var e = t.isArray(arguments[0]) ? arguments[0] : arguments;
            return this.update.apply(this, e), this._trigger("changeDate"), this.setValue(), this
        },
        setUTCDates: function () {
            var e = t.isArray(arguments[0]) ? arguments[0] : arguments;
            return this.update.apply(this, t.map(e, this._utc_to_local)), this._trigger("changeDate"), this.setValue(), this
        },
        setDate: n("setDates"),
        setUTCDate: n("setUTCDates"),
        setValue: function () {
            var t = this.getFormattedDate();
            return this.isInput ? this.element.val(t).change() : this.component && this.element.find("input").val(t).change(), this
        },
        getFormattedDate: function (a) {
            a === e && (a = this.o.format);
            var i = this.o.language;
            return t.map(this.dates, function (t) {
                return y.formatDate(t, a, i)
            }).join(this.o.multidateSeparator)
        },
        setStartDate: function (t) {
            return this._process_options({
                startDate: t
            }), this.update(), this.updateNavArrows(), this
        },
        setEndDate: function (t) {
            return this._process_options({
                endDate: t
            }), this.update(), this.updateNavArrows(), this
        },
        setDaysOfWeekDisabled: function (t) {
            return this._process_options({
                daysOfWeekDisabled: t
            }), this.update(), this.updateNavArrows(), this
        },
        setDatesDisabled: function (t) {
            this._process_options({
                datesDisabled: t
            }), this.update(), this.updateNavArrows()
        },
        place: function () {
            if (this.isInline) return this;
            var e = this.picker.outerWidth(),
                a = this.picker.outerHeight(),
                i = 10,
                s = t(this.o.container).width(),
                n = t(this.o.container).height(),
                r = t(this.o.container).scrollTop(),
                o = t(this.o.container).offset(),
                h = [];
            this.element.parents().each(function () {
                var e = t(this).css("z-index");
                "auto" !== e && 0 !== e && h.push(parseInt(e))
            });
            var l = Math.max.apply(Math, h) + 10,
                d = this.component ? this.component.parent().offset() : this.element.offset(),
                c = this.component ? this.component.outerHeight(!0) : this.element.outerHeight(!1),
                u = this.component ? this.component.outerWidth(!0) : this.element.outerWidth(!1),
                p = d.left - o.left,
                f = d.top - o.top;
            this.picker.removeClass("datepicker-orient-top datepicker-orient-bottom datepicker-orient-right datepicker-orient-left"), "auto" !== this.o.orientation.x ? (this.picker.addClass("datepicker-orient-" + this.o.orientation.x), "right" === this.o.orientation.x && (p -= e - u)) : d.left < 0 ? (this.picker.addClass("datepicker-orient-left"), p -= d.left - i) : p + e > s ? (this.picker.addClass("datepicker-orient-right"), p = d.left + u - e) : this.picker.addClass("datepicker-orient-left");
            var g, D, v = this.o.orientation.y;
            if ("auto" === v && (g = -r + f - a, D = r + n - (f + c + a), v = Math.max(g, D) === D ? "top" : "bottom"), this.picker.addClass("datepicker-orient-" + v), "top" === v ? f += c : f -= a + parseInt(this.picker.css("padding-top")), this.o.rtl) {
                var m = s - (p + u);
                this.picker.css({
                    top: f,
                    right: m,
                    zIndex: l
                })
            } else this.picker.css({
                top: f,
                left: p,
                zIndex: l
            });
            return this
        },
        _allow_update: !0,
        update: function () {
            if (!this._allow_update) return this;
            var e = this.dates.copy(),
                a = [],
                i = !1;
            return arguments.length ? (t.each(arguments, t.proxy(function (t, e) {
                e instanceof Date && (e = this._local_to_utc(e)), a.push(e)
            }, this)), i = !0) : (a = this.isInput ? this.element.val() : this.element.data("date") || this.element.find("input").val(), a = a && this.o.multidate ? a.split(this.o.multidateSeparator) : [a], delete this.element.data().date), a = t.map(a, t.proxy(function (t) {
                return y.parseDate(t, this.o.format, this.o.language)
            }, this)), a = t.grep(a, t.proxy(function (t) {
                return t < this.o.startDate || t > this.o.endDate || !t
            }, this), !0), this.dates.replace(a), this.dates.length ? this.viewDate = new Date(this.dates.get(-1)) : this.viewDate < this.o.startDate ? this.viewDate = new Date(this.o.startDate) : this.viewDate > this.o.endDate && (this.viewDate = new Date(this.o.endDate)), i ? this.setValue() : a.length && String(e) !== String(this.dates) && this._trigger("changeDate"), !this.dates.length && e.length && this._trigger("clearDate"), this.fill(), this
        },
        fillDow: function () {
            var t = this.o.weekStart,
                e = "<tr>";
            if (this.o.calendarWeeks) {
                this.picker.find(".datepicker-days thead tr:first-child .datepicker-switch").attr("colspan", function (t, e) {
                    return parseInt(e) + 1
                });
                var a = '<th class="cw">&#160;</th>';
                e += a
            }
            for (; t < this.o.weekStart + 7;) e += '<th class="dow">' + m[this.o.language].daysMin[t++ % 7] + "</th>";
            e += "</tr>", this.picker.find(".datepicker-days thead").append(e)
        },
        fillMonths: function () {
            for (var t = "", e = 0; 12 > e;) t += '<span class="month">' + m[this.o.language].monthsShort[e++] + "</span>";
            this.picker.find(".datepicker-months td").html(t)
        },
        setRange: function (e) {
            e && e.length ? this.range = t.map(e, function (t) {
                return t.valueOf()
            }) : delete this.range, this.fill()
        },
        getClassNames: function (e) {
            var a = [],
                i = this.viewDate.getUTCFullYear(),
                n = this.viewDate.getUTCMonth(),
                r = new Date;
            return e.getUTCFullYear() < i || e.getUTCFullYear() === i && e.getUTCMonth() < n ? a.push("old") : (e.getUTCFullYear() > i || e.getUTCFullYear() === i && e.getUTCMonth() > n) && a.push("new"), this.focusDate && e.valueOf() === this.focusDate.valueOf() && a.push("focused"), this.o.todayHighlight && e.getUTCFullYear() === r.getFullYear() && e.getUTCMonth() === r.getMonth() && e.getUTCDate() === r.getDate() && a.push("today"), -1 !== this.dates.contains(e) && a.push("active"), (e.valueOf() < this.o.startDate || e.valueOf() > this.o.endDate || -1 !== t.inArray(e.getUTCDay(), this.o.daysOfWeekDisabled)) && a.push("disabled"), this.o.datesDisabled.length > 0 && t.grep(this.o.datesDisabled, function (t) {
                return s(e, t)
            }).length > 0 && a.push("disabled", "disabled-date"), this.range && (e > this.range[0] && e < this.range[this.range.length - 1] && a.push("range"), -1 !== t.inArray(e.valueOf(), this.range) && a.push("selected")), a
        },
        fill: function () {
            var i, s = new Date(this.viewDate),
                n = s.getUTCFullYear(),
                r = s.getUTCMonth(),
                o = this.o.startDate !== -1 / 0 ? this.o.startDate.getUTCFullYear() : -1 / 0,
                h = this.o.startDate !== -1 / 0 ? this.o.startDate.getUTCMonth() : -1 / 0,
                c = 1 / 0 !== this.o.endDate ? this.o.endDate.getUTCFullYear() : 1 / 0,
                u = 1 / 0 !== this.o.endDate ? this.o.endDate.getUTCMonth() : 1 / 0,
                p = m[this.o.language].today || m.en.today || "",
                f = m[this.o.language].clear || m.en.clear || "";
            if (n > d && (s = new a(n - l, r, s.getDate()), n = s.getUTCFullYear(), r = s.getUTCMonth()), !isNaN(n) && !isNaN(r)) {
                this.picker.find(".datepicker-days thead .datepicker-switch").text(m[this.o.language].months[r] + " " + (n + l)), this.picker.find("tfoot .today").text(p).toggle(this.o.todayBtn !== !1), this.picker.find("tfoot .clear").text(f).toggle(this.o.clearBtn !== !1), this.updateNavArrows(), this.fillMonths();
                var g = a(n, r - 1, 28),
                    D = y.getDaysInMonth(g.getUTCFullYear(), g.getUTCMonth());
                g.setUTCDate(D), g.setUTCDate(D - (g.getUTCDay() - this.o.weekStart + 7) % 7);
                var v = new Date(g);
                v.setUTCDate(v.getUTCDate() + 42), v = v.valueOf();
                for (var w, k = []; g.valueOf() < v;) {
                    if (g.getUTCDay() === this.o.weekStart && (k.push("<tr>"), this.o.calendarWeeks)) {
                        var C = new Date(+g + (this.o.weekStart - g.getUTCDay() - 7) % 7 * 864e5),
                            T = new Date(Number(C) + (11 - C.getUTCDay()) % 7 * 864e5),
                            b = new Date(Number(b = a(T.getUTCFullYear(), 0, 1)) + (11 - b.getUTCDay()) % 7 * 864e5),
                            _ = (T - b) / 864e5 / 7 + 1;
                        k.push('<td class="cw">' + _ + "</td>")
                    }
                    if (w = this.getClassNames(g), w.push("day"), this.o.beforeShowDay !== t.noop) {
                        var U = this.o.beforeShowDay(this._utc_to_local(g));
                        U === e ? U = {} : "boolean" == typeof U ? U = {
                            enabled: U
                        } : "string" == typeof U && (U = {
                            classes: U
                        }), U.enabled === !1 && w.push("disabled"), U.classes && (w = w.concat(U.classes.split(/\s+/))), U.tooltip && (i = U.tooltip)
                    }
                    w = t.unique(w), k.push('<td class="' + w.join(" ") + '"' + (i ? ' title="' + i + '"' : "") + ">" + g.getUTCDate() + "</td>"), i = null, g.getUTCDay() === this.o.weekEnd && k.push("</tr>"), g.setUTCDate(g.getUTCDate() + 1)
                }
                this.picker.find(".datepicker-days tbody").empty().append(k.join(""));
                var M = this.picker.find(".datepicker-months").find("th:eq(1)").text(n + l).end().find("span").removeClass("active");
                if (t.each(this.dates, function (t, e) {
                    e.getUTCFullYear() === n && M.eq(e.getUTCMonth()).addClass("active")
                }), (o > n || n > c) && M.addClass("disabled"), n === o && M.slice(0, h).addClass("disabled"), n === c && M.slice(u + 1).addClass("disabled"), this.o.beforeShowMonth !== t.noop) {
                    var F = this;
                    t.each(M, function (e, a) {
                        if (!t(a).hasClass("disabled")) {
                            var i = new Date(n, e, 1),
                                s = F.o.beforeShowMonth(i);
                            s === !1 && t(a).addClass("disabled")
                        }
                    })
                }
                k = "", n = 10 * parseInt((n + l) / 10, 10);
                var S = this.picker.find(".datepicker-years").find("th:eq(1)").text(n + "-" + (n + 9)).end().find("td");
                n -= 1;
                for (var x, Y = t.map(this.dates, function (t) {
                    return t.getUTCFullYear()
                }), N = -1; 11 > N; N++) x = ["year"], -1 === N ? x.push("old") : 10 === N && x.push("new"), -1 !== t.inArray(n, Y) && x.push("active"), (o > n || n > c) && x.push("disabled"), k += '<span class="' + x.join(" ") + '">' + n + "</span>", n += 1;
                S.html(k)
            }
        },
        updateNavArrows: function () {
            if (this._allow_update) {
                var t = new Date(this.viewDate),
                    e = t.getUTCFullYear(),
                    a = t.getUTCMonth();
                switch (this.viewMode) {
                    case 0:
                        this.picker.find(".prev").css(this.o.startDate !== -1 / 0 && e <= this.o.startDate.getUTCFullYear() && a <= this.o.startDate.getUTCMonth() ? {
                            visibility: "hidden"
                        } : {
                                visibility: "visible"
                            }), this.picker.find(".next").css(1 / 0 !== this.o.endDate && e >= this.o.endDate.getUTCFullYear() && a >= this.o.endDate.getUTCMonth() ? {
                                visibility: "hidden"
                            } : {
                                    visibility: "visible"
                                });
                        break;
                    case 1:
                    case 2:
                        this.picker.find(".prev").css(this.o.startDate !== -1 / 0 && e <= this.o.startDate.getUTCFullYear() ? {
                            visibility: "hidden"
                        } : {
                                visibility: "visible"
                            }), this.picker.find(".next").css(1 / 0 !== this.o.endDate && e >= this.o.endDate.getUTCFullYear() ? {
                                visibility: "hidden"
                            } : {
                                    visibility: "visible"
                                })
                }
            }
        },
        click: function (e) {
            e.preventDefault();
            var i, s, n, r = t(e.target).closest("span, td, th");
            if (1 === r.length) switch (r[0].nodeName.toLowerCase()) {
                case "th":
                    switch (r[0].className) {
                        case "datepicker-switch":
                            this.showMode(1);
                            break;
                        case "prev":
                        case "next":
                            var o = y.modes[this.viewMode].navStep * ("prev" === r[0].className ? -1 : 1);
                            switch (this.viewMode) {
                                case 0:
                                    this.viewDate = this.moveMonth(this.viewDate, o), this._trigger("changeMonth", this.viewDate);
                                    break;
                                case 1:
                                case 2:
                                    this.viewDate = this.moveYear(this.viewDate, o), 1 === this.viewMode && this._trigger("changeYear", this.viewDate)
                            }
                            this.fill();
                            break;
                        case "today":
                            var h = new Date;
                            h = a(h.getFullYear(), h.getMonth(), h.getDate(), 0, 0, 0), this.showMode(-2);
                            var l = "linked" === this.o.todayBtn ? null : "view";
                            this._setDate(h, l);
                            break;
                        case "clear":
                            this.clearDates()
                    }
                    break;
                case "span":
                    r.hasClass("disabled") || (this.viewDate.setUTCDate(1), r.hasClass("month") ? (n = 1, s = r.parent().find("span").index(r), i = this.viewDate.getUTCFullYear(), this.viewDate.setUTCMonth(s), this._trigger("changeMonth", this.viewDate), 1 === this.o.minViewMode && this._setDate(a(i, s, n))) : (n = 1, s = 0, i = parseInt(r.text(), 10) || 0, this.viewDate.setUTCFullYear(i), this._trigger("changeYear", this.viewDate), 2 === this.o.minViewMode && this._setDate(a(i, s, n))), this.showMode(-1), this.fill());
                    break;
                case "td":
                    r.hasClass("day") && !r.hasClass("disabled") && (n = parseInt(r.text(), 10) || 1, i = this.viewDate.getUTCFullYear(), s = this.viewDate.getUTCMonth(), r.hasClass("old") ? 0 === s ? (s = 11, i -= 1) : s -= 1 : r.hasClass("new") && (11 === s ? (s = 0, i += 1) : s += 1), this._setDate(a(i, s, n)))
            }
            this.picker.is(":visible") && this._focused_from && t(this._focused_from).focus(), delete this._focused_from
        },
        _toggle_multidate: function (t) {
            var e = this.dates.contains(t);
            if (t || this.dates.clear(), -1 !== e ? (this.o.multidate === !0 || this.o.multidate > 1 || this.o.toggleActive) && this.dates.remove(e) : this.o.multidate === !1 ? (this.dates.clear(), this.dates.push(t)) : this.dates.push(t), "number" == typeof this.o.multidate)
                for (; this.dates.length > this.o.multidate;) this.dates.remove(0)
        },
        _setDate: function (t, e) {
            t.getUTCFullYear() > d && (t = new a(t.getUTCFullYear() - l, t.getMonth() + 1, t.getDate())), e && "date" !== e || this._toggle_multidate(t && new Date(t)), e && "view" !== e || (this.viewDate = t && new Date(t)), this.fill(), this.setValue(), e && "view" === e || this._trigger("changeDate");
            var i;
            this.isInput ? i = this.element : this.component && (i = this.element.find("input")), i && i.change(), !this.o.autoclose || e && "date" !== e || this.hide()
        },
        moveMonth: function (t, a) {
            if (!t) return e;
            if (!a) return t;
            var i, s, n = new Date(t.valueOf()),
                r = n.getUTCDate(),
                o = n.getUTCMonth(),
                h = Math.abs(a);
            if (a = a > 0 ? 1 : -1, 1 === h) s = -1 === a ? function () {
                return n.getUTCMonth() === o
            } : function () {
                return n.getUTCMonth() !== i
            }, i = o + a, n.setUTCMonth(i), (0 > i || i > 11) && (i = (i + 12) % 12);
            else {
                for (var l = 0; h > l; l++) n = this.moveMonth(n, a);
                i = n.getUTCMonth(), n.setUTCDate(r), s = function () {
                    return i !== n.getUTCMonth()
                }
            }
            for (; s();) n.setUTCDate(--r), n.setUTCMonth(i);
            return n
        },
        moveYear: function (t, e) {
            return this.moveMonth(t, 12 * e)
        },
        dateWithinRange: function (t) {
            return t >= this.o.startDate && t <= this.o.endDate
        },
        keydown: function (t) {
            if (!this.picker.is(":visible")) return void (27 === t.keyCode && this.show());
            var e, a, s, n = !1,
                r = this.focusDate || this.viewDate;
            switch (t.keyCode) {
                case 27:
                    this.focusDate ? (this.focusDate = null, this.viewDate = this.dates.get(-1) || this.viewDate, this.fill()) : this.hide(), t.preventDefault();
                    break;
                case 37:
                case 39:
                    if (!this.o.keyboardNavigation) break;
                    e = 37 === t.keyCode ? -1 : 1, t.ctrlKey ? (a = this.moveYear(this.dates.get(-1) || i(), e), s = this.moveYear(r, e), this._trigger("changeYear", this.viewDate)) : t.shiftKey ? (a = this.moveMonth(this.dates.get(-1) || i(), e), s = this.moveMonth(r, e), this._trigger("changeMonth", this.viewDate)) : (a = new Date(this.dates.get(-1) || i()), a.setUTCDate(a.getUTCDate() + e), s = new Date(r), s.setUTCDate(r.getUTCDate() + e)), this.dateWithinRange(s) && (this.focusDate = this.viewDate = s, this.setValue(), this.fill(), t.preventDefault());
                    break;
                case 38:
                case 40:
                    if (!this.o.keyboardNavigation) break;
                    e = 38 === t.keyCode ? -1 : 1, t.ctrlKey ? (a = this.moveYear(this.dates.get(-1) || i(), e), s = this.moveYear(r, e), this._trigger("changeYear", this.viewDate)) : t.shiftKey ? (a = this.moveMonth(this.dates.get(-1) || i(), e), s = this.moveMonth(r, e), this._trigger("changeMonth", this.viewDate)) : (a = new Date(this.dates.get(-1) || i()), a.setUTCDate(a.getUTCDate() + 7 * e), s = new Date(r), s.setUTCDate(r.getUTCDate() + 7 * e)), this.dateWithinRange(s) && (this.focusDate = this.viewDate = s, this.setValue(), this.fill(), t.preventDefault());
                    break;
                case 32:
                    break;
                case 13:
                    r = this.focusDate || this.dates.get(-1) || this.viewDate, this.o.keyboardNavigation && (this._toggle_multidate(r), n = !0), this.focusDate = null, this.viewDate = this.dates.get(-1) || this.viewDate, this.setValue(), this.fill(), this.picker.is(":visible") && (t.preventDefault(), "function" == typeof t.stopPropagation ? t.stopPropagation() : t.cancelBubble = !0, this.o.autoclose && this.hide());
                    break;
                case 9:
                    this.focusDate = null, this.viewDate = this.dates.get(-1) || this.viewDate, this.fill(), this.hide()
            }
            if (n) {
                this._trigger(this.dates.length ? "changeDate" : "clearDate");
                var o;
                this.isInput ? o = this.element : this.component && (o = this.element.find("input")), o && o.change()
            }
        },
        showMode: function (t) {
            t && (this.viewMode = Math.max(this.o.minViewMode, Math.min(2, this.viewMode + t))), this.picker.children("div").hide().filter(".datepicker-" + y.modes[this.viewMode].clsName).css("display", "block"), this.updateNavArrows()
        }
    };
    var p = function (e, a) {
        this.element = t(e), this.inputs = t.map(a.inputs, function (t) {
            return t.jquery ? t[0] : t
        }), delete a.inputs, g.call(t(this.inputs), a).bind("changeDate", t.proxy(this.dateUpdated, this)), this.pickers = t.map(this.inputs, function (e) {
            return t(e).data("datepicker")
        }), this.updateDates()
    };
    p.prototype = {
        updateDates: function () {
            this.dates = t.map(this.pickers, function (t) {
                return t.getUTCDate()
            }), this.updateRanges()
        },
        updateRanges: function () {
            var e = t.map(this.dates, function (t) {
                return t.valueOf()
            });
            t.each(this.pickers, function (t, a) {
                a.setRange(e)
            })
        },
        dateUpdated: function (e) {
            if (!this.updating) {
                this.updating = !0;
                var a = t(e.target).data("datepicker"),
                    i = a.getUTCDate(),
                    s = t.inArray(e.target, this.inputs),
                    n = s - 1,
                    r = s + 1,
                    o = this.inputs.length;
                if (-1 !== s) {
                    if (t.each(this.pickers, function (t, e) {
                        e.getUTCDate() || e.setUTCDate(i)
                    }), i < this.dates[n])
                        for (; n >= 0 && i < this.dates[n];) this.pickers[n--].setUTCDate(i);
                    else if (i > this.dates[r])
                        for (; o > r && i > this.dates[r];) this.pickers[r++].setUTCDate(i);
                    this.updateDates(), delete this.updating
                }
            }
        },
        remove: function () {
            t.map(this.pickers, function (t) {
                t.remove()
            }), delete this.element.data().datepicker
        }
    };
    var f = t.fn.datepicker,
        g = function (a) {
            var i = Array.apply(null, arguments);
            i.shift();
            var s;
            return this.each(function () {
                var n = t(this),
                    h = n.data("datepicker"),
                    l = "object" == typeof a && a;
                if (!h) {
                    var d = r(this, "date"),
                        c = t.extend({}, D, d, l),
                        f = o(c.language),
                        g = t.extend({}, D, f, d, l);
                    if (n.hasClass("input-daterange") || g.inputs) {
                        var v = {
                            inputs: g.inputs || n.find("input").toArray()
                        };
                        n.data("datepicker", h = new p(this, t.extend(g, v)))
                    } else n.data("datepicker", h = new u(this, g))
                }
                return "string" == typeof a && "function" == typeof h[a] && (s = h[a].apply(h, i), s !== e) ? !1 : void 0
            }), s !== e ? s : this
        };
    t.fn.datepicker = g;
    var D = t.fn.datepicker.defaults = {
        autoclose: !1,
        beforeShowDay: t.noop,
        beforeShowMonth: t.noop,
        calendarWeeks: !1,
        clearBtn: !1,
        toggleActive: !1,
        daysOfWeekDisabled: [],
        datesDisabled: [],
        endDate: 1 / 0,
        forceParse: !0,
        format: "dd/mm/yyyy",
        keyboardNavigation: !0,
        language: "th",
        minViewMode: 0,
        multidate: !1,
        multidateSeparator: ",",
        orientation: "auto",
        rtl: !1,
        startDate: -1 / 0,
        startView: 0,
        todayBtn: !1,
        todayHighlight: !1,
        weekStart: 0,
        disableTouchKeyboard: !1,
        enableOnReadonly: !0,
        container: "body"
    },
        v = t.fn.datepicker.locale_opts = ["format", "rtl", "weekStart"];
    t.fn.datepicker.Constructor = u;
    var m = t.fn.datepicker.dates = {

        en: {
            days: ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"],
            daysShort: ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun"],
            daysMin: ["Su", "Mo", "Tu", "We", "Th", "Fr", "Sa", "Su"],
            months: ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"],
            monthsShort: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"],
            today: "Today",
            clear: "Clear"
        },
        th: {
            days: ["อาทิตย์", "จันทร์", "อังคาร", "พุธ", "พฤหัส", "ศุกร์", "เสาร์", "อาทิตย์"],
            daysShort: ["อา", "จ", "อ", "พ", "พฤ", "ศ", "ส", "อา"],
            daysMin: ["อา", "จ", "อ", "พ", "พฤ", "ศ", "ส", "อา"],
            months: ["มกราคม", "กุมภาพันธ์", "มีนาคม", "เมษายน", "พฤษภาคม", "มิถุนายน", "กรกฎาคม", "สิงหาคม", "กันยายน", "ตุลาคม", "พฤศจิกายน", "ธันวาคม"],
            monthsShort: ["ม.ค.", "ก.พ.", "มี.ค.", "เม.ย.", "พ.ค.", "มิ.ย.", "ก.ค.", "ส.ค.", "ก.ย.", "ต.ค.", "พ.ย.", "ธ.ค."],
            today: "วันนี้"
        }
    },
        y = {
            modes: [{
                clsName: "days",
                navFnc: "Month",
                navStep: 1
            }, {
                clsName: "months",
                navFnc: "FullYear",
                navStep: 1
            }, {
                clsName: "years",
                navFnc: "FullYear",
                navStep: 10
            }],
            isLeapYear: function (t) {
                return t % 4 === 0 && t % 100 !== 0 || t % 400 === 0
            },
            getDaysInMonth: function (t, e) {
                return [31, y.isLeapYear(t) ? 29 : 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31][e]
            },
            validParts: /dd?|DD?|mm?|MM?|yy(?:yy)?/g,
            nonpunctuation: /[^ -\/:-@\[\u3400-\u9fff-`{-~\t\n\r]+/g,
            parseFormat: function (t) {
                var e = t.replace(this.validParts, "\x00").split("\x00"),
                    a = t.match(this.validParts);
                if (!e || !e.length || !a || 0 === a.length) throw new Error("Invalid date format.");
                return {
                    separators: e,
                    parts: a
                }
            },
            parseDate: function (i, s, n) {
                function r() {
                    var t = this.slice(0, c[l].length),
                        e = c[l].slice(0, t.length);
                    return t.toLowerCase() === e.toLowerCase()
                }
                if (!i) return e;
                if (i instanceof Date) return i;
                "string" == typeof s && (s = y.parseFormat(s));
                var o, h, l, d = /([\-+]\d+)([dmwy])/,
                    c = i.match(/([\-+]\d+)([dmwy])/g);
                if (/^[\-+]\d+[dmwy]([\s,]+[\-+]\d+[dmwy])*$/.test(i)) {
                    for (i = new Date, l = 0; l < c.length; l++) switch (o = d.exec(c[l]), h = parseInt(o[1]), o[2]) {
                        case "d":
                            i.setUTCDate(i.getUTCDate() + h);
                            break;
                        case "m":
                            i = u.prototype.moveMonth.call(u.prototype, i, h);
                            break;
                        case "w":
                            i.setUTCDate(i.getUTCDate() + 7 * h);
                            break;
                        case "y":
                            i = u.prototype.moveYear.call(u.prototype, i, h)
                    }
                    return a(i.getUTCFullYear(), i.getUTCMonth(), i.getUTCDate(), 0, 0, 0)
                }
                c = i && i.match(this.nonpunctuation) || [], i = new Date;
                var p, f, g = {},
                    D = ["yyyy", "yy", "M", "MM", "m", "mm", "d", "dd"],
                    v = {
                        yyyy: function (t, e) {
                            return t.setUTCFullYear(e)
                        },
                        yy: function (t, e) {
                            return t.setUTCFullYear(2e3 + e)
                        },
                        m: function (t, e) {
                            if (isNaN(t)) return t;
                            for (e -= 1; 0 > e;) e += 12;
                            for (e %= 12, t.setUTCMonth(e); t.getUTCMonth() !== e;) t.setUTCDate(t.getUTCDate() - 1);
                            return t
                        },
                        d: function (t, e) {
                            return t.setUTCDate(e)
                        }
                    };
                v.M = v.MM = v.mm = v.m, v.dd = v.d, i = a(i.getFullYear(), i.getMonth(), i.getDate(), 0, 0, 0);
                var w = s.parts.slice();
                if (c.length !== w.length && (w = t(w).filter(function (e, a) {
                    return -1 !== t.inArray(a, D)
                }).toArray()), c.length === w.length) {
                    var k;
                    for (l = 0, k = w.length; k > l; l++) {
                        if (p = parseInt(c[l], 10), o = w[l], isNaN(p)) switch (o) {
                            case "MM":
                                f = t(m[n].months).filter(r), p = t.inArray(f[0], m[n].months) + 1;
                                break;
                            case "M":
                                f = t(m[n].monthsShort).filter(r), p = t.inArray(f[0], m[n].monthsShort) + 1
                        }
                        g[o] = p
                    }
                    var C, T;
                    for (l = 0; l < D.length; l++) T = D[l], T in g && !isNaN(g[T]) && (C = new Date(i), v[T](C, g[T]), isNaN(C) || (i = C))
                }
                return i
            },
            formatDate: function (e, i, s) {
                if (!e) return "";
                "string" == typeof i && (i = y.parseFormat(i));
                var n = e.getUTCFullYear();
                n > d ? e = new a(e.getUTCFullYear() - l, e.getUTCMonth(), e.getDate()) : n += l;
                var r = {
                    d: e.getUTCDate(),
                    D: m[s].daysShort[e.getUTCDay()],
                    DD: m[s].days[e.getUTCDay()],
                    m: e.getUTCMonth() + 1,
                    M: m[s].monthsShort[e.getUTCMonth()],
                    MM: m[s].months[e.getUTCMonth()],
                    yy: n.toString().substring(2),
                    yyyy: n
                };
                r.dd = (r.d < 10 ? "0" : "") + r.d, r.mm = (r.m < 10 ? "0" : "") + r.m, e = [];
                for (var o = t.extend([], i.separators), h = 0, c = i.parts.length; c >= h; h++) o.length && e.push(o.shift()), e.push(r[i.parts[h]]);
                return e.join("")
            },
            headTemplate: '<thead><tr><th class="prev">&#171;</th><th colspan="5" class="datepicker-switch"></th><th class="next">&#187;</th></tr></thead>',
            contTemplate: '<tbody><tr><td colspan="7"></td></tr></tbody>',
            footTemplate: '<tfoot><tr><th colspan="7" class="today"></th></tr><tr><th colspan="7" class="clear"></th></tr></tfoot>'
        };
    y.template = '<div class="datepicker"><div class="datepicker-days"><table class=" table-condensed">' + y.headTemplate + "<tbody></tbody>" + y.footTemplate + '</table></div><div class="datepicker-months"><table class="table-condensed">' + y.headTemplate + y.contTemplate + y.footTemplate + '</table></div><div class="datepicker-years"><table class="table-condensed">' + y.headTemplate + y.contTemplate + y.footTemplate + "</table></div></div>", t.fn.datepicker.DPGlobal = y, t.fn.datepicker.noConflict = function () {
        return t.fn.datepicker = f, this
    }, t.fn.datepicker.version = "1.4.0", t(document).on("focus.datepicker.data-api click.datepicker.data-api", '[data-provide="datepicker"]', function (e) {
        var a = t(this);
        a.data("datepicker") || (e.preventDefault(), g.call(a, "show"))
    }), t(function () {
        g.call(t('[data-provide="datepicker-inline"]'))
    })
}(window.jQuery);