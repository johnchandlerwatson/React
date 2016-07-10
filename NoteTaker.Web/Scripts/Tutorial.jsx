//require('moment');
//require('showdown');
var Note = React.createClass({
    render: function () {
        var converter = new Showdown.converter();
        var rawMarkup = converter.makeHtml(this.props.children.toString());
        var date = moment(this.props.note.CreatedDate, "YYYY-MM-DD HH:mm"); //todo get the freaking date to work
        return (
          <div className="well">
            <h2 className="noteAuthor">
                {this.props.author.FirstName} 
            </h2>
            <span dangerouslySetInnerHTML={{__html: rawMarkup }}></span>
         </div>
      );
    }
});

var NoteList = React.createClass({
    render: function () {
        var noteNodes = this.props.data.map(function (note, index) {
            index++;
            return (
                <Note key={index} author={note.Author} note={note}>
                    {note.Text}
                </Note>
            );
        });
        return (
          <div className="noteList">
            {noteNodes}
          </div>
        );
    }
});

var NoteForm = React.createClass({
    handleSubmit: function (e) {
        e.preventDefault();
        var authorsFirstName = this.refs.authorsFirstName.value.trim();
        var text = this.refs.text.value.trim();
        if (!text || !authorsFirstName) {
            return;
        }
        this.props.onnoteSubmit({ Author: { FirstName : authorsFirstName }, Text: text });
        this.refs.authorsFirstName.value = '';
        this.refs.text.value = '';
        $("#namebox").focus(); //todo why is this not working
        return;
    },
    render: function() {
        return (
          <form className="noteForm" onSubmit={this.handleSubmit} >
            <input id="namebox" type="text" placeholder="Your name" ref="authorsFirstName" />
            <input type="text" placeholder="Say something..." ref="text" />
            <input type="submit" value="Post" />
          </form>
      );
    }
});

var NoteBox = React.createClass({
    loadnotesFromServer: function () {
        var xhr = new XMLHttpRequest();
        xhr.open('get', this.props.url, true);
        xhr.onload = function () {
            var data = JSON.parse(xhr.responseText);
            this.setState({ data: data });
        }.bind(this);
        xhr.send();
    },
    handleNoteSubmit: function (note) {
//        var data = new FormData();
//        data.append('Author', note.Author);
//        data.append('Text', note.Text);

        var xhr = new XMLHttpRequest(); 
        xhr.open('post', this.props.submitUrl, true);       
        xhr.onload = function () {
            this.loadnotesFromServer();
        }.bind(this);
        xhr.setRequestHeader('Content-Type', 'application/json');
        var stringifiedData = JSON.stringify(note);
        xhr.send(stringifiedData);
    },
    getInitialState: function () {
        return { data: [] };
    },
    componentDidMount: function () {
        this.loadnotesFromServer();
        window.setInterval(this.loadnotesFromServer, this.props.pollInterval);
    },
    render: function() {
        return (
          <div className="jumbotron">
            <div className="container">
                <h1>Notes</h1>
                <NoteForm onnoteSubmit={this.handleNoteSubmit} />
            </div>
            <NoteList data={this.state.data} />           
          </div>
      );
    }
});


ReactDOM.render(
    <NoteBox url="/notes" submitUrl="/notes/new" pollInterval={2000} />,
    document.getElementById('content')
);