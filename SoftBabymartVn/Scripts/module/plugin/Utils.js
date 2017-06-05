window.waitcount = 0;
var functionReload = [];
var Utils = {
    MapArray: function (rawArray, model) {
        var mappedData = ko.utils.arrayMap(rawArray, function (value) {
            var item = new model();
            ko.mapping.fromJS(value, {}, item);
            return item;
        });
        return mappedData;
    },
    confirm: function (message, callback) {
        bootbox.confirm(message, function (result) {
            if (result) {
                callback()
            }
        });
    },
    notify: function (title, msg, type) {
        var str = "error";
        switch (type) {
            case "success":
                str = "success";
                break;
            case "error":
                str = "error";
                break;
            case "warning":
                str = "warning";
                break;
        };
        $.gritter.add({
            // (string | mandatory) the heading of the notification
            title: title,
            // (string | mandatory) the text inside the notification
            text: msg,
            class_name: 'gritter-' + str
        });
    },
    modal: function (s) {

    },
    addTabDynamic: function (title, url, idDiv, isReload, data) {
        if ($(idDiv).tabs('exists', title) && isReload == undefined) {
            $(idDiv).tabs('select', title);
        } else {
            if (isReload)
                $(idDiv).tabs('close', title);
            Utils.showWait(true);
            $.ajax({
                type: "POST",
                url: url,
                cache: false,
                contentType: 'application/json; charset=utf-8',
                data: ko.toJSON(data),
            }).done(function (result) {
                $(idDiv).tabs('add', {
                    title: title,
                    content: result,
                    closable: true
                });

            }).always(function () {
                Utils.showWait(false);
            });

            //        if (data != undefined) {
            //        Utils.showWait(true);
            //        $.ajax({
            //            type: "POST",
            //            url: url,
            //            cache: false,
            //            contentType: 'application/json; charset=utf-8',
            //            data: ko.toJSON(data),
            //        }).done(function (result) {
            //            $(idDiv).tabs('add', {
            //                title: title,
            //                content: result,
            //                closable: true
            //            });

            //        }).always(function () {
            //            Utils.showWait(false);
            //        });
            //    } else {
            //        Utils.showWait(true);
            //        $.post(url, function (result) {
            //            $(idDiv).tabs('add', {
            //                title: title,
            //                content: result,
            //                closable: true
            //            });
            //            Utils.showWait(false);
            //        });
            //    }
        }
    },

    showWait: function (show, delay) {
        var overlay = "<div class='overlay'></div><div class='pageloading'><img src='../Images/img/loading.gif' /></div>";
        if (show) {
            $('body').append(overlay);
            window.waitcount = window.waitcount + 1;
            $(".pageloading").show();
            $(".overlay").show();
        }
        else {
            window.waitcount = window.waitcount - 1;
            if (window.waitcount == 0) {
                var timeout = 0;
                if (delay != undefined && delay > 0) {
                    timeout = delay;
                }
                setTimeout(function () {
                    $(".pageloading").hide().remove();
                    $(".overlay").hide().remove();
                }, timeout);

            }
        }
    },
    datelong: function (value) {
        if (value) {
            var date;
            if (value.constructor === Date)
                date = value;
            else
                date = new Date(parseInt(value.substr(6)));
            return Globalize.format(date, 'f');
        }
    },
    dateshort: function (value) {
        if (value) {
            var date;
            if (value.constructor === Date)
                date = value;
            else
                date = new Date(parseInt(value.substr(6)));
            return Globalize.format(date, 'd');
        }
    },
    url: function (path) {
        return (location.origin + path);
    },
    randomString: function (len, charSet) {
        charSet = charSet || 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
        var randomString = '';
        for (var i = 0; i < len; i++) {
            var randomPoz = Math.floor(Math.random() * charSet.length);
            randomString += charSet.substring(randomPoz, randomPoz + 1);
        }
        return randomString;
    },

    loadListCatalog: function (callback) {
        Utils.showWait(true);
        $.ajax({
            type: "GET",
            url: "/ChannelCatalog/LoadLstCatalog_Channel"
        }).done(function (data) {
            Utils.showWait(false);
            callback(data);
        }).fail(function () {
            Utils.showWait(false);
        });

    },
    loadListSuppliers: function (callback) {
        Utils.showWait(true);
        $.ajax({
            type: "GET",
            url: "/Suppliers/LoadLstSuppliers"
        }).done(function (data) {
            Utils.showWait(false);
            callback(data);
        }).fail(function () {
            Utils.showWait(false);
        });

    },
    loadListUnit: function (callback) {
        Utils.showWait(true);
        $.ajax({
            type: "GET",
            url: "/Unit/LoadLstUnit"
        }).done(function (data) {
            Utils.showWait(false);
            callback(data);
        }).fail(function () {
            Utils.showWait(false);
        });

    },
    loadListChannel: function (callback) {
        Utils.showWait(true);
        $.ajax({
            type: "GET",
            url: "/Common/LoadLstChannel"
        }).done(function (data) {
            Utils.showWait(false);
            callback(data.Data);
        }).fail(function () {
            Utils.showWait(false);
        });
    },
    LoadLstBranches: function (callback) {
        Utils.showWait(true);
        $.ajax({
            type: "GET",
            url: "/Common/LoadLstBranches"
        }).done(function (data) {
            Utils.showWait(false);
            callback(data);
        }).fail(function () {
            Utils.showWait(false);
        });
    },
    functionReloadPush: function (obj) {
        functionReload.push(obj);
    },
    functionReloadAction: function () {
        functionReload.forEach(function (entry) {
            entry.Start();
        });
    },
}