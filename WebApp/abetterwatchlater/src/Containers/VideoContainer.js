import '../Style/VideoContainer.css'
import VideoItem from '../Components/VideoItem';

function VideoContainer({videoList}) {
    return (
        <div className="parent">
            {
                videoList.map(video => {
                    return <VideoItem key={video.videoId} video={{...video}} />
                })
            }
        </div>
    )
}

export default VideoContainer;