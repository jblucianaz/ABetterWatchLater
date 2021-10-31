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

  componentWillMount() {
    fetch('https://localhost:5001/abetterwatchlater')
    .then((response) => {
      console.log(response)
    })
  }

  render() {
    return (
      <div className="App">
        <SearchBar/>
        <ChannelContainer/>
        <VideoContainer/>
      </div>
    );
  }
}

export default App;
