var Item = function (classargs) {
    var me = this;

    var skin = $('.oxsection.item');

    var Init = function () {
        //id, name, datatypeid

        skin.append("object page for '" + classargs.name + "'.")
    };

    me.Destroy = function () {

    };

    Init();
}