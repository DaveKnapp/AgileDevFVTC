$(ScrollingSearch).scroll(function () {
    if (($(ScrollingSearch).scrollTop() > $(searchResult).height() - 400) && (isGettingNewItems == false) && HasMoreItems) {
        document.getElementById("FakeSearchButton").click();
        isGettingNewItems = true;
    }
});


var nextSearchPage = 0;
var gameNextInputIndex = 0;
var isGettingNewItems = false;
var HasMoreItems = true;

function SearchHasNoMoreItems() {
    HasMoreItems = false
}

function SetScrollingPageInput() {
    document.getElementById("ScrollingSearchPageInput").value = nextSearchPage;
    document.getElementById("ScrollingSearchTitleInput").value = document.getElementById("Search").value;
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
    document.getElementById("UserGames").appendChild(item.parentNode);

    var gameInputDiv = document.getElementById("userInputGames");

    var title = item.children[1].innerHTML;
    var igdbId = item.children[2].children[0].value;

    var newdiv = document.createElement('div');
    newdiv.innerHTML = '<input name="CurrentUser.Games[' + gameNextInputIndex + '].ID" value ="0" type="hidden" />' +
                         '<input name="CurrentUser.Games[' + gameNextInputIndex + '].igdbID" value="' + igdbId + '" type="hidden"/>' +
                       '<input name="CurrentUser.Games[' + gameNextInputIndex + '].Title" value="' + title + '" type="hidden"/>';

    document.getElementById("userInputGames").appendChild(newdiv);

    gameNextInputIndex++;
}

function GameListItemClicked(event) {
    var inputGames = document.getElementById("userInputGames");

    for (var i = 0; i < inputGames.children.length; i++) {
        if (inputGames.children[i].children[1].value == event.currentTarget.children[2].children[0].value) {
            inputGames.children[i].remove();
            var item = event.currentTarget.remove();
        }
    }
}

setInputIndex();