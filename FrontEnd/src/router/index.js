import Vue from 'vue'
import Vuetify from 'vuetify'
import Router from 'vue-router'
import 'vuetify/dist/vuetify.min.css'
import Home from '@/Views/Home'
import Publish from '@/Views/Publish'
import Logging from '@/Views/Logging'
import Login from '@/Views/Login'
import UserList from '@/Views/UserList'
import UserManagement from '@/Views/UserManagement'

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
  // {
  //   path: '/chat',
  //   name: 'Chat',
  //   component: Chat,
  //   props: true,
  //   beforeEnter: (to, from, next) => {
  //     if (to.params.name) {
  //       next()
  //     } else {
  //       next({name: 'Login'})
  //     }
  //   }
  // },
  {
    path: '/UserManagement',
    name: 'UserManagement',
    component: UserList
  },
  {
    path: '/Logging',
    name: 'Logging',
    component: Logging
  },

  {
    path: '/UserManagementTest',
    name: 'UserManagementTest',
    component: UserManagement
  }
  ]
})