import "prismjs/prism";
import "prismjs/components/prism-bash";
import "prismjs/components/prism-javascript";
import "prismjs/components/prism-jsx";
import "./_mocks";
import React from "react";
import ReactDOM from "react-dom"; 
import { Provider } from "react-redux";
import { SettingsProvider } from "binaryblox-react-ui"; 
import { enableES5 } from "immer";
import "./"
import App from "./App"; 
import store from "./store/";
import * as serviceWorker from "./serviceWorker";
import "./environment/env"
// const baseUrl = document
//   .getElementsByTagName("base")[0]
//   .getAttribute("href") as string | undefined; 

enableES5();

ReactDOM.render(
  <Provider store={store}>
    <SettingsProvider> 
        <App /> 
    </SettingsProvider>
  </Provider>,
  document.getElementById("root")
);

serviceWorker.register();


// Uncomment the line above that imports the serviceWorker function
// and the line below to register the generated service worker.
// By default create-react-app includes a service worker to improve the
// performance of the application by caching static assets. This service
// worker can interfere with the Identity UI, so it is
// disabled by default when Identity is being used.
//
//serviceWorker.register();
// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
//serviceWorker.unregister();
