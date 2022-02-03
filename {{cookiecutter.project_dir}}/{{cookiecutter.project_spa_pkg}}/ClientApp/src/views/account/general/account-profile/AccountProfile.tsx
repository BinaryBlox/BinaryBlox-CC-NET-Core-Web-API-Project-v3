import React from "react";
import { Link as RouterLink } from "react-router-dom";
import PropTypes from "prop-types";
import clsx from "clsx";
import {
  Avatar,
  Box,
  Button,
  Card,
  CardActions,
  CardContent,
  Link,
  Typography, 
} from "@material-ui/core";
import { useBxAuth, AuthUser } from "binaryblox-react-ui";
import { IAccountProfile } from "./AccountProfile.types";
import { useAccountProfileStyles } from "./AccountProfile.styles";

const AccountProfile: React.FC<IAccountProfile> = ({
  className,
  user,
  ...rest
}) => {
  const classes = useAccountProfileStyles();

  return (
    <Card className={clsx(classes.root, className)} {...rest}>
      <CardContent>
        <Box
          display="flex"
          alignItems="center"
          flexDirection="column"
          textAlign="center"
        >
          <Avatar
            className={classes.avatar}
            src={user ? user.avatar : "test"}
          />
          <Typography
            className={classes.name}
            color="textPrimary"
            gutterBottom
            variant="h3"
          >
            {user ? user.name : "Tony"}
          </Typography>
          <Typography color="textPrimary" variant="body1">
            Your tier:{" "}
            {/* <Link
              component={RouterLink}
              to="/pricing"
            >
              {user ? user.tier : "premium"}
            </Link> */}
          </Typography>
        </Box>
      </CardContent>
      <CardActions>
        <Button fullWidth variant="text">
          Remove picture
        </Button>
      </CardActions>
    </Card>
  );
};

AccountProfile.propTypes = {
  className: PropTypes.string,
  // @ts-ignore
  user: PropTypes.object.isRequired,
};

export default AccountProfile;
