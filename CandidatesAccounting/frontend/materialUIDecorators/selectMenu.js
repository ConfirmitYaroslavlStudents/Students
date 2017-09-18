import React from 'react';
import List, { ListItem, ListItemText } from 'material-ui/List';
import Menu, { MenuItem } from 'material-ui/Menu';

export default class SelectMenu extends React.Component {
  constructor(props) {
    super(props);
    const selected = props.options.indexOf(props.selectedOption);
    this.state = ({
      nchorEl: undefined,
      open: false,
      selectedIndex: selected
    });
    this.handleClickListItem = this.handleClickListItem.bind(this);
    this.handleMenuItemClick = this.handleMenuItemClick.bind(this);
    this.handleRequestClose = this.handleRequestClose.bind(this);
  }

  handleClickListItem(event) {
    this.setState({ open: true, anchorEl: event.currentTarget });
  };

  handleMenuItemClick(event, index) {
    this.setState({ selectedIndex: index, open: false });
    this.props.onChange(this.props.options[index]);
  };

  handleRequestClose() {
    this.setState({ open: false });
  };

  render() {
    return (
      <div style={{marginBottom: -15}}>
        <List>
          <ListItem
            button
            aria-haspopup="true"
            aria-controls="lock-menu"
            aria-label={this.props.label}
            onClick={this.handleClickListItem}
          >
            <ListItemText
              primary={this.props.label}
              secondary={this.props.options[this.state.selectedIndex]}
            />
          </ListItem>
        </List>
        <Menu
          id="lock-menu"
          anchorEl={this.state.anchorEl}
          open={this.state.open}
          onRequestClose={this.handleRequestClose}
        >
          {this.props.options.map((option, index) =>
            <MenuItem
              key={option}
              selected={index === this.state.selectedIndex}
              onClick={event => this.handleMenuItemClick(event, index)}
            >
              {option}
            </MenuItem>,
          )}
        </Menu>
      </div>
    );
  }
}

SelectMenu.propTypes = {
  options: React.PropTypes.object.isRequired,
  selectedOption: React.PropTypes.object.isRequired,
  onChange: React.PropTypes.func,
  label: React.PropTypes.string,
};