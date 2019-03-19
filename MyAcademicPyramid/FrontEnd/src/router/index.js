import Vue from 'vue'
import Router from 'vue-router'
import Home from '@/components/Home'
import Publish from '@/components/Publish'

Vue.use(Router)

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
    }
  ]
})
