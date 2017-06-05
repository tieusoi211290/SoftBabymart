var Common = Common || {};
Common.mvBranches = function () {
    var self = this;
    self.Option = ko.observableArray();
    self.Channel = ko.observableArray();
    self.SelectBranches = ko.observable(0);
    self.SelectChannel = ko.observable(0);

    self.SelectBranches.subscribe(function (val) {
        if (val)
            self.Channel(val.soft_Channel);
    });

    self.SelectChannel.subscribe(function (val) {
        if (val) {
            $.ajax({
                type: "GET",
                url: Utils.url("/Partial/SetChannel"),
                cache: false,
                data: { ChannelId: val.Id }
            }).always(function () {
                Utils.functionReloadAction();
            });
        }
    });
};

Common.mvCommon = function () {
    var self = this;
    self.mvBranches = ko.observable(new Common.mvBranches);

    self.ConfigSystem = function () {
        $.ajax({
            type: "GET",
            url: Utils.url("/Common/GetConfigSys"),
            cache: false,
            dataType: 'json',
        }).done(function (data) {
            if (!data.isError) {
                self.mvBranches().Option(data.Data.User.Branches);
                var braches = ko.utils.arrayFirst(self.mvBranches().Option(), function (o) {
                    return o.BranchesId == data.Data.User.BranchesId;
                });
                self.mvBranches().SelectBranches(braches);
            } else
                Utils.notify("Thông báo", data.messaging, 'error');
        });
    };

    self.Start = function () {
        ko.applyBindings(self);
        self.ConfigSystem();
    };
}