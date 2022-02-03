import React, { useState, useEffect, useCallback, FC } from "react";
import { Link as RouterLink } from "react-router-dom";
import clsx from "clsx";
import moment from "moment";
import PropTypes from "prop-types";
import PerfectScrollbar from "react-perfect-scrollbar";
import {
  Box,
  Card,
  CardHeader,
  Divider,
  IconButton,
  SvgIcon,
  Table,
  TableBody,
  TableCell,
  TableHead,
  TablePagination,
  TableRow,
  makeStyles,
} from "@material-ui/core";
import { ArrowRight as ArrowRightIcon } from "react-feather";
import { useBxIsMountedRef } from "binaryblox-react-ui";


import axiosUtil from "../../../../utils/axios.util";
import { Invoice } from "../../../../models/customer.model";
import BxLabel from "../../../../components/bx-lib/BxLabel";
import BxGenericMoreButton from "../../../../components/bx-lib/BxGenericMoreButton";

interface InvoicesProps {
  className?: string;
}

const useStyles = makeStyles(() => ({
  root: {},
}));

const Invoices: FC<InvoicesProps> = ({ className, ...rest }) => {
  const classes = useStyles();
  const isMountedRef = useBxIsMountedRef();
  const [invoices, setInvoices] = useState<Invoice[]>([]);

  const getInvoices = useCallback(async () => {
    try {
      const response = await axiosUtil.get<{ invoices: Invoice[] }>(
        "/api/customers/1/invoices"
      );

      if (isMountedRef.current) {
        setInvoices(response.data.invoices);
      }
    } catch (err) {
      console.error(err);
    }
  }, [isMountedRef]);

  useEffect(() => {
    getInvoices();
  }, [getInvoices]);

  return (
    <Card className={clsx(classes.root, className)} {...rest}>
      <CardHeader action={<BxGenericMoreButton />} title="Customer invoices" />
      <Divider />
      <PerfectScrollbar>
        <Box minWidth={1150}>
          <Table>
            <TableHead>
              <TableRow>
                <TableCell>ID</TableCell>
                <TableCell>Date</TableCell>
                <TableCell>Description</TableCell>
                <TableCell>Payment Method</TableCell>
                <TableCell>Total</TableCell>
                <TableCell>Status</TableCell>
                <TableCell align="right">Actions</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {invoices.map((invoice) => (
                <TableRow key={invoice.id}>
                  <TableCell>#{invoice.id}</TableCell>
                  <TableCell>
                    {moment(invoice.issueDate).format("DD/MM/YYYY | HH:MM")}
                  </TableCell>
                  <TableCell>{invoice.description}</TableCell>
                  <TableCell>{invoice.paymentMethod}</TableCell>
                  <TableCell>
                    {invoice.currency}
                    {invoice.value}
                  </TableCell>
                  <TableCell>
                    {/* <Label color={statusColors[invoice.status]} > */}
                    <BxLabel color="primary">{invoice.status}</BxLabel>
                  </TableCell>
                  <TableCell align="right">
                    <IconButton
                      component={RouterLink}
                      to="/app/management/invoices/1"
                    >
                      <SvgIcon fontSize="small">
                        <ArrowRightIcon />
                      </SvgIcon>
                    </IconButton>
                  </TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </Box>
      </PerfectScrollbar>
      <TablePagination
        component="div"
        count={invoices.length}
        onChangePage={() => {}}
        onChangeRowsPerPage={() => {}}
        page={0}
        rowsPerPage={5}
        rowsPerPageOptions={[5, 10, 25]}
      />
    </Card>
  );
};

Invoices.propTypes = {
  className: PropTypes.string,
};

export default Invoices;
