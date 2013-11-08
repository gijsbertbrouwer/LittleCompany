var Mainmenu = new function () {
    var me = this;

    var skin;

    var buttons = []

    me.Init = function () {
        skin = $('#navigation');
    };

    me.Reset = function () {
        buttons = [];
        skin.empty();        
    };

    me.AddButton = function (b) {
        //b { caption, action, icon, section }

        if (!b) {
            Console.Log("Mainmenu.AddButton() - function called without data.");
            return;
        }

        if (!b.caption && !b.icon) {
            Console.Log("Mainmenu.AddButton() - button added without caption or icon.");
            return;
        }

        if (!b.section) { b.section = 'general'; }

        RenderButton(b);
    };

    var RenderButton = function (b) {

        buttons.push(b);

        var section = skin.find('.section.' + b.section);
        if (!section.length) {
            skin.append("<div class='section " + b.section + "'></div>");
            section = skin.find('.section.' + b.section);
        }


        section.append("<div class='button' clickid='" + (buttons.length - 1) + "'><div class='icon " + ((b.icon && b.icon.length == 1) ? "texticon" : "") + "'>" + b.icon + "</div><div class='caption'>" + b.caption + "</div></div>");

        AddEventListeners();

    };

    var AddEventListeners = function () {
        skin.find('.button').unbind('click').click(OnButtonClick);
    };

    var OnButtonClick = function () {
        var bid = $(this).attr('clickid');

        if (!bid) {
            ox.Log("Mainmenu.OnButtonClick() - button clicked without clickid. Cannot map callback function.");
            return;
        }

        var b = buttons[bid];
        if (b && typeof (b.action) == 'function') {
            b.action();
            return;
        }

        ox.Log("Mainmenu.OnButtonClick() - button missing action.");

    };

}