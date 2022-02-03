import React, { useState, useEffect, useCallback, FC } from "react";
import PropTypes from "prop-types";
import clsx from "clsx";
import moment from "moment";
import PerfectScrollbar from "react-perfect-scrollbar";
import {
  Box,
  Card,
  CardHeader,
  Divider,
  Typography,
  Table,
  TableBody,
  TableCell,
  TableRow,
  makeStyles,
} from "@material-ui/core";
import { useBxIsMountedRef } from "binaryblox-react-ui";

import axiosUtil from "../../../../utils/axios.util";
import BxLabel from "../../../../components/bx-lib/BxLabel";
import { CustomerLog } from "../../../../models/customer.model";

interface LogsProps {
  className?: string;
}

const useStyles = makeStyles(() => ({
  root: {},
  methodCell: {
    width: 100,
  },
  statusCell: {
    width: 64,
  },
}));

const Logs: FC<LogsProps> = ({ className, ...rest }) => {
  const classes = useStyles();
  const isMountedRef = useBxIsMountedRef();
  const [logs, setLogs] = useState<CustomerLog[]>([]);

  const getLogs = useCallback(async () => {
    try {
      const response = await axiosUtil.get<{ logs: CustomerLog[] }>(
        "/api/customers/1/logs"
      );

      if (isMountedRef.current) {
        setLogs(response.data.logs);
      }
    } catch (err) {
      console.error(err);
    }
  }, [isMountedRef]);

  useEffect(() => {
    getLogs();
  }, [getLogs]);

  return (
    <Card className={clsx(classes.root, className)} {...rest}>
      <CardHeader title="Customer logs" />
      <Divider />
      <PerfectScrollbar>
        <Box minWidth={1150}>
          <Table>
            <TableBody>
              {logs.map((log) => (
                <TableRow key={log.id}>
                  <TableCell className={classes.methodCell}>
                    <Typography variant="h6" color="textPrimary">
                      {log.method}
                    </Typography>
                  </TableCell>
                  <TableCell className={classes.statusCell}>
                    <BxLabel color={log.status === 200 ? "success" : "error"}>
                      {log.status}
                    </BxLabel>
                  </TableCell>
                  <TableCell>{log.route}</TableCell>
                  <TableCell>{log.description}</TableCell>
                  <TableCell align="right">{log.ip}</TableCell>
                  <TableCell align="right">
                    {moment(log.createdAt).format("YYYY/MM/DD | hh:mm:ss")}
                  </TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </Box>
      </PerfectScrollbar>
    </Card>
  );
};

Logs.propTypes = {
  className: PropTypes.string,
};

export default Logs;
