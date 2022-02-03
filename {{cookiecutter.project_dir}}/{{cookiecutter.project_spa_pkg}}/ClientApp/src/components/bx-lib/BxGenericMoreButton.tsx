import React, { useRef, useState, memo, FC } from "react";
import {
  ListItemIcon,
  ListItemText,
  Tooltip,
  IconButton,
  Menu,
  MenuItem,
  makeStyles,
  PopoverOrigin,
  PaperProps,
} from "@material-ui/core";
import MoreIcon from "@material-ui/icons/MoreVert";
import GetAppIcon from "@material-ui/icons/GetApp";
import FileCopyIcon from "@material-ui/icons/FileCopy";
import PictureAsPdfIcon from "@material-ui/icons/PictureAsPdf";
import ArchiveIcon from "@material-ui/icons/ArchiveOutlined";

const useStyles = makeStyles(() => ({
  menu: {
    width: 256,
    maxWidth: "100%",
  },
}));

const BxGenericMoreButton: FC = (props) => {
  const classes = useStyles();
  const moreRef = useRef<any>(null);
  const [openMenu, setOpenMenu] = useState<boolean>(false);

  const handleMenuOpen = (): void => {
    setOpenMenu(true);
  };

  const handleMenuClose = (): void => {
    setOpenMenu(false);
  };

  const anchorOrigin: PopoverOrigin = {
    vertical: "top",
    horizontal: "left",
  };
  
  const transferOrigin: PopoverOrigin = {
    vertical: "top",
    horizontal: "left",
  };

  const paperProps: Partial<PaperProps> = { className: classes.menu };

  return (
    <>
      <Tooltip title="More options">
        <IconButton onClick={handleMenuOpen} ref={moreRef} {...props}>
          <MoreIcon fontSize="small" />
        </IconButton>
      </Tooltip>
      <Menu
        anchorEl={moreRef.current}
        anchorOrigin={anchorOrigin}
        onClose={handleMenuClose}
        open={openMenu}
        PaperProps={paperProps}
        transformOrigin={transferOrigin}
      >
        <MenuItem>
          <ListItemIcon>
            <GetAppIcon />
          </ListItemIcon>
          <ListItemText primary="Import" />
        </MenuItem>
        <MenuItem>
          <ListItemIcon>
            <FileCopyIcon />
          </ListItemIcon>
          <ListItemText primary="Copy" />
        </MenuItem>
        <MenuItem>
          <ListItemIcon>
            <PictureAsPdfIcon />
          </ListItemIcon>
          <ListItemText primary="Export" />
        </MenuItem>
        <MenuItem>
          <ListItemIcon>
            <ArchiveIcon />
          </ListItemIcon>
          <ListItemText primary="Archive" />
        </MenuItem>
      </Menu>
    </>
  );
};

export default memo(BxGenericMoreButton);
