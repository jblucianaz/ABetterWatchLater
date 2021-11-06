import React, {Component} from "react";
import '../Style/SearchBar.css'

class SearchBar extends Component {
    constructor(props) {
        super(props)
        this.state = {searchText: "", placeHolder: "Rechercher"}
    }

    render() {
        return (
            <input id="searchBar" onChange={this.handleChange.bind(this)} placeholder={this.state.placeHolder} />
        )
    }

    handleChange(event) {
        this.setState({searchText: event.target.value})
    }
}

export default SearchBar