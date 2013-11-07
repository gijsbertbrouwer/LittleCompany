var Addorganisation = function (classargs) {
    var me = this;

    var skin = $("#popup_addorganisation");

    var Init = function () {
        $('.popup').hide();

        $('.blanket').fadeIn(300);
        skin.slideDown(200);

        AddEventListeners();

        skin.find('input').first().focus();
    };

    var AddEventListeners = function () {
        skin.find('.cancel').unbind('click').click(OnCancelClick);
        skin.find('#form_addorganisation').unbind('click').click(OnSubmit);
    };

    var OnCancelClick = function () {
        HidePopup();
    };

    var OnSubmit = function () {
        if (!ValidateForm()) { return; }

        ox.data.CommunicateWithServer({
            methodUrl: 'Objects.asmx/AddOrganisation',
            data: {
                'organisationName': skin.find('.name').val()
            },
            success: function (d) {

                if (d.ispositive) {
                    HidePopup();
                } else {
                    //TODO: handle errors
                    alert('error');
                }
            },
            error: function (d) {
                //TODO: handle errors
                alert('error: ' + d);
            }
        });

        return false;
    };

    var ValidateForm = function () {
        return (skin.find('.name').val());
    };

    var HidePopup = function () {
        skin.slideUp(200);
        $('.blanket').fadeOut(300, function () {
            $('.blanket').hide();
        });
    };

    Init();
}