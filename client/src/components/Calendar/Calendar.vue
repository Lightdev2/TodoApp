<script>
import Arrow from '@/assets/icons/right-arrow.vue';
import {
  computed,
  defineProps,
  ref,
  defineEmits,
  watch,
} from 'vue';
import { dayNames, monthNames } from '../utlis/lang';

const dayInputType = 'day';
const monthInputType = 'month';
const yearInputType = 'year';
const RIGHT = 'right';
const LEFT = 'left';
const RUS = 'ru';
const ENG = 'eng';
export default {
  name: 'Calendar',
};
</script>

<script setup>
const inputType = ref(dayInputType);
const calendar = ref(null);
const props = defineProps({
  isFirstDayMonday: {
    default: true,
    required: false,
    type: Boolean,
  },
  date: {
    required: true,
  },
  lang: {
    type: String,
    default: RUS,
  },
  minYear: {
    required: false,
    default: 1,
    validator: (value) => value > 0,
  },
  maxYear: {
    required: false,
  },
});
const emits = defineEmits(['update:date']);
const currentDate = ref(props.date);
const yearsRange = ref({
  start: '',
  end: '',
});
const yearVal = computed(() => currentDate.value.getFullYear());
const dateVal = computed(() => currentDate.value.getDate());
const monthVal = computed(() => currentDate.value.getMonth());
const daysPositions = computed(() => {
  if (props.isFirstDayMonday === true) {
    return [0, 1, 2, 3, 4, 5, 6];
  }
  return [6, 0, 1, 2, 3, 4, 5];
});
const dayNamesLocale = computed(() => {
  switch (props.lang) {
    case RUS:
      return dayNames.ru;
    case ENG:
      return dayNames.eng;
    default:
      return dayNames.eng;
  }
});
const formatedDays = computed(() => {
  const daysToShow = [];
  for (let i = 0; i < daysPositions.value.length; i += 1) {
    daysToShow.push(dayNamesLocale.value[daysPositions.value[i]].slice(0, 3));
  }
  return daysToShow;
});
const localizedMonths = computed(() => {
  switch (props.lang) {
    case RUS:
      return monthNames.ru;
    case ENG:
      return monthNames.eng;
    default:
      return monthNames.eng;
  }
});
const formatedMonths = computed(() => Array.from(Object.values(localizedMonths.value))
  .map((el) => el.slice(0, 3)));
const emitDateChange = () => {
  emits('update:date', currentDate.value);
};

watch(currentDate, () => {
  const year = yearVal.value;
  const { minYear } = props;
  const { maxYear } = props;
  if (year > maxYear) {
    setYear(maxYear);
  }
  if (year < minYear) {
    setYear(minYear);
  }
  emitDateChange();
});
const setYear = (_year) => {
  const day = dateVal.value;
  const month = monthVal.value;
  currentDate.value = new Date(_year, month, day);
};
//  меняет режим ввода
const changeInputType = () => {
  switch (inputType.value) {
    case dayInputType:
      inputType.value = monthInputType;
      calendar.value.focus();
      break;
    case monthInputType:
      inputType.value = yearInputType;
      calendar.value.focus();
      break;
    case yearInputType:
      inputType.value = dayInputType;
      calendar.value.focus();
      break;
    default: {
      break;
    }
  }
};
//  возвращает массив дней месяца для отображения
const getDays = () => {
  const currentYear = yearVal.value;
  const currentMonth = monthVal.value;
  const monthLength = new Date(currentYear, currentMonth + 1, 0).getDate();
  const month = [];
  // заполняем массив дня, днями принадлежащими ему
  for (let day = 1; day < monthLength + 1; day += 1) {
    month.push({
      day,
      currentMonthDay: true,
    });
  }
  //  первый день этого месяца 1-пн, 2-вт и.т.д
  let firstDay = new Date(currentYear, currentMonth, month[0].day).getDay();
  //  последний день прошлого месяца
  let lastMonthDate = new Date(currentYear, currentMonth, 0).getDate();
  if (firstDay !== 1) {
    if (firstDay === 0) {
      firstDay = 6;
    }
    const start = props.isFirstDayMonday === false ? 0 : 1;
    for (let i = start; i < firstDay; i += 1) {
      month.unshift({
        day: lastMonthDate,
        last: true,
      });
      lastMonthDate -= 1;
    }
  }
  if (props.isFirstDayMonday === false && firstDay === 1) {
    month.unshift({
      day: lastMonthDate,
      last: true,
    });
  }
  //  день следующего месяца для хвоста
  let nextDay = 0;
  for (let i = month.length; i < 42; i += 1) {
    month.push({
      day: nextDay += 1,
      next: true,
    });
    nextDay += 1;
  }
  return month;
};
const yearsRangeHeader = computed(() => {
  const rangeVal = yearsRange.value;
  return `${rangeVal.start} - ${rangeVal.end}`;
});
const setDay = (day, _month) => {
  const year = yearVal.value;
  const month = monthVal.value;
  const { maxYear } = props;
  const { minYear } = props;
  const date = new Date(year, _month || month, day);
  if (date.getFullYear() > maxYear || date.getFullYear() < minYear) {
    return;
  }
  currentDate.value = date;
};
const setMonth = (month) => {
  const year = yearVal.value;
  const day = dateVal.value;
  const monthLength = new Date(yearVal.value, month + 1, 0).getDate();
  let date = {};
  if (monthLength < dateVal.value) {
    date = new Date(yearVal.value, month, monthLength);
  } else {
    date = new Date(yearVal.value, month, dateVal.value);
  }
  currentDate.value = date;
};

