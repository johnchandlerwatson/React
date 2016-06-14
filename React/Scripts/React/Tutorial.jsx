var Comment = React.createClass({
    render: function () {
        var converter = new Showdown.converter();
        var rawMarkup = converter.makeHtml(this.props.children.toString());
        return (
          <div className="comment">
            <h2 className="commentAuthor">
              {this.props.author}
            </h2>
            <span dangerouslySetInnerHTML={{__html: rawMarkup }}></span>
         </div>
      );
    }
});

var CommentList = React.createClass({
    render: function () {
        var commentNodes = this.props.data.map(function (comment, index) {
            index++;
            return (
                <Comment key={index} author={comment.Author}>
                    {comment.Text}
                </Comment>
            );
        });
        return (
          <div className="commentList">
            {commentNodes}
          </div>
        );
    }
});

var CommentForm = React.createClass({
    handleSubmit: function (e) {
        e.preventDefault();
        var author = this.refs.author.value.trim();
        var text = this.refs.text.value.trim();
        if (!text || !author) {
            return;
        }
        this.props.onCommentSubmit({Author: author, Text: text});
        this.refs.author.value = '';
        this.refs.text.value = '';
        return;
    },
    render: function() {
        return (
          <form className="commentForm" onSubmit={this.handleSubmit} >
            <input type="text" placeholder="Your name" ref="author" />
            <input type="text" placeholder="Say something..." ref="text" />
            <input type="submit" value="Post" />
          </form>
      );
    }
});

var CommentBox = React.createClass({
    loadCommentsFromServer: function () {
        var xhr = new XMLHttpRequest();
        xhr.open('get', this.props.url, true);
        xhr.onload = function () {
            var data = JSON.parse(xhr.responseText);
            this.setState({ data: data });
        }.bind(this);
        xhr.send();
    },
    handleCommentSubmit: function (comment) {
        var data = new FormData();
        data.append('Author', comment.Author);
        data.append('Text', comment.Text);

        var xhr = new XMLHttpRequest();
        xhr.open('post', this.props.submitUrl, true);
        xhr.onload = function () {
            this.loadCommentsFromServer();
        }.bind(this);
        xhr.send(data);
    },
    getInitialState: function () {
        return { data: [] };
    },
    componentDidMount: function () {
        this.loadCommentsFromServer();
        window.setInterval(this.loadCommentsFromServer, this.props.pollInterval);
    },
    render: function() {
        return (
          <div className="commentBox">
            <h1>Comments</h1>
            <CommentList data={this.state.data} />
            <CommentForm onCommentSubmit={this.handleCommentSubmit} />
          </div>
      );
    }
});

var NavBar = React.createClass({
    handleCommentSubmit: function (login) {
        var data = new FormData();
        data.append('Username', login.Username);
        data.append('Password', login.Password);

        var xhr = new XMLHttpRequest();
        xhr.open('post', this.props.submitLoginUrl, true);
        xhr.send(data);
    },

    handleSubmit: function (e) {
        e.preventDefault();
        var username = this.refs.username.value.trim();
        var password = this.refs.password.value.trim();
        if (!password || !username) {
            return;
        }
        this.handleCommentSubmit({ Username: username, Password: password });
        this.refs.username.value = '';
        this.refs.password.value = '';
        return;
    },
    render: function() {
        return (
          <form className="loginForm" onSubmit={this.handleSubmit} >
            <input type="text" placeholder="username" ref="username" />
            <input type="text" placeholder="password" ref="password" />
            <input type="submit" value="Post" />
          </form>
      );
    }
});


ReactDOM.render(
    <NavBar submitLoginUrl="/login"/>,
    <CommentBox url="/comments" submitUrl="/comments/new" pollInterval={2000} />,
    document.getElementById('content')
);