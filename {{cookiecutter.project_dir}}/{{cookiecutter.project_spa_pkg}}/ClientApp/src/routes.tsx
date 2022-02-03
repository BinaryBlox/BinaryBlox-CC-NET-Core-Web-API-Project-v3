// import "@testing-library/jest-dom/extend-expect";
import React, { Suspense, Fragment, lazy } from "react";
import { Switch, Redirect, Route } from "react-router-dom";
import DashboardLayout from "./layouts/dashboard-layout";
import MainLayout from "./layouts/main-layout";
import BxLoadingScreen from "./components/bx-lib/BxLoadingScreen";
import { Routes } from "./utils/routing.util";
import { GuestGuard, AuthGuard } from "./components/route-guards";
import { RouteConstants } from "./constants";
import HomeView from "./views/home";
import { ApplicationPaths } from "./components/deprecate/api-authorization/ApiAuthorizationConstants";

const routes: Routes = [
  {
    exact: true,
    path: RouteConstants.RT_PAGE_NOT_FOUND,
    component: lazy(() => import("./views/errors/NotFound.view")),
  },
  // {
  //   exact: true,
  //   guard: GuestGuard,
  //   path: '/login',
  //   component: lazy(() => import('src/views/auth/LoginView'))
  // },
  // {
  //   exact: true,
  //   path: '/login-unprotected',
  //   component: lazy(() => import('src/views/auth/LoginView'))
  // },
  {
    exact: true,
    guard: GuestGuard,
    path: RouteConstants.RT_REGISTER,
    component: lazy(() => import("./views/register/RegisterView")),
  },
  {
    exact: true,
    path: RouteConstants.RT_REGISTER_UNPROTECTED,
    component: lazy(() => import("./views/register/RegisterView")),
  },
  {
    path: "/app",
    guard: AuthGuard,
    layout: DashboardLayout,
    routes: [
      {
        exact: true,
        path: '/app/account',
        component: lazy(() => import('./views/account/AccountView'))
      },
      // {
      //   exact: true,
      //   path: '/app/calendar',
      //   component: lazy(() => import('src/views/calendar/CalendarView'))
      // },
      // {
      //   exact: true,
      //   path: [
      //     '/app/chat/new',
      //     '/app/chat/:threadKey'
      //   ],
      //   component: lazy(() => import('src/views/chat/ChatView'))
      // },
      // {
      //   exact: true,
      //   path: '/app/chat',
      //   component: () => <Redirect to="/app/chat/new" />
      // },
      // {
      //   exact: true,
      //   path: '/app/extra/charts/apex',
      //   component: lazy(() => import('src/views/extra/charts/ApexChartsView'))
      // },
      // {
      //   exact: true,
      //   path: '/app/extra/forms/formik',
      //   component: lazy(() => import('src/views/extra/forms/FormikView'))
      // },
      // {
      //   exact: true,
      //   path: '/app/extra/forms/redux',
      //   component: lazy(() => import('src/views/extra/forms/ReduxFormView'))
      // },
      // {
      //   exact: true,
      //   path: '/app/extra/editors/draft-js',
      //   component: lazy(() => import('src/views/extra/editors/DraftEditorView'))
      // },
      // {
      //   exact: true,
      //   path: '/app/extra/editors/quill',
      //   component: lazy(() => import('src/views/extra/editors/QuillEditorView'))
      // },
      // {
      //   exact: true,
      //   path: '/app/kanban',
      //   component: lazy(() => import('src/views/kanban/KanbanView'))
      // },
      // {
      //   exact: true,
      //   path: [
      //     '/app/mail/label/:customLabel/:mailId?',
      //     '/app/mail/:systemLabel/:mailId?'
      //   ],
      //   component: lazy(() => import('src/views/mail/MailView'))
      // },
      // {
      //   exact: true,
      //   path: '/app/mail',
      //   component: () => <Redirect to="/app/mail/all" />
      // },
      // {
      //   exact: true,
      //   path: '/app/management/customers',
      //   component: lazy(() => import('src/views/customer/CustomerListView'))
      // },
      // {
      //   exact: true,
      //   path: '/app/management/customers/:customerId',
      //   component: lazy(() => import('src/views/customer/CustomerDetailsView'))
      // },
      // {
      //   exact: true,
      //   path: '/app/management/customers/:customerId/edit',
      //   component: lazy(() => import('src/views/customer/CustomerEditView'))
      // },
      // {
      //   exact: true,
      //   path: '/app/management/invoices',
      //   component: lazy(() => import('src/views/invoice/InvoiceListView'))
      // },
      // {
      //   exact: true,
      //   path: '/app/management/invoices/:invoiceId',
      //   component: lazy(() => import('src/views/invoice/InvoiceDetailsView'))
      // },
      // {
      //   exact: true,
      //   path: '/app/management/orders',
      //   component: lazy(() => import('src/views/order/OrderListView'))
      // },
      // {
      //   exact: true,
      //   path: '/app/management/orders/:orderId',
      //   component: lazy(() => import('src/views/order/OrderDetailsView'))
      // },
      // {
      //   exact: true,
      //   path: '/app/management/products',
      //   component: lazy(() => import('src/views/product/ProductListView'))
      // },
      // {
      //   exact: true,
      //   path: '/app/management/products/create',
      //   component: lazy(() => import('src/views/product/ProductCreateView'))
      // },
      // {
      //   exact: true,
      //   path: '/app/management',
      //   component: () => <Redirect to="/app/management/customers" />
      // },
      // {
      //   exact: true,
      //   path: '/app/projects/overview',
      //   component: lazy(() => import('src/views/project/OverviewView'))
      // },
      // {
      //   exact: true,
      //   path: '/app/projects/browse',
      //   component: lazy(() => import('src/views/project/ProjectBrowseView'))
      // },
      // {
      //   exact: true,
      //   path: '/app/projects/create',
      //   component: lazy(() => import('src/views/project/ProjectCreateView'))
      // },
      // {
      //   exact: true,
      //   path: '/app/projects/:id',
      //   component: lazy(() => import('src/views/project/ProjectDetailsView'))
      // },
      // {
      //   exact: true,
      //   path: '/app/projects',
      //   component: () => <Redirect to="/app/projects/browse" />
      // },
      {
        exact: true,
        path: '/app/reports/dashboard',
        component: lazy(() => import('./views/reports/dashboard-view'))
      },
      {
        exact: true,
        path: "/app/portal/dashboard",
        component: lazy(
          () => import("./layouts/dashboard-layout/DashboardLayout.container")
        ),
      },
      {
        exact: true,
        path: '/app/reports',
        component: () => <Redirect to="/app/reports/dashboard" />
      },
      // {
      //   exact: true,
      //   path: '/app/social/feed',
      //   component: lazy(() => import('src/views/social/FeedView'))
      // },
      // {
      //   exact: true,
      //   path: '/app/social/profile',
      //   component: lazy(() => import('src/views/social/ProfileView'))
      // },
      // {
      //   exact: true,
      //   path: '/app/social',
      //   component: () => <Redirect to="/app/social/profile" />
      // },
      {
        exact: true,
        path: "/app",
        component: () => <Redirect to="/app/reports/dashboard" />,
      },
      {
        component: () => <Redirect to="/404" />
      }
    ],
  },
  {
    path: "*",
    layout: MainLayout,
    routes: [
      {
        exact: true,
        path: "/",
        component: HomeView,
      },
      {
        exact: true,
        path: "/home",
        component: HomeView,
      },
      // {
      //   exact: true,
      //   path: "/loginmenu",
      //   component: lazy(
      //     () => import("./components/deprecate/api-authorization/LoginMenu")
      //   ),
      // },
      // {
      //   exact: true,
      //   path: "/authentication/login",
      //   component: lazy(() => import("./components/deprecate/api-authorization/Login")),
      // },
      {
        exact: true,
        path: "/registerone",
        component: () => <Redirect to={ApplicationPaths.Login} />,
      },

      // {
      //   exact: true,
      //   path: "/app",
      //   component: () => <Redirect to="/app/portal/dashboard" />,
      // },
      {
        exact: true,
        path: "/pricing",
        component: lazy(() => import("./views/pricing/PricingView")),
      },
      // {
      //   component: () => <Redirect to="/404" />
      // }
    ],
  },
];



export default routes;