const toggleDate = (direction) => {
  let nextDay = dateVal.value;
  let nextMonth = monthVal.value;
  let nextYear = yearVal.value;
  switch (direction) {
    case RIGHT:
      nextDay += 1;
      nextMonth += 1;
      nextYear += 10;
      break;
    case LEFT:
      nextDay -= 1;
      nextMonth -= 1;
      nextYear -= 10;
      break;
    default: {
      break;
    }
  }
  switch (inputType.value) {
    case dayInputType:
      setDay(nextDay);
      break;
    case monthInputType:
      setMonth(nextMonth);
      break;
    case yearInputType:
      setYear(nextYear);
      break;
    default: {
      break;
    }
  }
};

const dayInputHeader = computed(() => `${localizedMonths.value[monthVal.value]} ${yearVal.value}`);
//  выбор месяца и переход на выбор дня
const selectMonth = (idx) => {
  setMonth(idx);
  inputType.value = dayInputType;
  calendar.value.focus();
};
//  выбор года и переход на выбор дня
const selectYear = (year) => {
  if (year > props.maxYear || year < props.minYear) {
    return;
  }
  setYear(year);
  inputType.value = dayInputType;
  calendar.value.focus();
};
//  выбор дня
const selectDay = (day) => {
  //  если день из следующего месяца
  if (day.next === true) {
    setDay(day.day, monthVal.value + 1);
  }
  //  если день из предидущего месяца
  if (day.last === true) {
    setDay(day.day, monthVal.value - 1);
  }
  //  если день текущего
  if (day.currentMonthDay === true) {
    setDay(day.day);
  }
  calendar.value.focus();
};

/**
 * возвращает диапазон лет для отображения
 * устанавливает начало и конце для отображения
 */
const getYears = () => {
  const range = [];
  const lastNumOfYear = +yearVal.value % 10;
  const rangeStart = Math.floor(+yearVal.value / 10) * 10 - 1;
  const rangeEnd = rangeStart + 11;
  for (let year = rangeStart; year <= rangeEnd; year += 1) {
    range.push(year);
  }
  yearsRange.value.end = rangeEnd;
  yearsRange.value.start = rangeStart;
  return range;
};
const isDisabledYear = (year) => year > props.maxYear || year < props.minYear;
const isDisabledDay = (day) => day?.currentMonthDay !== true;
const isActiveYear = (year) => +year === +yearVal.value;
const isActiveMonth = (month) => month === monthVal.value;
const isActiveDay = (day) => day?.day === dateVal.value && day?.currentMonthDay === true;
const isDayInputType = computed(() => inputType.value === dayInputType);
</script>

