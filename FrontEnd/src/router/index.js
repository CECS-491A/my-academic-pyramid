import Vue from 'vue'
import Vuetify from 'vuetify'
import Router from 'vue-router'
import 'vuetify/dist/vuetify.min.css'
import Home from '@/Views/Home'
import Publish from '@/Views/Publish'
import Logging from '@/Views/Logging'
import Login from '@/Views/Login'
import UserManagement from '@/Views/UserManagement'
import LoginPrototype from '@/Views/LoginPrototype'
import Chat from '@/Views/Chat'
import UserRegistration from '@/Views/UserRegistration'
import UserHomePage from '@/Views/UserHomePage'
import Redirect from '@/Views/Redirect'
import UsageAnalysisDashboard from '@/Views/UsageAnalysisDashboard'
import Profile from '@/Views/Profile'
//import Search from '@/Views/Search'
import DiscussionForum from '@/Views/DiscussionForum'
//import PostQuestion from '@/components/DiscussionForum/PostQuestion'
// delete later
import TestComponent from '@/components/DiscussionForum/TestComponent'
import TestView from '@/Views/TestView'

import SchoolRegistration from '@/Views/SchoolRegistration'
import Search from '@/Views/Search'

Vue.use(Router)
Vue.use(Vuetify)

export default new Router({
  base: process.env.BASE_URL,
  routes: [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
    path: '/publish',
    name: 'Publish',
    component: Publish
  },
  {
    path: '/login',
    name: 'Login',
    component: Login
  },

  {
    path: '/Logging',
    name: 'Logging',
    component: Logging
  },

  {
    path: '/UserManagement',
    name: 'UserManagement',
    component: UserManagement
  },
  {
    path: '/LoginPrototype',
    name: 'LoginPrototype',
    component: LoginPrototype
  },
  {
    path: '/Chat',
    name: 'Chat',
    component: Chat
  },
  {
    path: "/UserRegistration",
    name: "UserRegistration",
    component: UserRegistration
  },
  {
    path: "/UserHomePage",
    name: "UserHomePage",
    component: UserHomePage
  },
  {
    path: "/Redirect",
    name: "Redirect",
    component: Redirect
  },
  {
    path: "/Dashboard",
    name: "Dashboard",
    component: UsageAnalysisDashboard
  },
  {
    path: "/DiscussionForum",
    name: "DiscussionForum",
    component: DiscussionForum
  },
  // delete later
  {
    path: "/TestComponent",
    name: "TestComponent",
    component: TestComponent
  },
  // delete later
  {
    path: "/TestView",
    name: "TestView",
    component: TestView
  },
  {
    path: "/SchoolRegistration",
    name: "SchoolRegistration",
    component: SchoolRegistration
  },
  {
   path: "/Search",
   name: "Search",
   component: Search
  },
  {
    path: '/Profile/:id',
    name: 'Profile',
    component: Profile
  }
  // },
  // {
  //   path: '/DiscussionForum/PostQuestion',
  //   name: 'PostQuestion',
  //   component: PostQuestion
  // }
  ]
})