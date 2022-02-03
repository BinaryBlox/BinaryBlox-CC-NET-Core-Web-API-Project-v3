import React, { FC, ReactNode } from "react";
import { Redirect } from "react-router-dom";
import { useBxAuth } from "binaryblox-react-ui";
import { BxVwLogin, BxVwRegister } from "binaryblox-react-ui-views";
import { AccountCircle, AlarmAdd, Done } from "@material-ui/icons";
interface AuthGuardProps {
  children?: ReactNode;
}
const RegisterView: FC = () => {
  //   const { isAuthenticated } = useBxAuth();

  //   if (!isAuthenticated) {
  //     return <Redirect to="/login" />;
  //   }
  const styleAttributes = { fontSize: 50 }
  const headerIcon = (
    <AccountCircle style={styleAttributes} color="secondary" />
  );

  return (
    <>
      <BxVwRegister
        headerTitle={"Register"}
        headerSubTitle={"Register here"}
        headerIcon={headerIcon}
        saveButtonTitle={"Save"} 
        cancelButtonTitle={"Cancel"}
      />
    </>
  );
};

export default RegisterView;
