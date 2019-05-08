<script>
  //Importing Bar class from the vue-chartjs wrapper
  import { Bar } from 'vue-chartjs'
  import axios from 'axios'
  //Exporting this so it can be used in other components
  export default {
    extends: Bar,
    data () {
      return {
        datacollection: {
          //Data to be represented on x-axis
          labels: ['December', 'January', 'February', 'March', 'April', 'May'],
          datasets: [
            {
              label: '# of Average Successful',
              backgroundColor: "rgba(75, 192, 192, 0.6)",
              pointBackgroundColor: 'white',
              borderWidth: 1,
              pointBorderColor: '#249EBF',
              //Data to be represented on y-axis
              data: [70, 50, 20, 10, 30, 100]
            }
          ]
        },
        //Chart.js options that controls the appearance of the chart
        options: {
          scales: {
            yAxes: [{
              ticks: {
                beginAtZero: true
              },
              gridLines: {
                display: true
              }
            }],
            xAxes: [ {
              gridLines: {
                display: false
              }
            }]
          },
          legend: {
            display: true
          },
          responsive: true,
          maintainAspectRatio: false
        }
      }
    },
    fetchData() {
      this.axios
        .get(`${this.$hostname}UAD/sLogin`, {
          headers: { "Content-Type": "application/Json" }
        })
        .then(response => {
          this.datacollection.data = response.data;
          console.log(response.data);
        })
        .catch(error => {
          console.log(error);
        });
    },
    fetchLabel() {
      this.axios
        .get(`${this.$hostname}UAD/recentMonths`, {
          headers: { "Content-Type": "application/Json" }
        })
        .then(response => {
          this.datacollection.labels = response.data;
          console.log(response.data);
        })
        .catch(error => {
          console.log(error);
        });
    },
    mounted () {
      //renderChart function renders the chart with the datacollection and options object.
      this.renderChart(this.datacollection, this.options)
    }
  }
</script>