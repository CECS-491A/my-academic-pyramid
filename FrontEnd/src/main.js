import Vue from 'vue'
import './plugins/vuetify'
import App from './App.vue'
import router from './router'
import axios from 'axios'
import VueAxios from 'vue-axios'
import VueChatScroll from 'vue-chat-scroll'


//Vue.prototype.$hostname = "https://api.myacademicpyramid.com/api/"
Vue.prototype.$hostname = "http://localhost:59364/api/"
Vue.prototype.$eventBus = new Vue()
Vue.config.productionTip = false
Vue.use(VueAxios, axios)
Vue.use(VueChatScroll)
new Vue({
  router: router,
  render: h => h(App),
}).$mount('#app')

