import Vue from 'vue'
import './plugins/vuetify'
import App from './App.vue'
import router from './router'
import axios from 'axios'
import VueAxios from 'vue-axios'

Vue.prototype.$hostname = "https://api.myacademicpyramid.com/api/usermanager/"
Vue.prototype.$eventBus = new Vue()
Vue.config.productionTip = false
Vue.use(VueAxios, axios)
new Vue({
  router: router,
  render: h => h(App),
}).$mount('#app')

