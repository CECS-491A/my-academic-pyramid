<template>
<v-app>
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
            v-model="school"
            :items="schools"
            label="School"
            v-if="!isStudent"
            @input="getDepartments"
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
            @input="getCourses"
        ></v-select>
        </v-flex>

        <v-flex
        shrink
        pa-1
        >
        <v-select
            v-model="course"
            :items="courses"
            label="Course"
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
        <v-toolbar flat>
        <v-toolbar-title v-if="!errorMessage">Search Results</v-toolbar-title>
        </v-toolbar>

        <v-data-table
            :headers="tableHeaders"
            :items="data.People"
            :expand="expand"
            item-key="AccountId"
            v-if="useTable"
        >
            <v-progress-linear v-slot:progress color="info" indeterminate v-if="loading"></v-progress-linear>
            <template v-slot:items="props">
                <tr @click="props.expanded = !props.expanded">
                <td>{{ props.item.FirstName }}</td>
                <td>{{ props.item.MiddleName }}</td>
                <td>{{ props.item.LastName }}</td>
                <td>{{ props.item.DepartmentName }}</td>
                <td>{{ props.item.SchoolName }}</td>
                </tr>
            </template>
            <template v-slot:expand="props">
                <v-card flat v-if="!hasProfile">
                    <v-card-text> <v-icon>view_list</v-icon> Courses: {{props.item.Courses.toString()}}</v-card-text>
                </v-card>
                <v-btn
                    v-if="hasProfile"
                    block color="secondary"
                    :to="{ name: 'Profile', params: { id: props.item.AccountId } }">
                        <v-icon dark>person</v-icon>
                        Student Profile
                </v-btn>
            </template>
        
        </v-data-table>

        <v-list two-line v-if="!useTable">
          <template v-for="post in data.ForumPosts">
            <v-list-tile
              :key="post.text"
              avatar
              ripple
              
            >
              <v-list-tile-content>
                <v-list-tile-title>{{ post.title }}</v-list-tile-title>
                <v-list-tile-sub-title class="text--primary">{{ post.headline }}</v-list-tile-sub-title>
                <v-list-tile-sub-title>{{ post.subtitle }}</v-list-tile-sub-title>
              </v-list-tile-content>

              <v-list-tile-action>
                <v-list-tile-action-text>{{ post.action }}</v-list-tile-action-text>
              </v-list-tile-action>

            </v-list-tile>
            <v-divider
              :key="post.text"
            ></v-divider>
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
</v-app>

</template>

<script>
import axios from 'axios'
export default {
    name: "Search",
    data () {
        return {
            openSearch: '',
            SearchInput: '',
            userAccount: null,
            category: 0,
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
                // {
                //     id: 2,
                //     text: "Forum Posts",
                //     value: 2
                // }
            ],
            school: 0,
            schools: [],
            department: 0,
            departments: [],
            course: 0,
            courses: [],
            expand: false,
            tableHeaders: [
                { text: 'First Name', value: 'FirstName' },
                { text: 'Middle Name', value: 'MiddleName' },
                { text: 'Last Name', value: 'LastName' },
                { text: 'Department', value: 'DepartmentName' },
                { text: 'School', value: 'SchoolName'}
            ],
            data: {
                People: [],
                ForumPosts: [],
                Message: ''
            },
            userId: '',
            errorMessage: null,
            loading: true,
            useTable: false,
            hasProfile: false,
            isStudent: true
        }
    },
    methods: {
        search: function() {
            this.loading = true;
            
            
            if (this.SearchInput.length === 0){
                this.errorMessage = "Search Input Cannot Be Blank";
            }
            if(this.department === null){
                this.department = 0;
            }
            if(this.category === null){
                this.errorMessage = "A Category Must Be Chosen";
            }
            else if(this.category === 0){
                this.useTable = true;
                this.hasProfile = true;
            }
            else if(this.category === 1){
                this.useTable = true;
                this.hasProfile = false;
            }
            else if(this.category === 2){
                this.useTable = false;
            }

            const url = `${this.$hostname}search/input`;
            
            axios
            .get(url, {
                params:{
                    AccountId: this.userId,
                    SearchCategory: this.category,
                    SearchSchool: this.school,
                    SearchDepartment: this.department,
                    SearchCourse: this.course,
                    SearchInput: this.SearchInput
                },
                headers: { "Content-Type": "application/json", Authorization: "Bearer " + sessionStorage.SITtoken }
                
            })
            .then(response =>{
                this.data = response.data;
                this.data.People.forEach(person => {
                    if(person.Courses == null){
                        person.Courses = 'None';
                    }
                });
            })
            .catch(error =>{
                this.errorMessage = error.response.data.Message
            })
            .finally(() => {
                this.loading = false;
            })
        },
        getSchools: function(){
            this.errorMessage = "";

            const url = `${this.$hostname}search/selections`;
            axios
            .get(url, {
                params:{
                    SearchCategory: 0
                },
                headers: { "Content-Type": "application/json", Authorization: "Bearer " + sessionStorage.SITtoken }
                
            })
            .then(response =>{
                this.schools = response.data;
                if(this.schools.length > 1){
                    this.schools = [{id: 0, text: "ALL", value: 0 }].concat(this.schools)
                }
                this.courses = [];
            })
            .catch(error =>{
                this.errorMessage = error.response.data.Message
            })
            
        },
        getDepartments: function(){
            this.errorMessage = "";

            const url = `${this.$hostname}search/selections`;
            axios
            .get(url, {
                params:{
                    SearchCategory: 1,
                    SearchSchool: this.school
                },
                headers: { "Content-Type": "application/json", Authorization: "Bearer " + sessionStorage.SITtoken }
                
            })
            .then(response =>{
                this.departments = response.data;
                if(this.departments.length > 1){
                    this.departments = [{id: 0, text: "ALL", value: 0 }].concat(this.departments)
                }
            })
            .catch(error =>{
                this.errorMessage = error.response.data.Message
            })
            
        },
        getCourses: function(){
            this.errorMessage = "";

            const url = `${this.$hostname}search/selections`;
            axios
            .get(url, {
                params:{
                    SearchCategory: 2,
                    SearchSchool: this.school,
                    SearchDepartment: this.department
                },
                headers: { "Content-Type": "application/json", Authorization: "Bearer " + sessionStorage.SITtoken }
                
            })
            .then(response =>{
                this.courses = response.data
                if(this.courses.length > 1){
                    this.courses = [{id: 0, text: "ALL", value: 0 }].concat(this.courses)
                }
            })
            .catch(error =>{
                this.errorMessage = error.response.data.Message
            })
            
        },
        getAccount: function(){
            this.errorMessage = "";

            const url = `${this.$hostname}search/account`;
            axios
            .get(url, {
                params:{
                    AccountId: this.userId,
                },
                headers: { "Content-Type": "application/json", Authorization: "Bearer " + sessionStorage.SITtoken }
                
            })
            .then(response =>{
                this.userAccount = response.data;
                this.isStudent = this.userAccount.IsStudent;
                if(!this.isStudent){
                    this.getSchools();
                }else{
                    this.school = this.userAccount.SchoolId;
                    this.getDepartments();
                }
            })
            .catch(error =>{
                this.errorMessage = error.response.data.Message
            })
        }
    },
    beforeMount(){
        this.userId = sessionStorage.SITuserId;
        //this.userId = "2"
        this.getAccount()
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
