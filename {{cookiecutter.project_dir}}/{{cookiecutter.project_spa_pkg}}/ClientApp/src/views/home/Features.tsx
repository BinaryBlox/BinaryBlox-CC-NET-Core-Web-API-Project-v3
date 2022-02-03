import React, { FC } from "react";
import clsx from "clsx";
import {
  Avatar,
  Button,
  Box,
  Container,
  Grid,
  Typography,
  makeStyles,
  Theme,
} from "@material-ui/core";
import { AppConstants } from "../../constants";

interface FeaturesProps {
  className?: string;
}

const useStyles = makeStyles((theme: Theme) => ({
  root: {
    backgroundColor: theme.palette.background.default,
    paddingTop: 128,
    paddingBottom: 128,
  },
  avatar: {
    backgroundColor: theme.palette.secondary.main,
    color: theme.palette.secondary.contrastText,
  },
}));

const Features: FC<FeaturesProps> = ({ className, ...rest }) => {
  const classes = useStyles();

  return (
    <div className={clsx(classes.root, className)} {...rest}>
      <Container maxWidth="lg">
        <Typography
          component="p"
          variant="overline"
          color="secondary"
          align="center"
        >
          Explore {`${AppConstants.COMPANY_NAME}`}
        </Typography>
        <Typography variant="h1" align="center" color="textPrimary">
         A Secure Portal to Manage Applications
        </Typography>
        <Box mt={8}>
          <Grid container spacing={3}>
            <Grid item xs={12} md={4}>
              <Box display="flex">
                <Avatar className={classes.avatar}>01</Avatar>
                <Box ml={2}>
                  <Typography variant="h4" gutterBottom color="textPrimary">
                    First Section
                  </Typography>
                  <Typography variant="body1" color="textPrimary">
                    Not just a set of tools, the package includes the most
                    common use cases of user flows like User Management, Second
                    Level Layout.
                  </Typography>
                </Box>
              </Box>
            </Grid>
            <Grid item xs={12} md={4}>
              <Box display="flex">
                <Avatar className={classes.avatar}>02</Avatar>
                <Box ml={2}>
                  <Typography variant="h4" gutterBottom color="textPrimary">
                    Second Section
                  </Typography>
                  <Typography variant="body1" color="textPrimary">
                    Not just a set of tools, the package includes the most
                    common use cases of user flows like User Management, Second
                    Level Layout.
                  </Typography>
                </Box>
              </Box>
            </Grid>
            <Grid item xs={12} md={4}>
              <Box display="flex">
                <Avatar className={classes.avatar}>03</Avatar>
                <Box ml={2}>
                  <Typography variant="h4" gutterBottom color="textPrimary">
                    Third Section
                  </Typography>
                  <Typography variant="body1" color="textPrimary" gutterBottom>
                    Not just a set of tools, the package includes the most
                    common use cases of user flows like User Management, Second
                    Level Layout.
                  </Typography>
                  {/* <Button
                    variant="outlined"
                    component="a"
                    href="https://sketch.cloud/s/q4a8e"
                    target="_blank"
                  >
                    Preview Design
                  </Button> */}
                </Box>
              </Box>
            </Grid>
          </Grid>
        </Box>
      </Container>
    </div>
  );
};
 

export default Features;
