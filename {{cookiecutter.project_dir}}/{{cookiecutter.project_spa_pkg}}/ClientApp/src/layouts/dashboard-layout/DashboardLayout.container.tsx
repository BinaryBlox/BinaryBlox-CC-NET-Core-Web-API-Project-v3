/* eslint-disable no-use-before-define */
/* eslint-disable react/prop-types */
import React, { FC, useEffect, useState } from "react";

import {
  BxSideBarHeader,
  BxSideBarFooter,
  BxPage,
  BxDashboardLayout,
  useBxConstructor,
  IMenuItem,
} from "binaryblox-react-ui";
import {
  BxTBarSettings,
  BxTBarSearch,
  BxTBarContacts,
  BxTBarAccount,
} from "binaryblox-react-ui-views";

import { Box } from "@material-ui/core";
import { IDashboardContainer, useMainDashboardStyles } from ".";
import { useDispatch } from "react-redux";
import { fetchMenuDatas } from "../../store/slices/entities/menu-data/MenuData.effects";
import { useSelector } from "../../store";
import { selectAllMenuDatas } from "../../store/slices/entities/menu-data/MenuData.slice";
import { users, sections } from "./DashboardLayout.data";
import {
  useAuth,
  AuthProvider,
  AuthContext,
  AuthContextProps,
} from "oidc-react";

const defaultState = users.find(
  (_user) => _user.email === "tony.henderson@kp.org"
);

const DashboardContainer: FC<IDashboardContainer> = ({ user, children }) => {
  const classes = useMainDashboardStyles();
  const dispatch = useDispatch();

  // For psuedo constructor
  const [hasInstantiated, setHasInstantiated] = useState(false);
  const [defaultUser, setDefaultUser] = useState(defaultState);
  const auth = useAuth();

  const handleSignOut = () => {
    console.log("HERE AT SIGNOUT");
    auth.signOutRedirect();
  };

  const menuItems: IMenuItem[] = [
    {
      key: "Profile-Test".toLowerCase().replace(" ", "-"),
      label: "Profile",
      path: "/app/social/profile",
      clickHandler: undefined,
    },
    {
      key: "Account-Test".toLowerCase().replace(" ", "-"),
      label: "Account",
      path: "/app/account",
      clickHandler: undefined,
    },
    {
      key: "Logout-Test".toLowerCase().replace(" ", "-"),
      label: "Logout",
      path: undefined,
      clickHandler: handleSignOut,
    },
  ];

  useBxConstructor(() => {
    console.log(
      `DEFAULT USER: ${JSON.stringify(auth.userData.profile, null, "\t")}`
    );
    defaultUser.name = `${auth.userData.profile.givenName} ${auth.userData.profile.familyName}`;
    setDefaultUser(defaultUser);
    console.log(`DEFAULT USER: ${auth.userData.profile.givenName}`);
    // Do something before render
    if (hasInstantiated) return;
    console.log("before render");

    //dispatch(fetchShows());
    setHasInstantiated(true);
  });

  //  Using useEffect to call the API once mounted and set the data
  useEffect(() => {
    //(async () => {

    dispatch(fetchMenuDatas());
  }, []);

  const allMenuData = useSelector(selectAllMenuDatas);

  return (
    <>
      <BxDashboardLayout
        styleClass={useMainDashboardStyles}
        onMobileClose={null}
        openMobile={null}
        onMobileNavOpen={null}
        menuItems={menuItems}
        groupedList={sections}
        header={
          <>
            <BxSideBarHeader user={defaultUser} />
          </>
        }
        footer={
          <>
            <BxSideBarFooter user={defaultUser} />
          </>
        }
        topbarChildren={
          <>
            <BxTBarSearch></BxTBarSearch>
            {/* <BxTBarContacts></BxTBarContacts> */}
            <BxTBarSettings></BxTBarSettings> 
            <Box ml={2}>
              <BxTBarAccount
                user={user ? user : defaultState}
                menuItems={menuItems}
              />
            </Box>
          </>
        }
      >
        <BxPage title="Dashboard" translate={'no'}>
          {/* <BxTable.DynamicTable
                filter={"name"}
                columns={columns}
                data={allShows}
                dense={true}
                onDataChange={handleDataChanged}
                renderRowSubComponent={renderRowSubComponent}
              /> */}
        </BxPage>
        {children}
      </BxDashboardLayout>
    </>
  );
};

export default DashboardContainer;
