import React, { useState, useCallback, useEffect, FC } from "react";

import PropTypes from "prop-types";
import clsx from "clsx";
import moment from "moment";
import {
  Box,
  Button,
  Card,
  CardContent,
  CardHeader,
  Divider,
  Table,
  TableBody,
  TableCell,
  TableRow,
  TextField,
  makeStyles,
  Theme,
  SelectProps,
} from "@material-ui/core";
import MaiIcon from "@material-ui/icons/MailOutline";
import { useBxIsMountedRef } from "binaryblox-react-ui";
import axiosUtil from "../../../../../utils/axios.util";
import { CustomerEmail } from "../../../../../models/customer.model";

interface EmailsProps {
  className?: string;
}

const emailOptions = [
  "Resend last invoice",
  "Send password reset",
  "Send verification",
];

const useStyles = makeStyles((theme: Theme) => ({
  root: {},
  cell: {
    padding: theme.spacing(1),
  },
}));

const Emails: FC<EmailsProps> = ({ className, ...rest }) => {
  const classes = useStyles();
  const isMountedRef = useBxIsMountedRef();
  const [emailOption, setEmailOption] = useState<string>(emailOptions[0]);
  const [emails, setEmails] = useState<CustomerEmail[]>([]);

  const nativeProps: SelectProps = { native: true };

  const getMails = useCallback(async () => {
    try {
      const response = await axiosUtil.get<{ emails: CustomerEmail[] }>(
        "/api/customers/1/emails"
      );

      if (isMountedRef.current) {
        setEmails(response.data.emails);
      }
    } catch (err) {
      console.error(err);
    }
  }, [isMountedRef]);

  useEffect(() => {
    getMails();
  }, [getMails]);

  return (
    <Card className={clsx(classes.root, className)} {...rest}>
      <CardHeader title="Send emails" />
      <Divider />
      <CardContent>
        <TextField
          fullWidth
          name="option"
          onChange={(event) => setEmailOption(event.target.value)}
          select
          SelectProps={nativeProps}
          value={emailOption}
          variant="outlined"
        >
          {emailOptions.map((option) => (
            <option key={option} value={option}>
              {option}
            </option>
          ))}
        </TextField>
        <Box mt={2}>
          <Button variant="contained" startIcon={<MaiIcon />}>
            Send email
          </Button>
        </Box>
        <Box mt={2}>
          <Table>
            <TableBody>
              {emails.map((email) => (
                <TableRow key={email.id}>
                  <TableCell className={classes.cell}>
                    {moment(email.createdAt).format("DD/MM/YYYY | HH:MM")}
                  </TableCell>
                  <TableCell className={classes.cell}>
                    {email.description}
                  </TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </Box>
      </CardContent>
    </Card>
  );
};

export default Emails;
