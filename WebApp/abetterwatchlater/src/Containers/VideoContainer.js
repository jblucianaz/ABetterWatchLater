import '../Components/VideoItem';
import VideoItem from '../Components/VideoItem';

function VideoContainer({videoList}) {
    return (
        videoList.map(video => {
            return <VideoItem key={video.videoId} video={{...video}} />
        })
    )
}

export default VideoContainer;