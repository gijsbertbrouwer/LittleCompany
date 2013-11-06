//used to be security
var User = function () {
    var me = this;

    me.securityToken;

    /*
    data:
        public int id
        public string name 
        public List<Favorite> favorites
        public DateTime creationdate
    */
    var data;

    me.Init = function () {
        var st = urlParams["securityToken"];
        if (st) {
            me.securityToken = { 'token': st };
            GetUserData();
        }
    };


    me.GetFavorites = function () {
        if (!data) { GetUserData(me.GetFavorites); return; }
        return data.favorites;
    }


    var GetUserData = function (success) {
        ox.data.CommunicateWithServer({
            methodUrl: 'User.asmx/GetUserData',
            success: function (d) {

                if (d.ispositive) {
                   
                    data = d.data;

                    if(typeof(success) == 'function') {
                        success();
                    }
                } else {
                    //TODO: handle errors
                }
            },
            error: function (d) {
                //TODO: handle errors
            }
        });
    };


}