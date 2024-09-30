<template>
    <div class="quiz-component">
        <div v-if="loading" class="content">
            notwowes
        </div>
        <div v-else class="card" style="width: 18rem;">
            <div v-if="isStarted">
                <div class="card-header">
                    {{currentQuestion.question}}
                </div>

                <ul v-for="(answer, answerId) of currentQuestion.answerOptions" class="list-group list-group-flush">
                    <li @click="doJob(answerId)" class="list-group-item">{{answerId}} {{answer}}</li>
                </ul>
            </div>
            <div v-else>
                Данная викторина посвящена: {{quiz.title}}
                <button @click="startQuiz">Начать!</button>
            </div>

        </div>
    </div>
</template>

<script lang="js">
    import { defineComponent } from 'vue';
    import { useAuthStore } from '../stores/auth-store.js';

    export default defineComponent({
        data() {
            return {
                quizId: this.$route.params.id,
                loading: true,
                quiz: null,
                questions: [],
                currentQuestionIndex: 0,
                authStore: useAuthStore(),
                length: 0,
                isStarted: false
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

                fetch('/api/quizes/' + this.quizId, {
                    headers: {
                        "Accept": "application/json",
                        "Authorization": "Bearer " + this.authStore.user.token  // передача токена в заголовке
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
                        this.quiz = json;
                        this.loading = false;
                        this.questions = json?.items;

                        return;
                    });
            },
            doJob(answerId) {
                alert(answerId);
            },
            startQuiz() {
                this.isStarted = true;
            },
        },
        computed: {
            currentQuestion() {
                return this.questions[this.currentQuestionIndex];
            }
        }
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