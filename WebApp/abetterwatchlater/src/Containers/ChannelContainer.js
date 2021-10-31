import ChannelItem from '../Components/ChannelItem';

function ChannelContainer({channelList}) {
    const style = 
    {
        display:"inline-block",
        listStyleType: "none"
    }
    
    return (
        <ul style={style}>
            {
                channelList.map(channel => {
                    return <ChannelItem key={channel.channelId} channel={{...channel}} />
                })
            }
        </ul>

    )
}

export default ChannelContainer;