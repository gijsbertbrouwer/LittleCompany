var Addperson = function (classargs) {
    var me = this;

    var skin = $("#popup_addperson");

    var Init = function () {
        $('.popup').hide();

        skin.find('input[type="text"]').val("");

        $('.blanket').fadeIn(300);
        skin.slideDown(200);

        AddEventListeners();

        skin.find('input').first().focus();
    };

    var AddEventListeners = function () {
        skin.find('.cancel').unbind('click').click(OnCancelClick);
        skin.find('#form_addperson').unbind('submit').submit(OnSubmit);
    };

    var OnCancelClick = function () {
        HidePopup();
    };

    var OnSubmit = function () {
        if (!ValidateForm()) { return; }

        ox.data.CommunicateWithServer({
            methodUrl: 'Objects.asmx/AddPerson',
            data: {
                'personName': skin.find('.name').val() 
            },
            success: function (d) {

                if (d.ispositive) {
                    HidePopup();
                } else {
                    ox.Log("Addperson.OnSubmit() - Negative result from WM.");
                }
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