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

    ShowFullAd: function(){
        ysdk.adv.showFullscreenAdv({
            callbacks: {
                onClose: function(wasShown) {
                  // some action after close
                },
                onError: function(error) {
                  // some action on error
                }
            }
        })
    },

    ShowAdsExtern: function(){
        ysdk.adv.showRewardedVideo({
            callbacks: {
                onOpen: () => {
                  console.log('Video ad open.');
                  myGameInstance.SendMessage("ADS", "StopGame");
                },
                onRewarded: () => {
                  console.log('Rewarded!');               
                },
                onClose: () => {
                  console.log('Video ad closed.');
                  myGameInstance.SendMessage("ADS", "MultiplierClicks");
                }, 
                onError: (e) => {
                  console.log('Error while open video ad:', e);
                }
            }
        })
    },
});