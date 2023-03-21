import Vue from 'vue'
import App from './App.vue'
import vuetify from './plugins/vuetify'
import router from './plugins/router'
import auth from './mixins/authMixins';
import error from './mixins/errorMixin';

Vue.mixin(auth);
Vue.mixin(error);

Vue.config.productionTip = false

new Vue({
  vuetify,
  router,
  render: h => h(App)
}).$mount('#app')
