<template>
<v-container>
<v-container v-if="forumState.postedQuestion">
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
        <v-btn small @click="viewAnswers()">See Answers</v-btn> {{"Answers: " + item.AnswerCount}}
        <v-btn small v-if="userId != item.AccountId" @click="postAnswer()">Post Answer</v-btn> {{"Exp needed to answer: " + item.ExpNeededToAnswer}}

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

<v-container v-if="!forumState.postedQuestion">
<v-card class="mx-auto" dark max-width="1400" max-height="800" v-for="item in questions" :key="item.AccountName">

  <!-- <v-card class="mx-auto" dark max-width="1400" max-height="800"> -->

    <v-card-title>{{item.AccountName}}
      <v-spacer></v-spacer>
      {{item.DateCreated}}
    </v-card-title>
    
    <v-card-text class="headline font-weight-bold"> {{item.Text}} </v-card-text>

    <v-card-actions>   
      <v-layout align-center>

        <v-spacer></v-spacer>

        <v-icon small v-if="userId == item.AccountId" @click="editQuestion(item.QuestionId)">edit</v-icon>

      </v-layout>
    </v-card-actions>

    <v-divider class ="ma-3"></v-divider>
    
  </v-card>
  </v-container>

<v-container v-if="forumState.viewAnswers">
<v-card class="mx-auto" dark max-width="1400" max-height="800" v-for="item in questions" :key="item.AccountName">

  <!-- <v-card class="mx-auto" dark max-width="1400" max-height="800"> -->

    <v-card-title>{{item.AccountName}}
      <v-spacer></v-spacer>
      {{item.DateCreated}}
    </v-card-title>
    
    <v-card-text class="headline font-weight-bold"> {{item.Text}} </v-card-text>

    <v-card-actions>   
      <v-layout align-center>

        <v-spacer></v-spacer>

        <v-icon small v-if="userId == item.AccountId" @click="editQuestion(item.QuestionId)">edit</v-icon>

      </v-layout>
    </v-card-actions>

    <v-divider class ="ma-3"></v-divider>
    
  </v-card>
  </v-container>
  
</v-container>
</template>


<script>
import axios from 'axios'
import ForumState from "@/services/ForumState";

  export default {
    name: "ViewQuestions",
    data () {
      return {
        forumState: ForumState.state,
        userAccount: null,
        isStudent: true,
        userId: "",
        schools: [],
        departments: [],
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
    //     this.getAccount()
    // },
    methods: {
      viewAnswers() {
        ForumState.viewAnswers();
      },
      postAnswer() {
        ForumState.openPostAnswerForm();
      },
      closeQuestion(qId) {
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
      editQuestion(item) {
            ForumState.openEditQuestionForm(item); 
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
                  schoolId: this.school
              }
            })
              .then(response => {
                  this.questions = response.data;
                  this.postedQuestion = true;
              })
              .catch(err => {
                  console.log(err);
              });
        },
        getDepartmentQuestions() {
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
                  departmentId: this.department
              }
            })
              .then(response => {
                  this.questions = response.data;
                  this.postedQuestion = true;
              })
              .catch(err => {
                  console.log(err);
              });
        },
        getCourseQuestions() {
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
                  courseId: this.course
              }
            })
              .then(response => {
                  this.questions = response.data;
                  this.postedQuestion = true;
              })
              .catch(err => {
                  console.log(err);
              });
        },
        getDraftQuestions() {
            this.axios({
              headers: {
                //Accept: "application/json", 
                "Content-Type": "application/Json",
                //Authorization: "Bearer" + sessionStorage.SITtoken
              },
              method: "GET", 
              crossDomain: true,
              url: this.$hostname + "DiscussionForum/DraftQuestions",
              // params: {
              // }
            })
              .then(response => {
                  this.questions = response.data;
                  this.postedQuestion = false;
              })
              .catch(err => {
                  console.log(err);
              });
        },
    }
  }
</script>

