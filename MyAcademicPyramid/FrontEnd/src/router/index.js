import Vue from 'vue'
import Router from 'vue-router'
import Home from '@/components/Home'
import Publish from '@/components/Publish'
import VueChatScroll from 'vue-chat-scroll'
import Login from '@/components/Login'
import UserList from '@/components/UserList'

Vue.use(VueChatScroll)


Vue.use(Router)

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
  }
  ]
})