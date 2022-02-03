import { makeStyles, Theme } from "@material-ui/core";

export const useSubscriptionStyles = makeStyles((theme: Theme) => ({
  root: {},
  overview: {
    padding: theme.spacing(3),
    display: "flex",
    alignItems: "center",
    flexWrap: "wrap",
    justifyContent: "space-between",
    [theme.breakpoints.down("md")]: {
      flexDirection: "column-reverse",
      alignItems: "flex-start",
    },
  },
  productImage: {
    marginRight: theme.spacing(1),
    height: 48,
    width: 48,
  },
  details: {
    padding: theme.spacing(3),
    display: "flex",
    alignItems: "center",
    flexWrap: "wrap",
    justifyContent: "space-between",
    [theme.breakpoints.down("md")]: {
      flexDirection: "column",
      alignItems: "flex-start",
    },
  },
}));
