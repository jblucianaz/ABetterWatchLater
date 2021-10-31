import '../Components/VideoItem';
import VideoItem from '../Components/VideoItem';

function VideoContainer() {
    const videosTest = 
    [
        {
        "videoId":"LCCtCDn2GOE",
        "title":"La Silicon Valley a quitté la Californie",
        "url":"https://www.youtube.com/watch?v=LCCtCDn2GOE",
        "channelId":"UCHELbmT1kt9ZLX51Aq-BUcA",
        "duration":"PT15M53S",
        "tags":
        [
            "silicon valley,pays émergents,investir startups"
        ],
        "thumbnail":"https://i.ytimg.com/vi/LCCtCDn2GOE/sddefault.jpg"
        }, 
        {
            "videoId":"ZKR9FSybiqI",
            "title":"Chris Hemsworth et les secrets de son physique musclé",
            "url":"https://www.youtube.com/watch?v=ZKR9FSybiqI",
            "channelId":"UC1zcyz6ftI_VpK0q3DqBA-g",
            "duration":"PT21M56S",
            "tags":
            [
                "musculation,fitnessmith,sèche,se muscler,se musculer rapidement,sécher rapidement,comment les acteurs réussissent,physique d'acteur,physique d'acteurs de comics,les acteurs sont-ils dopé,comment s'entrainer pour avoir un physique d'acteur,physique de mannequin,comics,héros de comics,physique d'héros de comics,comment etre musclé,comment optimiser sa progression physique"
            ],
            "thumbnail":"https://i.ytimg.com/vi/ZKR9FSybiqI/sddefault.jpg"
        }
    ]
    
    return (
        videosTest.map(video => {
            return <VideoItem key={video.videoId} video={{...video}} />
        })
    )
}

export default VideoContainer;