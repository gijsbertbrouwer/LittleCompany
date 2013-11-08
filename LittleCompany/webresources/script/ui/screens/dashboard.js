var Dashboard = function () {
    var me = this;

    var skin = $('.oxsection.dashboard');

    var canvasrenderer;

    var Init = function () {
        canvasrenderer = new Canvasrenderer();

        InitData();
    };

    var InitData = function () {

        if (ox.user.DataAvailable()) {
            OnDataAvailable();
        } else {
            setTimeout(InitData, 200);
        }
    };

    me.Destroy = function () {

    };

    var OnDataAvailable = function () {
        RenderFavorites();

        Mainmenu.AddButton({
            caption: "Add organisation",
            action: OnAddOrganisationButtonClick,
            icon: "O",
            section: ""
        });
        
        Mainmenu.AddButton({
            caption: "Add person",
            action: OnAddPersonButtonClick,
            icon: "P",
            section: ""
        });


        AddEventListeners();
    };

    var AddEventListeners = function () {
        skin.find('.star').unbind('hover').hover(OnStarOver, OnStarOut);
    };

    var OnAddOrganisationButtonClick = function () { 
        //via window custom event?
        new Addorganisation();
    };

    var OnAddPersonButtonClick = function () {
        //via window custom event?
        new Addperson();
    };

    var OnStarOver = function () {
        var cnv = $(this).find('.starcanvas')[0];
        if (cnv.getContext) {
            var ctx = cnv.getContext('2d');
            canvasrenderer.DrawStarIconHover(ctx);
        } else {
            //TODO: canvas-unsupported code here - render png's
        }
    };

    var OnStarOut = function () {
        var cnv = $(this).find('.starcanvas')[0];
        if (cnv.getContext) {
            var ctx = cnv.getContext('2d');
            canvasrenderer.DrawStarIcon(ctx);
        } else {
            //TODO: canvas-unsupported code here - render png's
        }
    };

    var RenderFavorites = function () {
        var favs = ox.user.GetFavorites();
        if (!favs || !favs.length) { return; }  //TODO: log


        var i = 0, l = favs.length, fav, favskin = "";
        for (i; i < l; i++) {
            fav = favs[i];

            /*
                public int id { get; set; }
                public int datatypeid { get; set; }
                public string datatypecaption { get; set; }
                public string name { get; set; }
            */

            switch (fav.datatypeid) {
                case 1:
                    fav.datatype = "organisation"
                    break;
                case 2:
                    fav.datatype = "contactperson"
                    break;
                case 3:
                    fav.datatype = "file"
                    break;
                default:
                    fav.datatype = ""
                    break;
            }

            favskin += "<div class='starred-item " + fav.datatype + "' clickid='" + fav.id + "'>" +
                "<table><tr>" +
                    "<td class='icon'>" +
                        "<canvas class='iconcanvas " + fav.datatype + "' width='40' height='40'></canvas>" +
                    "</td><td class='itemtext'>" +
                        "<div class='itemtextcontent'>" +
                            "<div class='itemtitle'>" + fav.name + "</div>" +
                            "<div class='itemtype'>" + fav.datatypecaption + "</div>" +
                        "</div>" +
                    "</td><td class='star'>" +
                        "<canvas class='starcanvas' width='20' height='20'></canvas>" +
                    "</td></tr>" +
                "</table>" +
            "</div>";
        }

        skin.find("#favorites-list").html(favskin);

        //render type icons
        skin.find('.iconcanvas').each(function (i, cnv) {
            if (cnv.getContext) {
                var ctx = cnv.getContext('2d');
                cnv = $(cnv);
                // drawing code here
                if (cnv.hasClass('organisation')) {
                    canvasrenderer.DrawOrganisationIcon(ctx);
                    return;
                }
                if (cnv.hasClass('file')) {
                    canvasrenderer.DrawFileIcon(ctx);
                    return;
                }
                if (cnv.hasClass('contactperson')) {
                    canvasrenderer.DrawContactpersonIcon(ctx);
                    return;
                }
            } else {
                //TODO: canvas-unsupported code here - render png's
            }
        });

        //render star icons
        skin.find('.starcanvas').each(function (i, cnv) {
            if (cnv.getContext) {
                var ctx = cnv.getContext('2d');
                canvasrenderer.DrawStarIcon(ctx);
            } else {
                //TODO: canvas-unsupported code here - render png's
            }
        });



    };

    Init();
}