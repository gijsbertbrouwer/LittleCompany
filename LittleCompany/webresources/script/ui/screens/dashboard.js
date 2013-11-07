var Dashboard = function () {
    var me = this;

    var skin = $('.oxsection.dashboard');

    var Init = function () {
        if (ox.user.DataAvailable()) {
            OnDataAvailable();
        } else {
            setTimeout(Init, 200);
        }
    };

    me.Destroy = function () {

    };

    var OnDataAvailable = function () {
        RenderFavorites();
        skin.append("<div class='addcompanybutton' style='background-color: #ccc; border-radius: 10px; height: 30px; width: 120px;'>add company</div>");

        AddEventListeners();
    };

    var AddEventListeners = function () {


        skin.find('.star').unbind('hover').hover(OnStarOver, OnStarOut);
        
        skin.find('.addcompanybutton').unbind('click').click(OnAddCompanyButtonClick)
    };

    var OnAddCompanyButtonClick = function () {
        //via window custom event?
        new Addorganisation();
    };

    var OnStarOver = function () {
        var cnv = $(this).find('.starcanvas')[0];
        if (cnv.getContext) {
            var ctx = cnv.getContext('2d');
            DrawStarIconHover(ctx);
        } else {
            //TODO: canvas-unsupported code here - render png's
        }
    };

    var OnStarOut = function () {
        var cnv = $(this).find('.starcanvas')[0];
        if (cnv.getContext) {
            var ctx = cnv.getContext('2d');
            DrawStarIcon(ctx);
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
                    DrawOrganisationIcon(ctx);
                    return;
                }
                if (cnv.hasClass('file')) {
                    DrawFileIcon(ctx);
                    return;
                }
                if (cnv.hasClass('contactperson')) {
                    DrawContactpersonIcon(ctx);
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
                DrawStarIcon(ctx);
            } else {
                //TODO: canvas-unsupported code here - render png's
            }
        });



    };

    var DrawOrganisationIcon = function (ctx) {
        // layer1/Path
        ctx.save();
        ctx.beginPath();
        ctx.moveTo(40.0, 40.0);
        ctx.lineTo(0.0, 40.0);
        ctx.lineTo(0.0, 0.0);
        ctx.lineTo(40.0, 0.0);
        ctx.lineTo(40.0, 40.0);
        ctx.closePath();
        ctx.fillStyle = "rgb(173, 180, 32)";
        ctx.fill();

        // layer1/Group

        // layer1/Group/Path
        ctx.save();
        ctx.beginPath();
        ctx.moveTo(19.7, 13.2);
        ctx.lineTo(20.2, 13.2);
        ctx.lineTo(24.0, 26.5);
        ctx.lineTo(20.0, 30.0);
        ctx.lineTo(16.0, 26.4);
        ctx.lineTo(19.7, 13.2);
        ctx.closePath();
        ctx.fillStyle = "rgb(255, 255, 255)";
        ctx.fill();

        // layer1/Group/Path
        ctx.beginPath();
        ctx.moveTo(19.7, 12.8);
        ctx.lineTo(20.2, 12.8);
        ctx.lineTo(21.4, 10.0);
        ctx.lineTo(18.5, 10.0);
        ctx.lineTo(19.7, 12.8);
        ctx.closePath();
        ctx.fill();
        ctx.restore();
        ctx.restore();
    };

    var DrawFileIcon = function (ctx) {

        // layer1/Group
        ctx.save();

        // layer1/Group/Compound Path
        ctx.save();
        ctx.beginPath();

        // layer1/Group/Compound Path/Path
        ctx.moveTo(24.1, 15.9);
        ctx.lineTo(24.1, 0.0);
        ctx.lineTo(0.0, 0.0);
        ctx.lineTo(0.0, 40.0);
        ctx.lineTo(40.0, 40.0);
        ctx.lineTo(40.0, 15.9);
        ctx.lineTo(24.1, 15.9);
        ctx.closePath();
        ctx.fillStyle = "rgb(129, 53, 139)";
        ctx.fill();

        // layer1/Group/Path
        ctx.beginPath();
        ctx.moveTo(27.3, 12.7);
        ctx.lineTo(40.0, 12.7);
        ctx.lineTo(27.3, 0.0);
        ctx.lineTo(27.3, 12.7);
        ctx.closePath();
        ctx.fill();
        ctx.restore();
        ctx.restore();
    };

    var DrawContactpersonIcon = function (ctx) {

        // layer1/Path
        ctx.save();
        ctx.beginPath();
        ctx.moveTo(40.0, 20.0);
        ctx.bezierCurveTo(40.0, 31.0, 31.0, 40.0, 20.0, 40.0);
        ctx.bezierCurveTo(9.0, 40.0, 0.0, 31.0, 0.0, 20.0);
        ctx.bezierCurveTo(0.0, 9.0, 9.0, 0.0, 20.0, 0.0);
        ctx.bezierCurveTo(31.0, 0.0, 40.0, 9.0, 40.0, 20.0);
        ctx.closePath();
        ctx.fillStyle = "rgb(0, 162, 222)";
        ctx.fill();

        // layer1/Group

        // layer1/Group/Path
        ctx.save();
        ctx.beginPath();
        ctx.moveTo(30.0, 30.0);
        ctx.bezierCurveTo(30.0, 29.6, 30.0, 29.1, 29.9, 28.6);
        ctx.bezierCurveTo(29.8, 27.5, 29.3, 26.6, 28.4, 26.0);
        ctx.lineTo(28.3, 25.9);
        ctx.bezierCurveTo(28.0, 25.7, 23.9, 24.1, 23.2, 23.8);
        ctx.bezierCurveTo(22.4, 24.4, 21.3, 24.8, 20.1, 24.8);
        ctx.bezierCurveTo(18.9, 24.8, 17.8, 24.4, 17.0, 23.7);
        ctx.lineTo(16.9, 23.7);
        ctx.bezierCurveTo(16.9, 23.7, 12.0, 25.7, 11.7, 25.9);
        ctx.lineTo(11.6, 26.0);
        ctx.bezierCurveTo(10.7, 26.6, 10.2, 27.5, 10.1, 28.6);
        ctx.bezierCurveTo(10.0, 29.0, 10.0, 29.5, 10.0, 30.0);
        ctx.lineTo(30.0, 30.0);
        ctx.closePath();
        ctx.fillStyle = "rgb(255, 255, 255)";
        ctx.fill();

        // layer1/Group/Path
        ctx.beginPath();
        ctx.moveTo(25.3, 16.7);
        ctx.bezierCurveTo(24.8, 20.3, 22.5, 23.0, 20.1, 23.0);
        ctx.bezierCurveTo(17.7, 23.0, 15.4, 20.3, 14.9, 16.7);
        ctx.bezierCurveTo(14.5, 13.2, 16.8, 10.0, 20.1, 10.0);
        ctx.bezierCurveTo(23.5, 10.0, 25.8, 13.2, 25.3, 16.7);
        ctx.closePath();
        ctx.fill();
        ctx.restore();
        ctx.restore();
    };

    var DrawStarIcon = function (ctx) {

        ctx.clearRect(0, 0, 20, 20);
        // layer1/Group
        ctx.save();

        // layer1/Group/Path
        ctx.save();
        ctx.beginPath();
        ctx.moveTo(10.0, 15.5);
        ctx.lineTo(3.8, 20.0);
        ctx.lineTo(6.0, 12.4);
        ctx.lineTo(0.0, 7.6);
        ctx.lineTo(7.5, 7.5);
        ctx.lineTo(10.0, 0.0);
        ctx.lineTo(12.5, 7.5);
        ctx.lineTo(20.0, 7.6);
        ctx.lineTo(14.0, 12.4);
        ctx.lineTo(16.2, 20.0);
        ctx.lineTo(10.0, 15.5);
        ctx.closePath();
        ctx.fillStyle = "rgb(255, 214, 109)";
        ctx.fill();

        // layer1/Group/Group

        // layer1/Group/Group/Path
        ctx.save();
        ctx.beginPath();
        ctx.moveTo(13.1, 8.1);
        ctx.lineTo(19.2, 8.3);
        ctx.lineTo(20.0, 7.6);
        ctx.lineTo(12.9, 7.5);
        ctx.lineTo(13.1, 8.1);
        ctx.closePath();
        ctx.fillStyle = "rgb(217, 217, 217)";
        ctx.fill();

        // layer1/Group/Group/Path
        ctx.beginPath();
        ctx.moveTo(6.7, 13.1);
        ctx.lineTo(6.0, 12.5);
        ctx.lineTo(3.8, 20.0);
        ctx.lineTo(4.9, 19.2);
        ctx.lineTo(6.7, 13.1);
        ctx.closePath();
        ctx.fill();

        // layer1/Group/Group/Path
        ctx.beginPath();
        ctx.moveTo(8.2, 8.1);
        ctx.lineTo(10.4, 1.3);
        ctx.lineTo(10.0, 0.0);
        ctx.lineTo(7.5, 7.5);
        ctx.lineTo(0.0, 7.6);
        ctx.lineTo(0.8, 8.3);
        ctx.lineTo(8.2, 8.1);
        ctx.closePath();
        ctx.fill();
        ctx.restore();
        ctx.restore();
        ctx.restore();
    };

    var DrawStarIconHover = function (ctx) {

        ctx.clearRect(0, 0, 20, 20);
        // layer1/Group
        ctx.save();

        // layer1/Group/Path
        ctx.save();
        ctx.beginPath();
        ctx.moveTo(10.0, 15.5);
        ctx.lineTo(3.8, 20.0);
        ctx.lineTo(6.0, 12.4);
        ctx.lineTo(0.0, 7.6);
        ctx.lineTo(7.5, 7.5);
        ctx.lineTo(10.0, 0.0);
        ctx.lineTo(12.5, 7.5);
        ctx.lineTo(20.0, 7.6);
        ctx.lineTo(14.0, 12.4);
        ctx.lineTo(16.2, 20.0);
        ctx.lineTo(10.0, 15.5);
        ctx.closePath();
        ctx.fillStyle = "rgb(255, 214, 109)";
        ctx.fill();

        // layer1/Group/Group

        // layer1/Group/Group/Path
        ctx.save();
        ctx.beginPath();
        ctx.moveTo(13.1, 8.1);
        ctx.lineTo(19.2, 8.3);
        ctx.lineTo(20.0, 7.6);
        ctx.lineTo(12.9, 7.5);
        ctx.lineTo(13.1, 8.1);
        ctx.closePath();
        ctx.fillStyle = "rgb(174, 174, 174)";
        ctx.fill();

        // layer1/Group/Group/Path
        ctx.beginPath();
        ctx.moveTo(6.7, 13.1);
        ctx.lineTo(6.0, 12.5);
        ctx.lineTo(3.8, 20.0);
        ctx.lineTo(4.9, 19.2);
        ctx.lineTo(6.7, 13.1);
        ctx.closePath();
        ctx.fill();

        // layer1/Group/Group/Path
        ctx.beginPath();
        ctx.moveTo(8.2, 8.1);
        ctx.lineTo(10.4, 1.3);
        ctx.lineTo(10.0, 0.0);
        ctx.lineTo(7.5, 7.5);
        ctx.lineTo(0.0, 7.6);
        ctx.lineTo(0.8, 8.3);
        ctx.lineTo(8.2, 8.1);
        ctx.closePath();
        ctx.fill();
        ctx.restore();
        ctx.restore();
        ctx.restore();
    };

    Init();
}