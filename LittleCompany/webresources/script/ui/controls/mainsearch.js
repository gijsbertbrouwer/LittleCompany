var Mainsearch = function () {

    var skin = $('#mainsearchbar');
    var queryform = skin.find("#mainsearchform");
    var quicksearchDOM = skin.find('.quicksearchresults');

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
        
        //TODO: process q for "... in ..." to handle datatypes

        if (q && q.length > 1) {    
            DoQuickSearch(q);
        } else {
            quicksearchDOM.slideUp(100);
        }

        
        
    };

    var OnSearchFormSubmit = function () {
        //TODO: handle actual search

        return false;
    };


    var DoQuickSearch = function (q) {

        ox.data.CommunicateWithServer({
            methodUrl: 'Objects.asmx/QuickSearch',
            data: {
                'quicksearch': {
                    'query': q,
                    'customerid': ox.user.GetId(),
                    'searchDataTypeId': undefined
                }
            },
            success: function (d) {

                if (d.ispositive) {
                    RenderQuickSearchResults(d.data.searchresults);
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
    };

    var RenderQuickSearchResults = function (res) {
        quicksearchDOM.find('.result').remove();   //remove existing results
        quicksearchDOM.find('.noresults').hide();   //hide "no results"

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

            resskin += "<div class='result' clickid='" + r.id + "'>" + r.name + "</div>";
        }

        quicksearchDOM.append(resskin);
    };

    Init();
};