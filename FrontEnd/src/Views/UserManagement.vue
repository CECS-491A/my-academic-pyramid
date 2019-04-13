<template>
  <div id="app">
    <v-app id="inspire">
      <v-container>
        <v-layout align-center justify-center row>
          <v-card>
            <v-toolbar color="cyan" dark>
              <v-toolbar-title>USER MANAGEMENT</v-toolbar-title>
            </v-toolbar>

            <v-flex table>
              <v-subheader>
                <v-text-field
                  v-model="search"
                  append-icon="search"
                  label="Search"
                  single-line
                  hide-details
                ></v-text-field>
              </v-subheader>

              <v-data-table
                :headers="headers"
                :items="users"
                :search="search"
                :expand="expand"
                :pagination.sync="pagination"
                item-key="UserName"
                class="elevation-1"
                loading
              >
                <template v-slot:items="props">
                  <tr @click="props.expanded = !props.expanded">
                    <td class="text-xs-left">{{ props.item.Id }}</td>
                    <td class="text-xs-left">{{ props.item.UserName }}</td>
                    <td class="text-xs-left">{{ props.item.FirstName }}</td>
                    <td class="text-xs-left">{{ props.item.LastName }}</td>
                    <td class="text-xs-left">{{ props.item.DateOfBirth }}</td>
                    <td class="text-xs-left">{{ props.item.Catergory }}</td>
                    <v-btn v-on:click="deleteUser(props.item.Id)">Delete</v-btn>
                    <v-btn v-on:click="showEditModal(props.item)">Edit</v-btn>
                  </tr>
                </template>
                <template v-slot:expand="props">
                  <v-card flat>
                    <header>Created Date</header>
                    <v-card-text>{{props.item.CreatedAt}}</v-card-text>
                  </v-card>
                </template>
                <v-alert
                  v-slot:no-results
                  :value="true"
                  color="error"
                  icon="warning"
                >Your search for "{{ search }}" found no results.</v-alert>
              </v-data-table>
            </v-flex>
            <v-flex>
              <v-btn v-on:click="showNewUserDiallog">Add</v-btn>
            </v-flex>
          </v-card>
        </v-layout>
      </v-container>
    </v-app>
    <CreateUserDiallog></CreateUserDiallog>
    <EditUserDialog></EditUserDialog>
  </div>
</template>




<script>
/*global console*/ /* eslint no-console: "off" */
import CreateUserDiallog from "@/components/CreateUserDiallog.vue";
import EditUserDialog from "@/components/EditUserDialog.vue";
export default {
  components: {
    CreateUserDiallog,
    EditUserDialog
  },
  data() {
    return {
      search: "",
      headers: [
        { text: "id", value: "Id" },
        {
          text: "UserName",
          align: "left",
          sortable: false,
          value: "UserName"
        },
        { text: "First Name", value: "Firstname" },
        { text: "Last Name", value: "LastName" },
        { text: "Date Of Birth", value: "DateOfBirth" },
        { text: "Catergory", value: "Catergory" }
      ],
      users: [],
      isCreateUserDiallogVisible: false,
      response: "",
      expand: false,
      pagination: {
        rowsPerPage: 25
      }
    };
  },

  created() {
    this.fetchUsers();
    this.$eventBus.$on("UpdateTable", () => {
      this.fetchUsers();
    });
  },
  watch: {
    $route: "fetchUsers",
    response: "fetchUsers",
    users: "fetchUsers"
  },
  methods: {
    handleUpdate() {
      this.em;
    },
    showEditModal(item) {
      this.$eventBus.$emit("EditUser", item);
    },
    showNewUserDiallog() {
      this.$eventBus.$emit("ShowDialog");
    },

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
    deleteUser(id) {
      this.axios.delete(this.$hostname +"UserManager/" + id).then(response => {
        this.response = response;
      });
    }
  }
};
</script>