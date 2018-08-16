import { Component } from 'react';
import DataLoader from 'components/dataLoader/dataLoader';
import Enter from 'pages/enter/enter';
import {
  HashRouter,
  Route,
  Switch
} from 'react-router-dom';

class Router extends Component {
  render() {
    return (
      <HashRouter>
        <Switch>
          <Route exact path='/' component={DataLoader}/>
          <Route path='/enter' component={Enter}/>
          <Route path='/grid' component={null}/>
        </Switch>
      </HashRouter>
    );
  }
}

export default Router;
