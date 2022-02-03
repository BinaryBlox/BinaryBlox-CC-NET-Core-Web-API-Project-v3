import React, { FC } from "react";
import clsx from "clsx";
import {
  Avatar,
  Box,
  Container,
  Typography,
  makeStyles,
  Theme,
} from "@material-ui/core";
import { AppConstants } from "../../constants";

interface TestimonialsProps {
  className?: string;
}

const useStyles = makeStyles((theme: Theme) => ({
  root: {
    backgroundColor: theme.palette.background.default,
    paddingTop: 128,
    paddingBottom: 128,
  },
  title: {
    fontWeight: theme.typography.fontWeightRegular,
  },
}));

const Testimonials: FC<TestimonialsProps> = ({ className, ...rest }) => {
  const classes = useStyles();

  return (
    <div className={clsx(classes.root, className)} {...rest}>
      <Container maxWidth="md">
        <Typography
          variant="h2"
          align="center"
          color="textPrimary"
          className={classes.title}
        > 
          &quot;{`${AppConstants.COMPANY_NAME}`} saved our deparments significant cost and resources.
          resources. Our business flow has improved drastically.&quot;
        </Typography>
        <Box mt={6} display="flex" justifyContent="center" alignItems="center">
          <Avatar src="/static/home/anthony.png" />
          <Box ml={2}>
            <Typography variant="body1" color="textPrimary">
              Tony Henderson
              <Typography
                color="textSecondary"
                display="inline"
                component="span"
              >
                , Employee @Kaiser Permanente
              </Typography>
            </Typography>
          </Box>
        </Box>
      </Container>
    </div>
  );
};

export default Testimonials;
