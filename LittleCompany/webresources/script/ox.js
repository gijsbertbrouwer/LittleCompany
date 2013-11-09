var ox;
var urlparams;

var OxFramework = function () {
    //get url parameters
    urlParams = {};
    var hasharr = window.location.hash.substr(1).split("&");
    var i = 0, l = hasharr.length, hp;
    for (i; i < l; i++) {
        hp = hasharr[i].split("=");
        urlParams[hp[0]] = hp[1];
    }
    //empty hash
    //window.location.hash = "";

    

    //initialise 
    ox = {};
    ox.user = new User();
    ox.data = new Data();
    ox.ui = new Ui();

    //create event
    ox.Event = function (type, screen, data) {
        if (typeof (data) != 'object') { data = { 'data': data }; }
        data.screen = screen;
        


        //// data should be in a 'details' object
        //var evt = new CustomEvent(type, {
        //    detail: data
        //});

        var evt = document.createEvent("CustomEvent");
        evt.initCustomEvent(type, false, false, data);

        window.dispatchEvent(evt);

    }

    ox.data.Init();
    ox.user.Init();
    ox.ui.Init();

    return ox;

}