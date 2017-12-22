import React from 'react';
import PropTypes from 'prop-types';
import Tabs, {Tab} from 'material-ui/Tabs';

export default function CustomTabs(props){
  return (
    <Tabs
      value={props.selected}
      indicatorColor="primary"
      textColor="primary"
      fullWidth
      centered
      onChange={props.onChange}
    >
      {
        props.tabs.map((tabContent, index) =>
          <Tab key={index} label={tabContent}/>
        )
      }
    </Tabs>
  );
}

CustomTabs.propTypes = {
  tabs: PropTypes.array.isRequired,
  selected: PropTypes.number.isRequired,
  onChange: PropTypes.func.isRequired,
};