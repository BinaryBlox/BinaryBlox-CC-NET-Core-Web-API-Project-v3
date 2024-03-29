import React, { useRef, useState, FC } from "react";
import { Link as RouterLink } from "react-router-dom";
import clsx from "clsx";
import {
  Breadcrumbs,
  Button,
  Grid,
  Link,
  Menu,
  MenuItem,
  SvgIcon,
  Typography,
  makeStyles,
  PopoverOrigin,
} from "@material-ui/core";
import NavigateNextIcon from "@material-ui/icons/NavigateNext";
import { Calendar as CalendarIcon } from "react-feather";

interface HeaderProps {
  className?: string;
}

const timeRanges = [
  {
    value: "today",
    text: "Today",
  },
  {
    value: "yesterday",
    text: "Yesterday",
  },
  {
    value: "last_30_days",
    text: "Last 30 days",
  },
  {
    value: "last_year",
    text: "Last year",
  },
];

const useStyles = makeStyles(() => ({
  root: {},
}));

const Header: FC<HeaderProps> = ({ className, ...rest }) => {
  const classes = useStyles();
  const actionRef = useRef<any>(null);
  const [isMenuOpen, setMenuOpen] = useState<boolean>(false);
  const [timeRange, setTimeRange] = useState<string>(timeRanges[2].text);

  const anchorOrigin: PopoverOrigin = {
    vertical: "bottom",
    horizontal: "center",
  };

  const transformOrigin: PopoverOrigin = {
    vertical: "top",
    horizontal: "center",
  };

  return (
    <Grid
      container
      spacing={3}
      justify="space-between"
      className={clsx(classes.root, className)}
      {...rest}
    >
      <Grid item>
        <Breadcrumbs
          separator={<NavigateNextIcon fontSize="small" />}
          aria-label="breadcrumb"
        >
          <Link
            variant="body1"
            color="inherit"
            to="/app"
            component={RouterLink}
          >
            Dashboard
          </Link>
          <Typography variant="body1" color="textPrimary">
            Reports
          </Typography>
        </Breadcrumbs>
        <Typography variant="h3" color="textPrimary">
          Here&apos;s what&apos;s happening
        </Typography>
      </Grid>
      <Grid item>
        <Button
          ref={actionRef}
          onClick={() => setMenuOpen(true)}
          startIcon={
            <SvgIcon fontSize="small">
              <CalendarIcon />
            </SvgIcon>
          }
        >
          {timeRange}
        </Button>
        <Menu
          anchorEl={actionRef.current}
          onClose={() => setMenuOpen(false)}
          open={isMenuOpen}
          getContentAnchorEl={null}
          anchorOrigin={anchorOrigin}
          transformOrigin={transformOrigin}
        >
          {timeRanges.map((_timeRange) => (
            <MenuItem
              key={_timeRange.value}
              onClick={() => setTimeRange(_timeRange.text)}
            >
              {_timeRange.text}
            </MenuItem>
          ))}
        </Menu>
      </Grid>
    </Grid>
  );
};

export default Header;
