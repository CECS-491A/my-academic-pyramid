<template>
  <section class="container">
    <h1>Demo examples of vue-charts</h1>
    <div class="columns">
      <div class="column">
        <h3>Line Chart</h3>
        <chartjs-line></chartjs-line>
      </div>
      <div class="column">
        <h3>Bar Chart</h3>
        <bar-chart></bar-chart>
      </div>
    </div>
    <div class="columns">
      <div class="column">
        <h3>Radar Chart</h3>
        <!--Radar Chart Example-->
      </div>
      <div class="column">
        <h3>Data Binding Line Chart</h3>
        <!--Data Binding Line Chart Example-->
      </div>
    </div>
  </section>
</template>

<script>
  import LineChart from '@/components/Dashboard/ExampleKevin'
  import AVG from '@/components/Dashboard/AvgSuccessfulLoginBarChart'
  import Re from '@/components/Dashboard/Reactive'

  export default {
    name: 'VueChartJS',
    components: {
      LineChart,
      AVG,
      Re
    }
  }
</script>

<style>

</style>