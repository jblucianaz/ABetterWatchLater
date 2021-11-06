import '../Style/VideoItem.css'

function VideoItem(props) {
    return (
      <div className="video-item">
        <img src={props.video.thumbnail} alt="thumbnail"></img>
        <p>{props.video.title}</p>
      </div>
    );
  }
  
  export default VideoItem;
  