﻿var Role = Role || {};
Role.mRole = function () {
    var self = this;
    self.Id = ko.observable();
    self.Role = ko.observable("");
    self.Note = ko.observable();
    self.IsEdit = ko.observable(false);
};
Role.mvRole = function () {
    var self = this;
    self.lstRole = ko.observableArray();
    self.LoadListRole = function () {
        Utils.showWait(true);
        $.ajax({
            type: "GET",
            url: Utils.url("/SYS_Role/GetListRole"),
        }).done(function (data) {
            if (data == null)
                return
            self.lstRole(Utils.MapArray(data, Role.mRole));
            var objnew = ko.observable(new Role.mRole);
            objnew().IsEdit(true)
            self.lstRole.push(objnew());
        }).always(function () {
            Utils.showWait(false);
        });
    };
    self.CancleChangeEdit = function (val) {
        val.IsEdit(false)
    };
    self.ChangeEdit = function (val) {
        ko.utils.arrayForEach(self.lstRole(), function (obj) {
            if (obj.Id())
                obj.IsEdit(false)
        });
        val.IsEdit(true)
    };
    self.SaveChange = function (val) {
        $.ajax({
            type: "POST",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            url: "/SYS_Role/AddOrUpdate",
            data: ko.toJSON({ model: val }),
        }).done(function (data) {
            self.LoadListRole();
            Utils.notify("Thông báo", data.messaging, data.success ? '' : 'error');
        });
    };
    self.Remove = function (d) {
        Utils.openConfirm('Xóa phân quyền này ?', function () {
            Utils.showWait(true);
            $.ajax({
                type: "DELETE",
                url: "/SYS_Role/Remove/" + d.Id()
            }).done(function (data) {
                self.LoadListRole();
                Utils.notify("Thông báo", data.messaging, data.success ? '' : 'error');
            }).always(function () {
                Utils.showWait(false);
            });
        });
    };
    self.Start = function () {
        ko.applyBindings(self, document.getElementById('roleViewId'));
        self.LoadListRole();
    };
};

