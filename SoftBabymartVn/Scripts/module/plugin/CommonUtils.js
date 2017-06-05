var CommonUtils = {
    SetDayFormat: function (inputDate, defaultFormat) {
        var date;
        if (inputDate.constructor === Date)
            date = new Date(inputDate);
        else
            date = new Date(parseInt(inputDate.substr(6)));
        date.setHours(0, 0, 0, 0);

        var today = new Date();
        today.setHours(0, 0, 0, 0);

        var yesterday = new Date();
        yesterday.setHours(0, 0, 0, 0);
        yesterday.setDate(yesterday.getDate() - 1);

        var tomorrow = new Date();
        tomorrow.setHours(0, 0, 0, 0);
        tomorrow.setDate(tomorrow.getDate() + 1);

        var strDate = "";
        var plusTime = false;

        switch (date.getTime()) {

            case today.getTime():
                strDate = Globalize.culture().calendar.today;
                if (defaultFormat && defaultFormat == 'f') plusTime = true;
                break;

            case yesterday.getTime():
                strDate = Globalize.culture().calendar.yesterday;
                if (defaultFormat && defaultFormat == 'f') plusTime = true;
                break;

            case tomorrow.getTime():
                strDate = Globalize.culture().calendar.tomorrow;
                if (defaultFormat && defaultFormat == 'f') plusTime = true;
                break;

            default:
                strDate = defaultFormat ? Globalize.format(inputDate, defaultFormat) : inputDate;
                break;
        }

        if (plusTime) {
            strDate += ' ' + Globalize.format(inputDate, 't');
        }

        return strDate;
    },
    SetTimeFormat: function (inputDate, defaultFormat, isLowerCaseDefault) {
        var diffSeconds = ((new Date()).getTime() - inputDate.getTime()) / 1000;
        var diffMinutes = Math.floor(diffSeconds / 60);

        if (diffMinutes < 1)
            return Globalize.culture().calendar.justNow;
        else if (diffMinutes < 2)
            return Globalize.culture().calendar.oneMinuteAgo;
        else if (diffMinutes < 60)
            return Globalize.culture().calendar.fewMinutesAgo.replace('m', diffMinutes);
        else {
            if (!defaultFormat)
                defaultFormat = Globalize.culture().calendar.patterns.C7;
            if (isLowerCaseDefault)
                return Globalize.format(inputDate, defaultFormat).toLowerCase();
            else
                return Globalize.format(inputDate, defaultFormat);
        }
    },
    formatDateTimeShort: function (value) {
         if (value) {
            var date;
            if (value.constructor === Date)
                date = value;
            else
                date = new Date(parseInt(value.substr(6)));
            return CommonUtils.SetDayFormat(date, 'f');
        }
        else
            return null;
    },
}