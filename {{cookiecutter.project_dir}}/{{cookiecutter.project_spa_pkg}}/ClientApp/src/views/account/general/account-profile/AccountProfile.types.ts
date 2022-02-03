import { AuthUser, IStyleClassName } from "binaryblox-react-ui";

export interface IAccountProfile extends IStyleClassName { 
  user: AuthUser;
}
