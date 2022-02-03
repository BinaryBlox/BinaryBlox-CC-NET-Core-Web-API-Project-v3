import React, { FC, ReactNode } from "react";
import { Redirect } from "react-router-dom"; 
import {useAuth} from "oidc-react";

interface AuthGuardProps {
  children?: ReactNode;
}

const AuthGuard: FC<AuthGuardProps> = ({ children }) => {

  const auth = useAuth();
 
  //const { isAuthenticated } = useBxAuth();
  const isAuthenticated:boolean = (auth && auth.userData) ? true : false; 

  console.log(`AuthGuard Is Authenticated: ${isAuthenticated}`)
  if (isAuthenticated === false) {
    console.log(`AM I HERE: ${isAuthenticated}`)
    return <Redirect to="/" />;
  }

  return <>{children}</>;
};

export default AuthGuard;
