<template>
<v-container fuild>
  <v-toolbar dark>
  <v-layout row>
  <v-toolbar-title centered>Discussion Forum   </v-toolbar-title> 
    
    <v-flex shrink pa-1>
    <v-select
      v-model="school"
      :items="schools"
      label="School"
    ></v-select>
    </v-flex>
      
    <v-flex shrink pa-1>
    <v-select
        v-model="department"
        :items="departments"
        label="Department"
        @input="getDepartments"
    ></v-select>
    </v-flex>
            
    <v-flex shrink pa-1>
    <v-select
        v-model="course"
        :items="courses"
        label="Course"
        @input="getCourses"
    ></v-select>
    </v-flex>
    <v-spacer></v-spacer>

    <v-btn small @click="postQuestionDialog = !postQuestionDialog" > My Drafts </v-btn>    

    <v-btn small @click="postQuestionDialog = !postQuestionDialog"  > Post </v-btn>




    <v-btn icon>
      <v-icon>refresh</v-icon>
    </v-btn>

  </v-layout>
  </v-toolbar>
    
  <v-dialog v-model="postQuestionDialog" max-width="750">
      <PostQuestionDialog></PostQuestionDialog>
  </v-dialog>

</v-container>
</template>

<script>
import PostQuestionDialog from "@/components/DiscussionForum/PostQuestionDialog";

import axios from 'axios'
  export default {
    name: "DFToolbar",
    components: {
        PostQuestionDialog,
    },
    data () {
      return {
        postQuestionDialog: false,
        userAccount: null,
        isStudent: true,
        userId: "",
        school: "",
        schools: [],
        department: "",
        departments: [],
        course: "",
        courses: [],
        response: ''
      }
    },

    // created() {
    //     this.getSchoolQuestions()
    // },
    
    beforeMount(){
        this.userId = sessionStorage.SITuserId;
        //this.userId = "2"
        this.getAccount()
    },
    methods: {
      close() {
      // this.$emit('close');
          postQuestionDialag = false;
      },
      // @Author: Krystal
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
      },
      getSchools: function(){
          this.errorMessage = "";

          const url = `${this.$hostname}search/selections`;
          Axios
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
          Axios
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
          Axios
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
      PostQuestion () {
        //this.$router.push("/DiscussionForum/PostQuestion")
      },
    }
  }
</script>

