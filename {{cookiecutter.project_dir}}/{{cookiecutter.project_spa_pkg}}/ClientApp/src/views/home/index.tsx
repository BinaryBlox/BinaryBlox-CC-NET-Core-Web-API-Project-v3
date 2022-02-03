import React, { FC } from "react";
import { makeStyles } from "@material-ui/core";
import { BxPage } from "binaryblox-react-ui";
import Hero from "./Hero";
import Features from "./Features";
import Testimonials from "./Testimonials";
import CTA from "./CTA";
import FAQS from "./FAQS";

const useStyles = makeStyles(() => ({
  root: {},
}));

const HomeView: FC = () => {
  const classes = useStyles();

  return (
    <BxPage translate={"no"} className={classes.root} title="Home">
      <Hero />
      <Features />
      <Testimonials />
      <CTA />
      <FAQS />
    </BxPage>
  );
};

export default HomeView;
