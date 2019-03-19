import Vue from 'vue'
import Hello from '@/components/HelloWorld'
import Router from 'vue-router'
import Login from '@/components/Login'
import UserList from '@/components/UserList'

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
      path: '/login',
      name: 'Login',
      component: Login
    },
    {
      path: '/UserManagement',
      name: 'UserManagement',
      component: UserList
    }
  ]
})