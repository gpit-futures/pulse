import 'rxjs';
import React from 'react';
import createHistory from 'history/createBrowserHistory';
import { render } from 'react-dom';
import { HashRouter as Router } from 'react-router-dom';
import { ConnectedRouter, routerMiddleware } from 'react-router-redux';
import { createStore, applyMiddleware, compose } from 'redux';
import { createEpicMiddleware } from 'redux-observable';
import { Provider } from 'react-redux';
import { loadingBarMiddleware } from 'react-redux-loading-bar';

import './polyfills';
import App from './components/containers/App/App';
import rootReducer from './root.reducer';
import rootEpic from './root.epic';
import { initialiseStart } from './ducks/initialise-app.duck';

console.log(`App started in ${process.env.NODE_ENV} mode`);

// Create a history of your choosing (we're using a browser history in this case)
const history = createHistory();

// Build the middleware for intercepting and dispatching navigation actions
const routerMiddlewareInstance = routerMiddleware(history);

const epicMiddleware = createEpicMiddleware(rootEpic);

/**
 * Create store with initial state structure
 * and enhance by middleware
 */
//store initial state
const initialState = {
  credentials: {},
  initialiseData: {},
  loginStatus: {},
  userAccount: {},
  patients: {},
  patientsCounts: {},
  basicSearchPatient: {},
  advancedSearchPatient: {},
  clinicalQuerySearch: {},
  patientsSummaries: {},
  isSidebarVisible: false,
  profileAppPreferences: {},
  patientsInfo: {},
  requestError: {},
};

//create store and enhance with middleware
let store;
if (process.env.NODE_ENV === 'production') {
  store = createStore(rootReducer, initialState, applyMiddleware(epicMiddleware, routerMiddlewareInstance, loadingBarMiddleware({ promiseTypeSuffixes: ['REQUEST', 'SUCCESS', 'FAILURE'] })));
} else {
  const composeEnhancers = window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose;
  store = createStore(rootReducer, initialState, composeEnhancers(applyMiddleware(epicMiddleware, routerMiddlewareInstance, loadingBarMiddleware({ promiseTypeSuffixes: ['REQUEST', 'SUCCESS', 'FAILURE'] }))));
}

//initialisation
store.dispatch(initialiseStart());

render(
  //Provider allows us to receive data from store of our app (by connect function)
  <Provider store={store}>
    <ConnectedRouter history={history}>
      <Router>
        <App />
      </Router>
    </ConnectedRouter>
  </Provider>,
  document.getElementById('app-root'),
);
