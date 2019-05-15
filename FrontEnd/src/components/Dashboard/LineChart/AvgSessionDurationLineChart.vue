<script>
import {Line} from 'vue-chartjs'

export default {
    extends: Line,
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
              label: '# of Average Session Duration (Minutes)',
              backgroundColor: "rgba(54, 162, 235, 0.6)",
              pointBackgroundColor: 'white',
              borderWidth: 1,
              pointBorderColor: '#249EBF',
              //Data to be represented on y-axis
              data: this.chartData
            }
          ]
      }, this.options)
    }
}
</script>
