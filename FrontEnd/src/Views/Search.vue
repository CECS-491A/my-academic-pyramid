<template>
<v-container fluid>
    <v-layout row>
      <v-flex
        shrink
        pa-1
      >
        <v-select
            v-model="category"
            :items="categories"
            label="Category"
        ></v-select>
      </v-flex>
      <v-flex
        shrink
        pa-1
      >
        <v-select
            v-model="department"
            :items="data.Departments"
            label="Department"
        ></v-select>
      </v-flex>
      <v-flex
        grow
        pa-1
      >
        <v-text-field
            class="searchBar"
            v-model="SearchInput" 
            label="Search..." 
            append-icon="search" 
            @click:append="search" 
            hide-details>
        </v-text-field>
      </v-flex>
    </v-layout>

    <div>
        <v-toolbar flat color="white">
        <v-toolbar-title>Search Results</v-toolbar-title>
        </v-toolbar>
        <v-data-table
        :headers="tableHeaders"
        :items="data.People"
        :expand="expand"
        item-key="Id"
        >
        <v-progress-linear v-slot:progress color="blue" indeterminate :active="loading"></v-progress-linear>
        <template v-slot:items="props">
            <tr @click="props.expanded = !props.expanded">
            <td>{{ props.item.FirstName }}</td>
            <td>{{ props.item.MiddleName }}</td>
            <td>{{ props.item.LastName }}</td>
            <td>{{ props.item.Department }}</td>
            </tr>
        </template>
        <template v-slot:expand="props">
            <v-card flat>
            <v-card-text>Peek-a-boo!</v-card-text>
            </v-card>
        </template>
        <template v-slot:no-data>
            <v-alert :value="errorMessage" color="error" icon="warning">
                {{ errorMessage }}
            </v-alert>
        </template>
        </v-data-table>
    </div>
  </v-container>

    
</template>

<script>
import axios from 'axios'

export default {
    name: "Search",
    data () {
        return {
            openSearch: '',
            SearchInput: '',
            category: '',
            categories: ['Students', 'Teachers', 'Forum Posts'],
            department: '',
            expand: false,
            tableHeaders: [
                { text: 'First Name', value: 'FirstName' },
                { text: 'Middle Name', value: 'MiddleName' },
                { text: 'Last Name', value: 'LastName' },
                { text: 'Department', value: 'Department' }
            ],
            data: {
                People: [],
                Message: '',
                Departments: []
            },
            userEmail: '',
            errorMessage: '',
            loading: false,
            url: ''
        }
  },
  methods: {
      search: function() {
          this.loading = true;
          this.userEmail = "krystalleon10@gmail.com";
          this.errorMessage = "";
          if (this.SearchInput.length === 0){
              this.errorMessage = "Search Input Cannot Be Blank";
          }

          if (this.errorMessage) return;

          if(this.category === "Students"){
              this.url = 'http://localhost:59364/api/search/students/'
          }
          else if(this.category === "Teachers"){
              this.url = 'http://localhost:59364/api/search/teachers/'
          }
          else{
              this.url = 'http://localhost:59364/api/search/posts/'
          }
          
          this.axios
          .get(this.url, {
              params:{
                  AccountEmail: this.userEmail,
                  SearchInput: this.SearchInput
              },
              headers: { "Content-Type": "application/json" }
              
          })
          .then(response =>{
              this.data = response.data
              if(this.data.People === null){
                  this.errorMessage = "No Results Found";
              }
          })
          .catch(searchError =>{
              this.errorMessage = searchError.response.data.Message
          })
          .finally(() => {
              this.loading = false;
          })
      },
      getDepartments: function(){
          this.userEmail = "krystalleon10@gmail.com";
          this.errorMessage = "";

          const url = 'http://localhost:59364/api/search/departments/'
          this.axios
          .get(url, {
              params:{
                  AccountEmail: this.userEmail,
                  SearchInput: "nothing"
              },
              headers: { "Content-Type": "application/json" }
              
          })
          .then(response =>{
              this.data = response.data
          })
          .catch(searchError =>{
              this.errorMessage = searchError.response.data.Message
          })
          
      }    
  }
}
</script>

<style lang="css">
/* .searchBar{
    margin-top: 20px;
    margin: 5px auto;
    padding: 20px;
} */

</style>
