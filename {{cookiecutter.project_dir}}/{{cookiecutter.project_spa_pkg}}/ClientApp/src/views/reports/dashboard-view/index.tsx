import React, { FC } from "react";
import { Container, Grid, makeStyles, Theme } from "@material-ui/core";
import { BxPage } from "binaryblox-react-ui";
import Header from "./Header";
import LatestProjects from "./LatestProjects";
import NewProjects from "./NewProjects";
import PerformanceOverTime from "./performance-over-time";
import RealTime from "./real-time";
import RoiPerCustomer from "./RoiPerCustomer";
import SystemHealth from "./SystemHealth";
import TeamTasks from "./team-tasks";
import TodaysMoney from "./TodaysMoney";

const useStyles = makeStyles((theme: Theme) => ({
  root: {
    backgroundColor: theme.palette.background.default,
    minHeight: "100%",
    paddingTop: theme.spacing(3),
    paddingBottom: theme.spacing(3),
  },
}));

const DashboardView: FC = () => {
  const classes = useStyles();

  return (
    <BxPage translate="no" className={classes.root} title="Dashboard">
      <Container maxWidth={false}>
        <Header />
        <Grid container spacing={3}>
          <Grid item lg={3} sm={6} xs={12}>
            <TodaysMoney />
          </Grid>
          <Grid item lg={3} sm={6} xs={12}>
            <NewProjects />
          </Grid>
          <Grid item lg={3} sm={6} xs={12}>
            <SystemHealth />
          </Grid>
          <Grid item lg={3} sm={6} xs={12}>
            <RoiPerCustomer />
          </Grid>
          <Grid item lg={3} xs={12}>
            <RealTime />
          </Grid>
          <Grid item lg={9} xs={12}>
            <PerformanceOverTime />
          </Grid>
          <Grid item lg={5} xl={4} xs={12}>
            <TeamTasks />
          </Grid>
          <Grid item lg={7} xl={8} xs={12}>
            <LatestProjects />
          </Grid>
        </Grid>
      </Container>
    </BxPage>
  );
};

export default DashboardView;
