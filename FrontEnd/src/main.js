import Vue from 'vue'
import './plugins/vuetify'
import App from './App.vue'
import router from './router'
import axios from 'axios'
import VueAxios from 'vue-axios'
import VueChatScroll from 'vue-chat-scroll'
import 'chart.js'
import 'hchs-vue-charts'


Vue.prototype.$hostname = "https://api.myacademicpyramid.com/api/"
Vue.prototype.$signalRHostName = "https://api.myacademicpyramid.com/"

// Vue.prototype.$hostname = "http://localhost:59364/api/"
// Vue.prototype.$signalRHostName = "http://localhost:59364/"

Vue.prototype.$ssoLoginPage = "https://kfc-sso.com/#/login"
Vue.prototype.$eventBus = new Vue()
Vue.config.productionTip = false
Vue.use(VueAxios, axios)
Vue.use(VueChatScroll)
Vue.use(window.VueCharts)

new Vue({
  router: router,
  render: h => h(App),
}).$mount('#app')

