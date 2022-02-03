import { makeStyles, Theme } from "@material-ui/core";

export const useAccountViewStyles = makeStyles((theme: Theme) => ({
  root: {
    backgroundColor: theme.palette.background.default,
    minHeight: "100%",
    paddingTop: theme.spacing(3),
    paddingBottom: theme.spacing(3),
  },
}));

export const useAccountViewHeaderStyles = makeStyles(() => ({
  root: {},
}));
