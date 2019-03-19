import Vue from 'vue'
import Hello from '@/components/HelloWorld'
import Router from 'vue-router'
import Home from '@/components/Home'
import Publish from '@/components/Publish'
import Login from '@/View/Login'
import Chat from '@/View/Chat'
import VueChatScroll from 'vue-chat-scroll'
import Vuetify from 'vuetify'
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
		name: 'Hello',
		component: Hello
	},
    {
      path: '/',
      name: 'Home',
      component: Home
    },
    {
      path: '/Publish',
      name: 'Publish',
      component: Publish
    },
    {
      path: '/login',
      name: 'Login',
      component: Login
    },
    {
      path: '/chat',
      name: 'Chat',
      component: Chat,
      props: true,
      beforeEnter: (to, from, next) => {
        if (to.params.name) {
          next()
        } else {
          next({name: 'Login'})
        }
      }
    },
    {
      path: '/UserManagement',
      name: 'UserManagement',
      component: UserList
    }
  ]
})