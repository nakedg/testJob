import Vue from 'vue'
import Router from 'vue-router'
import Home from './views/Home.vue'
import Users from './views/user/Users.vue'
import EditUser from './views/user/EditUser.vue';

Vue.use(Router)

export default new Router({
    mode: 'history',
    base: process.env.BASE_URL,
    linkActiveClass: 'active',
    linkExactActiveClass: 'active',
    routes: [
        {
            path: '/',
            name: 'home',
            component: Home
        },
        {
            path: '/user/list',
            name: 'users',
            component: Users
        },
        {
            path: '/user/add',
            component: EditUser
        },
        {
            path: '/user/edit/:id',
            component: EditUser
        }
    ]
})
