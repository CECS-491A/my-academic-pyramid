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
            :items="departments"
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
            v-if="useTable"
        >
            <v-progress-linear v-slot:progress color="blue" indeterminate v-if="loading"></v-progress-linear>
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
        
        </v-data-table>

        <v-list two-line v-if="!useTable">
          <template v-for="post in data.ForumPosts">
            <v-list-tile
              :key="post.title"
              avatar
              ripple
              
            >
              <v-list-tile-content>
                <v-list-tile-title>{{ item.title }}</v-list-tile-title>
                <v-list-tile-sub-title class="text--primary">{{ item.headline }}</v-list-tile-sub-title>
                <v-list-tile-sub-title>{{ item.subtitle }}</v-list-tile-sub-title>
              </v-list-tile-content>

              <v-list-tile-action>
                <v-list-tile-action-text>{{ item.action }}</v-list-tile-action-text>
              </v-list-tile-action>

            </v-list-tile>
          </template>
        </v-list>

        <v-alert
            :value="errorMessage"
            id="errorMessage"
            type="error"
            transition="scale-transition"
        >
            {{errorMessage}}
        </v-alert>
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
            category: null,
            categories: [
                {
                    id: 0,
                    text: "Students",
                    value: 0
                },
                {
                    id: 1,
                    text: "Teachers",
                    value: 1
                },
                {
                    id: 2,
                    text: "Forum Posts",
                    value: 2
                }
            ],
            department: '',
            departments: [],
            expand: false,
            tableHeaders: [
                { text: 'First Name', value: 'FirstName' },
                { text: 'Middle Name', value: 'MiddleName' },
                { text: 'Last Name', value: 'LastName' },
                { text: 'Department', value: 'Department' }
            ],
            data: {
                People: [],
                ForumPosts: [],
                Message: ''
            },
            userId: '',
            errorMessage: null,
            loading: false,
            useTable: false
        }
    },
    methods: {
        search: function() {
            this.loading = true;
            this.userId = sessionStorage.SITuserId;
            
            if (this.SearchInput.length === 0){
                this.errorMessage = "Search Input Cannot Be Blank";
            }
            if(this.department === null){
                this.department = 0;
            }
            if(this.category === null){
                this.errorMessage = "A Category Must Be Chosen";
            }
            else if(this.category === 0 || this.category === 1){
                this.useTable = true;
            }
            else if(this.category === 3){
                this.useTable = false;
            }

            const url = 'https://api.kfc-sso.com/api/search/input'
            
            axios
            .get(url, {
                params:{
                    AccountId: this.userId,
                    SearchCategory: this.category,
                    SearchDepartment: this.department,
                    SearchInput: this.SearchInput
                },
                headers: { "Content-Type": "application/json" }
                
            })
            .then(response =>{
                this.data = response.data
                if(this.data.People.length === 0){
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
            this.userId =  sessionStorage.getItem() "krystalleon10@gmail.com";
            this.errorMessage = "";

            const url = 'http://localhost:59364/api/search/departments/'
            axios
            .get(url, {
                params:{
                    AccountEmail: this.userId,
                },
                headers: { "Content-Type": "application/json" }
                
            })
            .then(response =>{
                this.departments = response.data
                if(this.departments.length === 0){
                    this.errorMessage = "No Results Found";
                }
                else{
                    this.departments = [{id: 0, text: "ALL", value: 0 }].concat(this.departments)
                }
            })
            .catch(searchError =>{
                this.errorMessage = searchError.response.data.Message
            })
            
        }    
    },
    beforeMount(){
        this.getDepartments()
    },
}
</script>

<style lang="css">
/* .searchBar{
    margin-top: 20px;
    margin: 5px auto;
    padding: 20px;
} */

</style>
