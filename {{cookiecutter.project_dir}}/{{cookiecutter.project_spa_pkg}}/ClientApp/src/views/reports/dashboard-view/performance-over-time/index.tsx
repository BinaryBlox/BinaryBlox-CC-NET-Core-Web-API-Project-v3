import React, { FC } from "react";
import clsx from "clsx"; 
import PerfectScrollbar from "react-perfect-scrollbar";
import {
  Box,
  Card,
  CardHeader,
  CardContent,
  Divider,
  makeStyles,
} from "@material-ui/core";
import BxGenericMoreButton from "../../../../components/bx-lib/BxGenericMoreButton";
import Chart from "./Chart";

interface PerformanceOverTimeProps {
  className?: string;
}

const useStyles = makeStyles(() => ({
  root: {},
  chart: {
    height: "100%",
  },
}));

const PerformanceOverTime: FC<PerformanceOverTimeProps> = ({
  className,
  ...rest
}) => {
  const classes = useStyles();
  const performance = {
    thisWeek: {
      data: [],
      labels: [],
    },
    thisMonth: {
      data: [],
      labels: [],
    },
    thisYear: {
      data: [10, 5, 11, 20, 13, 28, 18, 4, 13, 12, 13, 5],
      labels: [
        "Jan",
        "Feb",
        "Mar",
        "Apr",
        "May",
        "Jun",
        "Jul",
        "Aug",
        "Sep",
        "Oct",
        "Nov",
        "Dec",
      ],
    },
  };

  return (
    <Card className={clsx(classes.root, className)} {...rest}>
      <CardHeader
        action={<BxGenericMoreButton />}
        title="Performance Over Time"
      />
      <Divider />
      <CardContent>
        <PerfectScrollbar>
          <Box height={375} minWidth={500}>
            <Chart
              className={classes.chart}
              data={performance.thisYear.data}
              labels={performance.thisYear.labels}
            />
          </Box>
        </PerfectScrollbar>
      </CardContent>
    </Card>
  );
};
 

export default PerformanceOverTime;
