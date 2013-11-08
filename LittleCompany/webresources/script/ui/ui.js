var Ui = function () {
    
    var me = this;
    var openscreenclass;

    me.Init = function () {
        //handle all oxsections
        $('.oxsection').hide();

        //header, navigation
        InitScreen();

        //console
        new Console();

        //main search
        new Mainsearch();
    }

    me.Navigate = function (to) {
        //hide all screens
        $('.oxsection').hide();

        //remove old screen js
        if (openscreenclass) {
            if (typeof (openscreenclass.Destroy) == 'function') {
                openscreenclass.Destroy();
                openscreenclass = undefined;
            }
        }

        var dom = $('.oxsection.' + to); 
        if (dom.length) {
            dom.show();

            //instantiate new screen js
            var ns = to;
            ns = ns.substr(0, 1).toUpperCase() + ns.substr(1);
            ns = window[ns];

            if (typeof (ns) == 'function') {
                openscreenclass = new ns();
            }

        }
    };


    var InitScreen = function () {
        //header, navigation
        var h = $('#header');
        var n = $('#navigation');
        n.height($(window).height() - h.height());

        
        var cnv = $('#ondorlogocanvas')[0];
        if (cnv.getContext) {
            var ctx = cnv.getContext('2d');
            RenderOndorLogoCanvas(ctx);
        } else {
            //TODO: canvas-unsupported code here - render png's
        }

        AddEventListeners();
    };

    var AddEventListeners = function () {
        //todo
    };



    var RenderOndorLogoCanvas = function (ctx) {

        var gradient;

        // layer1/Group
        ctx.save();

        // layer1/Group/Group
        ctx.save();

        // layer1/Group/Group/Compound Path
        ctx.save();
        ctx.beginPath();

        // layer1/Group/Group/Compound Path/Path
        ctx.moveTo(72.8, 7.5);
        ctx.bezierCurveTo(75.6, 10.3, 77.0, 13.7, 77.0, 17.6);
        ctx.bezierCurveTo(77.0, 21.5, 75.6, 24.9, 72.8, 27.7);
        ctx.bezierCurveTo(70.0, 30.5, 66.7, 31.9, 62.7, 31.9);
        ctx.bezierCurveTo(58.8, 31.9, 55.4, 30.5, 52.6, 27.7);
        ctx.bezierCurveTo(49.9, 24.9, 48.5, 21.5, 48.5, 17.6);
        ctx.bezierCurveTo(48.5, 13.7, 49.9, 10.3, 52.6, 7.5);
        ctx.bezierCurveTo(55.4, 4.7, 58.8, 3.3, 62.7, 3.3);
        ctx.bezierCurveTo(66.7, 3.3, 70.0, 4.7, 72.8, 7.5);
        ctx.closePath();

        // layer1/Group/Group/Compound Path/Path
        ctx.moveTo(62.7, 6.7);
        ctx.bezierCurveTo(59.7, 6.7, 57.2, 7.8, 55.1, 9.9);
        ctx.bezierCurveTo(52.9, 12.0, 51.9, 14.6, 51.9, 17.6);
        ctx.bezierCurveTo(51.9, 20.6, 52.9, 23.1, 55.1, 25.3);
        ctx.bezierCurveTo(57.2, 27.4, 59.7, 28.5, 62.7, 28.5);
        ctx.bezierCurveTo(65.7, 28.5, 68.3, 27.4, 70.4, 25.3);
        ctx.bezierCurveTo(72.5, 23.1, 73.6, 20.6, 73.6, 17.6);
        ctx.bezierCurveTo(73.6, 14.6, 72.5, 12.0, 70.4, 9.9);
        ctx.bezierCurveTo(68.3, 7.8, 65.7, 6.7, 62.7, 6.7);
        ctx.closePath();
        ctx.fillStyle = "rgb(255, 255, 255)";
        ctx.fill();

        // layer1/Group/Group/Compound Path
        ctx.beginPath();

        // layer1/Group/Group/Compound Path/Path
        ctx.moveTo(83.6, 18.4);
        ctx.lineTo(83.6, 31.5);
        ctx.lineTo(80.2, 31.5);
        ctx.lineTo(80.2, 13.7);
        ctx.lineTo(83.6, 13.7);
        ctx.bezierCurveTo(84.0, 13.1, 85.2, 12.8, 87.3, 12.8);
        ctx.bezierCurveTo(89.4, 12.8, 91.1, 13.4, 92.5, 14.8);
        ctx.bezierCurveTo(93.9, 16.2, 94.6, 17.9, 94.6, 19.9);
        ctx.lineTo(94.6, 31.5);
        ctx.lineTo(91.3, 31.5);
        ctx.lineTo(91.3, 20.1);
        ctx.bezierCurveTo(91.3, 19.0, 90.9, 18.1, 90.1, 17.3);
        ctx.bezierCurveTo(89.3, 16.5, 88.3, 16.1, 87.3, 16.1);
        ctx.bezierCurveTo(86.2, 16.1, 85.2, 16.5, 84.4, 17.3);
        ctx.bezierCurveTo(84.0, 17.7, 83.7, 18.1, 83.6, 18.4);
        ctx.closePath();
        ctx.fill();

        // layer1/Group/Group/Compound Path
        ctx.beginPath();

        // layer1/Group/Group/Compound Path/Path
        ctx.moveTo(117.8, 31.5);
        ctx.lineTo(114.4, 31.5);
        ctx.lineTo(114.4, 29.9);
        ctx.bezierCurveTo(112.9, 31.3, 110.9, 32.0, 108.1, 32.0);
        ctx.bezierCurveTo(105.4, 32.0, 103.1, 31.0, 101.2, 29.1);
        ctx.bezierCurveTo(99.2, 27.2, 98.3, 24.9, 98.3, 22.1);
        ctx.bezierCurveTo(98.3, 19.4, 99.2, 17.1, 101.2, 15.2);
        ctx.bezierCurveTo(103.1, 13.2, 105.4, 12.3, 108.1, 12.3);
        ctx.bezierCurveTo(110.9, 12.3, 112.9, 13.0, 114.4, 14.5);
        ctx.lineTo(114.4, 3.7);
        ctx.lineTo(117.8, 3.7);
        ctx.lineTo(117.8, 31.5);
        ctx.closePath();

        // layer1/Group/Group/Compound Path/Path
        ctx.moveTo(112.7, 17.6);
        ctx.bezierCurveTo(111.4, 16.4, 109.9, 15.7, 108.1, 15.7);
        ctx.bezierCurveTo(106.4, 15.7, 104.9, 16.4, 103.6, 17.6);
        ctx.bezierCurveTo(102.4, 18.9, 101.7, 20.4, 101.7, 22.1);
        ctx.bezierCurveTo(101.7, 23.9, 102.4, 25.4, 103.6, 26.7);
        ctx.bezierCurveTo(104.9, 27.9, 106.4, 28.5, 108.1, 28.5);
        ctx.bezierCurveTo(109.9, 28.5, 111.4, 27.9, 112.7, 26.6);
        ctx.bezierCurveTo(113.6, 25.7, 114.2, 24.7, 114.4, 23.6);
        ctx.lineTo(114.4, 20.7);
        ctx.bezierCurveTo(114.2, 19.6, 113.6, 18.5, 112.7, 17.6);
        ctx.closePath();
        ctx.fill();

        // layer1/Group/Group/Compound Path
        ctx.beginPath();

        // layer1/Group/Group/Compound Path/Path
        ctx.moveTo(130.6, 12.3);
        ctx.bezierCurveTo(133.4, 12.3, 135.7, 13.2, 137.6, 15.2);
        ctx.bezierCurveTo(139.5, 17.1, 140.5, 19.4, 140.5, 22.1);
        ctx.bezierCurveTo(140.5, 24.9, 139.5, 27.2, 137.6, 29.1);
        ctx.bezierCurveTo(135.7, 31.0, 133.4, 32.0, 130.6, 32.0);
        ctx.bezierCurveTo(127.9, 32.0, 125.6, 31.0, 123.7, 29.1);
        ctx.bezierCurveTo(121.7, 27.2, 120.8, 24.9, 120.8, 22.1);
        ctx.bezierCurveTo(120.8, 19.4, 121.7, 17.1, 123.7, 15.2);
        ctx.bezierCurveTo(125.6, 13.2, 127.9, 12.3, 130.6, 12.3);
        ctx.closePath();

        // layer1/Group/Group/Compound Path/Path
        ctx.moveTo(126.1, 17.6);
        ctx.bezierCurveTo(124.8, 18.9, 124.2, 20.4, 124.2, 22.1);
        ctx.bezierCurveTo(124.2, 23.9, 124.8, 25.4, 126.1, 26.7);
        ctx.bezierCurveTo(127.3, 27.9, 128.8, 28.5, 130.6, 28.5);
        ctx.bezierCurveTo(132.4, 28.5, 133.9, 27.9, 135.1, 26.7);
        ctx.bezierCurveTo(136.4, 25.4, 137.0, 23.9, 137.0, 22.1);
        ctx.bezierCurveTo(137.0, 20.4, 136.4, 18.9, 135.1, 17.6);
        ctx.bezierCurveTo(133.9, 16.4, 132.4, 15.7, 130.6, 15.7);
        ctx.bezierCurveTo(128.9, 15.7, 127.3, 16.4, 126.1, 17.6);
        ctx.closePath();
        ctx.fill();

        // layer1/Group/Group/Compound Path
        ctx.beginPath();

        // layer1/Group/Group/Compound Path/Path
        ctx.moveTo(151.9, 16.6);
        ctx.bezierCurveTo(150.4, 16.9, 149.4, 17.4, 148.7, 18.1);
        ctx.bezierCurveTo(147.5, 19.3, 146.9, 20.7, 146.8, 22.4);
        ctx.lineTo(146.8, 31.5);
        ctx.lineTo(143.4, 31.5);
        ctx.lineTo(143.4, 13.7);
        ctx.lineTo(146.8, 13.7);
        ctx.lineTo(146.8, 16.6);
        ctx.bezierCurveTo(147.4, 15.0, 148.5, 13.9, 150.1, 13.4);
        ctx.lineTo(151.9, 16.6);
        ctx.closePath();
        ctx.fill();

        // layer1/Group/Group
        ctx.restore();

        // layer1/Group/Group/Compound Path
        ctx.save();
        ctx.beginPath();

        // layer1/Group/Group/Compound Path/Path
        ctx.moveTo(29.3, 5.0);
        ctx.bezierCurveTo(26.0, 1.7, 22.0, 0.0, 17.3, 0.0);
        ctx.lineTo(17.3, 0.0);
        ctx.lineTo(0.0, 0.0);
        ctx.lineTo(0.0, 17.3);
        ctx.lineTo(0.0, 17.3);
        ctx.bezierCurveTo(0.1, 21.8, 1.7, 25.7, 5.0, 29.0);
        ctx.bezierCurveTo(8.4, 32.3, 12.4, 34.0, 17.2, 34.0);
        ctx.bezierCurveTo(22.0, 34.0, 26.0, 32.3, 29.3, 29.0);
        ctx.bezierCurveTo(32.6, 25.6, 34.3, 21.6, 34.3, 17.0);
        ctx.bezierCurveTo(34.3, 12.3, 32.6, 8.3, 29.3, 5.0);
        ctx.closePath();

        // layer1/Group/Group/Compound Path/Path
        ctx.moveTo(23.7, 23.7);
        ctx.bezierCurveTo(21.9, 25.5, 19.7, 26.4, 17.2, 26.4);
        ctx.bezierCurveTo(14.6, 26.4, 12.4, 25.5, 10.6, 23.7);
        ctx.bezierCurveTo(8.8, 21.9, 7.9, 19.8, 7.9, 17.3);
        ctx.lineTo(17.3, 17.3);
        ctx.lineTo(17.3, 7.6);
        ctx.bezierCurveTo(19.7, 7.6, 21.9, 8.5, 23.7, 10.3);
        ctx.bezierCurveTo(25.5, 12.1, 26.4, 14.4, 26.4, 17.0);
        ctx.bezierCurveTo(26.4, 19.6, 25.5, 21.9, 23.7, 23.7);
        ctx.closePath();
        ctx.save();
        ctx.transform(1.000, 0.000, 0.000, -0.600, -29.0, -13.0);
        gradient = ctx.createRadialGradient(37.3, -20.3, 0.0, 37.3, -20.3, 22.0);
        gradient.addColorStop(0.00, "rgb(101, 200, 231)");
        gradient.addColorStop(1.00, "rgb(0, 162, 222)");
        ctx.fillStyle = gradient;
        ctx.fill();
        ctx.restore();
        ctx.restore();
        ctx.restore();
        ctx.restore();
    };

}