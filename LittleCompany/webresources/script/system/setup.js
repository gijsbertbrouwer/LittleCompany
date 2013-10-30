//entrance point for setting up javascript for the framework

var ox;

$(document).ready(function () {
    //initialise
    ox = new Ox();
    
    ox.data.data.GetDataFromServer({
        "methodUrl": "HelloWorld",
        "success": function (d) {
            debugger;
        }
    });
});

/***

    This class should provide access to all functionalities in the system: 
    initialise the most rudimentary form of security, data communication, logging and a console.

***/

//de O van ondor, de x omdat ox wel cool klinkt, en omdat een os ook altijd de wagen trekt :)
function Ox() {
    var me = this;

    me.console = new Console();
    me.Log = me.console.Log;

    me.data = {};
    me.data.security = {};
    me.data.security.securityToken = function() { return { 'token': 'test' }; }
    me.data.data = new Data();

};