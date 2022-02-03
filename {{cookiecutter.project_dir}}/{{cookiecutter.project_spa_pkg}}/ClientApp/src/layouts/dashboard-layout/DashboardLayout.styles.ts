import { makeStyles, Theme } from "@material-ui/core";

export const useMainDashboardStyles = makeStyles((theme: Theme) => ({
    root: {
      backgroundColor: theme.palette.background.default,
      display: "flex",
      height: "100%",
      overflow: "hidden",
      width: "100%" 
    },
    content: {
      flex: "1 1 auto",
      height: "100%",
      overflow: "auto",
    }, 
    dashboardContainer: {
      display: "flex",
      flex: "1 1 auto",
      overflow: "hidden",
    },
    dashboardWrapper: {
      display: "flex",
      flex: "1 1 auto",
      overflow: "hidden", 
      paddingTop: 64,
      [theme.breakpoints.up("lg")]: {
        paddingLeft: 256,
      },
    },
  
    title: {
      ...theme.typography.h5,
      // fontWeight: "bold",
      color: "inherit",
      paddingTop: theme.spacing(1),
      // paddingLeft: theme.spacing(2),
    },
    subtitle: {
      ...theme.typography.body2,
      color: "textSecondary",
      // paddingLeft: theme.spacing(2),
      // paddingTop: theme.spacing(0.5),
      // paddingBottom: theme.spacing(0),
    },
    appbar: {
      margin: 0, // For no gap in topbar
      zIndex: theme.zIndex.drawer + 100 
    },
    toolbar: { 
      minHeight: 48,
      paddingLeft: theme.spacing(1.5)  // Padding of drawer icon
    },
    mobileDrawer: {
      margin: 0, // For no gap in topbar
      width: 256,
    },
    desktopDrawer: {
      margin: 0, // For no gap in topbar
      width: 256,
      top: 64,
      height: "calc(100% - 64px)",
    },
    avatar: {
      cursor: "pointer",
      width: 64,
      height: 64,
    },
  }));