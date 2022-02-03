import { combineReducers } from "@reduxjs/toolkit";
import { reducer as menuDataReducer } from "./slices/entities/menu-data";
import { reducer as applicationClientViewModelReducer } from "./slices/entities/application-client-view-model";

// import { reducer as notificationReducer } from "./slices/notification";
import { useDispatch, useSelector } from "react-redux";
import { configurationFeatureReducers } from "binaryblox-configuration-api";
import { RootState } from ".";

const rootReducer = combineReducers({
  bxConfiguration: configurationFeatureReducers.bxConfiguration,
  bxConfigurationAggregate: configurationFeatureReducers.bxConfigurationAggregate,
  menuData: menuDataReducer,
  applicationClientViewModel: applicationClientViewModelReducer,
  // notifications: notificationReducer,
});

export default rootReducer;
