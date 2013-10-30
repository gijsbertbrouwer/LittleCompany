/**
*	CLASS Datamap
*	a list with key/value pairs
*
**/

function Datamap(map) {

    var me = this;
    var data = (map) ? map : {};
    var length = 0;

    //#region Public Function Add
    /*           Adds an item to the datamap
    *   key     the index on which the item is later to be found
    *   value   the item to add
    *   return  null
    **/
    me.Add = function (key, value, stopchange) {
        if ((!key) && (!value)) {
            ox.Log("datamap.Add:no key/value received"); return;
        }

        if (typeof (value) == "undefined") { value = key; }

        if (!data[key]) {
            length++;
        }

        data[key] = value;

        if (!stopchange) {
            //ox.util.eventmanager.Trigger(me, 'add', { 'key': key, 'value': value }); //dispatch change event
        }
    };
    //#endregion

    //#region Public Function AddRange
    /*           Adds a range of items to the datamap
    *   arr     Array. Of key/value pairs.
    *
    *   return  null.
    **/
    me.AddRange = function (arr) {
        if (!arr || !arr.length) { return arr; }
        var i = 0;
        var l = arr.length;

        for (i; i < l; i++) {
            me.Add(arr[i].key, arr[i].value), true;
        }

        //ox.util.eventmanager.Trigger(me, 'add', { 'range': arr }); //dispatch change event
    };
    //#endregion

    //#region   Public Function Get
    /*           Gets an item from the datamap
    *   key     the index on which the item is to be found
    *   return  the item found, or false
    **/
    me.Get = function (key) {
        return data[key] || false;
    };
    //#endregion

    //#region Public Function Has
    /*           Adds an item to the datamap
    *   key     the index on which the item is to be found
    *   return  Boolean. True if item is found
    **/
    me.Has = function (key) {
        return (data[key] !== undefined);
    }
    //#endregion


    //#region   Public Function AsArray
    /*           provides access to the data of the datamap in an array
    *   return  Array. Contains Objects with key and value
    **/
    me.AsArray = function () {
        var r = [], p; //array to return


        for (p in data) {
            r.push({ 'key': p, 'value': data[p] }); //create an object in the return array
        }

        ////has to be each because it's an object with properties
        //$.each(data, function (key, value) {    //for each object in our data
        //    r.push({ 'key': key, 'value': value }); //create an object in the return array
        //});

        return r;   //and return array
    };
    //#endregion

    //#region   Public Function All
    /*           provides access to the data of the datamap in an array
    *   return  Array. Contains Objects in an array
    **/
    me.All = function () {
        var r = [], p; //array to return

        for (p in data) {
            r.push(data[p]);
        }

        ////has to be each because it's an object with properties
        //$.each(data, function (key, value) {
        //    r.push(value);
        //});
        return r;
    }
    //#endregion


    //#region   Public Function Keys
    /*           provides access to the data of the datamap in an array
    *   return  Array. Contains Objects in an array
    **/
    me.Keys = function () {
        var r = [], p; //array to return

        for (p in data) {
            r.push(p);
        }

        return r;
    }
    //#endregion

    //#region Public Function Remove
    /*           Removes an item from the datamap
    *   key     the index of the item to be removed
    *   return  the deleted item, or false
    **/
    me.Remove = function (key) {
        var r = false;
        if (data[key]) {
            r = data[key];
            delete data[key];
            length--;
        }

        return r;
    }
    //#endregion

    //#region   Public Function Empty
    /*           Empties the datamap.
    *
    *   return  null.
    **/
    me.Empty = function () {
        data = {};
        length = 0;
    };
    //#endregion


    //#region   Public Function Length
    /*           Provides access to the length of the datamap
    *   return  int.
    **/
    me.Length = function () {
        return length;
    }
    //#endregion

    //#region   Public Function ToString
    /*           Returns the current datamap as string.
    *   return  String.
    **/
    me.ToString = function () {
        var r = "";
        for (var str in data) {
            r += "- " + str + "<br/>";
        }
        r = r.substr(0, r.length - "<br/>".length);

        return r;
    };
    //#endregion

    //#region   Public Function Sort
    /*           Sorts the datamap on the supplied function
    *   return  Array. The sorted array as key/value pairs
    **/
    me.Sort = function (sortfn, order) {
        var a = me.AsArray();
        a.sort(sortfn);

        if (order == 'asc') {
            a.reverse();
        }

        me.Empty();
        me.AddRange(a);

        return me.AsArray();

    };
    //#endregion


}


