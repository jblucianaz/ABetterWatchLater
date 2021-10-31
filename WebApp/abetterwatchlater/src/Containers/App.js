import React, {Component} from 'react'
import './App.css';
import '../Components/VideoItem';
import VideoContainer from './VideoContainer'
import SearchBar from '../Components/SearchBar'
import ChannelContainer from './ChannelContainer';

class App extends Component {
  constructor(props) {
    super(props)
    this.state = {
      videoList: [],
      channelList: []
    }
  }

  UNSAFE_componentWillMount() {
    this.initData()
  }

  render() {
    console.log(this.state)
    return (
      <div className="App">
        <SearchBar/>
        <ChannelContainer channelList={this.state.channelList}/>
        <VideoContainer videoList={this.state.videoList}/>
      </div>
    );
  }

  initData() {
    this.getAllVideos()
    this.getAllChannels()
  }

  getAllChannels() {
    fetch('https://localhost:5001/abetterwatchlater/channels')
      .then((response) => response.json())
      .then((data) => this.setState({channelList: data}))
    }

  getAllVideos() {
    return fetch('https://localhost:5001/abetterwatchlater/videos')
      .then((response) => response.json())
      .then((data) => this.setState({videoList: data}))
  }
}

export default App;
