import React, { useCallback, useState, useEffect, FC } from "react";
import clsx from "clsx";
import PerfectScrollbar from "react-perfect-scrollbar";
import {
  Box,
  Card,
  CardHeader,
  Divider,
  List,
  makeStyles,
} from "@material-ui/core";
import axiosUtil from "../../../../utils/axios.util";
import { useBxIsMountedRef } from "binaryblox-react-ui";
import BxGenericMoreButton from "../../../../components/bx-lib/BxGenericMoreButton";

import TaskItem from "./TaskItem";
import { Task } from "../../../../models/reports.model";

interface TeamTasksProps {
  className?: string;
}

const useStyles = makeStyles(() => ({
  root: {},
}));

const TeamTasks: FC<TeamTasksProps> = ({ className, ...rest }) => {
  const classes = useStyles();
  const isMountedRef = useBxIsMountedRef();
  const [tasks, setTasks] = useState<Task[]>([]);

  const getTasks = useCallback(async () => {
    try {
      const response = await axiosUtil.get<{ tasks: Task[] }>(
        "/api/reports/latest-tasks"
      );

      if (isMountedRef.current) {
        setTasks(response.data.tasks);
      }
    } catch (err) {
      console.error(err);
    }
  }, [isMountedRef]);

  useEffect(() => {
    getTasks();
  }, [getTasks]);

  return (
    <Card className={clsx(classes.root, className)} {...rest}>
      <CardHeader action={<BxGenericMoreButton />} title="Team Tasks" />
      <Divider />
      <PerfectScrollbar>
        <Box minWidth={400}>
          <List>
            {tasks.map((task, i) => (
              <TaskItem
                divider={i < tasks.length - 1}
                key={task.id}
                task={task}
              />
            ))}
          </List>
        </Box>
      </PerfectScrollbar>
    </Card>
  );
};
export default TeamTasks;
