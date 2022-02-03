import React, { FC } from "react";
import { Link as RouterLink } from "react-router-dom";
import clsx from "clsx";
import { useAuth } from "oidc-react";

import {
  AppBar,
  Box,
  Button,
  Divider,
  Toolbar,
  Hidden,
  Typography,
  Link,
  makeStyles,
} from "@material-ui/core";
import { AppConstants } from "../../constants";
import { BxPrimaryLogo } from "binaryblox-react-ui";

interface TopBarProps {
  className?: string;
}

const useStyles = makeStyles((theme) => ({
  root: {
    backgroundColor: theme.palette.background.default,
  },
  appbar: {
    margin: 0, // For no gap in topbar
    zIndex: theme.zIndex.drawer + 100,
  },
  toolbar: {
    minHeight: 48,
    paddingLeft: theme.spacing(1.5),
  },
  title: {
    ...theme.typography.h6,
    fontWeight: "bold",
    color: "inherit",
    paddingTop: theme.spacing(1),
    // paddingLeft: theme.spacing(2),
  },
  subtitle: {
    ...theme.typography.body2,
    color: "inherit",
    // paddingLeft: theme.spacing(2),
    paddingTop: theme.spacing(0.5),
    paddingBottom: theme.spacing(0),
  },
  logo: {
    marginRight: theme.spacing(2),
  },
  link: {
    fontWeight: theme.typography.fontWeightMedium,
    "& + &": {
      marginLeft: theme.spacing(2),
    },
  },
  divider: {
    width: 1,
    height: 32,
    marginLeft: theme.spacing(2),
    marginRight: theme.spacing(2),
  },
}));

const TopBar: FC<TopBarProps> = ({ className, ...rest }) => {
  const classes = useStyles();
  const auth = useAuth();

  const handleSignIn = () => {
    auth.signIn();
  };

  return (
    <AppBar className={classes.appbar} {...rest}>
      <Toolbar className={classes.toolbar}>
        <RouterLink to="/">
          <BxPrimaryLogo className={classes.logo} />
        </RouterLink>
        <Hidden mdDown>
          <Typography variant="caption" color="textSecondary">
            Version {AppConstants.APP_VERSION}
          </Typography>
        </Hidden>
        <Box flexGrow={1} />
        <Link
          className={classes.link}
          color="textSecondary"
          component={RouterLink}
          to="/home"
          underline="none"
          variant="body2"
        >
          About
        </Link>
        <Link
          className={classes.link}
          color="textSecondary"
          component={RouterLink}
          to="/home"
          underline="none"
          variant="body2"
        >
          Features
        </Link>
        {/* <Link
          className={classes.link}
          color="textSecondary"
          component={RouterLink}
          to="/pricing"
          underline="none"
          variant="body2"
        >
          Pricing
        </Link> */}
        <Link
          className={classes.link}
          color="textSecondary"
          component={RouterLink}
          to="/home"
          underline="none"
          variant="body2"
          onClick={handleSignIn}
        >
          login
        </Link>
        <Divider className={classes.divider} />
        <Button
          color="secondary"
          component="a"
          variant="contained"
          size="small"
          onClick={handleSignIn}
        >
          Sign Up
        </Button>
      </Toolbar>
    </AppBar>
  );
};

export default TopBar;
