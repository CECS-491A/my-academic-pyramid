import Vue from 'vue'
import Vuetify from 'vuetify'
import Router from 'vue-router'
import 'vuetify/dist/vuetify.min.css'
import Home from '@/components/Home'
import Publish from '@/components/Publish'
import Logging from '@/components/Logging'
import Login from '@/components/Login'
import UserList from '@/components/UserList'

Vue.use(Router)
Vue.use(Vuetify)

export default new Router({
  mode: 'history',
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
      path: '/UserManagement',
      name: 'UserManagement',
      component: UserList
    }
  }
  ]
})