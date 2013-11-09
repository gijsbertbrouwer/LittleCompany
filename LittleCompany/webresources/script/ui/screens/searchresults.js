var Searchresults = function (classargs) {
    var me = this;

    var skin = $('.oxsection.searchresults');

    var Init = function () {
        var query = classargs.query;

        skin.append("searching for '" + query + "'.")
    };

    me.Destroy = function () {

    };

    Init();
};