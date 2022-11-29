mergeInto(LibraryManager.library, {

    GetPlayerData: function(){
        myGameInstance.SendMessage('Yandex', 'SetName', player.getName());
        myGameInstance.SendMessage('Yandex', 'SetAvatar', player.getPhoto("medium"));
    },

    SaveExtern: function(data) {
        var dataString = UTF8ToString(data);
        var myobj = JSON.parse(dataString);
        player.setData(myobj);
    },

    LoadExtern: function(){
        player.getData().then(_data => {
            const myJson = JSON.stringify(_data);
            myGameInstance.SendMessage('Progress', 'SetDataInfo', myJson)
        })
    },
});