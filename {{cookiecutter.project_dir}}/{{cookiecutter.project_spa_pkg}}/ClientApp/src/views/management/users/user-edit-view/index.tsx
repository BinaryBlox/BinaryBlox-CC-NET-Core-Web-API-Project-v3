import React, { useState, useCallback, useEffect, FC } from "react";
import { Box, Container, makeStyles, Theme } from "@material-ui/core";
import { useBxIsMountedRef, BxPage } from "binaryblox-react-ui";
import CustomerEditForm from "./CustomerEditForm";
import axiosUtil from "../../../../utils/axios.util";
import { Customer } from "../../../../models/customer.model";

import Header from "./Header";

const useStyles = makeStyles((theme: Theme) => ({
  root: {
    backgroundColor: theme.palette.background.default,
    minHeight: "100%",
    paddingTop: theme.spacing(3),
    paddingBottom: theme.spacing(3),
  },
}));

const CustomerEditView: FC = () => {
  const classes = useStyles();
  const isMountedRef = useBxIsMountedRef();
  const [customer, setCustomer] = useState<Customer | null>(null);

  const getCustomer = useCallback(async () => {
    try {
      const response = await axiosUtil.get<{ customer: Customer }>(
        "/api/customers/1"
      );

      if (isMountedRef.current) {
        setCustomer(response.data.customer);
      }
    } catch (err) {
      console.error(err);
    }
  }, [isMountedRef]);

  useEffect(() => {
    getCustomer();
  }, [getCustomer]);

  if (!customer) {
    return null;
  }

  return (
    <BxPage translate="no" className={classes.root} title="Customer Edit">
      <Container maxWidth={false}>
        <Header />
      </Container>
      <Box mt={3}>
        <Container maxWidth="lg">
          <CustomerEditForm customer={customer} />
        </Container>
      </Box>
    </BxPage>
  );
};

export default CustomerEditView;
