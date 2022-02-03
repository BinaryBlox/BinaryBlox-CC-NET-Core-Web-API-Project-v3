import React, { useState, useEffect, useCallback, FC } from "react";
import { Link as RouterLink } from "react-router-dom";
import clsx from "clsx";
import {
  Box,
  Button,
  Card,
  CardHeader,
  List,
  ListItem,
  ListItemText,
  Typography,
  makeStyles,
  Theme,
} from "@material-ui/core";
import { useBxIsMountedRef } from "binaryblox-react-ui";
import NavigateNextIcon from "@material-ui/icons/NavigateNext";

import Chart from "./Chart";

interface RealTimeProps {
  className?: string;
}

const useStyles = makeStyles((theme: Theme) => ({
  root: {},
  current: {
    marginTop: theme.spacing(0.5),
    marginRight: theme.spacing(0.5),
  },
}));

const getRandomInt = (min: number, max: number): number => {
  // eslint-disable-next-line no-param-reassign
  min = Math.ceil(min);
  // eslint-disable-next-line no-param-reassign
  max = Math.floor(max);

  return Math.floor(Math.random() * (max - min + 1)) + min;
};

const RealTime: FC<RealTimeProps> = ({ className, ...rest }) => {
  const classes = useStyles();
  const isMountedRef = useBxIsMountedRef();
  const [data, setData] = useState<any[]>([
    163,
    166,
    161,
    159,
    99,
    163,
    173,
    166,
    167,
    183,
    176,
    172,
  ]);

  const getData = useCallback(() => {
    if (isMountedRef.current) {
      setData((prevData) => {
        const newData = [...prevData];

        newData.shift();
        newData.push(0);

        return newData;
      });
    }

    setTimeout(() => {
      if (isMountedRef.current) {
        setData((prevData) => {
          const newData = [...prevData];
          const random = getRandomInt(100, 200);

          newData.pop();
          newData.push(random);

          return newData;
        });
      }
    }, 500);
  }, [isMountedRef]);

  useEffect(() => {
    setInterval(() => getData(), 2000);
  }, [getData]);

  const labels = data.map((value, i) => i);

  const pages = [
    {
      pathname: "/app/projects",
      views: "24",
    },
    {
      pathname: "/app/chat",
      views: "21",
    },
    {
      pathname: "/cart",
      views: "15",
    },
    {
      pathname: "/cart/checkout",
      views: "8",
    },
  ];

  const headerClasses = { action: classes.current };

  const subheaderTypographyProps: any = { color: "textSecondary", variant: "body2" };
  const titleTypographyProps: any = { color: "textPrimary" };
  const primaryTypographyProps: any = { color: "textSecondary", variant: "body2" };

  return (
    <Card className={clsx(classes.root, className)} {...rest}>
      <CardHeader
        action={
          <Typography color="inherit" variant="h3">
            {data[data.length - 1] === 0
              ? data[data.length - 2]
              : data[data.length - 1]}
          </Typography>
        }
        classes={headerClasses}
        subheader="Page views per second"
        subheaderTypographyProps={subheaderTypographyProps}
        title="Active users"
        titleTypographyProps={titleTypographyProps}
      />
      <Chart data={data} labels={labels} />
      <List>
        {pages.map((page) => (
          <ListItem divider key={page.pathname}>
            <ListItemText
              primary={page.pathname}
              primaryTypographyProps={primaryTypographyProps}
            />
            <Typography color="inherit">{page.views}</Typography>
          </ListItem>
        ))}
      </List>
      <Box p={2} display="flex" justifyContent="flex-end">
        <Button
          component={RouterLink}
          size="small"
          to="#"
          endIcon={<NavigateNextIcon />}
        >
          See all
        </Button>
      </Box>
    </Card>
  );
};

export default RealTime;
