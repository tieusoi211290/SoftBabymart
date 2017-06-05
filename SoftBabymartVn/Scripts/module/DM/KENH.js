var KENH = KENH || {};
KENH.mKENH = function () {
    var self = this;
    self.Id = ko.observable();
    self.Kenh = ko.observable("");
    self.Note = ko.observable();
    self.IsEdit = ko.observable(false);
};
KENH.mvKENH = function () {
    var self = this;
    self.mKenh = ko.observable(new KENH.mKENH);
    self.lstKenh = ko.observableArray();
    self.LoadListKenh = function () {
        Utils.showWait(true);
        $.ajax({
            type: "GET",
            url: Utils.url("/KENH/LoadListKenh"),
        }).done(function (data) {
            if (data == null)
                return
            self.lstKenh(Utils.MapArray(data, KENH.mKENH));
            var objnew = ko.observable(new KENH.mKENH);
            objnew().IsEdit(true)
            self.lstKenh.push(objnew());
        }).always(function () {
            Utils.showWait(false);
        });
    };
    self.CancleChangeEdit = function (val) {
        val.IsEdit(false)
    };
    self.ChangeEdit = function (val) {
        ko.utils.arrayForEach(self.lstKenh(), function (obj) {
            if (obj.Id())
                obj.IsEdit(false)
        });
        val.IsEdit(true)
    };
    self.SaveChange = function (val) {
        if (val.Id()) {
            $.ajax({
                type: "PUT",
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                url: "/KENH/Edit",
                data: ko.toJSON({ model: val }),
            }).done(function (data) {
                self.LoadListKenh();
                Utils.notify("Thông báo", data.messaging, data.success ? '' : 'error');
            }).fail(function (err) {
                viewmodel.reset();
            });
        } else {
            $.ajax({
                type: "POST",
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                url: "/KENH/Add",
                data: ko.toJSON({ model: val }),
            }).done(function (data) {
                self.LoadListKenh();
                Utils.notify("Thông báo", data.messaging, data.success ? '' : 'error')
            }).fail(function (err) {
                viewmodel.reset();
            });
        }
    };
    self.Remove = function (d) {
        Utils.openConfirm('Xóa kênh này ?', function () {
            Utils.showWait(true);
            $.ajax({
                type: "DELETE",
                url: "/KENH/Remove/" + d.Id()
            }).done(function (data) {
                self.LoadListKenh();
                Utils.notify("Thông báo", data.messaging, data.success ? '' : 'error');
            }).always(function () {
                Utils.showWait(false);
            });
        });
    };
    self.Start = function () {
        ko.applyBindings(self, document.getElementById('kenhViewId'));
        debugger
        self.LoadListKenh();
    };
};

