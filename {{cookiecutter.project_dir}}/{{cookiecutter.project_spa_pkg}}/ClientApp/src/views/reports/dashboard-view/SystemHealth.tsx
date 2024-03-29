import React, { FC } from "react";
import clsx from "clsx";
import {
  Box,
  Card,
  LinearProgress,
  Typography,
  makeStyles,
  Theme,
} from "@material-ui/core";

interface SystemHealthProps {
  className?: string;
}

const useStyles = makeStyles((theme: Theme) => ({
  root: {
    padding: theme.spacing(3),
  },
  progress: {
    margin: theme.spacing(0, 1),
    flexGrow: 1,
  },
}));

const SystemHealth: FC<SystemHealthProps> = ({ className, ...rest }) => {
  const classes = useStyles();
  const data = {
    value: 97,
  };

  return (
    <Card className={clsx(classes.root, className)} {...rest}>
      <Typography
        component="h3"
        gutterBottom
        variant="overline"
        color="textSecondary"
      >
        System Health
      </Typography>
      <Box display="flex" alignItems="center" flexWrap="wrap">
        <Typography variant="h3" color="textPrimary">
          {data.value}%
        </Typography>
        <LinearProgress
          className={classes.progress}
          value={data.value}
          color="secondary"
          variant="determinate"
        />
      </Box>
    </Card>
  );
};

export default SystemHealth;
