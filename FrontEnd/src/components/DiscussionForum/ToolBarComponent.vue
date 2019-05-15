<template>
<!-- <v-app> -->
  <v-toolbar dark>
  <v-layout row>
  <v-toolbar-title centered>Discussion Forum </v-toolbar-title> 
    
    <v-flex shrink pa-1>
    <v-select
      v-model="school"
      :items="schools"
      label="School"
      v-if="!isStudent"
      @input="getDepartments"
    ></v-select>
    </v-flex>
      
    <v-flex shrink pa-1>
    <v-select
        v-model="department"
        :items="departments"
        label="Department"
        @input="getCourses"
    ></v-select>
    </v-flex>
            
    <v-flex shrink pa-1>
    <v-select
        v-model="course"
        :items="courses"
        label="Course"
    ></v-select>
    </v-flex>

    <v-flex grow pa-1    >
    <v-text-field
        class="searchBar"
        v-model="searchInput" 
        label="Search..." 
        append-icon="search" 
        
        hide-details>
    </v-text-field>
    </v-flex>

    <v-spacer></v-spacer>

    <v-btn small @click="myDrafts()" > My Drafts </v-btn>    

    <v-btn small @click="postQuestion()" > Post </v-btn>



    <v-btn icon>
      <v-icon @click="viewQuestions()">refresh</v-icon>
    </v-btn>

  </v-layout>
  </v-toolbar>
  <!-- </v-app> -->
</template>

<script>
import axios from 'axios'
import ForumState from "@/services/ForumState";
import AppSession from "@/services/AppSession"

  export default {
    name: "DFToolbar",
    data () {
      return {
        userAccount: null,
        isStudent: false,
        userId: "",
        school: 0,
        schools: [],
        department: 0,
        departments: [],
        course: 0,
        courses: [],
        response: '',
        searchInput: ''
      }
    },

    // created() {
    //     this.getSchoolQuestions()
    // },
    
    beforeMount(){
        this.userId = AppSession.state.userId;
        if(AppSession.state.category === "Student"){
            this.isStudent = true;
            this.school = AppSession.state.schoolId
            this.getDepartments(); 
        }
        else{
            this.getSchools();
        }
        //this.userId = "2"
    },
    methods: {
      // @Author: Krystal 
      
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
              if(this.schools.length > 0){
                  this.schools = [{id: 0, text: "NONE", value: 0 }].concat(this.schools)
              }
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
              this.courses = response.data;
          })
          .catch(error =>{
              this.errorMessage = error.response.data.Message
          })
      },
      // End Krytal

      postQuestion() {
        ForumState.setSchool(this.school)
        ForumState.setDepartment(this.department)
        ForumState.setCourse(this.course)
        ForumState.openPostQuestionForm()
      },
      
      myDrafts() {
        ForumState.viewDraftQuestions()
      },

      viewQuestions() {
        ForumState.setSchool(this.school)
        ForumState.setDepartment(this.department)
        ForumState.setCourse(this.course)
        if(this.course != null) {
          ForumState.getCourseQuestions()
        }
        else if(this.department != null) {
          ForumState.getDepartmentQuestions()
        }
        else if(this.school != null) {
          ForumState.getSchoolQuestions()
        }
        ForumState.viewPostedQuestions()
      },
    }
  }
</script>

