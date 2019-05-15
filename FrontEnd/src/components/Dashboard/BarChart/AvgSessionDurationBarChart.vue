<script>
  //Importing Bar class from the vue-chartjs wrapper
  import { Bar } from 'vue-chartjs'
  
  //Exporting this so it can be used in other components
  export default {
    extends: Bar,
    props: {
      chartData: {
        type: Array,
      },
      chartLabels: {
        type: Array,
      }
    },
    data () {
      return {
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
    mounted () {
      //renderChart function renders the chart with the datacollection and options object.
      this.renderChart({
        labels: this.chartLabels,
          datasets: [
            {
              label: '# of Average Successful Login',
              backgroundColor: "rgba(54, 162, 235, 0.6)",
              pointBackgroundColor: 'green',
              borderWidth: 1,
              pointBorderColor: '#249EBF',
              data: this.chartData
            }
          ]
      }, this.options)
    }
  }
</script>