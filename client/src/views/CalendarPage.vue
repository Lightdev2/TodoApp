<script>
import { ref, computed, watch } from 'vue';
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
  const monthLength = getDaysCount(year, month);
  const days = [];
  for (let i = 0; i < monthLength; i += 1) {
    days.push(i + 1);
  }
  const firstDayOfMonth = new Date(curYear.value, curMonth.value, 1).getDay() - 1;
  let lastDate = new Date(curYear.value, curMonth.value, 0).getDate();
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
const curDay = computed(() => curDate.value.getDate());
const friendlyDate = computed(() => `${monthNames[curMonth.value]}`);

const toggleDate = (_dir) => {
  let month = curMonth.value;
  const year = curYear.value;
  switch (_dir) {
    case LEFT:
      month -= 1;
      break;
    case RIGHT:
      month += 1;
      break;
    default:
      break;
  }
  curDate.value = new Date(year, month, 1, 0);
};

const yearsList = computed(() => {
  const years = [];
  const start = curYear.value - 100;
  const end = curYear.value + 100;
  for (let i = start; i < end; i += 1) {
    years.push(i);
  }
  return years.reverse();
});

const selectedYear = ref(curYear.value);

watch(selectedYear, () => {
  curDate.value = new Date(selectedYear.value, curMonth.value, curDay.value);
});
</script>

<template>
  <div class="page">
    <h1 class="page__title">Calendar</h1>
    <h2>Upcoming events</h2>
    <span class="calendar__events-subtitle">Don't miss schedule events</span>
    <div class="page__content">
      <div>
        <div class="calendar__events">
          <ul class="calendar__events-list">
            <li class="calendar__event" v-for="(event, idx) in events" :key="idx">
            <span class="calendar__event-time">
              <div class="calendar__event-priority"/>
              <span>10:00 - 11:00</span>
            </span>
              <h2 class="calendar__event-title">
                {{ events.length === 0 ? 'No events' : event }}
              </h2>
              <p class="calendar__event-snippet">
                Lorem ipsum dolor sit amet, consectetur adipisicing elit.
              </p>
            </li>
          </ul>
        </div>
      </div>

      <div class="calendar">
        <div class="calendar__controls">
          <button @click="toggleDate(LEFT)" class="calendar__control-btn">
            <arrow-icon class="calendar__arrow calendar__arrow--left"/>
          </button>
          <span class="calendar__head-date">{{ friendlyDate }}
          <select v-model="selectedYear">
            <option :selected="year === curYear" v-for="year in yearsList" :key="year">
              <span> {{ year }}</span>
            </option>
          </select>
          </span>
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
    @media screen and (max-width: 1024px) {
      flex-direction: column-reverse;
      flex-grow: 1;
    }
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
  margin-left: 10px;

  &__events-subtitle {
    color: #dedede;
    display: block;
    margin-bottom: 1rem;
  }

  &__head-date {
    cursor: pointer;
  }

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
    width: 300px;
    max-height: 500px;
    overflow-y: auto;
    @media screen and (max-width: 1024px) {
      margin-top: 20px;
      width: 100%;
      max-height: fit-content;
    }
  }

  &__events-list {
    list-style: none;
    margin-right: 1rem;
    @media screen and (max-width: 1024px) {
      margin: 0;
      width: 100%;
    }
  }

  &__event {
    &-time {
      display: flex;
      align-items: center;
      color: #dedede;
      margin-bottom: 5px;
    }

    &-priority {
      width: 10px;
      height: 10px;
      border-radius: 50%;
      background-color: #3454cf;
      margin-right: 5px;
    }

    &-title {
      font-size: 16px;
      margin-bottom: 5px;
    }

    &-snippet {
      color: #dedede;
    }

    padding: 10px;
    border-radius: 5px;
    border: 1px solid #dedede;
    cursor: pointer;

    &:not(:last-child) {
      margin-bottom: 10px;
    }
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

  &__years {
    padding-top: 50px;
    display: grid;
    grid-template-columns: repeat(7, 1fr);
    grid-template-rows: repeat(6, 60px);
  }

  &__year {
    text-align: center;
    cursor: pointer;
    border-radius: 5px;

    &:hover {
      background-color: #dedede;
    }
  }

  &__days {
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
