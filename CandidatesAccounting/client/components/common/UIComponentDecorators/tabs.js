import React from 'react';
import PropTypes from 'prop-types';
import Tabs, {Tab} from 'material-ui/Tabs';

export default function CustomTabs(props){
  return (
    <Tabs
      value={false}
      indicatorColor="primary"
      textColor="primary"
      fullWidth
      centered
      onChange={() => {if (props.onChange) { props.onChange(); }}}
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
  onChange: PropTypes.func,
};