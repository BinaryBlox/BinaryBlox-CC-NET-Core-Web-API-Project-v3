import { makeStyles, Theme } from "@material-ui/core";

export const useAccountProfileStyles = makeStyles((theme: Theme) => ({
  root: {},
  name: {
    marginTop: theme.spacing(1),
  },
  avatar: {
    height: 100,
    width: 100,
  },
}));
