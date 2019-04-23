<template>
    
</template>

<script>
import Axios from 'axios';
import { Bar } from 'vue-chartjs'

export default {
    extends: Bar,
    mounted() {
        this.renderChart(data, options)
    },
    data() {
    return {
        labels: ["Januaray", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"],
      errorheaders: [
        { text: "id", value: "ID" , sortable: false},
        {
          text: "date",
          // align: "left",
          // sortable: false,
          value: "Date"
        },
        { text: "message", value: "Message" },
        { text: "target site", value: "TargetSite" },
        { text: "line of code", value: "LineOfCode" },
        { text: "user", value: "User" },
        { text: "request", value: "Request" }
      ],
      telemetryheaders: [
        { text: "id", value: "ID" , sortable: false},
        {
          text: "date",
          // align: "left",
          // sortable: false,
          value: "Date"
        },
        { text: "action", value: "Action" },
        { text: "ip Adress", value: "IPAdress" },
        { text: "location", value: "Location" }
      ],
      fetchUsers() {
      this.axios
        .get(this.$hostname + "UserManager/", {
          headers: { "Content-Type": "application/Json" }
        })
        .then(response => {
          this.users = response.data;
          console.log(response.data);
        })
        .catch(error => {
          console.log(error);
        });
    },
      errors: [],
      telemetries: [],
      response: ""
    };
      
  }
    
}
</script>

<style>

</style>
