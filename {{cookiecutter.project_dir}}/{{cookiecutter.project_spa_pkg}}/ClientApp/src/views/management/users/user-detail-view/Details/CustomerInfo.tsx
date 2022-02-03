import React, { FC } from "react"; 
import clsx from "clsx";
import {
  Box,
  Button,
  Card,
  CardHeader,
  Divider,
  Table,
  TableBody,
  TableCell,
  TableRow,
  Typography,
  makeStyles,
  Theme,
} from "@material-ui/core";
import LockOpenIcon from "@material-ui/icons/LockOpenOutlined";
import PersonIcon from "@material-ui/icons/PersonOutline";

import BxLabel from "../../../../../components/bx-lib/BxLabel";
import { Customer } from "../../../../../models/customer.model";

interface CustomerInfoProps {
  customer: Customer;
  className?: string;
}

const useStyles = makeStyles((theme: Theme) => ({
  root: {},
  fontWeightMedium: {
    fontWeight: theme.typography.fontWeightMedium,
  },
}));

const CustomerInfo: FC<CustomerInfoProps> = ({
  customer,
  className,
  ...rest
}) => {
  const classes = useStyles();

  return (
    <Card className={clsx(classes.root, className)} {...rest}>
      <CardHeader title="Customer info" />
      <Divider />
      <Table>
        <TableBody>
          <TableRow>
            <TableCell className={classes.fontWeightMedium}>Email</TableCell>
            <TableCell>
              <Typography variant="body2" color="textSecondary">
                {customer.email}
              </Typography>
              <BxLabel color={customer.isVerified ? "success" : "error"}>
                {customer.isVerified ? "Email verified" : "Email not verified"}
              </BxLabel>
            </TableCell>
          </TableRow>
          <TableRow>
            <TableCell className={classes.fontWeightMedium}>Phone</TableCell>
            <TableCell>
              <Typography variant="body2" color="textSecondary">
                {customer.phone}
              </Typography>
            </TableCell>
          </TableRow>
          <TableRow>
            <TableCell className={classes.fontWeightMedium}>Country</TableCell>
            <TableCell>
              <Typography variant="body2" color="textSecondary">
                {customer.country}
              </Typography>
            </TableCell>
          </TableRow>
          <TableRow>
            <TableCell className={classes.fontWeightMedium}>
              State/Region
            </TableCell>
            <TableCell>
              <Typography variant="body2" color="textSecondary">
                {customer.state}
              </Typography>
            </TableCell>
          </TableRow>
          <TableRow>
            <TableCell className={classes.fontWeightMedium}>
              Address 1
            </TableCell>
            <TableCell>
              <Typography variant="body2" color="textSecondary">
                {customer.address1}
              </Typography>
            </TableCell>
          </TableRow>
          <TableRow>
            <TableCell className={classes.fontWeightMedium}>
              Address 2
            </TableCell>
            <TableCell>
              <Typography variant="body2" color="textSecondary">
                {customer.address2}
              </Typography>
            </TableCell>
          </TableRow>
        </TableBody>
      </Table>
      <Box p={1} display="flex" flexDirection="column" alignItems="flex-start">
        <Button startIcon={<LockOpenIcon />}>Reset &amp; Send Password</Button>
        <Button startIcon={<PersonIcon />}>Login as Customer</Button>
      </Box>
    </Card>
  );
};
 
export default CustomerInfo;