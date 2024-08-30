import { defineStore } from 'pinia';

import { fetchWrapper } from '../utils/fetch-wrapper';
import { router } from '../utils/router';

export const useAuthStore = defineStore({
    id: 'auth',
    state: () => ({
        // initialize state from local storage to enable user to stay logged in
        user: JSON.parse(localStorage.getItem('user')),
        returnUrl: null
    }),
    actions: {
        async login(username, password) {

            let response = await fetch('api/authenticate', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json;charset=utf-8',
                    'Authorization': `Bearer_test`
                },
                body: JSON.stringify({ username, password })
            });

            if (response.status === 403 || response.status === 401) {
                throw Error(await response.text());
            }

            let user = await response.json();

            // update pinia state
            this.user = user;

            // store user details and jwt in local storage to keep user logged in between page refreshes
            localStorage.setItem('user', JSON.stringify(user));

            // redirect to previous url or default to home page
            router.push(this.returnUrl || '/');
        },
        logout() {
            this.user = null;
            localStorage.removeItem('user');
            router.push('/login');
        }
    }
});