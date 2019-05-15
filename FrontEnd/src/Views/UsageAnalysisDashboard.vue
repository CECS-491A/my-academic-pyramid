<template>
  <section class="dashboard">
    <h1>Usage Analysis Dashboard</h1>
    <div class="Chart">
      <div class="column">
        <h3>Average Successful Login</h3>
        <avg-successful-login-bar-chart v-if="AvgSuccessfulLoginBarChartLoaded"
        :chart-data="AvgSuccessfulLoginBarChartData"
        :chart-labels="AvgSuccessfulLoginBarChartLabel"></avg-successful-login-bar-chart>
      </div>
      <div class="column">
        <h3>Average Session Duration</h3>
        <avg-session-duration-bar-chart v-if="AvgSessionDurationBarChartLoaded"
        :chart-data="AvgSessionDurationBarChartData"
        :chart-labels="AvgSessionDurationBarChartLabel"></avg-session-duration-bar-chart>
      </div>
      <div class="column">
        <h3>Failed login attempts vs Successful login attempts</h3>
        <failed-vs-successful-login-bar-chart v-if="FailedVsSuccessfulLoginBarChartLoaded"
        :chart-data="FailedVsSuccessfulLoginBarChartData"
        :chart-labels="FailedVsSuccessfulLoginBarChartLabel"></failed-vs-successful-login-bar-chart>
      </div>
      <div class="column">
        <h3>Top 5 average time spent per page</h3>
        <top-5-avg-time-spent-per-page-bar-chart v-if="Top5AvgTimeSpentPerPageBarChartLoaded"
        :chart-data="Top5AvgTimeSpentPerPageBarChartData"
        :chart-labels="Top5AvgTimeSpentPerPageBarChartLabel"></top-5-avg-time-spent-per-page-bar-chart>
      </div>
      <div class="column">
        <h3>Top 5 most used feature</h3>
        <top-5-most-used-feature-bar-chart v-if="Top5MostUsedFeatureBarChartLoaded"
        :chart-data="Top5MostUsedFeatureBarChartData"
        :chart-labels="Top5MostUsedFeatureBarChartLabel"></top-5-most-used-feature-bar-chart>
      </div>
      <div class="column">
        <h3>Timeline of average session duration</h3>
        <avg-session-duration-line-chart v-if="AvgSessionDurationBarChartLoaded"
        :chart-data="AvgSessionDurationBarChartData"
        :chart-labels="AvgSessionDurationBarChartLabel"></avg-session-duration-line-chart>
      </div>
      <div class="column">
        <h3>Timeline of number of logged in users per month</h3>
        <num-of-logged-in-users-line-chart v-if="NumOfLoggedInUsersLineChartLoaded"
        :chart-data="NumOfLoggedInUsersLineChartData"
        :chart-labels="NumOfLoggedInUsersLineChartLabel"></num-of-logged-in-users-line-chart>
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
        AvgSuccessfulLoginBarChartLoaded: false,
        AvgSuccessfulLoginBarChartData: [],
        AvgSuccessfulLoginBarChartLabel: [],

        AvgSessionDurationBarChartLoaded: false,
        AvgSessionDurationBarChartData: [],
        AvgSessionDurationBarChartLabel: [],

        FailedVsSuccessfulLoginBarChartLoaded: false,
        FailedVsSuccessfulLoginBarChartData: [],
        FailedVsSuccessfulLoginBarChartLabel: [],

        Top5AvgTimeSpentPerPageBarChartLoaded: false,
        Top5AvgTimeSpentPerPageBarChartData: [],
        Top5AvgTimeSpentPerPageBarChartLabel: [],

        Top5MostUsedFeatureBarChartLoaded: false,
        Top5MostUsedFeatureBarChartData: [],
        Top5MostUsedFeatureBarChartLabel: [],

        NumOfLoggedInUsersLineChartLoaded: false,
        NumOfLoggedInUsersLineChartData: [],
        NumOfLoggedInUsersLineChartLabel: []
      }
    },
    methods:
    {
      fetchAvgLoginData() {
        this.axios
          .get(`${this.$hostname}UAD/AvgSuccessfulLogin`, {
            headers: { "Content-Type": "application/Json" }
          })
          .then(response => {
            this.AvgSuccessfulLoginBarChartData = response.data.data;
            this.AvgSuccessfulLoginBarChartLabel = response.data.labels;
            this.AvgSuccessfulLoginBarChartLoaded = true;
          })
          .catch(error => {
            console.log(error);
          });
      },
      
      fetchAvgSessionDurationData() {
        this.axios
          .get(`${this.$hostname}UAD/AvgSessionDuration`, {
            headers: { "Content-Type": "application/Json" }
          })
          .then(response => {
            this.AvgSessionDurationBarChartData = response.data.data;
            this.AvgSessionDurationBarChartLabel = response.data.labels;
            this.AvgSessionDurationBarChartLoaded = true;
          })
          .catch(error => {
            console.log(error);
          });
      },
      fetchTotalFailedSuccessfulLoginData() {
        this.axios
          .get(`${this.$hostname}UAD/TotalFailedSuccessfulLogin`, {
            headers: { "Content-Type": "application/Json" }
          })
          .then(response => {
            this.FailedVsSuccessfulLoginBarChartData = response.data.data;
            this.FailedVsSuccessfulLoginBarChartLabel = response.data.labels;
            this.FailedVsSuccessfulLoginBarChartLoaded = true;
          })
          .catch(error => {
            console.log(error);
          });
      },
      fetchMostVisitedPageData() {
        this.axios
          .get(`${this.$hostname}UAD/MostVisitedPage`, {
            headers: { "Content-Type": "application/Json" }
          })
          .then(response => {
            this.Top5AvgTimeSpentPerPageBarChartData = response.data.data;
            this.Top5AvgTimeSpentPerPageBarChartLabel = response.data.labels;
            this.Top5AvgTimeSpentPerPageBarChartLoaded = true;
          })
          .catch(error => {
            console.log(error);
          });
      },
      fetchMostUsedFeatureData() {
        this.axios
          .get(`${this.$hostname}UAD/MostUsedFeature`, {
            headers: { "Content-Type": "application/Json" }
          })
          .then(response => {
            this.Top5MostUsedFeatureBarChartData = response.data.data;
            this.Top5MostUsedFeatureBarChartLabel = response.data.labels;
            this.Top5MostUsedFeatureBarChartLoaded = true;
          })
          .catch(error => {
            console.log(error);
          });
      },
      fetchUniqueLoggedInUserData() {
        this.axios
          .get(`${this.$hostname}UAD/UniqueLoggedInUser`, {
            headers: { "Content-Type": "application/Json" }
          })
          .then(response => {
            this.NumOfLoggedInUsersLineChartData = response.data.data;
            this.NumOfLoggedInUsersLineChartLabel = response.data.labels;
            this.NumOfLoggedInUsersLineChartLoaded = true;
          })
          .catch(error => {
            console.log(error);
          });
      }
    },
    async mounted () {
      this.fetchAvgLoginData()
      this.fetchAvgSessionDurationData()
      this.fetchTotalFailedSuccessfulLoginData()
      this.fetchMostVisitedPageData()
      this.fetchMostUsedFeatureData()
      this.fetchUniqueLoggedInUserData()
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