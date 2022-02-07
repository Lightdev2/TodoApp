<template>
  <div>
    <router-view></router-view>
  </div>
</template>

<script>
import { Subject } from 'rxjs';
import { provide } from 'vue';

export default {
  name: 'App',
};
</script>

<script setup>
const bus = new Subject();
provide('bus', bus);
const token = localStorage.getItem('Token');
if (token) {
  const ws = new WebSocket('wss://localhost:44336/ws');
  ws.onopen = () => {
    ws.send(token);
  };
  ws.onmessage = (msg) => {
    console.log(msg.data);
    bus.next(msg.data);
  };
}
</script>
<style>
@import "./styles/normalize.scss";
* {
  margin: 0;
  padding: 0;
  box-sizing: border-box;
  font-family: Roboto, sans-serif;
}
</style>
