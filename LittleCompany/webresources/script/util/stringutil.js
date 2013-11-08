var Stringutil = function () {
    var me = this;

    me.EndsWith = function(str, suffix) {
        return str.indexOf(suffix, str.length - suffix.length) !== -1;
    }
}