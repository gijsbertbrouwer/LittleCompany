var Console = new function () {
    var me = this;

    var skin;
    var logsDOM;
    var logs = [];

    me.openstring = "open console";
    var openstringmatch = "";

    var commandinput;

    var stringutil;

    me.Init = function () {
        ox.Log = me.Log;

        skin = $('#console');
        logsDOM = skin.find('.logs');
        commandinput = skin.find('.commandlineinput');

        stringutil = new Stringutil();

        AddClosedEventListeners();

        for (var i = 0; i < 100; i++) {
            me.Log("TEST LOG" + i);
        }
    };

    me.Log = function (message) {
        logs.push(message);
    };


    me.Open = function () {
        skin.show();

        RenderLogs();

        AddOpenEventListeners();

        commandinput.focus();
    };

    me.Close = function () {
        skin.hide();
        AddClosedEventListeners();
    };

    

    var RenderLogs = function () {
        var i = 0, l = logs.length, log, logskin = "";

        for (i; i < l; i++) {
            log = logs[i];

            logskin += "<div class='log'>" + log + "</div>";
            
        }

        
        logsDOM.append(logskin);
        logsDOM.animate({ scrollTop: logsDOM[0].scrollHeight }, 0);
    };

    var HandleCommand = function (c) {

        logsDOM.append("<div class='log command'>" + c + "</div>");
        logsDOM.animate({ scrollTop: logsDOM[0].scrollHeight }, 100);
    };


    var AddClosedEventListeners = function () {
        $(window).unbind('keypress').keypress(OnWindowKeyPress);
    };

    var OnWindowKeyPress = function (e) {
        openstringmatch += e.char;

        if (me.openstring == openstringmatch) {
            openstringmatch = "";
            me.Open();
            return false;
        }

        if (!stringutil.StartsWith(me.openstring, openstringmatch)) {
            openstringmatch = "";
        }

    };



    var AddOpenEventListeners = function () {
        skin.find('#commandform').unbind('submit').submit(OnCommandSubmit);
    };

    var OnCommandSubmit = function () {
        var c = commandinput.val();
        HandleCommand(c);

        commandinput.val("");
        return false;
    };

}