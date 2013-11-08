var Console = new function () {
    var me = this;

    var skin;
    var log = [];

    me.openstring = "open console";
    var openstringmatch = "";

    var stringutil;

    me.Init = function () {
        ox.Log = me.Log;

        skin = $('#console');

        stringutil = new Stringutil();

        AddClosedEventListeners();
    };

    me.Log = function (message) {
        log.push(message);
    };


    me.Open = function () {
        skin.show();
        AddOpenEventListeners();
    };

    me.Close = function () {
        skin.hide();
        AddClosedEventListeners();
    };



    var AddClosedEventListeners = function () {
        $(window).unbind('keypress').keypress(OnWindowKeyPress);
    };

    var OnWindowKeyPress = function (e) {
        openstringmatch += e.char;

        if (me.openstring == openstringmatch) {
            openstringmatch = "";
            me.Open();
            return;
        }

        if (!stringutil.StartsWith(me.openstring, openstringmatch)) {
            openstringmatch = "";
        }
    };



    var AddOpenEventListeners = function () {

    };

}