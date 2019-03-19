import Vue from 'vue'
import Router from 'vue-router'
import Home from '@/components/Home'
import Publish from '@/components/Publish'
import Login from '@/View/Login'
import Chat from '@/View/Chat'
import VueChatScroll from 'vue-chat-scroll'
import Vuetify from 'vuetify'

Vue.use(VueChatScroll)

Vue.use(Router)
Vue.use(Vuetify)

export default new Router({
  routes: [
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
    }
  ]
})
