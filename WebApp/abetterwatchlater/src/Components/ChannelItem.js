import './VideoItem.css'

function ChannelItem({channel}) {
    return (
      <li className="channel-item">
        <img src={channel.thumbnail} alt="thumbnail"></img>
        <p>{channel.name}</p>
      </li>
    );
  }
  
  export default ChannelItem;
  