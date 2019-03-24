<template>
  <div class="hello">
    <h1>{{ info }}</h1>
    <!-- <v-app id="inspire"> -->
      <v-container>
        <v-layout align-center justify-center row fill-height>
          <v-flex table>
            <v-subheader>Logs</v-subheader>
            <v-data-table 
              :headers="headers" 
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
                  @click="deleteItem(props.item.ID)"
                >
                  delete
                </v-icon>
                </td>
                <!-- <v-btn v-on:click="deleteUser(props.item.ID)">Delete</v-btn>
                <v-btn v-on:click="showEditModal(props.item)">Edit</v-btn> -->
              </template>
            </v-data-table>
          </v-flex>
        </v-layout>
      </v-container>
    <!-- </v-app> -->
  </div>
    
</template>

<script>
/*global console*/ /* eslint no-console: "off" */
// import EditModal from "@/components/EditModal.vue";
// import NewUserModal from "@/components/CreateUserModal.vue";
import axios from 'axios'

export default {
  // components: {
  //   EditModal,
  //   NewUserModal
  // },
  data() {
    return {
      headers: [
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
      errors: [],
      //isEditModalVisible: false,
      //isNewUserModalVisible: false,
      response: ""
    };
  },

  created() {
    this.fetchErrors();
    // this.$eventBus.$on("UpdateTable", () => {
    //   this.fetchErrors();
    //});
  },
  watch: {
    $route: "fetchErrors",
    response: "fetchErrors",
    users: "fetchErrors"
  },
  methods: {
    handleUpdate() {
      this.em;
    },
    // showEditModal(item) {
    //   this.isEditModalVisible = true;
    //   this.$eventBus.$emit('EditUser', item);

    // },
    // closeEditModal() {
    //   this.isEditModalVisible = false;
    // },
    // showNewUserModal() {
    //   this.isNewUserModalVisible = true;
    // },
    // closeNewUserModal() {
    //   this.isNewUserModalVisible = false;
    // },

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
    deleteUser(id) {
      this.axios
        .delete('http://localhost:59364/api/logging/deleteerror/' + id)
        .then(response => {
          this.response = response;
        });
    }
  }
};
</script>