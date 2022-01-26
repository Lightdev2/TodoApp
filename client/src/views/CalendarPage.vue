<script>
import { ref, computed } from 'vue';
import ArrowIcon from '@/assets/icons/arrow.vue';

const RIGHT = 'Right';
const LEFT = 'Left';

export default {
  name: 'CalendarPage',
  components: {
    ArrowIcon,
  },
};
</script>
<script setup>
const days = [
  'Пон',
  'Вто',
  'Сре',
  'Чет',
  'Пят',
  'Суб',
  'Вос',
];

const monthNames = [
  'Январь', 'Февраль', 'Март', 'Апрель',
  'Май', 'Июнь', 'Июль', 'Август', 'Сентябрь', 'Октябрь', 'Ноябрь', 'Декабрь',
];

const getDaysCount = (_year, _month) => new Date(_year, _month, 0).getDate();
const events = ref([]);
const getEvents = () => {
  const newEvents = [];
  if (Math.ceil(Math.random() * 100) < 40) {
    events.value = newEvents;
    return;
  }
  for (let i = 0; i < 10; i += 1) {
    newEvents.push(`Event ${Math.ceil(Math.random() * 100)}`);
  }
  events.value = newEvents;
};
const curDate = ref(new Date());
const getDays = () => {
  const year = curDate.value.getFullYear();
  const month = curDate.value.getMonth();
  const monthLenght = getDaysCount(year, month);
  const days = [];
  for (let i = 0; i < monthLenght; i += 1) {
    days.push(i + 1);
  }
  const firstDayOfMonth = new Date(curYear.value, curMonth.value, 1).getDay() - 1;
  let lastDate = new Date(curYear.value, curMonth.value, 0).getDate();
  console.log(firstDayOfMonth);
  if (firstDayOfMonth !== 0) {
    for (let i = 0; i < firstDayOfMonth; i += 1) {
      days.unshift(lastDate);
      lastDate -= 1;
    }
  }
  if (days.length < 42) {
    let counter = 1;
    while (days.length < 42) {
      days.push(counter);
      counter += 1;
    }
  }
  return days;
};

const curMonth = computed(() => curDate.value.getMonth());
const curYear = computed(() => curDate.value.getFullYear());
//  const curDay = computed(() => curDate.value.getDate());
const friendlyDate = computed(() => `${monthNames[curMonth.value]} ${curYear.value}`);
const toggleDate = (_dir) => {
  let month = null;
  switch (_dir) {
    case RIGHT:
      month = curMonth.value + 1;
      break;
    case LEFT:
      month = curMonth.value - 1;
      break;
    default:
      break;
  }
  curDate.value = new Date(curYear.value, month, 1, 0);
  //  console.log(curDate.value);
};

</script>

<template>
<div class="page">
<h1 class="page__title">Calendar</h1>
<div class="page__content">
  <div class="calendar">
    <div class="calendar__controls">
      <button @click="toggleDate(LEFT)" class="calendar__control-btn">
        <arrow-icon class="calendar__arrow calendar__arrow--left"/>
      </button>
      <span>{{ friendlyDate }}</span>
      <button @click="toggleDate(RIGHT)" class="calendar__control-btn">
        <arrow-icon class="calendar__arrow calendar__arrow--right"/>
      </button>
    </div>
    <div class="calendar__head-days">
      <div v-for="day in days" :key="day">{{ day }}</div>
    </div>
    <div class="calendar__days">
      <div class="calendar__day" v-for="(day,idx) in getDays()" :key="idx" @click="getEvents">
        {{ day }}
      </div>
    </div>
  </div>
  <div class="calendar__events">
    <h2>Events</h2>
    <ul class="calendar__events-list">
      <li v-for="(event, idx) in events" :key="idx">
        {{ events.length === 0 ? 'No events' : event }}
      </li>
    </ul>
  </div>
</div>

</div>
</template>

<style scoped lang="scss">
.page {
  display: flex;
  flex-direction: column;
  &__content {
    display: flex;
    justify-content: space-between;
  }
  &__title {
    margin-bottom: 1rem;
  }
}
.calendar {
  height: 500px;
  flex-grow: 1;
  box-shadow: 0 0 3px 1px #dedede;
  margin-right: 1rem;
  border: 1px solid #dedede;
  border-radius: 5px;
  padding: 5px;
  &__arrow {
    width: 20px;
    &--left {
      transform: rotate(-90deg);
    }
    &--right {
      transform: rotate(90deg);
    }
  }
  &__control-btn {
    border: none;
    background: transparent;
    cursor: pointer;
  }
  &__events {
    width: 200px;
  }
  &__events-list {
    list-style: none;
  }
  &__controls {
    display: flex;
    align-items: center;
    justify-content: space-between;
    margin-bottom: 1rem;
    padding: 5px 10px;
  }
  &__head-days {
    grid-template-columns: repeat(7, 14.28%);
    display: grid;
    gap: 5px;
    padding-bottom: 10px;
    border-bottom: 2px solid #dedede;
    margin-bottom: 1rem;
  }
  &__days {
    height: 100%;
    gap: 5px;
    display: grid;
    grid-template-columns: repeat(7, 1fr);
    grid-template-rows: repeat(6, 60px);
  }
  &__day {
    cursor: pointer;
    border-radius: 5px;
    &:hover {
      background-color: #dedede;
    }
  }
}
</style>
