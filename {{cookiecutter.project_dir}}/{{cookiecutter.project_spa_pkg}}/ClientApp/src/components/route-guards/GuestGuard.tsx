import React,{ FC, ReactNode }  from 'react'; 
import { Redirect } from 'react-router-dom'; 
import {useAuth} from "oidc-react";

interface IGuestGuard {
  children?: ReactNode;
}

const GuestGuard: FC<IGuestGuard> = ({ children }) => {

  const auth = useAuth();

  const isAuthenticated:boolean = (auth && auth.userData) ? true : false; 

  console.log(`GuestGuard Is Authenticated: ${isAuthenticated}`)
  if (isAuthenticated) {
    return <Redirect to="/app/account" />;
  }

  return (
    <>
      {children}
    </>
  );
};
 

export default GuestGuard;
