<template>
  <div>
    <router-view></router-view>
  </div>
  <notifications-container>
    <notification
      @close="handleNotificationClose"
      v-for="(notification,idx) in notifications"
      :idx="idx"
      :item="notification"
      :key="idx"
    >
      <template v-slot:content="item">
        {{ item.item }}
      </template>
    </notification>
  </notifications-container>
</template>

<script>
import { Subject } from 'rxjs';
import { provide, ref } from 'vue';
import NotificationsContainer from '@/components/NotificationsContainer.vue';
import Notification from '@/components/Notification.vue';

export default {
  name: 'App',
  components: {
    Notification,
    NotificationsContainer,
  },
};
</script>

<script setup>
const notifications = ref([]);
const bus = new Subject();
provide('bus', bus);
const token = localStorage.getItem('Token');
if (token) {
  const ws = new WebSocket('wss://localhost:44336/ws');
  ws.onopen = () => {
    ws.send(token);
  };
  ws.onmessage = (msg) => {
    notifications.value.push(msg.data);
    console.log(msg.data);
    bus.next(msg.data);
  };
}
const handleNotificationClose = (idx) => {
  console.log(idx, notifications.value[idx]);
  if (notifications.value.length === 1) {
    notifications.value = [];
    return;
  }
  notifications.value = notifications.value.splice(idx, 1);
};
</script>
<style>
@import "./styles/normalize.scss";
@import "./styles/colors.scss";

* {
  margin: 0;
  padding: 0;
  box-sizing: border-box;
  font-family: Roboto, sans-serif;
}
</style>
