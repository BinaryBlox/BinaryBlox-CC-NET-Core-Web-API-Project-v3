import React from "react";

import { Link as RouterLink } from "react-router-dom";
import clsx from "clsx";
import { Typography, Breadcrumbs, Link, makeStyles } from "@material-ui/core";
import NavigateNextIcon from "@material-ui/icons/NavigateNext";
import { IAccountViewHeader } from "./AccountView.types";
import { useAccountViewHeaderStyles } from "./AccountView.styles";

const AccountViewHeader: React.FC<IAccountViewHeader> = ({
  className,
  notifications,
  subscriptions,
  security,
  profile,
  settings,
  ...rest
}) => {
  const classes = useAccountViewHeaderStyles();

  return (
    <div className={clsx(classes.root, className)} {...rest}>
      <Breadcrumbs
        separator={<NavigateNextIcon fontSize="small" />}
        aria-label="breadcrumb"
      >
        <Link color="inherit" to="/app" component={RouterLink}>
          Dashboard
        </Link>
        <Typography color="textPrimary">Account</Typography>
      </Breadcrumbs>
      <Typography variant="h3" color="textPrimary">
        Settings
      </Typography>
    </div>
  );
};

export default AccountViewHeader;
