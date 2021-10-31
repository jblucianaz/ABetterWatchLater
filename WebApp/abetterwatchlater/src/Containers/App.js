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
    fetch('https://localhost:5001/abetterwatchlater')
      .then((response) => response.json())
      .then((data) => this.setState({videoList: data})) 
  }

  render() {
    return (
      <div className="App">
        <SearchBar/>
        <ChannelContainer/>
        <VideoContainer videoList={this.state.videoList}/>
      </div>
    );
  }

  populateLists() {
    // TODO: fetch videos AND channels data to update the state only once. 
  }
}

export default App;
