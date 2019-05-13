<template>
<v-container>
  <v-card class="mx-auto" dark max-width="1400" max-height="800" v-for="item in questions" :key="item.AccountName">

    <v-card-title>{{item.AccountName}}
      <v-spacer></v-spacer>
      {{item.DateCreated}}
    </v-card-title>
    
    <v-card-text class="headline font-weight-bold"> {{item.Text}} </v-card-text>

    <v-card-actions>   
      <v-layout align-center>
        <v-btn small>Mark As Spam</v-btn> {{"Spam Count: " + item.SpamCount}}
        <v-btn small>See Answers</v-btn> {{"Answers: " + item.AnswerCount}}
        <v-btn small>Post Answer</v-btn> {{"Exp needed to answer: " + item.ExpNeededToAnswer}}

        <v-spacer></v-spacer>

        <v-btn small>Close Question</v-btn> 
        <v-icon small>edit</v-icon>
        <v-icon small>delete</v-icon>

      </v-layout>
    </v-card-actions>

    <v-card-footer>{{ item.SchoolName + "/" + item.DepartmentName + "/" + item.CourseName }}</v-card-footer>

    <v-divider class ="ma-3"></v-divider>
    
  </v-card>
</v-container>
</template>


<script>
import axios from 'axios'
  export default {
    name: "QuestionCard",
    data () {
      return {
        ismyQuestion: true,
        userAccount: null,
        isStudent: true,
        userId: "",
        school: "",
        schools: [],
        department: "",
        departments: [],
        course: "",
        courses: [],
        response: '',
        questions: [ ]
      }
    },

    created() {
        this.getSchoolQuestions()
    },
    
    // beforeMount(){
    //     this.userId = sessionStorage.SITuserId;
    //     //this.userId = "2"
    //     this.getAccount()
    // },
    methods: {
      getSchoolQuestions() {
            this.axios({
              headers: {
                //Accept: "application/json", 
                "Content-Type": "application/Json",
                //Authorization: "Bearer" + sessionStorage.SITtoken
              },
              method: "GET", 
              crossDomain: true,
              url: this.$hostname + "DiscussionForum/GetQuestionsBySchool",
              params: {
                  schoolId: 1
              }
            })
              .then(response => {
                  this.questions = response.data;
              })
              .catch(err => {
                  console.log(err);
              });
        },
        getSchoolQuestions() {
            this.axios({
              headers: {
                //Accept: "application/json", 
                "Content-Type": "application/Json",
                //Authorization: "Bearer" + sessionStorage.SITtoken
              },
              method: "GET", 
              crossDomain: true,
              url: this.$hostname + "DiscussionForum/GetQuestionsBySchool",
              params: {
                  schoolId: 1
              }
            })
              .then(response => {
                  this.questions = response.data;
              })
              .catch(err => {
                  console.log(err);
              });
        },
    }
  }
</script>

