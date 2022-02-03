import mock from "./mock";
import { bxUtilWait, INavMenuItemList, Guid } from "binaryblox-react-ui";
import {
  BarChart as BarChartIcon,
  Briefcase as BriefcaseIcon,
  Mail as MailIcon,
  PieChart as PieChartIcon,
  Users as UsersIcon,
} from "react-feather";
import { MenuData } from "../store/slices/entities/menu-data/MenuData.slice";

interface Result {
  description: string;
  title: string;
}

mock.onGet("/api/dashboard/menudata/sidebar").reply(async () => {
  try {
    await bxUtilWait(1500);

    console.log("Mock getting called");

    const results: INavMenuItemList[] = [
      {
        subheader: "Reports",
        items: [
          {
            title: "Dashboard",
            icon: "PieChartIcon",
            href: "/app/reports/dashboard",
          },
          {
            title: "Detail",
            icon: "BarChartIcon",
            href: "/app/reports/dashboard-alternative",
            items: [
              {
                title: "Leads",
                href: "/app/management/customers",
              },
              {
                title: "Loans",
                href: "/app/management/customers/1",
              },
              {
                title: "Closed",
                href: "/app/management/customers/1/edit",
              },
              {
                title: "Cancelled/Denied",
                href: "/app/management/customers/1/edit",
              },
            ],
          },
        ],
      },

      {
        subheader: "Applications",
        items: [
          {
            title: "Projects",
            href: "/app/projects",
            icon: "BriefcaseIcon",
            items: [
              {
                title: "Overview",
                href: "/app/projects/overview",
              },
              {
                title: "Browse Projects",
                href: "/app/projects/browse",
              },
              {
                title: "Create Project",
                href: "/app/projects/create",
              },
              {
                title: "View Project",
                href: "/app/projects/1",
              },
            ],
          },
          {
            title: "Inbox",
            href: "/app/mail",
            icon: "MailIcon",
          },
        ],
      },
      {
        subheader: "Management",
        items: [
          {
            title: "Users",
            icon: "UsersIcon",
            href: "/app/management/customers",
            items: [
              {
                title: "List Users",
                href: "/app/management/customers",
              },
              {
                title: "View User",
                href: "/app/management/customers/1",
              },
              {
                title: "Edit User",
                href: "/app/management/customers/1/edit",
              },
            ],
          },
        ],
      },
    ];

    const data: any   = [{
      id: Guid.create().toString(),
      name: "Sidebar Menu",
      description: "Sidebar Menu Description", 
      items: undefined,
      status: "none",
    }];

    return [200,  data ];
  } catch (err) {
    console.error(err);
    return [500, { message: "Internal server error" }];
  }
});
