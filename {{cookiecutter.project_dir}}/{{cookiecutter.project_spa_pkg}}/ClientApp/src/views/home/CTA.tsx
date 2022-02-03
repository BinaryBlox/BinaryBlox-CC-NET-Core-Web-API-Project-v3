import React, { FC } from "react";
import clsx from "clsx";
import {
  Box,
  Button,
  Container,
  Typography,
  makeStyles,
  Theme,
} from "@material-ui/core";
import { AppConstants } from "../../constants";

interface CTAProps {
  className?: string;
}

const useStyles = makeStyles((theme: Theme) => ({
  root: {
    backgroundColor: theme.palette.background.default,
    paddingTop: 128,
    paddingBottom: 128,
  },
  browseButton: {
    marginLeft: theme.spacing(2),
  },
}));

const CTA: FC<CTAProps> = ({ className, ...rest }) => {
  const classes = useStyles();

  return (
    <div className={clsx(classes.root, className)} {...rest}>
      <Container maxWidth="lg">
        <Typography variant="h1" align="center" color="textPrimary">
          Ready for an easier way to access your data securely?
        </Typography>
        <Typography variant="h1" align="center" color="secondary">
          Join the experience and sign up with {`${AppConstants.COMPANY_NAME}`} up today!
        </Typography>
        <Box mt={6} display="flex" justifyContent="center" alignItems="center">
          <Button
            color="secondary"
            component="a"
            href="https://example.org"
            variant="contained"
          >
            Sign Up
          </Button>
        </Box>
      </Container>
    </div>
  );
};

export default CTA;
