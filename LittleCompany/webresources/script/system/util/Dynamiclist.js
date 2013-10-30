/**
*	CLASS Dynamiclist
*	provides a list that dispatches change events
**/

function Dynamiclist() {

    var me = this;
    this.data = []; //the data array, public accessable

    /**
    *   Public Function push
    *           Adds an item to the dynamic list
    *   item    the item to add
    *   return  null
    **/
    this.push = function (item) {
        //1. add item to list
        this.data.push(item);
    };

}
