Officers.mPWD = function () {
    var self = this;
    self.PwdCurent = ko.observable();
    self.PwdRelace = ko.observable();
    self.Pwd = ko.observable();
    self.Email = ko.observable();
    self.PwdCheck = ko.observable();
};
Officers.mvPWD = function () {
    var self = this;
    self.mOfficers = ko.observable(new Officers.mPWD);
    self.GetAcc = function () {
        Utils.showWait(true);
        $.ajax({
            type: "GET",
            url: "/SYS_Officers/Remove/"
        }).done(function (data) {
            ko.mapping.fromJS(data, {}, self.mOfficers);
            if (data.success == false)
                Utils.notify("Thông báo", data.messaging, data.success ? '' : 'error');
        }).always(function () {
            Utils.showWait(false);
        });
    };
    self.Start = function () {
        self.GetAcc();
    };
}