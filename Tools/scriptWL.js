// Transform a YouTube link to an ID.
function hrefToYouTubeId(href) {
	return href.replace("https://www.youtube.com/watch?v=", '').substring(0, 11);
}

// Save and download from browser console to file.
(function(console){

console.save = function(data, filename){

    if(!data) {
        console.error('Console.save: No data')
        return;
    }

    if(!filename) filename = 'console.json'

    if(typeof data === "object"){
        data = JSON.stringify(data, undefined, 4)
    }

    var blob = new Blob([data], {type: 'text/json'}),
        e    = document.createEvent('MouseEvents'),
        a    = document.createElement('a')

    a.download = filename
    a.href = window.URL.createObjectURL(blob)
    a.dataset.downloadurl =  ['text/json', a.download, a.href].join(':')
    e.initMouseEvent('click', true, false, window, 0, 0, 0, 0, 0, false, false, false, false, 0, null)
    a.dispatchEvent(e)
 }
})(console)

// MAIN

var idList = []

// Get all video 'blocks'.
var videoList = document.getElementsByTagName("ytd-playlist-video-renderer");

for(i = 0; i < videoList.length; i++) {
	var videoHref = videoList[i].children[1].getElementsByTagName('a')[1].href;
	
	var videoId = hrefToYouTubeId(videoHref);
	
	idList.push(videoId);
}

console.save(idList);