import React, { FC } from "react";
import clsx from "clsx";
import {
  Avatar,
  Box,
  Card,
  Typography,
  makeStyles,
  Theme,
} from "@material-ui/core";
import AttachMoneyIcon from "@material-ui/icons/AttachMoney";

interface RoiPerCustomerProps {
  className?: string;
}

const useStyles = makeStyles((theme: Theme) => ({
  root: {
    color: theme.palette.secondary.contrastText,
    backgroundColor: theme.palette.secondary.main,
    padding: theme.spacing(3),
    display: "flex",
    alignItems: "center",
    justifyContent: "space-between",
  },
  avatar: {
    backgroundColor: theme.palette.secondary.contrastText,
    color: theme.palette.secondary.main,
    height: 48,
    width: 48,
  },
}));

const RoiPerCustomer: FC<RoiPerCustomerProps> = ({ className, ...rest }) => {
  const classes = useStyles();
  const data = {
    value: "25.50",
    currency: "$",
  };

  return (
    <Card className={clsx(classes.root, className)} {...rest}>
      <Box flexGrow={1}>
        <Typography
          color="inherit"
          component="h3"
          gutterBottom
          variant="overline"
        >
          Roi per customer
        </Typography>
        <Box display="flex" alignItems="center" flexWrap="wrap">
          <Typography color="inherit" variant="h3">
            {data.currency}
            {data.value}
          </Typography>
        </Box>
      </Box>
      <Avatar className={classes.avatar} color="inherit">
        <AttachMoneyIcon />
      </Avatar>
    </Card>
  );
};

export default RoiPerCustomer;
