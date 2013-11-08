var Stringutil = function () {
    var me = this;

    me.EndsWith = function(str, suffix) {
        return str.indexOf(suffix, str.length - suffix.length) !== -1;
    }

    me.StartsWith = function (str, prefix) {
        return str.indexOf(prefix) == 0;
    }
}