import React, { FC } from "react";
import clsx from "clsx";
import {
  Box,
  Button,
  Container,
  Divider,
  Grid,
  Paper,
  Typography,
  makeStyles,
  Theme,
} from "@material-ui/core";
import { BxPage } from "binaryblox-react-ui";

const useStyles = makeStyles((theme: Theme) => ({
  root: {
    backgroundColor: theme.palette.background.default,
    minHeight: "100%",
    height: "100%",
    paddingTop: 120,
    paddingBottom: 120,
  },
  product: {
    position: "relative",
    padding: theme.spacing(5, 3),
    cursor: "pointer",
    transition: theme.transitions.create("transform", {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.leavingScreen,
    }),
    "&:hover": {
      transform: "scale(1.1)",
    },
  },
  productImage: {
    borderRadius: theme.shape.borderRadius,
    position: "absolute",
    top: -24,
    left: theme.spacing(3),
    height: 48,
    width: 48,
    fontSize: 24,
  },
  recommendedProduct: {
    backgroundColor: theme.palette.secondary.main,
    color: theme.palette.common.white,
  },
  chooseButton: {
    backgroundColor: theme.palette.common.white,
  },
}));

const PricingView: FC = () => {
  const classes = useStyles();

  return (
    <BxPage translate="no" className={classes.root} title="Pricing">
      <Container maxWidth="sm">
        <Typography align="center" variant="h1" color="textPrimary">
          The right price for you!
        </Typography>
        <Box mt={3}>
          <Typography align="center" variant="subtitle1" color="textSecondary">
            Enjoy contract processing that provides you and your borrowers the
            highest level of customer satisfaction. Our fee is paid by the
            Borrower in section B of the closing disclosure, allowing brokers
            and MLO's to earn and keep their full commission.
          </Typography>
        </Box>
      </Container>
      <Box mt="160px">
        <Container maxWidth="lg">
          <Grid container spacing={4}>
            <Grid item md={4} xs={12}>
              <Paper className={classes.product} elevation={1}>
                <img
                  alt="Product"
                  className={classes.productImage}
                  src="/static/images/products/product_standard.svg"
                />
                <Typography
                  component="h3"
                  gutterBottom
                  variant="overline"
                  color="textSecondary"
                >
                  Standard
                </Typography>
                <div>
                  <Typography
                    component="span"
                    display="inline"
                    variant="h3"
                    color="textPrimary"
                  >
                    $5
                  </Typography>
                  <Typography
                    component="span"
                    display="inline"
                    variant="subtitle2"
                    color="textSecondary"
                  >
                    /month
                  </Typography>
                </div>
                <Typography variant="overline" color="textSecondary">
                  Max 1 user
                </Typography>
                <Box my={2}>
                  <Divider />
                </Box>
                <Typography variant="body2" color="textPrimary">
                  1 loans/month
                  <br />
                  1 worklows
                  <br />
                  Pipeline dashboard
                  <br />
                  Email alerts
                </Typography>
                <Box my={2}>
                  <Divider />
                </Box>
                <Button
                  variant="contained"
                  fullWidth
                  className={classes.chooseButton}
                >
                  Choose
                </Button>
              </Paper>
            </Grid>
            <Grid item md={4} xs={12}>
              <Paper
                className={clsx(classes.product, classes.recommendedProduct)}
                elevation={1}
              >
                <img
                  alt="Product"
                  className={classes.productImage}
                  src="/static/images/products/product_premium--outlined.svg"
                />
                <Typography
                  component="h3"
                  gutterBottom
                  variant="overline"
                  color="inherit"
                >
                  Premium
                </Typography>
                <div>
                  <Typography
                    component="span"
                    display="inline"
                    variant="h3"
                    color="inherit"
                  >
                    $399
                  </Typography>
                  <Typography
                    component="span"
                    display="inline"
                    variant="subtitle2"
                    color="inherit"
                  >
                    /month
                  </Typography>
                </div>
                <Typography variant="overline" color="inherit">
                  Max 20 user
                </Typography>
                <Box my={2}>
                  <Divider />
                </Box>
                <Typography variant="body2" color="inherit">
                 200 loans/month
                  <br />
                  5 worklows
                  <br />
                  Pipeline dashboard
                  <br />
                  Email alerts
                </Typography>
                <Box my={2}>
                  <Divider />
                </Box>
                <Button
                  variant="contained"
                  fullWidth
                  className={classes.chooseButton}
                >
                  Choose
                </Button>
              </Paper>
            </Grid>
            <Grid item md={4} xs={12}>
              <Paper className={classes.product} elevation={1}>
                <img
                  alt="Product"
                  className={classes.productImage}
                  src="/static/images/products/product_extended.svg"
                />
                <Typography
                  component="h3"
                  gutterBottom
                  variant="overline"
                  color="textSecondary"
                >
                  Extended
                </Typography>
                <div>
                  <Typography
                    component="span"
                    display="inline"
                    variant="h3"
                    color="textPrimary"
                  >
                    $259
                  </Typography>
                  <Typography
                    component="span"
                    display="inline"
                    variant="subtitle2"
                    color="textSecondary"
                  >
                    /month
                  </Typography>
                </div>
                <Typography variant="overline" color="textSecondary">
                  Unlimited
                </Typography>
                <Box my={2}>
                  <Divider />
                </Box>
                <Typography variant="body2" color="textPrimary">
                  All from above
                  <br />
                  Unlimited 24/7 support
                  <br />
                  Personalised Page
                  <br />
                  Advertise your profile
                </Typography>
                <Box my={2}>
                  <Divider />
                </Box>
                <Button
                  variant="contained"
                  fullWidth
                  className={classes.chooseButton}
                >
                  Choose
                </Button>
              </Paper>
            </Grid>
          </Grid>
        </Container>
      </Box>
    </BxPage>
  );
};

export default PricingView;