<template>
  <div
    class="calendar"
    ref="calendar"
    tabindex="1"
    @keydown.left="toggleDate(LEFT)"
    @keydown.right="toggleDate(RIGHT)"
    @keydown.up="changeInputType"
    @keydown.down="changeInputType"
  >
    <div class="calendar__header">
      <div @click="toggleDate(LEFT)" class="calendar__header-control">
        <Arrow class="arrow arrow__left"/>
      </div>
      <div @click="changeInputType" class="calendar__header-title">
        <span>{{
            inputType === yearInputType ? yearsRangeHeader : dayInputHeader
          }}</span>
      </div>
      <div @click="toggleDate(RIGHT)" class="calendar__header-control">
        <Arrow class="arrow"/>
      </div>
    </div>
    <div class="calendar__header-days" v-if="isDayInputType">
      <span
        class="calendar__header-day"
        v-for="day in formatedDays"
        :key="day"
      >{{ day }}</span
      >
    </div>
    <div class="calendar__days" v-show="isDayInputType">
      <div
        class="calendar__day"
        v-for="(day, idx) in getDays()"
        :key="idx"
        :class="{
          'calendar__day--disabled': isDisabledDay(day),
          'calendar__day--active': isActiveDay(day),
        }"
        tabindex="1"
        @click="selectDay(day)"
        @keydown.enter="selectDay(day)"
      >
        {{ day.day }}
      </div>
    </div>
    <div
      class="calendar__months"
      v-show="inputType == monthInputType"
      tabindex="1"
    >
      <div
        class="calendar__month"
        v-for="(month, idx) in formatedMonths"
        :key="month"
        tabindex="1"
        @click="selectMonth(idx)"
        :class="{ 'calendar__month--active': isActiveMonth(idx) }"
        @keydown.enter="selectMonth(idx)"
      >
        {{ month }}
      </div>
    </div>

    <div class="calendar__years" v-show="inputType == yearInputType">
      <div
        class="calendar__year"
        v-for="year in getYears()"
        :key="year"
        tabindex="1"
        :class="{
          'calendar__year--active': isActiveYear(year),
          'calendar__year--disabled': isDisabledYear(year),
        }"
        @click="selectYear(year)"
        @keydown.enter="selectYear(year)"
      >
        {{ year }}
      </div>
    </div>
  </div>
</template>

<style lang="scss" scoped>
.calendar {
  max-width: 220px;
  background: var(--grey);
  box-shadow: 0 0 3px 1px var(--grey);
  height: auto;
  border: 1px solid #dedede;
  border-radius: 5px;
  padding: 3px;
  z-index: 999;

  &__header {
    display: flex;
    justify-content: space-between;
  }

  &__header-control {
    user-select: none;
    width: 20px;
    height: 20px;
  }

  &__header-title {
    user-select: none;
    cursor: pointer;
  }

  &__header-days {
    margin-top: 10px;
    display: flex;
    justify-content: space-between;
    margin-bottom: 10px;
    padding-bottom: 5px;
    border-bottom: 2px solid #dedede;
  }

  &__header-day {
    user-select: none;
    font-weight: 500;
    width: 24px;
    height: 24px;
    font-size: 0.875rem;
  }

  &__days {
    display: grid;
    gap: 5px;
    justify-content: space-around;
    grid-template-columns: repeat(7, 25px);
    grid-template-rows: repeat(6, 25px);
  }

  &__day {
    width: 24px;
    display: flex;
    align-items: center;
    justify-content: center;
    border-radius: 5px;
    height: 24px;
    font-size: 14px;
    cursor: pointer;

    &--active {
      background: var(--hoverPrimary);
    }

    &--disabled {
      opacity: 0.3;
    }

    &:focus,
    &:active {
      outline: none;
      border: 1px solid var(--hoverPrimary);
    }

    &:hover {
      background-color: var(--hoverPrimary);
    }
  }

  &__months {
    display: grid;
    grid-template-columns: repeat(3, auto);
    margin-top: 10px;
  }

  &__month {
    display: flex;
    align-items: center;
    justify-content: center;
    width: 60px;
    height: 60px;
    cursor: pointer;

    &:hover {
      background-color: var(--hoverPrimary);
      border-radius: 5px;
    }

    &--active {
      border-radius: 5px;
      background: var(--hoverPrimary);
    }
  }

  &__years {
    margin-top: 15px;
    display: grid;
    grid-template-columns: repeat(4, auto);
    gap: 5px;
  }

  &__year {
    width: 50px;
    height: 50px;
    display: flex;
    cursor: pointer;
    justify-content: center;
    align-items: center;

    &:hover {
      background-color: var(--hoverPrimary);
      border-radius: 5px;
    }

    &--active {
      background: var(--hoverPrimary);
      border-radius: 5px;
    }

    &--disabled {
      border-radius: 5px;
      opacity: 0.3;
    }
  }
}

.arrow {
  width: 12px;
  height: 12px;
  cursor: pointer;

  &__left {
    transform: rotate(180deg);
  }
}
</style>
