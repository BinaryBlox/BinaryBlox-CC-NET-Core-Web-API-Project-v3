import { ReactNode } from "react";
import { IStyleClassName } from "binaryblox-react-ui";

export interface IAccountView {}

export interface IAccountViewHeader extends IStyleClassName { 
  notifications?: ReactNode;
  subscriptions?: ReactNode;
  security?: ReactNode;
  profile?: ReactNode;
  settings?: ReactNode;
}
