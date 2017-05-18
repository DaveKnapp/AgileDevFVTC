$(ScrollingSearch).scroll(function () {
    if (($(ScrollingSearch).scrollTop() > $(searchResult).height() - 400) && (isGettingNewItems == false) && HasMoreItems) {
        document.getElementById("FakeSearchButton").click();
        isGettingNewItems = true;
    }
});


var nextSearchPage = 0;
var isGettingNewItems = false;
var HasMoreItems = true;

function SearchHasNoMoreItems() {
    HasMoreItems = false
}

function SetScrollingPageInput() {
    document.getElementById("ScrollingSearchPageInput").value = nextSearchPage;
    document.getElementById("ScrollingSearchTitleInput").value = document.getElementById("GameSearch").value;
}


function ClearSearchItems() {
    var searchList = document.getElementById("searchResult");
    HasMoreItems = true;
    while (searchList.firstChild != null) {
        searchList.firstChild.remove();
    }
}

function SetSearchPage() {
    nextSearchPage = 1;
    SetScrollingPageInput();
}

function IncrementSearchPage() {
    nextSearchPage = nextSearchPage + 1;
    isGettingNewItems = false;
    SetScrollingPageInput();
}

function setInputIndex() {
    var gameInputs = document.getElementById("userInputGames");

    gameNextInputIndex = gameInputs.children.length;
}

function ItemClicked(event) {
    var item = event.currentTarget;

    item.addEventListener("click", GameListItemClicked);
    document.getElementById("UserGames").insertBefore(item.parentNode, document.getElementById("UserGames").firstChild);

    var gameInputDiv = document.getElementById("userInputGames");

    var title = item.children[1].innerHTML;
    var igdbId = item.children[2].children[0].value;

    var newdiv = document.createElement('div');
    newdiv.innerHTML = '<input type="hidden" name="CurrentUser.Games.Index" value="' + igdbId + '" />' +
                       '<input name="CurrentUser.Games[' + igdbId + '].ID" value ="0" type="hidden" />' +
                       '<input name="CurrentUser.Games[' + igdbId + '].igdbID" value="' + igdbId + '" type="hidden"/>' +
                       '<input name="CurrentUser.Games[' + igdbId + '].Title" value="' + title + '" type="hidden"/>';

    document.getElementById("userInputGames").appendChild(newdiv);

}

function GameListItemClicked(event) {
    var inputGames = document.getElementById("userInputGames");

    for (var i = 0; i < inputGames.children.length; i++) {
        if (inputGames.children[i].children[2].value == event.currentTarget.children[2].children[0].value) {
            inputGames.children[i].remove();
            var item = event.currentTarget.remove();
        }
    }
}

function SubmitForm() {
    document.getElementById("SubmitButton").click();
}

setInputIndex();