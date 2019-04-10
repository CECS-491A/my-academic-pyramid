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
import ChatContainer from '@/Views//ChatContainer'
import UserRegistration from '@/Views/UserRegistration'

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
    component: ChatContainer
  },
  {
    path: "/UserRegistration",
    name: "UserRegistration",
    component:UserRegistration
  }
  ]
})