import { createRouter, createWebHistory } from 'vue-router';

import { useAuthStore } from '@/stores/auth-store';
import HelloWorld from '../components/HelloWorld.vue'
import Login from '../components/Login.vue'
import Quizes from '../components/Quizes.vue'
import Quiz from '../components/Quiz.vue'

export const router = createRouter({
    history: createWebHistory(),
    linkActiveClass: 'active',
    routes: [
        /*     { path: '/', component: HomeView },*/
        { path: '/', component: HelloWorld },
        { path: '/login', component: Login },
        { path: '/quizes', component: Quizes },
        { path: '/quizes/:id', component: Quiz, name:'quiz' }
    ]
});

router.beforeEach(async (to) => {
    // redirect to login page if not logged in and trying to access a restricted page
    const publicPages = ['/login'];
    const authRequired = !publicPages.includes(to.path);
    const auth = useAuthStore();

    if (authRequired && !auth.user) {
        auth.returnUrl = to.fullPath;
        return '/login';
    }

    if (authRequired && auth.user) {
        let jwtPayload = JSON.parse(window.atob(auth.user.token.split('.')[1]))
        let isExpired = Date.now() >= jwtPayload.exp * 1000;
        if (isExpired) {
            auth.logout();
        }
    }

    if (auth.user && to.path === '/login') {
        auth.returnUrl = to.fullPath;
        return '/';
    }
});