import ChannelItem from '../Components/ChannelItem';
import '../Style/ChannelContainer.css'

function ChannelContainer({channelList}) {

    
    return (
        <ul>
            {
                channelList.map(channel => {
                    return <ChannelItem key={channel.channelId} channel={{...channel}} />
                })
            }
        </ul>

    )
}

export default ChannelContainer;