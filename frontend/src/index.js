import React from 'react'
import ReactDOM from 'react-dom';
import Store from './Store/Store';
import { Provider } from 'react-redux';
import App from './Components/App';

import './Styles/Index.scss';

ReactDOM.render(
  <React.StrictMode>
    <Provider store={Store}>
      <App />
    </Provider>
  </React.StrictMode>,
  document.getElementById('root')
);