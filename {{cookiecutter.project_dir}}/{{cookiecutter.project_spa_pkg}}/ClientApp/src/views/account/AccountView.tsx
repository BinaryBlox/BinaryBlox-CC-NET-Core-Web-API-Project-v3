import { IAccountView } from "./AccountView.types";

// const AccountView: React.FC<AccountViewProps> = ({ foo }) => (
//     <div data-testid="AccountView" className="foo-bar">{foo}</div>
// );

import React, { useState, FC, ChangeEvent } from "react";
import {
  Box,
  Container,
  Divider,
  Tab,
  Tabs,
  makeStyles,
  Theme,
} from "@material-ui/core";

import { BxPage } from "binaryblox-react-ui";

import General from "./general/General";
import Subscription from "./subscription/Subscription";
import Notifications from "./notifications/Notifications";
import Security from "./security/Security";
import AccountViewHeader from "./AccountView.header";
import { useAccountViewStyles } from "./AccountView.styles";

const AccountView: React.FC<IAccountView> = () => {
  const classes = useAccountViewStyles();
  const [currentTab, setCurrentTab] = useState<string>("general");

  console.log("HERE AT ACCOUNT")
  const tabs = [
    { value: "general", label: "General" },
    { value: "subscription", label: "Subscription" },
    { value: "notifications", label: "Notifications" },
    { value: "security", label: "Security" },
  ];

  const handleTabsChange = (event: ChangeEvent<{}>, value: string): void => {
    setCurrentTab(value);
  };

  return (
    <BxPage translate="no" className={classes.root} title="Settings">
      <Container maxWidth="lg">
        <AccountViewHeader />
        <Box mt={3}>
          <Tabs
            onChange={handleTabsChange}
            scrollButtons="auto"
            value={currentTab}
            variant="scrollable"
            textColor="secondary"
          >
            {tabs.map((tab) => (
              <Tab key={tab.value} label={tab.label} value={tab.value} />
            ))}
          </Tabs>
        </Box>
        <Divider />
        <Box mt={3}>
          {currentTab === "general" && <General />}
          {currentTab === "subscription" && <Subscription />}
          {currentTab === "notifications" && <Notifications />}
          {currentTab === "security" && <Security />}
        </Box>
      </Container>
    </BxPage>
  );
};

export default AccountView;
