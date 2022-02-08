<template>
  <div class="login-page">
    <div class="login-page__content">
      <h2 class="login-page__subtitle">TodoApp</h2>
      <h1 class="login-page__title">Login</h1>
      <label class="login-page__input">
        <span class="login-page__label">Email adress</span>
        <TInput v-model="email"/>
      </label>
      <label class="login-page__input">
        <span class="login-page__label">Password</span>
        <TInput v-model="password"/>
      </label>
      <primary-button :disabled="isDisabled" @click="loginAsync">
        <span class="login-page__btn-label">Login</span>
      </primary-button>
      <span class="login-page__sign-up">Don't have an account?
      <router-link class="login-page__link" :to="{name: 'register'}">Sign up</router-link>
    </span>
    </div>
  </div>
</template>

<script>
import PrimaryButton from '@/components/Buttons/PrimaryButton.vue';
import TInput from '@/components/Inputs/TInput.vue';
import { ref, computed, inject } from 'vue';
import { useStore } from 'vuex';
import { useRouter } from 'vue-router';
import { signInAsync } from '@/api/api';

export default {
  name: 'LoginPage',
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
let ws = null;
const bus = inject('bus');
console.log(bus);
const loginAsync = () => {
  store.dispatch('signIn');
  const user = {
    password: password.value,
    email: email.value,
  };
  signInAsync(user)
    .then((res) => {
      localStorage.setItem('Token', res.token);
      ws = new WebSocket('wss://localhost:44336/ws');
      ws.onopen = () => {
        ws.send(res.token);
      };
      ws.onmessage = (msg) => {
        console.log(msg.data);
        bus.next(msg.data);
      };
    });
  router.push({
    name: 'home',
  });
};

// ws.onopen = () => {
//   console.log('open');
//   ws.send('data');
// };

const isDisabled = computed(() => password.value.length === 0 || email.value.length === 0);
</script>

<style scoped lang="scss">
.login-page {
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
