<template>
  <div class="hello">
    <!-- <v-app id="inspire"> -->
      <v-container>
        <v-layout align-center justify-center row fill-height>
          <v-flex table>
            <v-subheader>Errors</v-subheader>
            <v-data-table 
              :headers="errorheaders" 
              :items="errors" 
              :dark = true
              class="elevation-1"
              >
              <template slot="items" slot-scope="props">
                <td class="text-xs-left">{{ props.item.ID }}</td>
                <td class="text-xs-left">{{ props.item.Date }}</td>
                <td class="text-xs-left">{{ props.item.Message }}</td>
                <td class="text-xs-left">{{ props.item.TargetSite }}</td>
                <td class="text-xs-left">{{ props.item.LineOfCode }}</td>
                <td class="text-xs-left">{{ props.item.BS }}</td>
                <v-icon
                  small
                  @click="deleteError(props.item.ID)"
                >
                  delete
                </v-icon>
                </td>
              </template>
            </v-data-table>
          </v-flex>
        </v-layout>
      </v-container>
      <v-container>
        <v-layout align-center justify-center row fill-height>
          <v-flex table>
            <v-subheader>Telemetry</v-subheader>
            <v-data-table 
              :headers="telemetryheaders" 
              :items="telemetries" 
              :dark = true
              class="elevation-2"
              >
              <template slot="items" slot-scope="props">
                <td class="text-xs-left">{{ props.item.ID }}</td>
                <td class="text-xs-left">{{ props.item.Date }}</td>
                <td class="text-xs-left">{{ props.item.Action }}</td>
                <td class="text-xs-left">{{ props.item.IPAddress }}</td>
                <td class="text-xs-left">{{ props.item.Location }}</td>
                <v-icon
                  small
                  @click="deleteTelemetry(props.item.ID)"
                >
                  delete
                </v-icon>
                </td>
              </template>
            </v-data-table>
          </v-flex>
        </v-layout>
      </v-container>
    <!-- </v-app> -->
  </div>
    
</template>

<script>
import axios from 'axios'

export default {
  data() {
    return {
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
      errors: [],
      telemetries: [],
      response: ""
    };
      
  },

  created() {
    this.fetchErrors();
    this.fetchTelemetry();
  },
  watch: {
    $route: "fetchErrors",
    response: "fetchErrors",
    //errors: "fetchErrors",
    $route: "fetchTelemetry",
    response: "fetchTelemetry",
    //telemetries: "fetchTelemetry"
  },
  methods: {
    handleUpdate() {
      this.em;
    },

    fetchErrors() {
      this.axios
        .get('http://localhost:59364/api/logging/geterrors', {
          headers: { "Content-Type": "application/Json" }
        })
        .then(response => {
          this.errors = response.data;
          console.log(response.data);
        })
        .catch(error => {
          console.log(error);
        });
    },
    deleteError(id) {
      this.axios
        .delete('http://localhost:59364/api/logging/deleteerror/?id=' + id)
        .then(response => {
          this.response = response;
        });
    },
    fetchTelemetry() {
      this.axios
        .get('http://localhost:59364/api/logging/gettelemetries', {
          headers: { "Content-Type": "application/Json" }
        })
        .then(response => {
          this.telemetries = response.data;
          console.log(response.data);
        })
        .catch(error => {
          console.log(error);
        });
    },
    deleteTelemetry(id) {
      this.axios
        .delete('http://localhost:59364/api/logging/deletetelemetry/?id=' + id)
        .then(response => {
          this.response = response;
        });
    }
  }
};
</script>