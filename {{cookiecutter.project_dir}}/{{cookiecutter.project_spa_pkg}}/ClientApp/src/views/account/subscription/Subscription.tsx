import React, { useState, useEffect, useCallback } from "react";
import { Link as RouterLink } from "react-router-dom";
import clsx from "clsx";
import {
  Box,
  Button,
  Card,
  CardContent,
  CardHeader,
  Divider,
  Link,
  Paper,
  Typography,
} from "@material-ui/core";
import { useBxIsMountedRef } from "binaryblox-react-ui";
import { IAccountSubscription } from "./Subscription.types";
import { useSubscriptionStyles } from "./Subscription.styles";
import { axiosUtil } from "../../../utils";

const Subscription: React.FC<IAccountSubscription> = ({
  className,
  ...rest
}) => {
  const classes = useSubscriptionStyles();
  const isMountedRef = useBxIsMountedRef();
  const [subscription, setSubscription] = useState<any | null>(null);

  const getSubscription = useCallback(async () => {
    try {
      const response = await axiosUtil.get("/api/account/subscription");

      if (isMountedRef.current) {
        setSubscription(response.data.subscription);
      }
    } catch (err) {
      console.error(err);
    }
  }, [isMountedRef]);

  useEffect(() => {
    getSubscription();
  }, [getSubscription]);

  if (!subscription) {
    return null;
  }

  return (
    <Card className={clsx(classes.root, className)} {...rest}>
      <CardHeader title="Manage your subscription" />
      <Divider />
      <CardContent>
        <Paper variant="outlined">
          <Box className={classes.overview}>
            <div>
              <Typography display="inline" variant="h4" color="textPrimary">
                {subscription.currency}
                {subscription.price}
              </Typography>
              <Typography display="inline" variant="subtitle1">
                /mo
              </Typography>
            </div>
            <Box display="flex" alignItems="center">
              <img
                alt="Product"
                className={classes.productImage}
                src="/static/images/products/product_premium.svg"
              />
              <Typography variant="overline" color="textSecondary">
                {subscription.name}
              </Typography>
            </Box>
          </Box>
          <Divider />
          <Box className={classes.details}>
            <div>
              <Typography variant="body2" color="textPrimary">
                {`${subscription.proposalsLeft} proposals left`}
              </Typography>
              <Typography variant="body2" color="textPrimary">
                {`${subscription.templatesLeft} templates`}
              </Typography>
            </div>
            <div>
              <Typography variant="body2" color="textPrimary">
                {`${subscription.invitesLeft} invites left`}
              </Typography>
              <Typography variant="body2" color="textPrimary">
                {`${subscription.adsLeft} ads left`}
              </Typography>
            </div>
            <div>
              {subscription.hasAnalytics && (
                <Typography variant="body2" color="textPrimary">
                  Analytics dashboard
                </Typography>
              )}
              {subscription.hasEmailAlerts && (
                <Typography variant="body2" color="textPrimary">
                  Email alerts
                </Typography>
              )}
            </div>
          </Box>
        </Paper>
        <Box mt={2} display="flex" justifyContent="flex-end">
          <Button size="small" color="secondary" variant="contained">
            Upgrade plan
          </Button>
        </Box>
        <Box mt={2}>
          <Typography variant="body2" color="textSecondary">
            The refunds don&apos;t work once you have the subscription, but you
            can always{" "}
            {/* <Link color="secondary" component={RouterLink} to="#">
              Cancel your subscription
            </Link> */}
            .
          </Typography>
        </Box>
      </CardContent>
    </Card>
  );
};

export default Subscription;
