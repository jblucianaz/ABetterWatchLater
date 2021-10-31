import ChannelItem from '../Components/ChannelItem';

function ChannelContainer() {
    const channelsTest = 
    [
        {
            "channelId":"UCmzP_xaz-uyMMbac4DggR8A",
            "name":"52 minutes",
            "thumbnail":"https://yt3.ggpht.com/ytc/AKedOLTx2Y_aD1303h4zl5KszZ7HPKl34O1a9oRwAzFIbA=s240-c-k-c0x00ffffff-no-rj",
        },
        {
            "channelId":"UCKSVUHI9rbbkXhvAXK-2uxA",
            "name":"Supercar Blondie",
            "thumbnail":"https://yt3.ggpht.com/ytc/AKedOLRWs87BMvSHF7LdcusDS5IY17SeBIJbzlx0E5Q5AQ=s240-c-k-c0x00ffffff-no-rj",
        },
        {
            "channelId":"UCvRgiAmogg7a_BgQ_Ftm6fA",
            "name":"Le Précepteur",
            "thumbnail":"https://yt3.ggpht.com/ytc/AKedOLRS9A3_cSxwNcgrJ_OCjr_V41PKJoELNPFCKk_F=s240-c-k-c0x00ffffff-no-rj",
        }
    ]

    const style = 
    {
        display:"inline-block",
        listStyleType: "none"
    }
    
    return (
        <ul style={style}>
            {
                channelsTest.map(channel => {
                    return <ChannelItem key={channel.channelId} channel={{...channel}} />
                })
            }
        </ul>

    )
}

export default ChannelContainer;