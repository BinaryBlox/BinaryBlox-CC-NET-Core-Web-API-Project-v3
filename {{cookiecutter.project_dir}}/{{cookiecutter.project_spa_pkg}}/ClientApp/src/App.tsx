import React, { Component, useEffect, useMemo, useState, FC } from "react";
import { Router, Route, Redirect } from "react-router-dom";
import { Layout } from "./components/deprecate/Layout";
import { Home } from "./components/deprecate/Home";
import { FetchData } from "./components/deprecate/FetchData";
import { Counter } from "./components/deprecate/Counter";
import AuthorizeRoute from "./components/deprecate/api-authorization/AuthorizeRoute";
import ApiAuthorizationRoutes from "./components/deprecate/api-authorization/ApiAuthorizationRoutes";
import { ApplicationPaths } from "./components/deprecate/api-authorization/ApiAuthorizationConstants";
import { AuthProvider } from "oidc-react";
import DashboardContainer from "./layouts/dashboard-layout/DashboardLayout.container";
import {
  bxThemeLight,
  bxThemeDark,
  bxThemeAltDark,
  bxThemeAltLight,
  BxSettingsContext,
  ISettingsProvider,
  useBxSettings,
} from "binaryblox-react-ui";
import {
  ThemeProvider,
  CssBaseline,
  makeStyles,
  Theme,
  Box,
  TextField,
} from "@material-ui/core";
import MainLayout from "./layouts/main-layout";
import { createBrowserHistory } from "history";
import { GlobalStyles, ScrollReset } from "./components";
import { routingUtil } from "./utils";
import BxLoadingScreen from "./components/bx-lib/BxLoadingScreen";
import routes from "./routes";

import {
  getAccountFeatureSelectors,
  accountFeatureReducers,
  accountFeatureRequests,
  BxClientApplicationRequest,
  BxClientApplicationViewModel,

  // BxClientApplicationDTO,
  // IBxRequestMetadata,
  // RequestType,
  BxClientApplicationApiFactory,
} from "./lib/propertystax-account";

import { 
  configurationFeatureActions,
  configurationFeatureReducers,
  configurationFeatureRequests,
  configurationFeatureEntityAdapters,
  configurationFeatureStateTypes,
  BxConfigurationRequest,
  BxConfigurationViewModel,
  BxConfigurationAggregateRequest,
  BxConfigurationAggregateApiFactory,
  // BxClientApplicationDTO,
  // IBxRequestMetadata,
  // RequestType,
  BxConfigurationApiFactory,
} from "binaryblox-configuration-api";
import { RootState } from "./store";

import logo from "./logo.svg";
import LoggedIn from "./LoggedIn";
import "./App.css";
import { useDispatch, useSelector } from "react-redux";
import axiosInstance from "./utils/axios.util";
import { Button } from "reactstrap";

const App: React.FC = () => {
  //static displayName = App.name;

  const { settings } = useBxSettings();
  const [autoLogin, setAutoLogin] = useState<boolean>(true);

  settings.theme = "darkTheme";

  // const theme = createTheme({
  //   direction: settings.direction,
  //   responsiveFontSizes: settings.responsiveFontSizes,
  //   theme: settings.theme
  // });

  const users = [
    {
      id: "5e86809283e28b96d2d38537",
      avatar: "/static/images/avatars/avatar_6.png",
      canHire: false,
      country: "USA",
      email: "tony.henderson@kp.org",
      isPublic: true,
      name: "Tony Henderson",
      password: "Password123",
      phone: "+40 777666555",
      role: "admin",
      state: "New York",
      tier: "Premium",
    },
  ];

  const defaultUser = users.find(
    (_user) => _user.email === "tony.henderson@kp.org"
  );

  //   onSignIn: async (user: any) => {
  //     alert(process.env.IDENT_SIGN_IN_ALERT_MESSAGE);
  //     console.log(user);
  //     window.location.hash = "";
  //   },
  //   authority: process.env.IDENT_AUTHORITY ,
  //   clientId: process.env.IDENT_CLIENT ,
  //   responseType: process.env.IDENT_RESPONSE_TYPE ,
  //   redirectUri: process.env.IDENT_REDIRECT_URI ,
  //   post_logout_redirect_uri: process.env.IDENT_POST_LOGUOUT_REDIRECT_URL,
  // };

  const oidcConfig = { 
    authority: "{{cookiecutter.project_spa_oauth_authority_url}}",
    clientId: "{{cookiecutter.project_spa_oauth_client_id}}",
    responseType: "code",
    redirectUri:
      process.env.NODE_ENV === "development"
        ? "{{cookiecutter.project_spa_oauth_redirect_url}}"
        : "{{cookiecutter.project_spa_oauth_redirect_url}}",
    post_logout_redirect_uri:
      process.env.NODE_ENV === "development"
        ? "{{cookiecutter.project_spa_oauth_logout_redirect_url}}"
        : "{{cookiecutter.project_spa_oauth_logout_redirect_url}}",
  };

  const handleOnSignIn = async (user: any) => {
    //alert("You just signed in, BRO! Check out the console!"); 
    window.location.hash = "";
    setAutoLogin(true);
    history.push("/app/reports/dashboard");
  };
 
  const history = createBrowserHistory(); 
  return (
    <ThemeProvider theme={bxThemeDark}>
      <CssBaseline />
      <Router history={history}>
        <AuthProvider autoSignIn={false} onSignIn={handleOnSignIn} {...oidcConfig}>
          <GlobalStyles />
          <ScrollReset />
          {routingUtil.renderRoutes(routes, <BxLoadingScreen />)}
        </AuthProvider>
      </Router>
    </ThemeProvider>
  );
};

export default App;

// <Router history={history}>
// <GlobalStyles />
// <ScrollReset />
// <MainLayout>
// {routingUtil.renderRoutes(routes, <LoadingScreen/>)}
// {/* <Route exact path="/" component={Home} />
//   <Route path="/counter" component={Counter} />

//   <AuthorizeRoute path="/fetch-data" component={FetchData} />
//   <Route
//     path={ApplicationPaths.ApiAuthorizationPrefix}
//     component={ApiAuthorizationRoutes}
//   /> */}
// </MainLayout>
// {/* <DashboardContainer user={defaultUser}></DashboardContainer> */}
// </Router>

// <AuthProvider autoSignIn={false} onSignIn={onSignIn} {...oidcConfig}>
// {/* <div className="App">
//   <header className="App-header">
//     <img src={logo} className="App-logo" alt="logo" />
//     <p>OIDC React</p>
//     <LoggedIn />
//   </header>
// </div> */}
// <Router history={history}>
//   <GlobalStyles />
//   <ScrollReset />
//     {routingUtil.renderRoutes(routes, <LoadingScreen />)}
// </Router>
// </AuthProvider>
