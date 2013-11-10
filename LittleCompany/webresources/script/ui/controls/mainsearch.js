var Mainsearch = function () {

    var skin = $('#mainsearchbar');
    var queryform = skin.find("#mainsearchform");
    var quicksearchDOM = skin.find('.quicksearchresults');
    var quicksearchresults;

    var Init = function () {

        //TODO: no results caption

        AddEventListeners();
    };

    var AddEventListeners = function () {
        queryform.find('.searchquery').unbind('keyup').keyup(OnSearchQueryKeyUp);
        queryform.unbind('submit').submit(OnSearchFormSubmit);
    };

    var OnSearchQueryKeyUp = function () {
        var q = $(this).val();

        //add open console function from search bar
        if (q == Console.openstring) {
            Console.Open();
            $(this).val("");
            return;
        }

        if (q && $.trim(q).length > 1) {    
            DoQuickSearch(q);
        } else {
            quicksearchDOM.slideUp(100);
        }
        
        
    };

    var OnSearchFormSubmit = function () {
        //TODO: handle actual search
        var q = queryform.find('.searchquery').val();
        if(!q) { return; }

        var qd = GetQueryData(q);

        quicksearchDOM.slideUp(100);

        new ox.Event('navigate', 'searchresults', qd);

        return false;
    };


    var DoQuickSearch = function (q) {

        var qd = GetQueryData(q);

        if(!qd.query && !qd.datatype) { return; }

        ox.data.CommunicateWithServer({
            methodUrl: 'Objects.asmx/QuickSearch',
            data: {
                'quicksearch': {
                    'query': qd.query,
                    'customerid': ox.user.GetId(),
                    'searchDataTypeId': qd.datatype
                }
            },
            success: function (d) {

                if (d.ispositive) {
                    RenderQuickSearchResults(d.data.searchresults);
                } else {
                    ox.Log("Mainsearch.DoQuickSearch() - Negative result from WM.");
                }
            }
        });
    };

    var GetQueryData = function (q) {
        var qd = {};

        
        var su = new Stringutil();

        if (su.EndsWith(q, " ")) {
            q = q.substr(0, q.length - 1);
        }

        if (su.EndsWith(q, " i")) {
            q = q.substr(0, q.length - 2);
        }

        if (su.EndsWith(q, " in")) {
            q = q.substr(0, q.length - 3);
        }


        //process q for "... in ..." to handle datatypes
        var qarr = q.split(" in ");
        if (qarr.length > 1) {
            var dtlist = ox.data.GetDataTypeList();
            var foundtypeids = [];

            var typestring = qarr[qarr.length - 1];

            qd.query = q.substr(0, q.indexOf(qarr[qarr.length -2]));
            if (typestring) {

                var i = 0, l = dtlist.length, dt;
                for (i; i < l; i++) {
                    dt = dtlist[i];
                    if (dt && dt.caption && dt.caption.toLowerCase().indexOf(typestring.toLowerCase()) == 0) {
                        foundtypeids.push(dt.value);
                    }
                }

                if (foundtypeids.length == 1) {
                    qd.datatype = foundtypeids[0];
                }
            }
        } else {
            qd.query = q;
        }

        return qd;
    }

    var RenderQuickSearchResults = function (res) {
        quicksearchDOM.find('.object-item').remove();   //remove existing results
        quicksearchDOM.find('.noresults').hide();   //hide "no results"

        quicksearchresults = [];

        if (!res.length) {
            //render no results found
            quicksearchDOM.find('.noresults').show();   //show "no results"

            if (!quicksearchDOM.is(':visible')) {
                quicksearchDOM.slideDown(100);
            }
        }

        //render results
        if (!quicksearchDOM.is(':visible')) {
            quicksearchDOM.slideDown(100);
        }

        var i = 0, l = res.length, r, resskin = "";
        for (i; i < l; i++) {
            r = res[i];
            //datatypeid
            //id
            //name
            quicksearchresults[r.id] = r;

            resskin += RenderObjectBlock(r);
        }

        quicksearchDOM.append(resskin);

        var canvasrenderer = new Canvasrenderer();

        //render type icons
        quicksearchDOM.find('.iconcanvas').each(function (i, cnv) {
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
                if (cnv.hasClass('person')) {
                    canvasrenderer.DrawContactpersonIcon(ctx);
                    return;
                }
            } else {
                //TODO: canvas-unsupported code here - render png's
            }
        });


        //BIND
        quicksearchDOM.find('.object-item').unbind('click').click(OnObjectBlockClick);
    };

    var RenderObjectBlock = function (obj) {
        //obj { datatypeid, id, name }

        var dtlist = ox.data.GetDataTypeList();

        var dtcaption = "unknown type";

        var i = 0, l = dtlist.length, dt;
        for (i; i < l; i++) {
            dt = dtlist[i];
            if (dt && dt.value == obj.datatypeid) {
                dtcaption = dt.caption;
            }
        }

        var objskin = "<div class='object-item " + dtcaption.toLowerCase() + "' clickid='" + obj.id + "'>" +
            "<table><tr>" +
                "<td class='icon'>" +
                    "<canvas class='iconcanvas " + dtcaption.toLowerCase() + "' width='40' height='40'></canvas>" +
                "</td><td class='itemtext'>" +
                    "<div class='itemtextcontent'>" +
                        "<div class='itemtitle'>" + obj.name + "</div>" +
                        "<div class='itemtype' clickid='" + obj.datatypeid + "'>" + dtcaption + "</div>" +
                    "</div>" +
                "</td></tr>" +
            "</table>" +
        "</div>";

        return objskin;
    };

    var OnObjectBlockClick = function () {
        var cid = $(this).attr('clickid');
        if(!cid) {
            ox.Log("Mainsearch.OnObjectBlockClick() - Object clicked without clickid.");
            return;
        }

        var obj = quicksearchresults[cid];
        if (!obj) {
            ox.Log("Mainsearch.OnObjectBlockClick() - Object could not be found by clickid. Clickid: '" + cid + "'. ");
            return;
        }

        if (quicksearchDOM.is(':visible')) {
            quicksearchDOM.slideUp(100);
        }
        new ox.Event('navigate', 'item', obj);
    };

    Init();
};