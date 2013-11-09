﻿var Ui = function () {
    
    var me = this;
    var openscreenclass;

    me.Init = function () {
        ox.Navigate = ox.ui.Navigate;

        //handle all oxsections
        $('.oxsection').hide();

        //header, navigation
        InitScreen();

        //main search
        new Mainsearch();

        //console
        Console.Init();

        //main menu
        Mainmenu.Init();

    }

    var Navigate = function (e) {

        var to = e.detail.screen;
        var data = e.detail;


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
                openscreenclass = new ns(data);
            } else {
                ox.Log("Ui.Navigate() - Trying to navigate to a screen without js class (" + to + ".js).")
            }

        } else {
            ox.Log("Ui.Navigate() - Trying to navigate to a screen without DOM skin in home.html.")
        }
    };


    var InitScreen = function () {
        //header, navigation
        var h = $('#header');
        var n = $('#navigation');
        n.height($(window).height() - h.height());

        var cr = new Canvasrenderer();
        var cnv = $('#ondorlogocanvas')[0];
        if (cnv.getContext) {
            var ctx = cnv.getContext('2d');
            cr.DrawOndorLogoCanvas(ctx);
        } else {
            //TODO: canvas-unsupported code here - render png's
        }

        AddEventListeners();
    };

    var AddEventListeners = function () {
        window.addEventListener('navigate', Navigate);
    };



}