import '@fortawesome/fontawesome-free/css/all.css';

import 'vue-loading-overlay/dist/vue-loading.css';

import 'bootstrap';
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap-vue/dist/bootstrap-vue.css';
import './assets/css/site.css';

import Vue from 'vue';
import App from './App.vue';
import router from './router';
import BootstrapVue from 'bootstrap-vue';
import VeeValidate from 'vee-validate';

Vue.use(BootstrapVue);

const veeValidateConfig = {
    inject: false,
    fieldsBagName: 'formFields'
};
Vue.use(VeeValidate, veeValidateConfig)

Vue.config.productionTip = false

new Vue({
  router,
  render: h => h(App)
}).$mount('#app')
