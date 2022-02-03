import React, { useState, useEffect, useCallback, FC } from "react";
import { Box, Container, makeStyles, Theme } from "@material-ui/core";

import { useBxIsMountedRef, BxPage } from "binaryblox-react-ui";


import axiosUtil from "../../../../utils/axios.util";
import { Customer } from "../../../../models/customer.model";

import Header from "./Header";
import Results from "./Results";

const useStyles = makeStyles((theme: Theme) => ({
  root: {
    backgroundColor: theme.palette.background.default,
    minHeight: "100%",
    paddingTop: theme.spacing(3),
    paddingBottom: theme.spacing(3),
  },
}));

const CustomerListView: FC = () => {
  const classes = useStyles();
  const isMountedRef = useBxIsMountedRef();
  const [customers, setCustomers] = useState<Customer[]>([]);

  const getCustomers = useCallback(async () => {
    try {
      const response = await axiosUtil.get<{ customers: Customer[] }>(
        "/api/customers"
      );

      if (isMountedRef.current) {
        setCustomers(response.data.customers);
      }
    } catch (err) {
      console.error(err);
    }
  }, [isMountedRef]);

  useEffect(() => {
    getCustomers();
  }, [getCustomers]);

  return (
    <BxPage translate="no" className={classes.root} title="Customer List">
      <Container maxWidth={false}>
        <Header />
        <Box mt={3}>
          <Results customers={customers} />
        </Box>
      </Container>
    </BxPage>
  );
};

export default CustomerListView;
