import React, { FC } from "react";
import clsx from "clsx";
import moment from "moment";
import {
  Avatar,
  IconButton,
  ListItem,
  ListItemProps,
  ListItemText,
  Tooltip,
  makeStyles,
  Theme
} from "@material-ui/core";
import { AvatarGroup } from "@material-ui/lab";
import OpenInNewIcon from "@material-ui/icons/OpenInNew";
import { Task } from "../../../../models/reports.model";

interface TaskItemProps extends ListItemProps {
  className?: string;
  task: Task;
  button?: any; // Fix warning
}

const useStyles = makeStyles((theme: Theme) => ({
  root: {},
  viewButton: {
    marginLeft: theme.spacing(2),
  },
}));

const primaryTypographyProps: any = { variant: "h6", noWrap: true };

const TaskItem: FC<TaskItemProps> = ({ className, task, ...rest }) => {
  const classes = useStyles();

  let deadline = "N/A";

  if (task.deadline) {
    const now = moment();
    const deadlineMoment = moment(task.deadline);

    if (deadlineMoment.isAfter(now) && deadlineMoment.diff(now, "day") < 3) {
      deadline = `${deadlineMoment.diff(now, "day")} days remaining`;
    }
  }

  return (
    <ListItem className={clsx(classes.root, className)} {...rest}>
      <ListItemText
        primary={task.title}
        primaryTypographyProps={primaryTypographyProps}
        secondary={deadline}
      />
      <AvatarGroup max={3}>
        {task.members.map((member) => (
          <Tooltip key={member.name} title="View">
            <Avatar src={member.avatar} />
          </Tooltip>
        ))}
      </AvatarGroup>
      <Tooltip title="View task">
        <IconButton className={classes.viewButton} edge="end">
          <OpenInNewIcon fontSize="small" />
        </IconButton>
      </Tooltip>
    </ListItem>
  );
};

export default TaskItem;
