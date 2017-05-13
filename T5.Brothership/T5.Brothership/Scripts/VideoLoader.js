'use strict';
function r(f) { /in/.test(document.readyState) ? setTimeout('r(' + f + ')', 9) : f() }
r(function () {
    if (!document.getElementsByClassName) {
        // IE8 support
        var getElementsByClassName = function (node, classname) {
            var a = [];
            var re = new RegExp('(^| )' + classname + '( |$)');
            var els = node.getElementsByTagName("*");
            for (var i = 0, j = els.length; i < j; i++)
                if (re.test(els[i].className)) a.push(els[i]);
            return a;
        }
        var videos = getElementsByClassName(document.body, "youtube");
    } else {
        var videos = document.getElementsByClassName("youtube");
    }

    var nb_videos = videos.length;
    for (var i = 0; i < nb_videos; i++) {
        // Based on the YouTube ID, we can easily find the thumbnail image
        videos[i].style.backgroundImage = 'url(http://i.ytimg.com/vi/' + videos[i].id + '/sddefault.jpg)';
        // Overlay the Play icon to make it look like a video player
        var play = document.createElement("div");
        play.setAttribute("class", "play");
        videos[i].appendChild(play);

        videos[i].onclick = function () {
            // Create an iFrame with autoplay set to true
            var iframe = document.createElement("iframe");
            var iframe_url = "https://www.youtube.com/embed/" + this.id + "?autoplay=1&autohide=1&fs=true";
            if (this.getAttribute("data-params")) iframe_url += '&' + this.getAttribute("data-params");
            iframe.setAttribute("src", iframe_url);
            iframe.setAttribute("frameborder", '0');
            iframe.setAttribute("allowfullscreen", 'allowfullscreen');

            // The height and width of the iFrame should be the same as parent
            iframe.style.top = 0;
            iframe.style.bottom = 0;
            iframe.style.left = 0;
            iframe.style.right = 0;
            iframe.style.position = 'absolute';

            // Replace the YouTube thumbnail with YouTube Player
            this.parentNode.replaceChild(iframe, this);
        }
    }
    SetTwitchVids();
});

function SetTwitchVids() {
    if (!document.getElementsByClassName) {
        // IE8 support
        var getElementsByClassName = function (node, classname) {
            var a = [];
            var re = new RegExp('(^| )' + classname + '( |$)');
            var els = node.getElementsByTagName("*");
            for (var i = 0, j = els.length; i < j; i++)
                if (re.test(els[i].className)) a.push(els[i]);
            return a;
        }
        var videos = getElementsByClassName(document.body, "twitch");
    } else {
        var videos = document.getElementsByClassName("twitch");
    }

    var nb_videos = videos.length;
    for (var i = 0; i < nb_videos; i++) {
        // Based on the YouTube ID, we can easily find the thumbnail image
    //    videos[i].style.backgroundImage = 'url(https://static-cdn.jtvnw.net/s3_vods/7579dac7a5_dolphinchemist_25168716080_641715250/thumb/thumb0-320x180.jpg)';
      //  videos[i].style.backgroundSize = '125% 100%';
        //videos[i].style.backgroundImage = 'url(http://i.ytimg.com/vi/' + videos[i].id + '/sddefault.jpg)';
        // Overlay the Play icon to make it look like a video player
        var play = document.createElement("div");
        play.setAttribute("class", "play");
        videos[i].appendChild(play);

        videos[i].onclick = function () {
            // Create an iFrame with autoplay set to true
            var iframe = document.createElement("iframe");
            var iframe_url = "http://player.twitch.tv/?video=" + this.id + "&autoplay=true";

           // var iframe_url = "https://www.youtube.com/embed/" + this.id + "?autoplay=1&autohide=1";
            if (this.getAttribute("data-params")) iframe_url += '&' + this.getAttribute("data-params");
            iframe.setAttribute("src", iframe_url);
            iframe.setAttribute("frameborder", '0');
            iframe.setAttribute("allowfullscreen", 'allowfullscreen');


            // The height and width of the iFrame should be the same as parent
            iframe.style.top = 0;
            iframe.style.bottom = 0;
            iframe.style.left = 0;
            iframe.style.right = 0;
            iframe.style.position = 'absolute';

            // Replace the YouTube thumbnail with YouTube Player
            this.parentNode.replaceChild(iframe, this);
        }
    }
}
