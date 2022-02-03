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
import BxLabel from "../../../components/bx-lib/BxLabel";

interface TodaysMoneyProps {
  className?: string;
}

const useStyles = makeStyles((theme: Theme) => ({
  root: {
    padding: theme.spacing(3),
    display: "flex",
    alignItems: "center",
    justifyContent: "space-between",
  },
  BxLabel: {
    marginLeft: theme.spacing(1),
  },
  avatar: {
    backgroundColor: theme.palette.secondary.main,
    color: theme.palette.secondary.contrastText,
    height: 48,
    width: 48,
  },
}));

const TodaysMoney: FC<TodaysMoneyProps> = ({ className, ...rest }) => {
  const classes = useStyles();
  const data = {
    value: "24,000",
    currency: "$",
    difference: 4,
  };

  return (
    <Card className={clsx(classes.root, className)} {...rest}>
      <Box flexGrow={1}>
        <Typography
          component="h3"
          gutterBottom
          variant="overline"
          color="textSecondary"
        >
          Todays money
        </Typography>
        <Box display="flex" alignItems="center" flexWrap="wrap">
          <Typography variant="h3" color="textPrimary">
            {data.currency}
            {data.value}
          </Typography>
          <BxLabel
            className={classes.BxLabel}
            color={data.difference > 0 ? "success" : "error"}
          >
            {data.difference > 0 ? "+" : ""}
            {data.difference}%
          </BxLabel>
        </Box>
      </Box>
      <Avatar className={classes.avatar}>
        <AttachMoneyIcon />
      </Avatar>
    </Card>
  );
}; 

export default TodaysMoney;
