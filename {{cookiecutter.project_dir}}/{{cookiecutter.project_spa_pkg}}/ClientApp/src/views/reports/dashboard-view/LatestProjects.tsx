import React, { useCallback, useEffect, useState, FC } from "react";
import { Link as RouterLink } from "react-router-dom";
import clsx from "clsx";
import moment from "moment";
import numeral from "numeral";
import PerfectScrollbar from "react-perfect-scrollbar";
import {
  Avatar,
  Box,
  Card,
  CardHeader,
  Divider,
  Button,
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableRow,
  TableSortLabel,
  Tooltip,
  makeStyles,
  Theme,
} from "@material-ui/core";
import NavigateNextIcon from "@material-ui/icons/NavigateNext";
import { useBxIsMountedRef, bxUtilInitials } from "binaryblox-react-ui";

import axiosUtil from "../../../utils/axios.util"; 
import BxGenericMoreButton from "../../../components/bx-lib/BxGenericMoreButton";
import { Project } from "../../../models/reports.model";

interface LatestProjectsProps {
  className?: string;
}

const technologyMap: Record<string, string> = {
  "html-css": "/static/images/technologies/html.svg",
  "react-js": "/static/images/technologies/react-js.svg",
  "vue-js": "/static/images/technologies/vue-js.svg",
  angular: "/static/images/technologies/angular.svg",
  figma: "/static/images/technologies/figma.svg",
  sketch: "/static/images/technologies/sketch.svg",
};

const useStyles = makeStyles((theme: Theme) => ({
  root: {},
  technology: {
    height: 30,
    "& + &": {
      marginLeft: theme.spacing(1),
    },
  },
}));

const LatestProjects: FC<LatestProjectsProps> = ({ className, ...rest }) => {
  const classes = useStyles();
  const isMountedRef = useBxIsMountedRef();
  const [projects, setProjects] = useState<Project[]>([]);

  const getProjects = useCallback(async () => {
    try {
      const response = await axiosUtil.get<{ projects: Project[] }>(
        "/api/reports/latest-projects"
      );

      if (isMountedRef.current) {
        setProjects(response.data.projects);
      }
    } catch (err) {
      console.error(err);
    }
  }, [isMountedRef]);

  useEffect(() => {
    getProjects();
  }, [getProjects]);

  return (
    <Card className={clsx(classes.root, className)} {...rest}>
      <CardHeader action={<BxGenericMoreButton />} title="Latest Projects" />
      <Divider />
      <PerfectScrollbar>
        <Box minWidth={900}>
          <Table>
            <TableHead>
              <TableRow>
                <TableCell>Title</TableCell>
                <TableCell>Author</TableCell>
                <TableCell>Budget</TableCell>
                <TableCell>Technology</TableCell>
                <TableCell align="right" sortDirection="desc">
                  <Tooltip enterDelay={300} title="Sort">
                    <TableSortLabel active direction="desc">
                      Created
                    </TableSortLabel>
                  </Tooltip>
                </TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {projects.map((project) => (
                <TableRow hover key={project.id}>
                  <TableCell>{project.title}</TableCell>
                  <TableCell>
                    <Box display="flex" alignItems="center">
                      <Avatar alt="Author" src={project.author.avatar}>
                        {bxUtilInitials(project.author.name)}
                      </Avatar>
                      <Box ml={1}>{project.author.name}</Box>
                    </Box>
                  </TableCell>
                  <TableCell>
                    {numeral(project.budget).format(
                      `${project.currency}0,0.00`
                    )}
                  </TableCell>
                  <TableCell>
                    {project.technologies.map((technology) => (
                      <img
                        alt="Tech"
                        key={technology}
                        className={classes.technology}
                        src={technologyMap[technology]}
                      />
                    ))}
                  </TableCell>
                  <TableCell align="right">
                    {moment(project.createdAt).format("DD MMM, YYYY")}
                  </TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </Box>
      </PerfectScrollbar>
      <Box p={2} display="flex" justifyContent="flex-end">
        <Button
          component={RouterLink}
          size="small"
          to="/app/projects"
          endIcon={<NavigateNextIcon />}
        >
          See all
        </Button>
      </Box>
    </Card>
  );
};

export default LatestProjects;
