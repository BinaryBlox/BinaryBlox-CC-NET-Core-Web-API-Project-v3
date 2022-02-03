import { IMenuItem, INavMenuItemList } from "binaryblox-react-ui";
import {
  BarChart as BarChartIcon,
  Briefcase as BriefcaseIcon,
  Mail as MailIcon,
  PieChart as PieChartIcon,
  Users as UsersIcon,
} from "react-feather";

export const users = [
  {
    id: "5e86809283e28b96d2d38537",
    avatar: "/static/images/avatars/avatar_6.png",
    canHire: false,
    country: "USA",
    email: "tony.henderson@kp.org",
    isPublic: true,
    name: "Tony Henderson",
    password: "Password123",
    phone: "+40 777666555",
    role: "admin",
    state: "New York",
    tier: "Premium",
  },
];





export const sections: INavMenuItemList[] = [
  {
    subheader: "Reports",
    items: [
      {
        title: "Dashboard",
        icon: PieChartIcon,
        href: "/app/reports/dashboard",
      },
      {
        title: "Detail",
        icon: BarChartIcon,
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
        icon: BriefcaseIcon,
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
        icon: MailIcon,
      },
      // {
      //   title: 'Social Platform',
      //   href: '/app/social',
      //   icon: ShareIcon,
      //   items: [
      //     {
      //       title: 'Profile',
      //       href: '/app/social/profile'
      //     },
      //     {
      //       title: 'Feed',
      //       href: '/app/social/feed'
      //     }
      //   ]
      // },
      // {
      //   title: 'Boards',
      //   href: '/app/kanban',
      //   icon: TrelloIcon
      // },

      // {
      //   title: 'Chat',
      //   href: '/app/chat',
      //   icon: MessageCircleIcon,
      //   info: () => (React.createElement(Chip, {color:"secondary", size:"small", label: "Updated"}))
      // },
      // {
      //   title: 'Calendar',
      //   href: '/app/calendar',
      //   icon: CalendarIcon,
      //   info: () => (React.createElement(Chip, {color:"secondary", size:"small", label: "Updated"}))
      // }
    ],
  },
  {
    subheader: "Management",
    items: [
      {
        title: "Users",
        icon: UsersIcon,
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
      // {
      //   title: 'Products',
      //   icon: ShoppingCartIcon,
      //   href: '/app/management/products',
      //   items: [
      //     {
      //       title: 'List Products',
      //       href: '/app/management/products'
      //     },
      //     {
      //       title: 'Create Product',
      //       href: '/app/management/products/create'
      //     }
      //   ]
      // },
      // {
      //   title: 'Orders',
      //   icon: FolderIcon,
      //   href: '/app/management/orders',
      //   items: [
      //     {
      //       title: 'List Orders',
      //       href: '/app/management/orders'
      //     },
      //     {
      //       title: 'View Order',
      //       href: '/app/management/orders/1'
      //     }
      //   ]
      // },
      // {
      //   title: 'Invoices',
      //   icon: ReceiptIcon,
      //   href: '/app/management/invoices',
      //   items: [
      //     {
      //       title: 'List Invoices',
      //       href: '/app/management/invoices'
      //     },
      //     {
      //       title: 'View Invoice',
      //       href: '/app/management/invoices/1'
      //     }
      //   ]
      // }
    ],
  },
];
