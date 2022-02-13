<template>
  <div>
    Overview page
    <div>
      <h2>
        Total todos progress
        <apexchart width="50%" type="line" :options="options" :series="series"></apexchart>
      </h2>
    </div>
  </div>
</template>

<script>
export default {
  name: 'OverviewPage',
};
</script>

<script setup>
const date = new Date();
const days = new Date(date.getFullYear(), date.getMonth() + 1, 0).getDate();
const temp = [];
for (let i = 1; i <= days; i += 1) {
  temp.push(i);
}
const options = {
  chart: {
    id: 'vuechart-example',
  },
  xaxis: {
    categories: temp,
  },
};

const series = [{
  name: 'completed todos',
  data: temp,
}];
</script>

<style scoped>

</style>
