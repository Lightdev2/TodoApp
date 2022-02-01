<template>
  <div class="register-page">
    <div class="register-page__content">
      <h2 class="register-page__subtitle">TodoApp</h2>
      <h1 class="register-page__title">Register</h1>
      <label class="register-page__input">
        <span class="register-page__label">Email adress</span>
        <TInput v-model="email"/>
      </label>
      <label class="register-page__input">
        <span class="register-page__label">Password</span>
        <TInput v-model="password"/>
      </label>
      <primary-button :disabled="isDisabled" @click="register">
        <span class="register-page__btn-label">Register</span>
      </primary-button>
      <span class="register-page__sign-up">Already have an account?
      <router-link class="register-page__link" :to="{name: 'login'}">Sign in</router-link>
    </span>
    </div>
  </div>
</template>

<script>
import PrimaryButton from '@/components/Buttons/PrimaryButton.vue';
import TInput from '@/components/Inputs/TInput.vue';
import { useStore } from 'vuex';
import { useRouter } from 'vue-router';
import { ref, computed } from 'vue';
import { signUpAsync, signInAsync } from '../api/api';

export default {
  name: 'RegisterPage',
  components: {
    PrimaryButton,
    TInput,
  },
};
</script>

<script setup>

const password = ref('');
const email = ref('');
const store = useStore();
const router = useRouter();

const register = () => {
  const user = {
    email: email.value,
    password: password.value,
  };
  signUpAsync(user)
    .then((res) => {
      signInAsync(user)
        .then((res) => {
          if (res) {
            sessionStorage.setItem('JWT', res.token);
            store.commit('signIn');
            router.push({ name: 'home' });
          }
        });
    });
};

const isDisabled = computed(() => password.value.length === 0 || email.value.length === 0);
</script>

<style scoped lang="scss">
.register-page {
  display: flex;
  align-items: center;
  justify-content: center;
  height: 100vh;

  &__subtitle {
    margin-bottom: 2rem;
  }

  &__content {
    display: flex;
    flex-direction: column;
  }

  &__input {
    display: flex;
    flex-direction: column;
    margin-bottom: 1rem;
    width: 300px;
  }

  &__title {
    margin-bottom: 1.5rem;
  }

  &__label {
    display: block;
    margin-bottom: 0.2rem;
    padding: 0.2rem 0;
  }

  &__btn-label {
    display: block;
    color: #fff;
    font-size: 16px;
    padding: 0.2rem 0;
  }

  &__sign-up {
    display: block;
    margin-top: 1rem;
  }

  &__link {
    text-decoration: none;
    color: #3454cf;
  }
}
</style>
