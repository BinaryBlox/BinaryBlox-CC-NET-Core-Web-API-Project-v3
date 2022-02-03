import React, { FC, useEffect, useMemo } from "react";
import clsx from "clsx";
import {
  Box,
  Container,
  Grid,
  Typography,
  makeStyles,
  Theme,
} from "@material-ui/core";
import { AppConstants } from "../../constants";
import {
  configurationFeatureEntityAdapters,
  BxConfigurationAggregateApiFactory,
  configurationFeatureRequests,
  configurationFeatureStateTypes,
} from "binaryblox-configuration-api";
import { RootState } from "../../store"; 
import { useDispatch, useSelector } from "react-redux";
import { BxDynamicTable } from "binaryblox-react-ui";
interface HeroProps {
  className?: string;
}

const useStyles = makeStyles((theme: Theme) => ({
  root: {
    backgroundColor: theme.palette.background.default,
    paddingTop: 200,
    paddingBottom: 200,
    [theme.breakpoints.down("md")]: {
      paddingTop: 60,
      paddingBottom: 60,
    },
  },
  technologyIcon: {
    height: 40,
    margin: theme.spacing(1),
  },
  image: {
    perspectiveOrigin: "left center",
    transformStyle: "preserve-3d",
    perspective: 1500,
    "& > img": {
      maxWidth: "90%",
      height: "auto",
      transform: "rotateY(-35deg) rotateX(15deg)",
      backfaceVisibility: "hidden",
      boxShadow: theme.shadows[16],
    },
  },
  shape: {
    position: "absolute",
    top: 0,
    left: 0,
    "& > img": {
      maxWidth: "90%",
      height: "auto",
    },
  },
}));

const Hero: FC<HeroProps> = ({ className, ...rest }) => {
  const classes = useStyles();

  const configSelectors = configurationFeatureEntityAdapters.bxConfigurationAggregate.getSelectors<
    RootState
  >(
    (state: { [name: string]: any }) =>
      state[configurationFeatureStateTypes.BxConfigurationAggregateState]
  );

  const bxEntity = {};

  const bxRequest = {
    metadata: { appAction: "POST", requestType: "GetAll" },
    requestBody: {
      id: "11111111-1111-1111-1111-111111111111",
      entity: {
        configId: "9683433e-f36b-1410-8fe0-00f7ca0ad523",
      },
    },
  };

  const bxAppClientApiFunc = BxClientApplicationApiFactory;
  const bxConfigApiFunc = BxConfigurationAggregateApiFactory;
  const bxAppClient = accountFeatureRequests.BxClientApplication;
  const bxConfig = configurationFeatureRequests.bxConfigurationAggregate;

  const dispatch = useDispatch();

  const allConfigs = useSelector(configSelectors.selectAll);

  const columns = useMemo(
    () => [
      {
        Header: "Name",
        accessor: "name",
      },
      {
        Header: "Type",
        accessor: "type",
      },
      {
        Header: "ConfigId",
        accessor: "configId",
      },
    ],
    []
  );

  //  Using useEffect to call the API once mounted and set the data
  useEffect(() => {
    console.log("Fetch Create Request - PRE");

    //(async () => {
    // dispatch(bxAppClient.fetchGetRequest<RootState>(bxAppClientApiFunc, bxRequest, undefined, "", true));
    dispatch(
      bxConfig.fetchGetAll_GetAllConfig<RootState>(
        bxConfigApiFunc,
        bxRequest,
        undefined,
        "",
        true
      )
    );

    // dispatch(
    //   bxConfig.fetchGetAll_GetAllOtherConfig<RootState>(
    //     bxConfigApiFunc,
    //     bxRequest,
    //     undefined,
    //     "",
    //     true
    //   )
    // );

    console.log(
      `All data coming back: ********** ${JSON.stringify(
        allConfigs,
        null,
        "\t"
      )}`
    );
  }, []);

  return (
    <div className={clsx(classes.root, className)} {...rest}>
      <Container maxWidth="lg">
        <Grid container spacing={3}>
          <Grid item xs={12} md={5}>
            <BxDynamicTable
              filter={"name"}
              columns={columns}
              data={allConfigs}
              dense={true}
            />
            <Box
              display="flex"
              flexDirection="column"
              justifyContent="center"
              height="100%"
            >
              <Typography variant="overline" color="secondary">
                Introducing
              </Typography>
              <Typography variant="h1" color="textPrimary">
                {`${AppConstants.COMPANY_NAME} Application Portal`}
              </Typography>
              <Box mt={3}>
                <Typography variant="body1" color="textSecondary">
                  Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed
                  do eiusmod tempor incididunt ut labore et dolore magna aliqua.
                  Ut enim ad minim veniam, quis nostrud exercitation ullamco
                  laboris nisi ut aliquip ex ea commodo consequat. Duis aute
                  irure dolor in reprehenderit in voluptate velit esse cillum
                  dolore eu fugiat nulla pariatur. Excepteur sint occaecat
                  cupidatat non proident, sunt in culpa qui officia deserunt
                  mollit anim id est laborum.
                </Typography>
              </Box>
              <Box mt={3}>
                <Grid container spacing={3}>
                  <Grid item>
                    <Typography variant="h1" color="secondary">
                      30+
                    </Typography>
                    <Typography variant="overline" color="textSecondary">
                      Workflows
                    </Typography>
                  </Grid>
                  <Grid item>
                    <Typography variant="h1" color="secondary">
                      2M+
                    </Typography>
                    <Typography variant="overline" color="textSecondary">
                      Items Processed
                    </Typography>
                  </Grid>
                  <Grid item>
                    <Typography variant="h1" color="secondary">
                      1000+
                    </Typography>
                    <Typography variant="overline" color="textSecondary">
                      API(s)
                    </Typography>
                  </Grid>
                </Grid>
              </Box>
              {/* <Box mt={3}>
                <img
                  alt="Javascript"
                  className={classes.technologyIcon}
                  src="/static/images/javascript.svg"
                />
                <img
                  alt="Typescript"
                  className={classes.technologyIcon}
                  src="/static/images/typescript.svg"
                />
              </Box> */}
            </Box>
          </Grid>
          <Grid item xs={12} md={7}>
            <Box position="relative">
              <div className={classes.shape}>
                <img alt="Shapes" src="/static/home/shapes.svg" />
              </div>
              <div className={classes.image}>
                <img
                  alt="Presentation"
                  src="/static/home/borrower-portal@3x-1.png"
                />
              </div>
            </Box>
          </Grid>
        </Grid>
      </Container>
    </div>
  );
};

export default Hero;
