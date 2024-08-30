<script setup>
import { Form, Field, ErrorMessage } from 'vee-validate';

import { useField } from 'vee-validate';

const { errorMessage, meta, value } = useField('fieldName');

import { useAuthStore } from '../stores/auth-store.js';

function onSubmit(values, { setErrors }) {
    const authStore = useAuthStore();
    const { username, password } = values;

    return authStore.login(username, password)
        .catch(error => setErrors({ apiError: error }));
}

const validateName = value => !!value ? true : 'This field is required';
const validatePassword = value => !!value ? true : 'This field is required';

</script>

<template>
    <div>
        <div class="alert alert-info">
            Username: test<br />
            Password: test
        </div>
        <h2>Login</h2>
        <Form @submit="onSubmit" v-slot="{errors}">
            <div class="form-group">
                <label>Username</label>
                <Field name="username" :rules="validateName" v-slot="{ field, errors, errorMessage }">
                    <input v-bind="field" type="text" class="form-control" />
                    <span class="badge text-bg-warning">{{ errorMessage }}</span>
                </Field>
            </div>
            <div class="form-group">
                <label>Password</label>
                <Field name="password" :rules="validatePassword" v-slot="{ field, errors, errorMessage }">
                    <input v-bind="field" type="text" class="form-control" />
                    <span class="badge text-bg-warning">{{ errorMessage }}</span>
                </Field>
            </div>
            <div class="form-group">
                <button class="btn btn-primary">
                    <!--<span v-show="true" class="spinner-border spinner-border-sm mr-1"></span>-->
                    Login
                </button>
            </div>
            <div v-if="errors.apiError" class="alert alert-danger mt-3 mb-0">{{errors.apiError}}</div>
        </Form>
    </div>
</template>