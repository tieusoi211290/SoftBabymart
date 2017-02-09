var Utils = {
    MapArray: function (rawArray, model) {
        var mappedData = ko.utils.arrayMap(rawArray, function (value) {
            var item = new model();
            ko.mapping.fromJS(value, {}, item);
            return item;
        });
        return mappedData;
    },
    showWait: function (show) {
        if (show) {
            $('.pageloading').show();
        }
        else {
            $('.pageloading').hide();
        }
    },
    openConfirm: function (message, callback) {
        $.modal.confirm(message, function () {
            callback(true)
        }, function () {
        })
    },
    notify: function (title, msg, error) {
        notify(title, msg, {
            system: false,
            vPos: 'bottom',
            hPos: 'right',
            autoClose: true,
            icon: '/Images/img/demo/icon.png',
            iconOutside: true,
            closeButton: true,
            showCloseOnHover: true,
            groupSimilar: true,
            typecss: error === 'error' ? 'red-gradient' : 'green-gradient'
        });

    },
    loadListNPP: function (callback) {
        Utils.showWait(true);
        $.ajax({
            type: "GET",
            url: "/NPP/GetList"
        }).done(function (data) {
            Utils.showWait(false);
            callback(data)
        }).fail(function () {
            Utils.showWait(false);
        });
    },
    loadGroupProduct: function (callback) {
        Utils.showWait(true);
        $.ajax({
            type: "GET",
            url: "/Product/GetListGroupProduct"
        }).done(function (data) {
            Utils.showWait(false);
            callback(data);
        }).fail(function () {
            Utils.showWait(false);
        });
    },
    loadListKho: function (callback) {
        Utils.showWait(true);
        $.ajax({
            type: "GET",
            url: "/KHO/GetList"
        }).done(function (data) {
            callback(data);
            Utils.showWait(false);
        }).fail(function () {
            Utils.showWait(false);
        });
    },
    loadListDvt: function (callback) {
        Utils.showWait(true);
        $.ajax({
            type: "GET",
            url: "/Dvt/GetList"
        }).done(function (data) {
            Utils.showWait(false);
            callback(data);
        }).fail(function () {
            Utils.showWait(false);
        });
    },
    addTabDynamic: function (title, url, idDiv) {
        if ($(idDiv).tabs('exists', title)) {
            $(idDiv).tabs('select', title);
        } else {
            $.post(url, function (result) {
                $(idDiv).tabs('add', {
                    title: title,
                    content: result,
                    closable: true
                });
            });

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
}