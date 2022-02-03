import React from 'react'; 
import clsx from 'clsx';
import PropTypes from 'prop-types';
import { Grid, makeStyles } from '@material-ui/core';
 

import { FolderPlus } from 'react-feather';

import AccountProfile from './account-profile/AccountProfile';
import AccountSettings from './account-settings/AccountSettings'; 
import {useBxAuth, AuthUser} from "binaryblox-react-ui"; 

import {IAccountGeneral} from "./General.types";


const useStyles = makeStyles(() => ({
  root: {}
}));

const General: React.FC<IAccountGeneral> = ({ className, ...rest }) => {
  const classes = useStyles();
  //const { user } = useBxAuth();
  const user: AuthUser  =  {avatar: "test", email: "tony.henderson@gmail.com", id:"1", name:"Tony Henderson"}
  return (
    <Grid
      className={clsx(classes.root, className)}
      container
      spacing={3}
      {...rest}
    >
      <Grid
        item
        lg={4}
        md={6}
        xl={3}
        xs={12}
      >
        <AccountProfile user={user} />
      </Grid>
      <Grid
        item
        lg={8}
        md={6}
        xl={9}
        xs={12}
      >
        <AccountSettings user={user} />
      </Grid>
    </Grid>
  );
}

General.propTypes = {
  className: PropTypes.string
};

export default General;
