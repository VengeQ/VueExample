<template>
    <div class="quizes-component">
        <h1>Викторины</h1>
        <div v-for="quiz of quizes">
            <router-link :to="{name: 'quiz', params: {id:quiz.id}}">{{quiz.title}}</router-link>
        </div>
        <!--<router-view></router-view>-->
    </div>
</template>

<script lang="js">
    import { defineComponent } from 'vue';
    import { useAuthStore } from '../stores/auth-store.js';

    export default defineComponent({
        data() {
            return {
                loading: false,
                quizes: null,
                authStore: useAuthStore()
            };
        },
        created() {
            // fetch the data when the view is created and the data is
            // already being observed
            this.fetchData();
        },
        watch: {
            // call again the method if the route changes
            '$route': 'fetchData'
        },
        methods: {
            fetchData() {
                this.quiz = null;
                this.loading = true;

                fetch('api/quizes', {
                    headers: {
                        "Accept": "application/json",
                        "Authorization": "Bearer " + this.authStore.user.token  // �������� ������ � ���������
                    }
                })
                    .then(r => {
                        if (!r.ok) {
                            throw new Error('Error occurred!')
                        }
                        return r.json()
                    }).catch((err) => {
                        console.log(err)
                    })
                    .then(json => {
                        this.quizes = json;
                        this.loading = false;
                        return;
                    });
            },
            doJob(answerId) {
                alert(answerId);
            }
        },
    });
</script>

<style scoped>
    th {
        font-weight: bold;
    }

    th, td {
        padding-left: .5rem;
        padding-right: .5rem;
    }

    .weather-component {
        text-align: center;
    }

    table {
        margin-left: auto;
        margin-right: auto;
    }
</style>