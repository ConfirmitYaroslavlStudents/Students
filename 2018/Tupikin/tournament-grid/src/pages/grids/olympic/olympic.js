import { Component } from 'react';
import 'treantjs/Treant.css';
import 'treantjs/examples/tennis-draw/example7.css';

import 'treantjs/Treant.js';
import 'treantjs/vendor/raphael.js';
import 'treantjs/examples/tennis-draw/example7.js';

class Olympic extends Component {
  constructor(props) {
    super(props);

    this.data = {

    };
  }

  render() {
    return (
      <div className={'chart'} id='OrganiseChart6'></div>
    );
  }
}

export default Olympic;
