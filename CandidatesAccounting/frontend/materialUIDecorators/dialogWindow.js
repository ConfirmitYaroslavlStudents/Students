import React from 'react';
import Button from 'material-ui/Button';
import Dialog from 'material-ui/Dialog';
import AppBar from 'material-ui/AppBar';
import Toolbar from 'material-ui/Toolbar';
import Typography from 'material-ui/Typography';
import CloseIcon from 'material-ui-icons/Close';
import SaveIcon from 'material-ui-icons/Save';
import IconButton from 'material-ui/IconButton';
import Slide from 'material-ui/transitions/Slide';
import FabButton from './fabButton';
import FlatButton from './flatButton';

export default class DialogWindow extends React.Component {
  constructor(props) {
    super(props);
    this.state = ({ open: false });
    this.handleOpen = this.handleOpen.bind(this);
    this.handleSave = this.handleSave.bind(this);
    this.handleClose = this.handleClose.bind(this);
  }

  handleOpen() {
    if (this.props.open) {
      this.props.open();
    }
    this.setState({ open: true });
  };

  handleSave() {
    if (this.props.save) {
      this.props.save();
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
      default:
        openButton = <FlatButton onClick={this.handleOpen} color="primary" text={this.props.openButtonContent}/>;
    }
    return (
      <div style={{"display": "inline"}}>

        {openButton}

        <Dialog
          open={this.state.open}
          onRequestClose={this.handleClose}
          transition={<Slide direction="up" />}
        >
          <AppBar style={{position: 'relative'}}>
            <Toolbar>
              <Typography type="title" color="inherit" style={{flex: 1}}>
                {this.props.label}
              </Typography>
              <Button color="contrast" onClick={this.handleSave} style={{marginLeft: 30}}>
                <SaveIcon/>
                save
              </Button>
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