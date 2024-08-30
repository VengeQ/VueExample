<script setup>
import HelloWorld from './components/HelloWorld.vue'
import { RouterLink, RouterView } from 'vue-router';

import { useAuthStore } from '@/stores/auth-store.js';    


const authStore = useAuthStore();
</script>

<!--<template>
  <header>
    <img alt="Vue logo" class="logo" src="./assets/logo.svg" width="125" height="125" />

    <div class="wrapper">
      <HelloWorld msg="You did it!" />
    </div>
  </header>-->

    <!--<Login></Login>-->
<!--</template>-->

<template>
    <div class="app-container bg-light">
        <nav v-show="authStore.user" class="navbar navbar-expand navbar-dark bg-dark">
            <div class="navbar-nav">
                <RouterLink to="/" class="nav-item nav-link">Home</RouterLink>
                <a @click="authStore.logout()" class="nav-item nav-link">Logout</a>
            </div>
        </nav>
        <div class="container pt-4 pb-4">
            <RouterView />
        </div>
    </div>
</template>

<style scoped>
header {
  line-height: 1.5;
}

.logo {
  display: block;
  margin: 0 auto 2rem;
}

@media (min-width: 1024px) {
  header {
    display: flex;
    place-items: center;
    padding-right: calc(var(--section-gap) / 2);
  }

  .logo {
    margin: 0 2rem 0 0;
  }

  header .wrapper {
    display: flex;
    place-items: flex-start;
    flex-wrap: wrap;
  }
}
</style>
