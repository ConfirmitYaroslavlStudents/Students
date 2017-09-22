import React from 'react';
import PropTypes from 'prop-types';
import Tabs, { Tab } from 'material-ui/Tabs';
import AppBar from 'material-ui/AppBar';
import SwipeableViews from 'react-swipeable-views';

export default class FullWidthTabs extends React.Component {
  constructor(props) {
    super(props);
    this.state = { value: props.selected ? props.selected : 0 };
  };

  handleChange(event, value) {
    this.setState({value: value });
  };

  handleChangeIndex(index) {
    this.setState({ value: index });
  };

  render() {
    return (
      <div className="custom-main">
        <AppBar className="tabs-bar" color="default">
          <Tabs
            value={this.state.value}
            onChange={this.handleChange.bind(this)}
            indicatorColor="primary"
            textColor="primary"
            fullWidth
            centered
          >
            {this.props.labels.map((label, index) =>
              <Tab key={'tab' + index} label={label} />
            )}
          </Tabs>
        </AppBar>
        <SwipeableViews
          index={this.state.value}
          onChangeIndex={this.handleChangeIndex.bind(this)}
        >
          {this.props.tabs.map((oneTab, index) =>
            <div key={'tab' + index}>
              {index === this.state.value ? oneTab : <div> </div>}
            </div>
          )}
        </SwipeableViews>
      </div>
    );
  }
}

FullWidthTabs.propTypes = {
  selected: PropTypes.number.isRequired,
  labels: PropTypes.array.isRequired,
  tabs: PropTypes.array.isRequired,
};