<template>
<v-container>
  <v-card class="mx-auto" dark max-width="1400" max-height="800" v-for="item in questions" :key="item.AccountName">

  <!-- <v-card class="mx-auto" dark max-width="1400" max-height="800"> -->

    <v-card-title>{{item.AccountName}}
      <v-spacer></v-spacer>
      {{item.DateCreated}}
    </v-card-title>
    
    <v-card-text class="headline font-weight-bold"> {{item.Text}} </v-card-text>

    <v-card-actions>   
      <v-layout align-center>
        <v-btn small v-if="userId != item.AccoutId">Mark As Spam</v-btn> {{"Spam Count: " + item.SpamCount}}
        <v-btn small>See Answers</v-btn> {{"Answers: " + item.AnswerCount}}
        <v-btn small v-if="userId != item.AccountId">Post Answer</v-btn> {{"Exp needed to answer: " + item.ExpNeededToAnswer}}

        <v-spacer></v-spacer>

        <v-btn small v-if="userId == item.AccountId" @click="closeQuestion(item.QuestionId)">Close Question</v-btn> 
        <v-icon small v-if="userId == item.AccountId" @click="editQuestion(item.QuestionId)">edit</v-icon>
        <!-- not in the business rules to delete a question 
            <v-icon small v-if="userId == item.AccountId" @click="deleteQuestion()">delete</v-icon> -->

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
        //ismyQuestion: true,
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
    
    beforeMount(){
        this.userId = sessionStorage.SITuserId;
        this.getAccount()
    },
    methods: {
    //   isMyQuestion: function() {
            
    //   },
      closeQuestion: function(qId) {
            const url = `${this.$hostname}DiscussionForum/CloseQuestion`;
            
            axios
            .post(url, {
                params:{
                    questionId: qId
                },
                headers: { "Content-Type": "application/json", Authorization: "Bearer " + sessionStorage.SITtoken }
                
            })
            .then(response =>{
                this.item.IsClosed = response.data;
            })
            .catch(error =>{
                this.errorMessage = error.response.data.Message
            })
      },
      editQuestion: function(qId) {
            // open up form to update question 
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
                  // or id? if not change backend from id to name
                  schoolId: this.school.Id
              }
            })
              .then(response => {
                  this.questions = response.data;
              })
              .catch(err => {
                  console.log(err);
              });
        },
        getDepartmentQuestions(dId) {
            this.axios({
              headers: {
                //Accept: "application/json", 
                "Content-Type": "application/Json",
                //Authorization: "Bearer" + sessionStorage.SITtoken
              },
              method: "GET", 
              crossDomain: true,
              url: this.$hostname + "DiscussionForum/GetQuestionsByDepartment",
              params: {
                  departmentId: dId
              }
            })
              .then(response => {
                  this.questions = response.data;
              })
              .catch(err => {
                  console.log(err);
              });
        },
        getCourseQuestions(cId) {
            this.axios({
              headers: {
                //Accept: "application/json", 
                "Content-Type": "application/Json",
                //Authorization: "Bearer" + sessionStorage.SITtoken
              },
              method: "GET", 
              crossDomain: true,
              url: this.$hostname + "DiscussionForum/GetQuestionsByCourse",
              params: {
                  courseId: cId
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

