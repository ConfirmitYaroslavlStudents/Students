import React from 'react';
import Dialog from 'material-ui/Dialog';
import AppBar from 'material-ui/AppBar';
import Toolbar from 'material-ui/Toolbar';
import Typography from 'material-ui/Typography';
import CloseIcon from 'material-ui-icons/Close';
import IconButton from 'material-ui/IconButton';
import Slide from 'material-ui/transitions/Slide';
import FabButton from './fabButton';
import FlatButton from './flatButton';

export default class DialogWindow extends React.Component {
  constructor(props) {
    super(props);
    this.state = ({ open: false });
    this.handleOpen = this.handleOpen.bind(this);
    this.handleAccept = this.handleAccept.bind(this);
    this.handleClose = this.handleClose.bind(this);
  }

  handleOpen() {
    if (this.props.open) {
      this.props.open();
    }
    this.setState({ open: true });
  };

  handleAccept() {
    if (this.props.accept) {
      this.props.accept();
    }
    this.setState({ open: false });
  }

  handleClose() {
    if (this.props.close) {
      this.props.close();
    }
    this.setState({ open: false });
  };

  render() {
    let openButton;
    switch(this.props.openButtonType) {
      case 'fab':
        openButton = <FabButton onClick={this.handleOpen} color="primary" icon={this.props.openButtonContent}/>;
        break;
      case 'icon':
        openButton = <IconButton onClick={this.handleOpen}>{this.props.openButtonContent}</IconButton>;
        break;
      default:
        openButton = <FlatButton onClick={this.handleOpen} color="primary" text={this.props.openButtonContent}/>;
    }
    let acceptButton;
    if (!this.props.withoutAcceptButton) {
      acceptButton =
        <FlatButton color="contrast" onClick={this.handleAccept} text={this.props.acceptButtonContent} />
    }
    return (
      <div style={{"display": "inline"}}>

        {openButton}

        <Dialog
          fullScreen={this.props.fullScreen}
          open={this.state.open}
          onRequestClose={this.handleClose}
          transition={<Slide direction="up" />}
        >
          <AppBar style={{position: 'relative'}}>
            <Toolbar>
              <Typography type="title" color="inherit" style={{flex: 1}}>
                {this.props.label}
              </Typography>

              {acceptButton}

              <IconButton color="contrast" onClick={this.handleClose} style={{marginRight: -15}} aria-label="Close">
                <CloseIcon/>
              </IconButton>
            </Toolbar>
          </AppBar>

          {this.props.content}

        </Dialog>
      </div>
    );
  }
}