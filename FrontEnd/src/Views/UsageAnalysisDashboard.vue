<template>
  <section class="dashboard">
    <h1>Usage Analysis Dashboard</h1>
    <div class="Chart">
      <div class="column">
        <h3>Average Successful Login</h3>
        <avg-successful-login-bar-chart v-if="loaded"
        :chart-data="data"
        :chart-labels="label"></avg-successful-login-bar-chart>
      </div>
      <div class="column">
        <h3>Average Session Duration</h3>
        <avg-session-duration-bar-chart></avg-session-duration-bar-chart>
      </div>
      <div class="column">
        <h3>Failed login attempts vs Successful login attempts</h3>
        <failed-vs-successful-login-bar-chart></failed-vs-successful-login-bar-chart>
      </div>
      <div class="column">
        <h3>Top 5 average time spent per page</h3>
        <top-5-avg-time-spent-per-page-bar-chart></top-5-avg-time-spent-per-page-bar-chart>
      </div>
      <div class="column">
        <h3>Top 5 most used feature</h3>
        <top-5-most-used-feature-bar-chart></top-5-most-used-feature-bar-chart>
      </div>
      <div class="column">
        <h3>Timeline of average session duration</h3>
        <avg-session-duration-line-chart></avg-session-duration-line-chart>
      </div>
      <div class="column">
        <h3>Timeline of number of logged in users per month</h3>
        <num-of-logged-in-users-line-chart></num-of-logged-in-users-line-chart>
      </div>
    </div>
  </section>
</template>

<script>
  import AvgSuccessfulLoginBarChart from '@/components/Dashboard/BarChart/AvgSuccessfulLoginBarChart'
  import AvgSessionDurationBarChart from '@/components/Dashboard/BarChart/AvgSessionDurationBarChart'
  import FailedVsSuccessfulLoginBarChart from '@/components/Dashboard/BarChart/FailedVsSuccessfulLoginBarChart'
  import Top5AvgTimeSpentPerPageBarChart from '@/components/Dashboard/BarChart/Top5AvgTimeSpentPerPageBarChart'
  import Top5MostUsedFeatureBarChart from '@/components/Dashboard/BarChart/Top5MostUsedFeatureBarChart'

  import AvgSessionDurationLineChart from '@/components/Dashboard/LineChart/AvgSessionDurationLineChart'
  import NumOfLoggedInUsersLineChart from '@/components/Dashboard/LineChart/NumOfLoggedInUsersLineChart'
  
  import axios from 'axios'

  export default {
    name: 'VueChartJS',
    components: 
    {
      // Bar Chart 
      AvgSuccessfulLoginBarChart,
      AvgSessionDurationBarChart,
      FailedVsSuccessfulLoginBarChart,
      Top5AvgTimeSpentPerPageBarChart,
      Top5MostUsedFeatureBarChart,
      // Line Chart
      AvgSessionDurationLineChart,
      NumOfLoggedInUsersLineChart
    },
    data () {
      return {
        loaded: false,
        data: [],
        label: []
      }
    },
    methods:
    {
      fetchData() {
        this.axios
          .get(`${this.$hostname}UAD/AvgSuccessfulLogin`, {
            headers: { "Content-Type": "application/Json" }
          })
          .then(response => {
            this.data = response.data.data;
            this.label = response.data.labels;
            this.loaded = true;
            console.log(response.data);
          })
          .catch(error => {
            console.log(error);
          });
      }
    },
    async mounted () {
      this.fetchData()
  }
}
</script>

<style>
.dashboard h1 {
  font-size: 50px;
  color:#000000;
  margin: auto;
  margin-left: 20px;
  padding: 15px 0;
}
.Chart {
  background:white;
  border-radius: 15px;
  box-shadow: 0px 2px 15px rgba(25, 25, 25, 0.27);
  margin:  25px 0;
  
}

.Chart h3 {
  margin-top: 0;
  margin-left: 20px;
  padding: 15px 0;
  font-size:20px;
  color:  rgba(255, 0,0, 0.5);
  border-bottom: 1px solid #323d54;
}
</style>